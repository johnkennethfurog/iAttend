using iAttend.Student.DependencyServices;
using iAttend.Student.Interfaces;
using iAttend.Student.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace iAttend.Student.ViewModels
{
	public class ReportFilterPageViewModel : ViewModelBase
	{

        internal List<TeacherSubject> _subjects = new List<TeacherSubject>();

        public ObservableCollection<TeacherSubject> SelectedSubjects { get; private set; }

        public ReportFilterPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IMessageService messageService,
            ITeacherService teacherService) : base(navigationService)
        {
            SelectedSubjects = new ObservableCollection<TeacherSubject>();
            _pageDialogService = pageDialogService;
            _messageService = messageService;
            _teacherService = teacherService;
        }

        private DateTime _dateFrom;
        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set { SetProperty(ref _dateFrom, value); }
        }

        private DateTime _dateTo;
        public DateTime DateTo
        {
            get { return _dateTo; }
            set { SetProperty(ref _dateTo, value); }
        }

        private bool _generateAll;
        public bool GenerateAll
        {
            get { return _generateAll; }
            set { SetProperty(ref _generateAll, value); }
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var subjects = parameters["subjects"] as List<TeacherSubject>;
            var selectedSubject = parameters["subject"] as TeacherSubject;

            if (selectedSubject != null)
                SelectedSubjects.Add(selectedSubject);

            _subjects.AddRange(subjects);

        }

        private DelegateCommand<TeacherSubject> _deleteCommand;
        public DelegateCommand<TeacherSubject> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<TeacherSubject>(ExecuteDeleteCommand));

        void ExecuteDeleteCommand(TeacherSubject subj)
        {
            SelectedSubjects.Remove(subj);
            _subjects.Add(subj);
        }


        private DelegateCommand _selectSubjectCommand;
        private readonly IPageDialogService _pageDialogService;
        private readonly IMessageService _messageService;
        private readonly ITeacherService _teacherService;

        public DelegateCommand SelectSubjectCommand =>
            _selectSubjectCommand ?? (_selectSubjectCommand = new DelegateCommand(ExecuteSelectSubjectCommand));

        async void ExecuteSelectSubjectCommand()
        {
            var selected = await _pageDialogService.DisplayActionSheetAsync("", null, "Cancel",
                _subjects.Select(x => x.Tag).ToArray());

            if(selected != null)
            {
                var subj = _subjects.FirstOrDefault(x => x.Tag == selected);
                _subjects.Remove(subj);
                SelectedSubjects.Add(subj);
            }
        }

        private DelegateCommand _generateReportCommand;
        public DelegateCommand GenerateReportCommand =>
            _generateReportCommand ?? (_generateReportCommand = new DelegateCommand(ExecuteGenerateReportCommand));

        async void ExecuteGenerateReportCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var isSuccess = await _teacherService.GenerateReport(SelectedSubjects.ToList(),
                    GenerateAll ? new DateTime(DateTime.Now.Year - 1, 1, 1) : DateFrom.Date,
                    GenerateAll ? new DateTime(DateTime.Now.Year + 1, 1, 1) : DateTo.Date);
            }
            catch(TeacherServiceException ex)
            {
                _messageService.ShowMessage(ex.ExceptionMessage);
            }
            catch (Exception)
            {
                _messageService.ShowMessage("Something went wrong");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
