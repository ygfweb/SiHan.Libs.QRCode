using SiHan.Libs.QRCode.Internal.Rendering;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace SiHan.Libs.QRCode.Internal
{
    /// <summary>
    /// A smart class to encode some content to a barcode image
    /// </summary>
    internal class BarcodeWriter : BarcodeWriter<SKBitmap>
    {
        /// <summary>
        /// 
        /// </summary>
        public BarcodeWriter()
        {
            Renderer = new SKBitmapRenderer();
        }
    }
}
