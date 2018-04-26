using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton.Abstraction
{
  public abstract class ActionButtonContent : View
  {
    /// <summary>
    /// The color property.
    /// </summary>
    public static readonly BindableProperty ColorProperty =
                           BindableProperty.Create(nameof(Color),
                                                   typeof(Color),
                                                   typeof(ActionButtonContent),
                                                   Color.Transparent);

    /// <summary>
    /// Gets or sets the color.
    /// </summary>
    /// <value>The color.</value>
    public Color Color
    {
      get
      {
        return (Color)GetValue(ColorProperty);
      }

      set
      {
        SetValue(ColorProperty, value);
      }
    }
  }
}
