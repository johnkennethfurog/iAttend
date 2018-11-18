using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iAttend.Student.ViewModels
{
	public class SwitchPageViewModel : ViewModelBase
	{

        public SwitchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private DelegateCommand _gotoStudentCommand;
        public DelegateCommand GotoStudentCommand =>
            _gotoStudentCommand ?? (_gotoStudentCommand = new DelegateCommand(ExecuteGotoStudentCommand));

        async void ExecuteGotoStudentCommand()
        {
            await NavigationService.NavigateAsync(nameof(Views.StudentLandingPage));
        }

        private DelegateCommand _gotoTeacherCommand;
        public DelegateCommand GotoTeacherCommand =>
            _gotoTeacherCommand ?? (_gotoTeacherCommand = new DelegateCommand(ExecuteGotoTeacherCommand));

        async void ExecuteGotoTeacherCommand()
        {
            await NavigationService.NavigateAsync(nameof(Views.TeacherLandingPage));
        }
    }
}
