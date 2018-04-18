using System;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace FuryTechs.FloatingActionButton.Droid.Renderers.ContentTypes
{
  public interface IConvertable
  {
    void SetSize(Abstraction.Size size);

    Bitmap ToBitmap();
    Bitmap ToBitmap(int sizing);

    BitmapDrawable ToDrawable();
    BitmapDrawable ToDrawable(int sizing);
  }
}
