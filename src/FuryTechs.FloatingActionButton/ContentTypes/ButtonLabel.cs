using System;
using System.Collections.Generic;
using System.Text;
using FuryTechs.FloatingActionButton.Abstraction;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton.ContentTypes
{
  public class ButtonLabel : ActionButtonContent
  {
    public static readonly BindableProperty IconProperty =
                           BindableProperty.Create(nameof(Text),
                                                   typeof(string),
                                                   typeof(ButtonLabel),
                                                   string.Empty);


    /// <summary>
    /// Gets or sets the icon (and the text as well).
    /// </summary>
    /// <value>The icon.</value>
    public string Text
    {
      get
      {
        return (string)GetValue(IconProperty);
      }
      set
      {
        SetValue(IconProperty, value);
      }
    }
  }
}
