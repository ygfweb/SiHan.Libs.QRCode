using SiHan.Libs.Image;
using SiHan.Libs.QRCode.Internal;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace SiHan.Libs.QRCode
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public static class QrCodeHelper
    {
        /// <summary>
        /// 读取二维码图片
        /// </summary>
        internal static string ReadStream(Stream imageFile)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }
            using (var stream = new SKManagedStream(imageFile))
            {
                using (SKBitmap bitmap = SKBitmap.Decode(stream))
                {
                    if (bitmap == null)
                    {
                        return "";
                    }
                    else
                    {
                        Internal.BarcodeReader reader = new Internal.BarcodeReader();
                        Result result = reader.Decode(bitmap);
                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 读取二维码图片
        /// </summary>
        public static string ReadImage(ImageWrapper image)
        {
            using (MemoryStream stream = image.CreateMemoryStream())
            {
                return ReadStream(stream);
            }
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="contents">待生成二维码的文字</param>
        /// <param name="logoImage">logo图片</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="disableMargin">是否禁止生成边界</param>
        /// <returns></returns>
        internal static byte[] CreateStream(string contents, Stream logoImage = null, int width = 360, int height = 360, bool disableMargin = true)
        {
            if (string.IsNullOrEmpty(contents))
            {
                return null;
            }
            //本文地址：http://www.cnblogs.com/Interkey/p/qrcode.html
            EncodingOptions options = null;
            BarcodeWriter writer = null;
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
                ErrorCorrection = ErrorCorrectionLevel.H,
            };
            if (disableMargin)
            {
                options.Margin = 0;
            }
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            // 获取二维码图片
            using (SKBitmap qrBitmap = writer.Write(contents))
            {
                if (qrBitmap == null)
                {
                    throw new ImageException("生成二维码失败");
                }
                using (SKSurface surface = SKSurface.Create(new SKImageInfo(qrBitmap.Width, qrBitmap.Height)))
                {
                    SKCanvas canvas = surface.Canvas;
                    canvas.Clear(SKColors.White);
                    // 绘制二维码
                    canvas.DrawBitmap(qrBitmap, 0, 0);
                    if (logoImage != null && logoImage.Length > 0)
                    {
                        using (SKManagedStream managedStream = new SKManagedStream(logoImage))
                        {
                            using (SKBitmap logoBitmap = SKBitmap.Decode(managedStream))
                            {
                                if (logoBitmap != null)
                                {
                                    int deltaHeight = qrBitmap.Height - logoBitmap.Height;
                                    int deltaWidth = qrBitmap.Width - logoBitmap.Width;
                                    canvas.DrawBitmap(logoBitmap, deltaWidth / 2, deltaHeight / 2);
                                }
                            }
                        }
                    }
                    using (SKImage image = surface.Snapshot())
                    {
                        using (SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100))
                        {
                            return data.ToArray();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="logoImage">logo图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="disableMargin">禁止白边（默认为true）</param>
        /// <returns></returns>
        public static ImageWrapper CreateImage(string text, ImageWrapper logoImage = null, int width = 360, int height = 360, bool disableMargin = true)
        {
            if (logoImage == null)
            {
                byte[] bytes = QrCodeHelper.CreateStream(text, null, width, height, disableMargin);
                return new ImageWrapper(bytes);
            }
            else
            {
                using (MemoryStream stream = logoImage.CreateMemoryStream())
                {
                    byte[] bytes = QrCodeHelper.CreateStream(text, stream, width, height, disableMargin);
                    return new ImageWrapper(bytes);
                }
            }
        }
    }
}