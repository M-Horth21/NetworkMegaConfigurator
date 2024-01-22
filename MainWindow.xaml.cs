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
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace NetworkMegaConfigurator
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    void TopBarMouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton == MouseButtonState.Pressed)
      {
        DragMove();
      }
    }

    void btnClose_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    void OnRecentActionMouseWheel(object sender, MouseWheelEventArgs e)
    {
      var scrollViewer = (ScrollViewer)sender;
      if (scrollViewer == null) return;

      scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta / 3);
      e.Handled = true;
    }
  }
}
