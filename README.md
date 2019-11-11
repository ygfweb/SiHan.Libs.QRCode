# SiHan.Libs.QRCode
## 介绍

该库基于netstandard2.0，使用唯一的公开类`QrCodeHelper`实现二维码读取与生成。

该库可在ASP.NET Core 2.X、3.X、Winform中使用。

## 安装

```
PM> Install-Package SiHan.Libs.QRCode
```

## 部署

该库基于SiHan.Libs.Image，因此在linux下部署，需要拷贝[libSkiaSharp.so](https://github.com/mono/SkiaSharp/releases/tag/v1.68.0)文件。

