using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton
{
  public abstract class FloatingActionButton<TEnum> : View
  {
    #region BindableProperties
    public static readonly BindableProperty IconProperty =
      BindableProperty.Create(nameof(Icon),
                              typeof(TEnum),
                              typeof(FloatingActionButton<TEnum>),
                              default(TEnum));

    public static readonly BindableProperty SizeProperty =
      BindableProperty.Create(nameof(Size),
                              typeof(int),
                              typeof(FloatingActionButton<TEnum>),
                              default(int));

    #endregion

    /// <summary>
    /// Gets or sets the icon (and the text as well).
    /// </summary>
    /// <value>The icon.</value>
    public TEnum Icon
    {
      get
      {
        return (TEnum)GetValue(IconProperty);
      }
      set
      {
        SetValue(IconProperty, value);
      }
    }

    /// <summary>
    /// Gets or sets the size of the button
    /// </summary>
    /// <value>The size of the button.</value>
    public int Size
    {
      get { return (int)GetValue(SizeProperty); }
      set
      {
        SetValue(SizeProperty, value);
        SetValue(WidthRequestProperty, value);
        SetValue(HeightRequestProperty, value);
      }
    }
  }
}