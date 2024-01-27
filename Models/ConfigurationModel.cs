using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Models
{
  class ConfigurationModel : IEquatable<ConfigurationModel>
  {
    public string AdapterName { get; set; }
    public string IpAddress { get; set; }
    public string SubnetMask { get; set; }
    public string Gateway { get; set; }
    public bool Dhcp { get; set; }

    public bool Equals(ConfigurationModel? other)
    {
      return AdapterName == other.AdapterName
        && IpAddress == other.IpAddress
        && SubnetMask == other.SubnetMask
        && Gateway == other.Gateway
        && Dhcp == other.Dhcp;
    }
  }
}
