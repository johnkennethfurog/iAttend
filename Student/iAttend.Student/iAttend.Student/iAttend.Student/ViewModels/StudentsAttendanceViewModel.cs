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
	public class StudentsAttendanceViewModel : ViewModelBase
	{
        private readonly IStudentService _studentService;
        private readonly IMessageService _messageService;

        internal int _scheduleId;

        public StudentsAttendanceViewModel(INavigationService navigationService,
            IStudentService studentService,
            IMessageService messageService) : base(navigationService)
        {
            _studentService = studentService;
            _messageService = messageService;
            Attendances = new ObservableCollection<StudentAttendance>();
        }

        public ObservableCollection<StudentAttendance> Attendances { get; private set; }

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

        private string _studentNumber;
        public string StudentNumber
        {
            get { return _studentNumber; }
            set { SetProperty(ref _studentNumber, value); }
        }

        private string _studentName;
        public string StudentName
        {
            get { return _studentName; }
            set { SetProperty(ref _studentName, value); }
        }

        private string _studentAvatar;
        public string StudentAvatar
        {
            get { return _studentAvatar; }
            set { SetProperty(ref _studentAvatar, value); }
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (IsOnNavigatingToTriggered)
                return;

            base.OnNavigatingTo(parameters);

            StudentNumber = parameters["studentNumber"] as string;
            StudentName = parameters["studentName"] as string;
            StudentAvatar = parameters["studentAvatar"] as string;
            _scheduleId = (int) parameters["schedId"];

            await FetchAttendances();
        }


        async Task FetchAttendances()
        {


            try
            {
                var attendances = await _studentService.GetAttendances(_studentNumber, _scheduleId);
                PresentCount = attendances.Count(x => x.IsPresent);
                AbsentCount = attendances.Count(x => !x.IsPresent);

                attendances.ForEach(x =>
                {
                    Attendances.Add(x);
                });

            }
            catch (StudentServiceException studEx)
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
