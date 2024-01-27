using NetworkMegaConfigurator.Models;
using NetworkMegaConfigurator.Stores;
using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Commands
{
  class ToggleFavoriteCommand : BaseCommand
  {
    private readonly ConfigureAdapterViewModel _configureAdapterViewModel;

    public ToggleFavoriteCommand(ConfigureAdapterViewModel configureAdapterViewModel)
    {
      _configureAdapterViewModel = configureAdapterViewModel;
    }
    public override void Execute(object? parameter)
    {
      bool favorite = (bool)parameter;

      ConfigurationModel config = new()
      {
        AdapterName = _configureAdapterViewModel.AdapterName,
        IpAddress = _configureAdapterViewModel.IpAddress,
        SubnetMask = _configureAdapterViewModel.Mask,
        Gateway = _configureAdapterViewModel.Gateway,
        Dhcp = _configureAdapterViewModel.DhcpEnabled
      };

      if (favorite) FavoritesStore.AddFavorite(config);
      else FavoritesStore.RemoveFavorite(config);
    }
  }
}
