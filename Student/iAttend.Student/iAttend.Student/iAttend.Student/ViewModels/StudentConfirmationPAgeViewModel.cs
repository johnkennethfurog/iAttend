using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using iAttend.Student.Models;
using iAttend.Student.Interfaces;
using iAttend.Student.DependencyServices;

namespace iAttend.Student.ViewModels
{
    public class StudentConfirmationPageViewModel : ViewModelBase
    {
        private readonly IStudentService _studentService;
        private readonly IMessageService _messageService;
        private readonly IPreferences _preferences;

        private StudentInfo _student;
        public StudentInfo Student
        {
            get { return _student; }
            set { SetProperty(ref _student, value); }
        }

        private string _avatar = "ic_trimex.png";
        public string Avatar
        {
            get { return _avatar; }
            set { SetProperty(ref _avatar, value); }
        }

        private bool _isDisposing;
        public bool IsDisposing
        {
            get { return _isDisposing; }
            set { SetProperty(ref _isDisposing, value); }
        }



        private bool _infoFetched;
        public bool InfoFetched
        {
            get { return _infoFetched; }
            set { SetProperty(ref _infoFetched, value); }
        }

        private string _studentNumber = "10-A00028";
        public string StudentNumber
        {
            get { return _studentNumber; }
            set { SetProperty(ref _studentNumber, value); }
        }

        public StudentConfirmationPageViewModel(
            INavigationService navigationService,
            IStudentService studentService,
            IMessageService messageService,
            IPreferences preferences) : base(navigationService)
        {
            _studentService = studentService;
            _messageService = messageService;
            _preferences = preferences;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (IsOnNavigatingToTriggered)
                return;

            base.OnNavigatingTo(parameters);
        }

        public override void Destroy()
        {
            IsDisposing = true;
            base.Destroy();
        }

        private DelegateCommand _verifyStudentNumber;
        public DelegateCommand VerifyStudentNumber =>
            _verifyStudentNumber ?? (_verifyStudentNumber = new DelegateCommand(ExecuteVerifyStudentNumber));

        async void ExecuteVerifyStudentNumber()
        {
            if (IsBusy)
                return;

            IsBusy = true;  

            try
            {
                Student = await _studentService.GetStudent(StudentNumber);
                Avatar = Student.Avatar;
                InfoFetched = true;

            }
            catch (StudentServiceException studEx)
            {
                _messageService.ShowMessage(studEx.ExceptionMessage);
            }
            catch(Exception ex)
            {
                _messageService.ShowMessage(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }


        private DelegateCommand _setStudentCommand;
        public DelegateCommand SetStudentCommand =>
            _setStudentCommand ?? (_setStudentCommand = new DelegateCommand(ExecuteSetStudentCommand));

        async void ExecuteSetStudentCommand()
        {
            _preferences.Set(Helpers.Student.STUDENT_KEY, Student);
            Helpers.Student.CurrentStudent = Student;
            await NavigationService.NavigateAsync($"app:///NavigationPage/{nameof(Views.StudentLandingPage)}",animated:false);
        }


        private DelegateCommand _backCommand;
        public DelegateCommand BackCommand =>
            _backCommand ?? (_backCommand = new DelegateCommand(ExecuteBackCommand));

        void ExecuteBackCommand()
        {
            Student = null;
            InfoFetched = false;
        }
    }
}
