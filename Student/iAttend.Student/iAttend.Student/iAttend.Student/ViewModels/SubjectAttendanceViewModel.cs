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
	public class SubjectAttendanceViewModel : ViewModelBase
	{
        internal string _studentNumber = "10-A00028";

        private string _placeAndTime;
        public string PlaceAndTime
        {
            get { return _placeAndTime; }
            set { SetProperty(ref _placeAndTime, value); }
        }

        private int _presentCouse;
        public int PresentCount
        {
            get { return _presentCouse; }
            set { SetProperty(ref _presentCouse, value); }
        }

        private int _absentCount;
        public int AbsentCount
        {
            get { return _absentCount; }
            set { SetProperty(ref _absentCount, value); }
        }

        private readonly IStudentService _studentService;
        private readonly IMessageService _messageService;

        public ObservableCollection<StudentAttendance> Attendances { get; private set; }

        private StudentSubject _subject;
        public StudentSubject Subject
        {
            get { return _subject; }
            set { SetProperty(ref _subject, value); }
        }

        public SubjectAttendanceViewModel(
            INavigationService navigationService,
            IStudentService studentService,
            IMessageService messageService) : base(navigationService)
        {
            _studentService = studentService;
            _messageService = messageService;

            Attendances = new ObservableCollection<StudentAttendance>();
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (IsOnNavigatingToTriggered)
                return;

            base.OnNavigatingTo(parameters);

            var subject = parameters["subject"] as StudentSubject;

            Subject = subject;
            SetSubject();
            await FetchAttendances();

        }

        private void SetSubject()
        {
            PlaceAndTime = $"{Subject.Room} | { Subject.Time}";
        }

        async Task FetchAttendances()
        {


            try
            {
                var attendances = await _studentService.GetAttendances(_studentNumber, Subject.ScheduleID);
                PresentCount = attendances.Count(x => x.IsPresent);
                AbsentCount = attendances.Count(x => !x.IsPresent);

                attendances.ForEach(x =>
                {
                    Attendances.Add(x);
                });

            }
            catch(StudentServiceException studEx)
            {
                _messageService.ShowMessage(studEx.ExceptionMessage);
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage("Unable to fetch attendance");
            }            
        }
    }
}
