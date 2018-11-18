using iAttend.Student.DependencyServices;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using Prism.Commands;
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
	public class SubjectStudentsPageViewModel : ViewModelBase
	{
        internal ActiveAttendance _activeSessionId;
        internal bool _isInitializing = true;

        private int _presentCount;
        public int PresentCount
        {
            get { return _presentCount; }
            set { SetProperty(ref _presentCount, value); }
        }

        private int _absentCount;
        public int AbsentCount
        {
            get { return _absentCount; }
            set { SetProperty(ref _absentCount, value); }
        }

        public ObservableCollection<TeacherStudentAttendance> StudentAttendances { get; set; }

        private bool _attendanceSessionStarted = false;
        public bool AttendanceSessionStarted
        {
            get { return _attendanceSessionStarted; }
            set { SetProperty(ref _attendanceSessionStarted, value); }
        }

        private TeacherSubject _teacherSubject;
        public TeacherSubject TeacherSubject
        {
            get { return _teacherSubject; }
            set { SetProperty(ref _teacherSubject, value); }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);

                if(!_isInitializing)
                    RefreshCommand.Execute();
            }
        }

        private readonly ITeacherService _teacherService;
        private readonly IMessageService _messageService;
        private readonly IPageDialogService _pageDialogService;

        public SubjectStudentsPageViewModel(INavigationService navigationService,
            ITeacherService teacherService,
            IMessageService messageService,
            IPageDialogService pageDialogService) : base(navigationService)
        {
            _teacherService = teacherService;
            _messageService = messageService;
            _pageDialogService = pageDialogService;
            StudentAttendances = new ObservableCollection<TeacherStudentAttendance>();
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            SelectedDate = DateTime.Now;

            TeacherSubject = parameters["subject"] as TeacherSubject;

            await FetchStudents();
        }

        void SetAttendanceStat()
        {
            PresentCount = StudentAttendances.Count(x => x.IsPresent);
            AbsentCount = StudentAttendances.Count - PresentCount;
        }


        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCommand));

        async void ExecuteRefreshCommand()
        {
            await FetchStudents();
        }

        async Task FetchStudents()
        {
            if (IsBusy)
                return;

            try
            {
                StudentAttendances.Clear();
                IsBusy = true;
                
                var students = await _teacherService.GetStudents(TeacherSubject.SchedID, SelectedDate.Date);

                students.ForEach(x =>
                {
                    StudentAttendances.Add(x);
                });

                SetAttendanceStat();
            }
            catch (TeacherServiceException teacherEx)
            {
                _messageService.ShowMessage(teacherEx.ExceptionMessage);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Unable to fetch students");
            }
            finally
            {
                if (_isInitializing)
                    _isInitializing = false;

                IsBusy = false;
            }
        }

        private DelegateCommand _filterCommand;
        public DelegateCommand FilterCommand =>
            _filterCommand ?? (_filterCommand = new DelegateCommand(ExecuteFilterCommand));

        async void ExecuteFilterCommand()
        {
            await NavigationService.NavigateAsync(nameof(Views.TeachersStudentFilterPage));
        }

        private DelegateCommand<TeacherStudentAttendance> _markUnmarkStudentAttendanceCommand;
        public DelegateCommand<TeacherStudentAttendance> MarkUnMarkStudentAttendanceCommand =>
            _markUnmarkStudentAttendanceCommand ?? (_markUnmarkStudentAttendanceCommand = new DelegateCommand<TeacherStudentAttendance>(ExecuteMarkUnMarkStudentAttendanceCommand));

        async void ExecuteMarkUnMarkStudentAttendanceCommand(TeacherStudentAttendance student)
        {
            try
            {
                if (!await AllowToExecuteMarkingOfAttendance())
                    return;

                if (student.IsPresent)
                {
                    await _teacherService.UnmarkStudentAttendance(student.StudentNumber, TeacherSubject.SchedID);
                    student.IsPresent = false;
                    AbsentCount++;
                    PresentCount--;
                }
                else
                {
                    await _teacherService.MarkStudentAttendance(student.StudentNumber, TeacherSubject.SchedID);
                    student.IsPresent = true;
                    AbsentCount--;
                    PresentCount++;
                }

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

        async  Task<bool> AllowToExecuteMarkingOfAttendance()
        {
            if (SelectedDate.Date <= DateTime.Now.Date)
                return true;

            return await _pageDialogService.DisplayAlertAsync(null, "The date you've selected is ahead of current date, Do you still want to mark student's attendance?", "Yes", "No");
        }

        private DelegateCommand _attendanceSessionCommand;
        public DelegateCommand AttendanceSessionCommand =>
            _attendanceSessionCommand ?? (_attendanceSessionCommand = new DelegateCommand(ExecuteAttendanceSessionCommand, CanExecuteAttendanceSessionCommand).ObservesProperty(()=> IsBusy));

        private bool CanExecuteAttendanceSessionCommand()
        {
            return !IsBusy;
        }

        async void ExecuteAttendanceSessionCommand()
        {

            try
            {
                if (!AttendanceSessionStarted)
                {
                    var activeSession = await _teacherService.StartAttendanceSession(TeacherSubject.SchedID);
                    _activeSessionId = activeSession;
                    AttendanceSessionStarted = true;
                }
                else
                {
                    var deactivateSession = await _teacherService.StopAttendanceSession(_activeSessionId.AttendanceSessionId);
                    AttendanceSessionStarted = false;
                }
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
    }
}
