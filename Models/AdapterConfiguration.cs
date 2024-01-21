using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkMegaConfigurator.Exceptions;

namespace NetworkMegaConfigurator.Models
{
  public class AdapterConfiguration
  {
    public bool DhcpEnabled { get; }
    public string IpAddress { get; }
    public string SubnetMask { get; }
    public string Gateway { get; }

    public bool ValidateString()
    {
      if (string.IsNullOrEmpty(IpAddress))
      {
        throw new InvalidIpException(IpAddress);
      }

      if (IpAddress.Split('.').Length != 4)
      {
        throw new InvalidIpException(IpAddress);
      }

      return true;
    }
  }
}
