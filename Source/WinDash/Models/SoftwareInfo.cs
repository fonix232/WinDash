using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDash.Models
{
    public class SoftwareInfo
    {
        public String OSVersion
        {
            get
            {
                ulong version = 0;
                if (!ulong.TryParse(Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamilyVersion, out version))
                {
                    return "Windows version unknown";
                }
                else
                {
                    return String.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}",
                    (version & 0xFFFF000000000000) >> 48,
                    (version & 0x0000FFFF00000000) >> 32,
                    (version & 0x00000000FFFF0000) >> 16,
                    version & 0x000000000000FFFF);
                }
            }
        }
    }
}
