using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ZXing;

namespace iAttend.Student.ViewModels
{
	public class ScannerPageViewModel : BindableBase
	{
        public ScannerPageViewModel()
        {

        }

        private DelegateCommand<Result> _scanResultCommand;
        public DelegateCommand<Result> ScanResultCommand =>
            _scanResultCommand ?? (_scanResultCommand = new DelegateCommand<Result>(ExecuteScanResultCommand));

        void ExecuteScanResultCommand(Result parameter)
        {
        }
	}
}
