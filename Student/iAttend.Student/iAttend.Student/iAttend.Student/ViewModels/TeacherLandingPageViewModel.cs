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
	public class TeacherLandingPageViewModel : ViewModelBase
	{
        private readonly ITeacherService _teacherService;

        public TeacherLandingPageViewModel(
            INavigationService navigationService,
            ITeacherService teacherService) : base(navigationService)
        {
            _teacherService = teacherService;
            Subjects = new ObservableCollection<TeacherSubject>();
        }

        public ObservableCollection<TeacherSubject> Subjects { get; set; }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            await FetchSubjects();
        }

        async Task FetchSubjects()
        {
            var subjects = await _teacherService.GetSubjects();
            subjects.ForEach(x =>
            {
                Subjects.Add(x);
            });
        }

        private DelegateCommand<TeacherSubject> _viewStudentAttendanceCommand;
        public DelegateCommand<TeacherSubject> ViewStudentAttendanceCommand =>
            _viewStudentAttendanceCommand ?? (_viewStudentAttendanceCommand = new DelegateCommand<TeacherSubject>(ExecuteViewStudentAttendanceCommand));

        async void ExecuteViewStudentAttendanceCommand(TeacherSubject subject)
        {
            var param = new NavigationParameters
            {
                {"subject",subject }
            };

            await NavigationService.NavigateAsync(nameof(Views.SubjectStudentsPage), param);
        }
    }
}
