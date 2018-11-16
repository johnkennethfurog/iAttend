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

        public ObservableCollection<StudentAttendance> Attendances { get; private set; }

        private StudentSubject _subject;
        public StudentSubject Subject
        {
            get { return _subject; }
            set { SetProperty(ref _subject, value); }
        }

        public SubjectAttendanceViewModel(
            INavigationService navigationService,
            IStudentService studentService) : base(navigationService)
        {
            _studentService = studentService;

            Attendances = new ObservableCollection<StudentAttendance>();
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var subject = parameters["subject"] as StudentSubject;

            Subject = subject;
            SetSubject();
            await FetchAttendances();

        }

        private void SetSubject()
        {
            PlaceAndTime = $"{Subject.Subject.Room} | { Subject.Subject.Time}";
        }

        async Task FetchAttendances()
        {
            var attendances = await _studentService.GetAttendances("12-A00004", (int)Subject.Subject.Id);
            PresentCount = attendances.Count(x => x.IsPresent);
            AbsentCount = attendances.Count(x => !x.IsPresent);

            attendances.ForEach(x =>
            {
                Attendances.Add(x);
            });
            
        }
    }
}
