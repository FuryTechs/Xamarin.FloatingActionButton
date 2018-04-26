using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using FuryTechs.FloatingActionButton;
using FuryTechs.FloatingActionButton.Abstraction;
using FuryTechs.FloatingActionButton.Droid.Renderers.ContentTypes;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(FontAwesomeIcon), typeof(FontAweseomeIconRenderer))]
namespace FuryTechs.FloatingActionButton.Droid.Renderers.ContentTypes
{
  public class FontAweseomeIconRenderer : ViewRenderer<FontAwesomeIcon, TextView>, IConvertable
  {
    public FontAweseomeIconRenderer(Context context) : base(context)
    {
    }

    private Size size;
    public void SetSize(Size size)
    {
      this.size = size;
      if (Control != null)
        Control.TextSize = this.size == Size.Mini ? 14 : 18;
    }

    public Bitmap ToBitmap()
    {
      return ToBitmap(LayoutParams.WrapContent);
    }

    public Bitmap ToBitmap(int sizing)
    {
      if (!(MeasuredWidth > 0 && MeasuredHeight > 0))
      {
        Measure(MeasureSpec.MakeMeasureSpec(sizing, MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(sizing, MeasureSpecMode.Exactly));
        Layout(0, 0, MeasuredWidth, MeasuredHeight);
      }
      Bitmap bmp = Bitmap.CreateBitmap(MeasuredHeight, MeasuredWidth,
                                       Bitmap.Config.Argb8888);
      Canvas cvs = new Canvas(bmp);
      Draw(cvs);
      return bmp;
    }

    public BitmapDrawable ToDrawable(int sizing)
    {
      return new BitmapDrawable(Resources, ToBitmap(sizing));
    }

    public BitmapDrawable ToDrawable()
    {
      return ToDrawable(LayoutParams.WrapContent);
    }

    protected override void OnElementChanged(ElementChangedEventArgs<FontAwesomeIcon> e)
    {
      base.OnElementChanged(e);

      SetNativeControl(new TextView(Context)
      {
        Text = ((char)Element.Icon).ToString(),
        Gravity = GravityFlags.CenterHorizontal | GravityFlags.CenterVertical,
        Typeface = Typeface.CreateFromAsset(Context.Assets, "FontAwesome.ttf"),
        TextSize = size == Size.Normal ? 28 : 12,
        TextAlignment = TextAlignment.Center,
      });
      Control.SetTextColor(Element.Color.ToAndroid());
      Control.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
    }

  }
}
