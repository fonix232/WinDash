using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinDash.Models;

namespace WinDash.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private LanguageInfo _LanguageInfo;
        public LanguageInfo LanguageInfo
        {
            get { return _LanguageInfo; }
            set { Set(ref _LanguageInfo, value); }
        }
    }
}
