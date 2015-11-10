using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinDash.Models;

namespace WinDash.ViewModels
{
    public class DeviceInfoViewModel : ViewModelBase
    {

        private DeviceInfo _DeviceInfo = new DeviceInfo();
        public DeviceInfo DeviceInfo
        {
            get { return _DeviceInfo; }
            set { Set(ref _DeviceInfo, value); }
        }


        private NetworkInfo _NetworkInfo = new NetworkInfo();
        public NetworkInfo NetworkInfo
        {
            get { return _NetworkInfo; }
            set { Set(ref _NetworkInfo, value); }
        }


        private SoftwareInfo _SoftwareInfo = new SoftwareInfo();
        public SoftwareInfo SoftwareInfo
        {
            get { return _SoftwareInfo; }
            set { Set(ref _SoftwareInfo, value); }
        }
    }
}
