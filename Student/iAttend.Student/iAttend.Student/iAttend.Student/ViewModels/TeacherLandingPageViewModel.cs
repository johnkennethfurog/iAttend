using iAttend.Student.DependencyServices;
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
        private readonly IMessageService _messageService;

        public TeacherLandingPageViewModel(
            INavigationService navigationService,
            ITeacherService teacherService,
            IMessageService messageService) : base(navigationService)
        {
            _teacherService = teacherService;
            _messageService = messageService;
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

            try
            {
                var subjects = await _teacherService.GetSubjects();
                subjects.ForEach(x =>
                {
                    Subjects.Add(x);
                });
            }
            catch(TeacherServiceException teacherEx)
            {
                _messageService.ShowMessage(teacherEx.ExceptionMessage);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Unable to fetch instructors subject");
            }
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
