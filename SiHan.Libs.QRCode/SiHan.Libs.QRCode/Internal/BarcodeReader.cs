using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace SiHan.Libs.QRCode.Internal
{
    /// <summary>
    /// a barcode reader class which can be used with the SKBitmap type from SkiaSharp
    /// </summary>
    internal class BarcodeReader : BarcodeReader<SKBitmap>
    {
        /// <summary>
        /// define a custom function for creation of a luminance source with our specialized SKBitmap-supporting class
        /// </summary>
        private static readonly Func<SKBitmap, LuminanceSource> defaultCreateLuminanceSource =
           (image) => new SKBitmapLuminanceSource(image);

        /// <summary>
        /// constructor which uses a custom luminance source with SKImage support
        /// </summary>
        public BarcodeReader()
           : base(null, defaultCreateLuminanceSource, null)
        {
        }
    }
}
