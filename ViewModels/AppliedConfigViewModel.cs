using NetworkMegaConfigurator.Commands;
using NetworkMegaConfigurator.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkMegaConfigurator.ViewModels
{
  class AppliedConfigViewModel : ViewModelBase
  {
    readonly ConfigureAdapterViewModel _configViewModel;
    readonly ModalNavigationStore _modalNavigationStore;

    string _caption = "";
    public string Caption
    {
      get
      {
        return _caption;
      }
      set
      {
        _caption = value;
        OnPropertyChanged(nameof(Caption));
      }
    }

    string _buttonCaption = "";
    public string ButtonCaption
    {
      get
      {
        return _buttonCaption;
      }
      set
      {
        _buttonCaption = value;
        OnPropertyChanged(nameof(ButtonCaption));
      }
    }

    bool _favorite;
    public bool Favorite
    {
      get
      {
        return _favorite;
      }
      set
      {
        _favorite = value;
        OnPropertyChanged(nameof(Favorite));
      }
    }

    bool _configComplete;
    public bool ConfigComplete
    {
      get
      {
        return _configComplete;
      }
      set
      {
        _configComplete = value;
        OnPropertyChanged(nameof(ConfigComplete));
      }
    }

    bool _readyToClose = false;

    public bool ReadyToClose
    {
      get
      {
        return _readyToClose;
      }
      set
      {
        _readyToClose = value;
        OnPropertyChanged(nameof(ReadyToClose));

      }
    }

    static List<string> _buttonCaptions = new()
    {
      "Thanks fam",
      "Bet",
      "No cap",
      "That's bussin",
      "That hits different",
      "Dank",
    };

    public ICommand ToggleFavorite { get; }
    public ICommand CloseModal { get; }

    public AppliedConfigViewModel(ConfigureAdapterViewModel configViewModel, ModalNavigationStore modalNavigationStore)
    {
      _configViewModel = configViewModel;
      _modalNavigationStore = modalNavigationStore;
      ToggleFavorite = new ToggleFavoriteCommand(configViewModel);
      CloseModal = new CloseModalCommand(_modalNavigationStore);
      Caption = $"Configuring {_configViewModel.AdapterName}...";

      var random = new Random();
      ButtonCaption = _buttonCaptions[random.Next(_buttonCaptions.Count)];

      Task.Run(WaitForConfigApplied);
    }

    async Task WaitForConfigApplied()
    {
      await Task.Delay(1000);
      ReadyToClose = true;
      Caption = "Configured!";

    }
  }
}
