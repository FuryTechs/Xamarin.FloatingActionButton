using System;
using System.Collections.Generic;
using System.Text;
using FuryTechs.FloatingActionButton.Abstraction;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton.ContentTypes
{
  public class ButtonLabel : ActionButtonContent
  {
    /// <summary>
    /// The text property.
    /// </summary>
    public static readonly BindableProperty TextProperty =
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
        return (string)GetValue(TextProperty);
      }
      set
      {
        SetValue(TextProperty, value);
      }
    }

    /// <summary>
    /// The font property.
    /// </summary>
    public static readonly BindableProperty FontProperty =
                           BindableProperty.Create(nameof(ButtonLabel.Font),
                                                   typeof(Font),
                                                   typeof(ButtonLabel),
                                                   default(Font));


    /// <summary>
    /// Gets or sets the icon (and the text as well).
    /// </summary>
    /// <value>The icon.</value>
    public Font Font
    {
      get
      {
        return (Font)GetValue(FontProperty);
      }
      set
      {
        SetValue(FontProperty, value);
      }
    }

    /// <summary>
    /// The font size property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty =
                           BindableProperty.Create(nameof(ButtonLabel.FontSize),
                                                   typeof(int),
                                                   typeof(ButtonLabel),
                                                   default(int));


    /// <summary>
    /// Gets or sets the icon (and the text as well).
    /// </summary>
    /// <value>The icon.</value>
    public int FontSize
    {
      get
      {
        return (int)GetValue(FontSizeProperty);
      }
      set
      {
        SetValue(FontSizeProperty, value);
      }
    }

  }
}
