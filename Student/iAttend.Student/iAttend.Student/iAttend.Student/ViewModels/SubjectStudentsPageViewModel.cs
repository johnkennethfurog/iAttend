using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace iAttend.Student.ViewModels
{
	public class SubjectStudentsPageViewModel : ViewModelBase
	{
        public ObservableCollection<TeacherStudentAttendance> StudentAttendances { get; set; }

        private TeacherSubject _teacherSubject;
        public TeacherSubject TeacherSubject
        {
            get { return _teacherSubject; }
            set { SetProperty(ref _teacherSubject, value); }
        }

        private readonly ITeacherService _teacherService;

        public SubjectStudentsPageViewModel(INavigationService navigationService,
            ITeacherService teacherService) : base(navigationService)
        {
            _teacherService = teacherService;

            StudentAttendances = new ObservableCollection<TeacherStudentAttendance>();
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            TeacherSubject = parameters["subject"] as TeacherSubject;

            await FetchStudents();
        }

        async Task FetchStudents()
        {
            var isPresent = true;
            var students = await _teacherService.GetStudents(2);
            students.ForEach(x =>
            {
                isPresent = !isPresent;
                x.IsPresent = isPresent;
                StudentAttendances.Add(x);
            });
        }

        private DelegateCommand<TeacherStudentAttendance> _markUnmarkStudentAttendanceCommand;
        public DelegateCommand<TeacherStudentAttendance> MarkUnMarkStudentAttendanceCommand =>
            _markUnmarkStudentAttendanceCommand ?? (_markUnmarkStudentAttendanceCommand = new DelegateCommand<TeacherStudentAttendance>(ExecuteMarkUnMarkStudentAttendanceCommand));

        async void ExecuteMarkUnMarkStudentAttendanceCommand(TeacherStudentAttendance student)
        {
            if (student.IsPresent)
            {
                await _teacherService.UnmarkStudentAttendance(student.StudentNumber, TeacherSubject.SchedID);
                student.IsPresent = false;
            }
            else
            {
                await _teacherService.MarkStudentAttendance(student.StudentNumber, TeacherSubject.SchedID);
                student.IsPresent = true;
            }
        }
    }
}
