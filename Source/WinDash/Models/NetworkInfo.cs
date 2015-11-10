using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Security.Credentials;

namespace WinDash.Models
{
    public class NetworkInfo : ObservableObject
    {
        #region Private/Static fields
        private readonly static uint EthernetIanaType = 6;
        private readonly static uint WirelessInterfaceIanaType = 71;
        private static WiFiAccessStatus? accessStatus;
        private Dictionary<WiFiAvailableNetwork, WiFiAdapter> _wifiNetworks;
        #endregion

        #region Public properties
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

        public String CurrentIpc4Address
        {
            get
            {
                var icp = NetworkInformation.GetInternetConnectionProfile();
                if (icp != null && icp.NetworkAdapter != null && icp.NetworkAdapter.NetworkAdapterId != null)
                {
                    var name = icp.ProfileName;

                    var hostnames = NetworkInformation.GetHostNames();

                    foreach (var hn in hostnames)
                    {
                        if (hn.IPInformation != null &&
                            hn.IPInformation.NetworkAdapter != null &&
                            hn.IPInformation.NetworkAdapter.NetworkAdapterId != null &&
                            hn.IPInformation.NetworkAdapter.NetworkAdapterId == icp.NetworkAdapter.NetworkAdapterId &&
                            hn.Type == HostNameType.Ipv4)
                        {
                            return hn.CanonicalName;
                        }
                    }
                }
                return "No Internet connection";
            }
        }

        public List<WiFiAvailableNetwork> WifiNetworks { get { return _wifiNetworks.Keys.ToList(); } }

        public WiFiAvailableNetwork CurrentWifiNetwork
        {
            get
            {
                var connectionProfiles = NetworkInformation.GetConnectionProfiles();

                if (connectionProfiles.Count < 1)
                {
                    return null;
                }

                var validProfiles = connectionProfiles.Where(profile =>
                {
                    return (profile.IsWlanConnectionProfile && profile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.None);
                });

                if (validProfiles.Count() < 1)
                {
                    return null;
                }

                var firstProfile = validProfiles.First() as ConnectionProfile;

                return _wifiNetworks.Keys.FirstOrDefault(wifiNetwork => wifiNetwork.Ssid.Equals(firstProfile.ProfileName));
            }
        }
        #endregion
        
        #region Public methods
        public async void UpdateWifiInfo()
        {
            _wifiNetworks = new Dictionary<WiFiAvailableNetwork, WiFiAdapter>();
            var adapters = WiFiAdapter.FindAllAdaptersAsync();

            foreach (var adapter in await adapters)
            {
                await adapter.ScanAsync();

                if (adapter.NetworkReport == null)
                {
                    continue;
                }

                foreach (var network in adapter.NetworkReport.AvailableNetworks)
                {
                    if (!HasSsid(_wifiNetworks, network.Ssid))
                    {
                        _wifiNetworks[network] = adapter;
                    }
                }
            }
        }

        public async Task<bool> Connect(WiFiAvailableNetwork network,bool autoConnect, PasswordCredential password = null)
        {
            if (network == null)
            {
                return false;
            }

            WiFiConnectionResult result;

            if(password == null)
                result = await _wifiNetworks[network].ConnectAsync(network, autoConnect ? WiFiReconnectionKind.Automatic : WiFiReconnectionKind.Manual);
            else 
                result = await _wifiNetworks[network].ConnectAsync(network, autoConnect ? WiFiReconnectionKind.Automatic : WiFiReconnectionKind.Manual, password);

            return (result.ConnectionStatus == WiFiConnectionStatus.Success);
        }

        public void Disconnect(WiFiAvailableNetwork network)
        {
            _wifiNetworks[network].Disconnect();
        }
        #endregion

        #region Helpers
        private bool HasSsid(Dictionary<WiFiAvailableNetwork, WiFiAdapter> resultCollection, string ssid)
        {
            foreach (var network in resultCollection)
            {
                if (!string.IsNullOrEmpty(network.Key.Ssid) && network.Key.Ssid == ssid)
                {
                    return true;
                }
            }
            return false;
        }

        private static async Task<bool> TestAccess()
        {
            if (!accessStatus.HasValue)
            {
                accessStatus = await WiFiAdapter.RequestAccessAsync();
            }

            return (accessStatus == WiFiAccessStatus.Allowed);
        }
        #endregion
    }

    public class Connection
    {
        public string NetworkName { get; set; }
        public string NetworkIpv6 { get; set; }
        public string NetworkIpv4 { get; set; }
        public string NetworkStatus { get; set; }
    }
}
