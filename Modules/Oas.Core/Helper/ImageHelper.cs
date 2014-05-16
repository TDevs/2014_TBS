using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace TDevs.Core
{

    public static class ImageHelper
    {
        public static Bitmap ReduceBitmap(Bitmap sourceImage)
        {

            if (sourceImage == null || sourceImage.Width <= 1 || sourceImage.Height <= 1)
                return null;
            Bitmap sourceImageBitmap = sourceImage as Bitmap;
            if (sourceImageBitmap == null || sourceImageBitmap.PixelFormat != PixelFormat.Format1bppIndexed)
                return null;
            Bitmap tmpImage = new Bitmap(sourceImage.Width / 2, sourceImage.Height / 2, PixelFormat.Format1bppIndexed);
            BitmapData sourceData = sourceImageBitmap.LockBits(new Rectangle(0, 0, sourceImageBitmap.Width, sourceImageBitmap.Height),
                ImageLockMode.ReadOnly, sourceImageBitmap.PixelFormat);
            BitmapData destData = tmpImage.LockBits(new Rectangle(0, 0, tmpImage.Width, tmpImage.Height),
                ImageLockMode.ReadOnly, tmpImage.PixelFormat);
            try
            {
                for (int i = 0; i < tmpImage.Height; i++)
                {
                    #region Older
                    //byte* sourceRow = (byte*)sourceData.Scan0 + (i * 2 * sourceData.Stride);
                    //byte* destRow = (byte*)destData.Scan0 + (i * destData.Stride);
                    //for (int j = 0; j < destData.Stride; destRow[j++] = 0) ;
                    //for (int j = 0; j < tmpImage.Width; j++)
                    //{
                    //    int sourceShift = 7 - (j % 4) * 2;
                    //    destRow[j / 8] |= (byte)(((sourceRow[j / 4] & (1 << sourceShift)) >> sourceShift) << (7 - (j % 8)));
                    //}
                    #endregion

                }
            }
            catch { }
            sourceImageBitmap.UnlockBits(sourceData);
            tmpImage.UnlockBits(destData);
            return tmpImage;
        }
    }
}
