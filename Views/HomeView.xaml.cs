﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkMegaConfigurator.Views
{
  /// <summary>
  /// Interaction logic for HomeView.xaml
  /// </summary>
  public partial class HomeView : UserControl
  {
    public HomeView()
    {
      InitializeComponent();
    }

    void OnRecentActionMouseWheel(object sender, MouseWheelEventArgs e)
    {
      var scrollViewer = (ScrollViewer)sender;
      if (scrollViewer == null) return;

      scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta / 3);
      e.Handled = true;
    }

    private void OnLoad(object sender, RoutedEventArgs e)
    {
      this.Focus();
    }
  }
}
