using iAttend.Student.DependencyServices;
using iAttend.Student.Helpers;
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
        internal AttendanceState _attendanceState = AttendanceState.All;
        internal List<TeacherStudentAttendance> _studentAttendances;

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

            _allActionButton = ActionSheetButton.CreateButton("All", ExecuteAllAction);
            _presentActionButton = ActionSheetButton.CreateButton("Present Only", ExecutePresentAction);
            _absentActionButton = ActionSheetButton.CreateButton("Absent Only", ExecuteAbsentAction);

            _studentAttendances = new List<TeacherStudentAttendance>();
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
            PresentCount = _studentAttendances.Count(x => x.IsPresent);
            AbsentCount = _studentAttendances.Count - PresentCount;
        }

        private DelegateCommand<TeacherStudentAttendance> _viewStudentsAttendanceCommand;
        public DelegateCommand<TeacherStudentAttendance> ViewStudentsAttendanceCommand =>
            _viewStudentsAttendanceCommand ?? (_viewStudentsAttendanceCommand = new DelegateCommand<TeacherStudentAttendance>(ExecuteViewStudentsAttendanceCommand));

        async void ExecuteViewStudentsAttendanceCommand(TeacherStudentAttendance studentAttendance)
        {
            var param = new NavigationParameters
            {
                { "studentNumber" , studentAttendance.StudentNumber},
                { "studentName" , studentAttendance.StudentName},
                { "studentAvatar" , studentAttendance.Avatar},
                { "schedId", TeacherSubject.SchedID }
            };

            await NavigationService.NavigateAsync(nameof(Views.StudentsAttendance), param);
        }

        private DelegateCommand _resetCommand;
        public DelegateCommand ResetCommand =>
            _resetCommand ?? (_resetCommand = new DelegateCommand(ExecuteResetCommand));

        void ExecuteResetCommand()
        {
            SelectedDate = DateTime.Now;
        }


        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCommand));

        async void ExecuteRefreshCommand()
        {
            await FetchStudents();
        }

        void LoadStudentToList()
        {

            List<TeacherStudentAttendance> students = new List<TeacherStudentAttendance>();

            switch(_attendanceState)
            {
                case AttendanceState.All:
                    students.AddRange(_studentAttendances);
                    break;
                case AttendanceState.Present:
                    students.AddRange(_studentAttendances.Where(x => x.IsPresent));
                    break;
                case AttendanceState.Absent:
                    students.AddRange(_studentAttendances.Where(x => !x.IsPresent));
                    break;
            }

            students.ForEach(x =>
            {
                StudentAttendances.Add(x);

            });
        }

        async Task FetchStudents()
        {
            if (IsBusy)
                return;

            try
            {
                _studentAttendances.Clear();
                StudentAttendances.Clear();

                IsBusy = true;
                
                var students = await _teacherService.GetStudents(TeacherSubject.SchedID, SelectedDate.Date);
                _studentAttendances.AddRange(students);
                LoadStudentToList();
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

        private  IActionSheetButton _allActionButton;
        private  IActionSheetButton _presentActionButton;
        private  IActionSheetButton _absentActionButton;

        private  void ExecutePresentAction()
        {
            _attendanceState = AttendanceState.Present;
            StudentAttendances.Clear();
            LoadStudentToList();
        }


        private  void ExecuteAbsentAction()
        {
            _attendanceState = AttendanceState.Absent;
            StudentAttendances.Clear();
            LoadStudentToList();
        }

        private  void ExecuteAllAction()
        {
            _attendanceState = AttendanceState.All;
            StudentAttendances.Clear();
            LoadStudentToList();
        }

        private DelegateCommand _filterCommand;
        public DelegateCommand FilterCommand =>
            _filterCommand ?? (_filterCommand = new DelegateCommand(ExecuteFilterCommand));

        async void ExecuteFilterCommand()
        {
            switch(_attendanceState)
            {
                case AttendanceState.All:
                    await ShowFilter(_presentActionButton, _absentActionButton);
                    return;
                case AttendanceState.Absent:
                    await ShowFilter(_allActionButton, _presentActionButton);
                    return;
                case AttendanceState.Present:
                    await ShowFilter(_allActionButton, _absentActionButton);
                    return;
            }
        }

        async Task ShowFilter(params IActionSheetButton[] actionSheetButton)
        {
            await _pageDialogService.DisplayActionSheetAsync("Filter Atendance", actionSheetButton);
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
                    await _teacherService.MarkStudentAttendance(student.StudentNumber, TeacherSubject.SchedID, _selectedDate.Date);
                    student.IsPresent = true;
                    AbsentCount--;
                    PresentCount++;
                }

                if (_attendanceState != AttendanceState.All)
                    StudentAttendances.Remove(student);

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
            if (SelectedDate.Date == DateTime.Now.Date)
                return true;

            return await _pageDialogService.DisplayAlertAsync(null, "The date you've selected not equal to current date, Do you still want to mark student's attendance?", "Yes", "No");
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
                    var activeSession = await _teacherService.StartAttendanceSession(TeacherSubject.SchedID, _teacherSubject.Room);
                    _activeSessionId = activeSession;
                    AttendanceSessionStarted = true;
                }
                else
                {
                    var deactivateSession = await _teacherService.StopAttendanceSession(_activeSessionId.AttendanceSessionId,_teacherSubject.Room);
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
