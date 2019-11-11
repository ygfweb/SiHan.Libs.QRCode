using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace SiHan.Libs.QRCode.Internal
{
    internal class SKBitmapLuminanceSource : BaseLuminanceSource
    {
        /// <summary>
        /// initializing constructor
        /// </summary>
        /// <param name="image"></param>
        public SKBitmapLuminanceSource(SKBitmap image)
           : base(image.Width, image.Height)
        {
            CalculateLuminance(image);
        }

        /// <summary>
        /// internal constructor used by CreateLuminanceSource
        /// </summary>
        /// <param name="luminances"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected SKBitmapLuminanceSource(byte[] luminances, int width, int height)
           : base(luminances, width, height)
        {
        }

        /// <summary>
        /// Should create a new luminance source with the right class type.
        /// The method is used in methods crop and rotate.
        /// </summary>
        /// <param name="newLuminances">The new luminances.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
        {
            return new SKBitmapLuminanceSource(newLuminances, width, height);
        }

        private void CalculateLuminance(SKBitmap src)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            var pixels = src.Pixels;
            for (var index = 0; index < src.Width * src.Height; index++)
            {
                var pixel = pixels[index];
                // Calculate luminance cheaply, favoring green.
                var luminance = (byte)((RChannelWeight * pixel.Red + GChannelWeight * pixel.Green + BChannelWeight * pixel.Blue) >> ChannelWeight);
                luminances[index] = (byte)(((luminance * pixel.Alpha) >> 8) + (255 * (255 - pixel.Alpha) >> 8));
            }
        }
    }
}
