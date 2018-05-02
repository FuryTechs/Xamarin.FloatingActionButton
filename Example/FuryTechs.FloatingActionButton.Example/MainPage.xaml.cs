using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton.Example
{
  public partial class MainPage : ContentPage
  {
    int i = 0;
    static Random rnd = new Random();
    public MainPage()
    {
      InitializeComponent();
      Fab.Clicked += Fab_Clicked;
      MiniFab.Clicked += MiniFab_Clicked;
      AddButton();
    }

    void AddButton()
    {
      Task.Factory.StartNew(() =>
      {
        Thread.Sleep(5000);
        ActionButton btn = null;
        Color color = new Color(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble());
        btn = new ActionButton()
        {
          ButtonColor = color,
          Color = color.Luminosity < 0.3 ? Color.White : Color.Black,
          Size = i % 2 == 0 ? Abstraction.Size.Normal : Abstraction.Size.Mini,
          Clicked = (obj, evt) =>
          {
            if (btn?.Rotation == 0)
            {
              btn?.RotateTo(315, 500, Easing.SpringOut);
            }
            else
            {
              btn?.RotateTo(0, 500, Easing.SpringOut);
            }
          },
          Content = new FontAwesomeIcon()
          {
            Icon = FAIcons.FACheck
          }
        };
        Menu.Contents.Add(btn);
        if (i++ < 2)
          AddButton();
      });

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

    void MiniFab_Clicked(object arg1, EventArgs arg2)
    {
      if (MiniFab.Rotation == 0)
      {
        MiniFab.RotateTo(315, 500, Easing.SpringOut);
      }
      else
      {
        MiniFab.RotateTo(0, 500, Easing.SpringOut);
      }
    }
  }
}
