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
    public class LandingPageViewModel : ViewModelBase
	{
        private readonly IStudentService _studentService;
        private readonly IQrScanningService _qrScanningService;
        private readonly IMessageService _messageService;

        public ObservableCollection<StudentSubject> StudentSubjects { get; set; }

        public LandingPageViewModel(
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
            catch (Exception)
            {
                _messageService.ShowMessage("Unable to Scan QR code, please try again");
            }

            //await NavigationService.NavigateAsync(nameof(Views.ScannerPage));
        }

        internal PayloadStudentAttendance ExtractPayload(string result)
        {

            var values = result.Split('|');

            if (values.Count() != 2)
                return null;

            var payload = new PayloadStudentAttendance
            {
                AttendanceSessionId = int.Parse(values[0]),
                StudentNumber = values[1]
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
                var subjects = await _studentService.GetSubjects("12-A00004");
                subjects.ForEach(subj =>
                {
                    StudentSubjects.Add(subj);
                });

            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Unable to fetch student subjects");
            }        }
    }
}
