using iAttend.Student.DependencyServices;
using iAttend.Student.EventAggs;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace iAttend.Student.ViewModels
{
	public class TeacherLandingPageViewModel : ViewModelBase
	{
        private readonly ITeacherService _teacherService;
        private readonly IMessageService _messageService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IEventAggregator _eventAggregator;

        public TeacherLandingPageViewModel(
            INavigationService navigationService,
            ITeacherService teacherService,
            IMessageService messageService,
            IPageDialogService pageDialogService,
            IEventAggregator eventAggregator) : base(navigationService)
        {
            _teacherService = teacherService;
            _messageService = messageService;
            _pageDialogService = pageDialogService;
            _eventAggregator = eventAggregator;

            Subjects = new ObservableCollection<TeacherSubject>();
            _eventAggregator.GetEvent<AttendanceStartedEvent>().Subscribe(AttendanceStarted);
            _eventAggregator.GetEvent<ScheduleAddedEvent>().Subscribe(ScheduleAdded);

        }

        private void ScheduleAdded(TeacherSubject sched)
        {
            Subjects.Add(sched);
        }

        private void AttendanceStarted(AttendanceStartedEventArg attendance)
        {
            HasActiveSession = attendance.IsActive;
            if (attendance.IsActive)
                ActiveTeacherSubject = Subjects.FirstOrDefault(x => x.SchedID == attendance.ScheduleId);
        }

        public ObservableCollection<TeacherSubject> Subjects { get; set; }

        private TeacherSubject _activeTeaacherSubject;
        public TeacherSubject ActiveTeacherSubject
        {
            get { return _activeTeaacherSubject; }
            set { SetProperty(ref _activeTeaacherSubject, value); }
        }

        private bool _hasActiveSession;
        public bool HasActiveSession
        {
            get { return _hasActiveSession; }
            set { SetProperty(ref _hasActiveSession, value); }
        }

        private int _absentCount;
        public int AbsentCount
        {
            get { return _absentCount; }
            set { SetProperty(ref _absentCount, value); }
        }

        private bool _hasAbsentNotif;
        public bool HasAbsentNotif
        {
            get { return _hasAbsentNotif; }
            set { SetProperty(ref _hasAbsentNotif, value); }
        }

        private string _roomAndTime;
        public string RoomAndTime
        {
            get { return _roomAndTime; }
            set { SetProperty(ref _roomAndTime, value); }
        }

        private DelegateCommand _logoutCommand;
        public DelegateCommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = new DelegateCommand(ExecuteLogoutCommand));

        async void ExecuteLogoutCommand()
        {
            var logout = await _pageDialogService.DisplayAlertAsync(null, "Are you sure you want to sign out?", "Yes", "No");

            if (!logout)
                return;

            _teacherService.SignOut();
            await NavigationService.NavigateAsync($"app:///{nameof(Views.LoginPage)}", animated: false);

        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCommand));

        async void ExecuteRefreshCommand()
        {
            await FetchSubjects();
            await FetchAbsentStat();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (IsOnNavigatingToTriggered)
                return;

            base.OnNavigatingTo(parameters);
            RefreshCommand.Execute();
        }

        public override void Destroy()
        {
            base.Destroy();
            _eventAggregator.GetEvent<AttendanceStartedEvent>().Unsubscribe(AttendanceStarted);
        }

        async Task FetchSubjects()
        {

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Subjects.Clear();

                var subjects = await _teacherService.GetSubjects();
                subjects.ForEach(x =>
                {
                    Subjects.Add(x);
                });

                SetActiveAttendanceSession();
            }
            catch(TeacherServiceException teacherEx)
            {
                _messageService.ShowMessage(teacherEx.ExceptionMessage);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Unable to fetch instructors subject");
            }
            finally
            {
                IsBusy = false;
            }
        }

        void SetActiveAttendanceSession()
        {
            ActiveTeacherSubject = Subjects.FirstOrDefault(x => x.IsOpen);

            HasActiveSession = ActiveTeacherSubject != null;
            RoomAndTime = $"{ActiveTeacherSubject.Room}|{ActiveTeacherSubject.Time}";

        }

        private DelegateCommand<TeacherSubject> _viewStudentAttendanceCommand;
        public DelegateCommand<TeacherSubject> ViewStudentAttendanceCommand =>
            _viewStudentAttendanceCommand ?? (_viewStudentAttendanceCommand = new DelegateCommand<TeacherSubject>(ExecuteViewStudentAttendanceCommand));

        async void ExecuteViewStudentAttendanceCommand(TeacherSubject subject)
        {
            var param = new NavigationParameters
            {
                {"subject",subject },
                {"subjects",Subjects.ToList() }
            };

            await NavigationService.NavigateAsync(nameof(Views.SubjectStudentsPage), param);
        }

        private DelegateCommand _stopAttendanceSession;
        private AbsentStat _absentStat;

        public DelegateCommand StopAttendanceSession =>
            _stopAttendanceSession ?? (_stopAttendanceSession = new DelegateCommand(ExecuteStopAttendanceSession));

        async void ExecuteStopAttendanceSession()
        {
            try
            {
                var deactivateSession = await _teacherService.StopAllAttendanceSession(ActiveTeacherSubject.SchedID, ActiveTeacherSubject.Room);
                HasActiveSession = false;
            }
            catch (TeacherServiceException teacherEx)
            {
                _messageService.ShowMessage(teacherEx.ExceptionMessage);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Something went wrong");
            }
        }


        async Task FetchAbsentStat()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                _absentStat = await _teacherService.GetAbsentStat();
                AbsentCount = _absentStat.Count;
                HasAbsentNotif = AbsentCount > 0;
            }
            catch (TeacherServiceException teacherEx)
            {
                _messageService.ShowMessage(teacherEx.ExceptionMessage);
            }
            catch(Exception ex)
            {
                _messageService.ShowMessage("Something went wrong");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private DelegateCommand _gotoAbsentListCommand;
        public DelegateCommand GotoAbsentListCommand =>
            _gotoAbsentListCommand ?? (_gotoAbsentListCommand = new DelegateCommand(ExecuteGotoAbsentListCommand));

        async void ExecuteGotoAbsentListCommand()
        {
            var param = new NavigationParameters
            {
                {"absents",_absentStat.Absents }
            };
            await NavigationService.NavigateAsync(nameof(Views.AbsentStatPage),param);
        }

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCommand));

        async void ExecuteAddCommand()
        {
            await NavigationService.NavigateAsync(nameof(Views.AddSchedulePage));
        }
    }
}
