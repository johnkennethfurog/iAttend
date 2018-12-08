using iAttend.Student.DependencyServices;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iAttend.Student.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{
        private readonly ITeacherService _teacherService;
        private readonly IMessageService _messageService;

        public LoginPageViewModel(
            INavigationService navigationService,
            ITeacherService teacherService,
            IMessageService messageService) : base(navigationService)
        {
            _teacherService = teacherService;
            _messageService = messageService;
        }

        private string _instructorNumber;
        public string InstructorNumber
        {
            get { return _instructorNumber; }
            set { SetProperty(ref _instructorNumber, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand));

        async void ExecuteLoginCommand()
        {
            if (IsBusy)
                return;


            try
            {
                IsBusy = true;
                var loggedIn = await _teacherService.SignIn(InstructorNumber, Password);

                if(loggedIn)
                    await NavigationService.NavigateAsync($"app:///NavigationPage/{nameof(Views.TeacherLandingPage)}", animated: false);
            }
            catch(TeacherServiceException teacherException)
            {
                _messageService.ShowMessage(teacherException.ExceptionMessage);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message);
            }

            finally
            {
                IsBusy = false;
            }
        }
    }
}
