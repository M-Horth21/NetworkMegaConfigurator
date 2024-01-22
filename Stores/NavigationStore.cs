using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Stores
{
  public class NavigationStore
  {
    public event Action CurrentViewModelChanged;

    ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
      get => _currentViewModel;
      set
      {
        _currentViewModel = value;
        OnCurrentViewModelChanged();
      }
    }

    void OnCurrentViewModelChanged()
    {
      CurrentViewModelChanged?.Invoke();
    }
  }
}
