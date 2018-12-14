using iAttend.Student.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace iAttend.Student.ViewModels
{
	public class AbsentStatPageViewModel : ViewModelBase
	{

        public ObservableCollection<Absent> AbsentStats { get; private set; }

        public AbsentStatPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            AbsentStats = new ObservableCollection<Absent>();

        }


        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (IsOnNavigatingToTriggered)
                return;

            base.OnNavigatingTo(parameters);

            var absentStat = parameters["absents"] as List<Absent>;
            absentStat.ForEach(x =>
            {
                AbsentStats.Add(x);
            });
        }
    }
}
