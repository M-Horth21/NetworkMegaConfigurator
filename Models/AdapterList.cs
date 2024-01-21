using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Models
{
  public class AdapterList
  {
    readonly List<Adapter> _adapterList;

    public AdapterList()
    {
      _adapterList = new List<Adapter>();
    }

    public void AddAdapter(Adapter newAdapter)
    {
      _adapterList.Add(newAdapter);
    }
  }
}
