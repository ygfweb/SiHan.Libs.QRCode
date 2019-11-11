using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace SiHan.Libs.QRCode.Internal
{
    /// <summary>
    /// extensions methods which are working directly on any IBarcodeReaderGeneric implementation
    /// </summary>
    internal static class BarcodeReaderExtensions
    {
        /// <summary>
        /// uses the IBarcodeReaderGeneric implementation and the <see cref="SKBitmapLuminanceSource"/> class for decoding
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Result Decode(this IBarcodeReaderGeneric reader, SKBitmap image)
        {
            var luminanceSource = new SKBitmapLuminanceSource(image);
            return reader.Decode(luminanceSource);
        }

        /// <summary>
        /// uses the IBarcodeReaderGeneric implementation and the <see cref="SKBitmapLuminanceSource"/> class for decoding
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Result[] DecodeMultiple(this IBarcodeReaderGeneric reader, SKBitmap image)
        {
            var luminanceSource = new SKBitmapLuminanceSource(image);
            return reader.DecodeMultiple(luminanceSource);
        }
    }
}
