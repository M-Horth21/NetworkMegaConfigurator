using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Stores
{
  class ModalNavigationStore
  {
    public event Action CurrentViewModelChanged;

    ViewModelBase _currentViewModel = null;

    public ViewModelBase CurrentViewModel
    {
      get => _currentViewModel;
      set
      {
        _currentViewModel = value;
        OnCurrentViewModelChanged();
      }
    }

    public bool IsOpen => CurrentViewModel != null;

    public void Close()
    {
      CurrentViewModel = null;
    }

    void OnCurrentViewModelChanged()
    {
      CurrentViewModelChanged?.Invoke();
    }
  }
}
