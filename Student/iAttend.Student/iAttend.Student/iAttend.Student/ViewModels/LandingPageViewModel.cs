using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iAttend.Student.ViewModels
{
	public class LandingPageViewModel : ViewModelBase
	{

        public LandingPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private DelegateCommand _openQrScannerCommand;
        public DelegateCommand OpenQrScannerCommand =>
            _openQrScannerCommand ?? (_openQrScannerCommand = new DelegateCommand(ExecuteOpenQrScannerCommand));

        async void ExecuteOpenQrScannerCommand()
        {
            await NavigationService.NavigateAsync(nameof(Views.ScannerPage));
        }
	}
}
