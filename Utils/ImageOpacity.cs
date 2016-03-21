//http://www.geekpedia.com/code110_Set-Image-Opacity-Using-Csharp.html
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoPuTTY
{
	class ImageOpacity
    {
        public static Image Set(Image image, float opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            ColorMatrix cmx = new ColorMatrix();
            cmx.Matrix33 = opacity;

            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cmx, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, ia);
            ia.Dispose();
            gfx.Dispose();

            return bmp;
        }
    }
}