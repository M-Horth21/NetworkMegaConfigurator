using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Models
{
  public class Adapter
  {
    public NetworkInterface NetInterface { get; }

    public Adapter(NetworkInterface netInterface)
    {
      NetInterface = netInterface;
    }

    public override bool Equals(object? obj)
    {
      return obj is NetworkInterface netInterface &&
        this.NetInterface.Name == netInterface.Name;
    }

    public override int GetHashCode() => HashCode.Combine(NetInterface.Name);

    public override string ToString() => NetInterface.Name;
  }
}
