using iAttend.Student.DependencyServices;
using iAttend.Student.EventAggs;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iAttend.Student.ViewModels
{
	public class AddSchedulePageViewModel : ViewModelBase
	{

        private List<Subj> _subjects;
        public List<Subj> Subjects
        {
            get { return _subjects; }
            set { SetProperty(ref _subjects, value); }
        }

        private Subj _selectedSubject;
        public Subj SelectedSubject
        {
            get { return _selectedSubject; }
            set { SetProperty(ref _selectedSubject, value); }
        }

        private string _room;
        public string Room
        {
            get { return _room; }
            set { SetProperty(ref _room, value); }
        }


        private string _day;
        public string Day
        {
            get { return _day; }
            set { SetProperty(ref _day, value); }
        }

        private TimeSpan _from;
        public TimeSpan From
        {
            get { return _from; }
            set { SetProperty(ref _from, value); }
        }

        private TimeSpan _to;
        public TimeSpan To
        {
            get { return _to; }
            set { SetProperty(ref _to, value); }
        }

        private readonly ITeacherService _teacherService;
        private readonly IMessageService _messageService;
        private readonly IEventAggregator _eventAggregator;

        public AddSchedulePageViewModel(INavigationService navigationService,
            ITeacherService teacherService,
            IMessageService messageService,
            IEventAggregator eventAggregator) : base(navigationService)
        {
            _teacherService = teacherService;
            _messageService = messageService;
            _eventAggregator = eventAggregator;
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            await FetchSubjects();
        }

        async Task FetchSubjects()
        {
            var subj = await _teacherService.GetAllSubject();
            Subjects = subj;
        }

        private DelegateCommand _addScheduleCommand;
        public DelegateCommand AddScheduleCommand =>
            _addScheduleCommand ?? (_addScheduleCommand = new DelegateCommand(ExecuteAddScheduleCommand));

        async void ExecuteAddScheduleCommand()
        {
            try
            {
                var sched = await _teacherService.AddSubject(GetPayload());
                _messageService.ShowMessage("Schedule added");
                _eventAggregator.GetEvent<ScheduleAddedEvent>().Publish(sched);
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private PayloadAddSchedule GetPayload()
        {

            if (SelectedSubject == null)
                return null;

            var payload = new PayloadAddSchedule()
            {
                DayOfWeek = GetDayOfWeek(),
                Room = Room,
                SubjectCode = SelectedSubject.Code,
                TimeFrom = new DateTime() + From,
                TimeTo = new DateTime() + To
            };

            return payload; 
            
        }

        private int GetDayOfWeek()
        {
            DayOfWeek day = (DayOfWeek) Enum.Parse(typeof(DayOfWeek), Day);
            
            return (int)day;
        }
    }
}
