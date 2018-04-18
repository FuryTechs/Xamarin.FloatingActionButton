using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton.Abstraction
{
  public abstract class ActionButtonContent : View
  {
    public static readonly BindableProperty ColorProperty = 
                           BindableProperty.Create(nameof(Color), 
                                                   typeof(Color), 
                                                   typeof(ActionButtonContent), 
                                                   Color.LightGray,
                                                   propertyChanged: HandleBindingPropertyChangedDelegate);
    public Color Color { get; set; }

    static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
    {
      if(bindable is ActionButtonContent abc) {
        if(newValue is Color c) {
          abc.Color = c;
        }
      }
    }


  }
}
