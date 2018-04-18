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

    void FabMini_Clicked(object arg1, EventArgs arg2)
    {
      //if (FabMini.Rotation == 0)
      //{
      //  FabMini.RotateTo(315, 500, Easing.CubicInOut);
      //}
      //else
      //{
      //  FabMini.RotateTo(0, 500, Easing.CubicInOut);
      //}
    }

  }
}
