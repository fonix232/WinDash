using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace WinDash.Models
{
    public class DeviceInfo
    {
        public String Name
        {
            get
            {
                var hostname = NetworkInformation.GetHostNames()
                    .FirstOrDefault(x => x.Type == HostNameType.DomainName);

                if (hostname != null)
                {
                    return hostname.CanonicalName;
                }
                return "<no device name>";
            }
        }

        public String BoardName
        {
            get
            {
                switch (Board)
                {
                    case Board.RPI2:
                        return "Raspberry Pi 2";
                    case Board.MBM:
                        return "MinnowBoard Max";
                    case Board.DB410:
                        return "DragonBoard 410c";
                    case Board.Generic:
                    default:
                        return "IoTCore Board";
                    case Board.Unkown:
                        return "ERROR Reading board info";
                }

            }
        }


        public Uri BoardImage
        {
            get
            {
                switch (Board)
                {
                    case Board.RPI2:
                        return new Uri("ms-appx:///Assets/RaspberryPiBoard.png");
                    case Board.MBM:
                        return new Uri("ms-appx:///Assets/MBMBoard.png");
                    case Board.DB410:
                        return new Uri("ms-appx:///Assets/DB410Board.png");
                    default:
                        return new Uri("ms-appx:///Assets/GenericBoard.png");
                }
            }
        }

        private Board _board = Board.Unkown;
        public Board Board
        {
            get
            {
                if (_board == Board.Unkown)
                {
                    var deviceInfo = new EasClientDeviceInformation();
                    if (deviceInfo.SystemProductName.IndexOf("MinnowBoard", StringComparison.OrdinalIgnoreCase) >= 0)
                        _board = Board.MBM;
                    else if (deviceInfo.SystemProductName.IndexOf("Raspberry", StringComparison.OrdinalIgnoreCase) >= 0)
                        _board = Board.RPI2;
                    else if (deviceInfo.SystemProductName == "SBC")
                        _board = Board.DB410;
                    else
                        _board = Board.Generic;
                }

                return _board;
            }
        }
    }

    public enum Board
    {
        RPI2,
        MBM,
        DB410,
        Generic,
        Unkown
    }
}
