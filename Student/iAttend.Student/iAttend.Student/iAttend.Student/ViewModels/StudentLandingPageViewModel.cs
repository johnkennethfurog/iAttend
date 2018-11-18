using iAttend.Student.DependencyServices;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using iAttend.Student.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace iAttend.Student.ViewModels
{
    public class StudentLandingPageViewModel : ViewModelBase
	{
        private readonly IStudentService _studentService;
        private readonly IQrScanningService _qrScanningService;
        private readonly IMessageService _messageService;

        internal string _studentNumber = "10-A00028";

        public ObservableCollection<StudentSubject> StudentSubjects { get; set; }

        public StudentLandingPageViewModel(
            INavigationService navigationService,
            IStudentService studentService,
            IQrScanningService qrScanningService,
            IMessageService messageService) : base(navigationService)
        {
            _studentService = studentService;
            _qrScanningService = qrScanningService;
            _messageService = messageService;
            StudentSubjects = new ObservableCollection<StudentSubject>();

        }

        private DelegateCommand<StudentSubject> _viewAttendanceCommand;
        public DelegateCommand<StudentSubject> ViewAttendanceCommand =>
            _viewAttendanceCommand ?? (_viewAttendanceCommand = new DelegateCommand<StudentSubject>(ExecuteViewAttendanceCommand));

        async void ExecuteViewAttendanceCommand(StudentSubject subject)
        {
            var param = new NavigationParameters
            {
                { "subject",subject }
            };

            await NavigationService.NavigateAsync(nameof(SubjectAttendance),param);
        }

        private DelegateCommand _openQrScannerCommand;
        public DelegateCommand OpenQrScannerCommand =>
            _openQrScannerCommand ?? (_openQrScannerCommand = new DelegateCommand(ExecuteOpenQrScannerCommand));

        async void ExecuteOpenQrScannerCommand()
        {
            var result = await _qrScanningService.ScanAsync();
            if (string.IsNullOrEmpty(result))
                return;

            var payload = ExtractPayload(result);
            if(payload == null)
            {
                _messageService.ShowMessage("Invalid QR code format");
                return;
            }

            try
            {
                var success = await _studentService.ScanDocument(payload);
                _messageService.ShowMessage("Subject attendance marked!");
            }
            catch(StudentServiceException studEx)
            {
                _messageService.ShowMessage(studEx.ExceptionMessage);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Unable to Scan QR code, please try again");
            }
        }

        internal PayloadStudentAttendance ExtractPayload(string result)
        {

            var values = result.Split('|');

            if (values.Count() != 2 || values[1] != "trimex")
                return null;

            var payload = new PayloadStudentAttendance
            {
                AttendanceSessionId = int.Parse(values[0]),
                StudentNumber = _studentNumber
            };

            return payload;
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            await FetchStudentSubejcts();
        }

        async Task FetchStudentSubejcts()
        {
            try
            {
                var subjects = await _studentService.GetSubjects(_studentNumber);
                subjects.ForEach(subj =>
                {
                    StudentSubjects.Add(subj);
                });

            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Unable to fetch student subjects");
            }
        }
    }
}
