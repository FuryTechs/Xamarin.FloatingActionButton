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
      //Fab.Clicked += Fab_Clicked;
      //MiniFab.Clicked += MiniFab_Clicked;
    }

    void Fab_Clicked(object arg1, EventArgs arg2)
    {
      //if (Fab.Rotation == 0)
      //{
      //  Fab.RotateTo(315, 500, Easing.SpringOut);
      //}
      //else
      //{
      //  Fab.RotateTo(0, 500, Easing.SpringOut);
      //}
    }

    void MiniFab_Clicked(object arg1, EventArgs arg2)
    {
      //if (MiniFab.Rotation == 0)
      //{
      //  MiniFab.RotateTo(315, 500, Easing.SpringOut);
      //}
      //else
      //{
      //  MiniFab.RotateTo(0, 500, Easing.SpringOut);
      //}
    }
  }
}
