using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Exceptions
{
  public class InvalidIpException : Exception
  {
    public string IpInput { get; }

    public InvalidIpException(string ipInput)
    {
      IpInput = ipInput;
    }

    public InvalidIpException(string? message, string ipInput) : base(message)
    {
      IpInput = ipInput;
    }

    public InvalidIpException(string? message, Exception? innerException, string ipInput) : base(message, innerException)
    {
      IpInput = ipInput;
    }
  }
}
