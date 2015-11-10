using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace WinDash.Models
{
    public class NetworkInfo
    {
        private readonly static uint EthernetIanaType = 6;
        private readonly static uint WirelessInterfaceIanaType = 71;


        public String DirectConnectionName
        {
            get
            {
                var icp = NetworkInformation.GetInternetConnectionProfile();
                if (icp != null)
                {
                    if (icp.NetworkAdapter.IanaInterfaceType == EthernetIanaType)
                    {
                        return icp.ProfileName;
                    }
                }

                return null;
            }
        }


        public String CurrentNetworkName
        {
            get
            {
                var icp = NetworkInformation.GetInternetConnectionProfile();
                if (icp != null)
                {
                    return icp.ProfileName;
                }

                return "No Internet connection";
            }
        }
    }
}
