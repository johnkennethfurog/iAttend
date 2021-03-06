﻿using iAttend.Student.DependencyServices;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using iAttend.Student.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
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
        private readonly IConnectivity _connectivity;
        private readonly IWifiConnector _wifiConnector;
        private StudentInfo _student;
        public StudentInfo Student
        {
            get { return _student; }
            set { SetProperty(ref _student, value); }
        }

        public ObservableCollection<StudentSubject> StudentSubjects { get; set; }

        public StudentLandingPageViewModel(
            INavigationService navigationService,
            IStudentService studentService,
            IQrScanningService qrScanningService,
            IMessageService messageService,
            IConnectivity connectivity,
            IWifiConnector wifiConnector) : base(navigationService)
        {
            _studentService = studentService;
            _qrScanningService = qrScanningService;
            _messageService = messageService;
            _connectivity = connectivity;
            _wifiConnector = wifiConnector;
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
                return;

            var connect =await _wifiConnector.ConnectToWifi("DESKTOP-T51EO2S 0988", "88888888");

            if(!connect)
            {
                _messageService.ShowMessage("Unable to connect");
                return;
            }

            await SendRequest(payload);
           
        }

        async Task SendRequest(PayloadStudentAttendance payload ,int trialCount = 0)
        {
            try
            {
                
                var success = await _studentService.ScanDocument(payload);
                _messageService.ShowMessage("Subject attendance marked!");
                _wifiConnector.Disconnect();
            }
            catch (StudentServiceException studEx)
            {

                if (trialCount < 3 && studEx.StatusCode == -1)
                    await SendRequest(payload, trialCount++);
                else
                {
                    _wifiConnector.Disconnect();
                    _messageService.ShowMessage(studEx.ExceptionMessage);
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Unable to Scan QR code, please try again");
            }
        }

        internal PayloadStudentAttendance ExtractPayload(string result)
        {

            var values = result.Split('|');

            if (values.Count() != 4)
            {
                _messageService.ShowMessage("Invalid QR code format");
                return null;
            }


            if (!StudentIsInculdedInMasterList(values[0]))
            {
                _messageService.ShowMessage("You are not included in master list");
                return null;
            }


            var x = values[0];

            var subject = JsonConvert.DeserializeObject<SchedulePayload>(values[3]);

            var payload = new PayloadStudentAttendance
            {
                AttendanceSessionId = int.Parse(values[1]),
                StudentNumber = _student.StudentNumber,
                Guid = values[2],
                StudentName = Student.StudentName,
                Subject = subject.Subject,
                Time = subject.Time
            };

            return payload;
        }

       bool StudentIsInculdedInMasterList(string source)
        {
            var masterlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(source);
            return masterlist.Contains(_student.StudentNumber);
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (IsOnNavigatingToTriggered)
                return;

            base.OnNavigatingTo(parameters);
            Student = Helpers.Student.CurrentStudent;
            await FetchStudentSubejcts();
        }

        async Task FetchStudentSubejcts()
        {
            if (!_connectivity.IsConnected)
                return;

            try
            {
                var subjects = await _studentService.GetSubjects(_student.StudentNumber);
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
