using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton.Example
{
  public partial class MainPage : ContentPage
  {

    public MainPage()
    {
      InitializeComponent();
      Fab.Clicked += Fab_Clicked;
    }

    void Fab_Clicked(object arg1, EventArgs arg2)
    {
      if (Fab.Rotation == 0)
      {
        Fab.RotateTo(315, 500, Easing.SpringOut);
      }
      else
      {
        Fab.RotateTo(0, 500, Easing.SpringOut);
      }
    }
  }
}
