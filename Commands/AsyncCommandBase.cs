﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Commands
{
  internal abstract class AsyncCommandBase : BaseCommand
  {
    bool _isExecuting;

    bool IsExecuting
    {
      get
      {
        return _isExecuting;
      }
      set
      {
        _isExecuting = value;
        OnCanExecuteChanged();
      }
    }

    public override bool CanExecute(object? parameter)
    {
      return !IsExecuting && base.CanExecute(parameter);
    }

    public override async void Execute(object? parameter)
    {
      IsExecuting = true;

      try
      {
        await ExecuteAsync(parameter);
      }
      finally
      {
        IsExecuting = false;
      }
    }

    public abstract Task ExecuteAsync(object parameter);
  }
}
