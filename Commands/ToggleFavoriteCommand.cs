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
    private readonly ConfigurationModel _configModel;

    public ToggleFavoriteCommand(ConfigurationModel configModel)
    {
      _configModel = configModel;
    }

    public override void Execute(object? parameter)
    {
      bool favorite = (bool)parameter;

      if (favorite) FavoritesStore.AddFavorite(_configModel);
      else FavoritesStore.RemoveFavorite(_configModel);
    }
  }
}
