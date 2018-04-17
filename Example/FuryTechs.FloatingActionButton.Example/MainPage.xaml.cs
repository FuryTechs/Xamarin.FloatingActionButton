using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton.Example
{
  public partial class MainPage : ContentPage
  {
    public void OnClick(object sender, EventArgs eventArgs)
    {
      if (fab.Rotation == 0)
      {
        fab.RotateTo(315, 500, Easing.CubicInOut);
      }
      else
      {
        fab.RotateTo(0, 500, Easing.CubicInOut);
      }
    }

    public MainPage()
    {
      InitializeComponent();
    }
  }
}
