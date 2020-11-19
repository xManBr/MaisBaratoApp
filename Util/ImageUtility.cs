using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Mercoplano.Maisbarato.Server.RESTful.Util
{
    public class ImageUtility
    {
        public static Byte[] ResizeImage2(byte[] PassedImage, ref int width, ref int height)
        {
            if ( (PassedImage != null ) && (PassedImage.Length > 0) )
            {
                MemoryStream bitmapDataStream = new MemoryStream(PassedImage);
                //Bitmap bitmap = new Bitmap(bitmapDataStream);

                byte[] resizedImage;
                using (Image orginalImage = Image.FromStream(bitmapDataStream))
                {
                    ImageFormat orginalImageFormat = orginalImage.RawFormat;
                    int orginalImageWidth = orginalImage.Width;
                    int orginalImageHeight = orginalImage.Height;
                    int resizedImageWidth = width; // Type here the width you want
                    int resizedImageHeight = height;
                    //int resizedImageHeight = Convert.ToInt32(resizedImageWidth * orginalImageHeight / orginalImageWidth);

                    if (orginalImageHeight > orginalImageWidth)
                    {
                        resizedImageWidth = Convert.ToInt32(resizedImageHeight * orginalImageWidth / orginalImageHeight);
                    }
                    else
                    {
                        resizedImageHeight = Convert.ToInt32(resizedImageWidth * orginalImageHeight / orginalImageWidth);
                    }

                    using (Bitmap bitmapResized = new Bitmap(orginalImage, resizedImageWidth, resizedImageHeight))
                    {
                        using (MemoryStream streamResized = new MemoryStream())
                        {
                            bitmapResized.Save(streamResized, orginalImageFormat);
                            resizedImage = streamResized.ToArray();
                        }
                    }
                    width = resizedImageWidth;
                    height = resizedImageHeight;
                }
                return resizedImage;
            }
            else
            {
                return new byte[] { };



            }
        }

        /*
        public static byte[] ReziseImageAlt()
        {
            Bitmap startBitmap = CreateBitmapFromBytes(imageBytes); // write CreateBitmapFromBytes  
            Bitmap newBitmap = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.DrawImage(startBitmap, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, startBitmap.Width, startBitmap.Height), GraphicsUnit.Pixel);
            }

            byte[] newBytes = CreateBytesFromBitmap(newBitmap); // write CreateBytesFromBitmap 

            return newBytes;
        }
        */

        // (RESIZE an image in a byte[] variable.)  
        public static byte[] CreateThumbnail(byte[] PassedImage, int LargestSide, ref int width)
        {
            byte[] ReturnedThumbnail;

            using (System.IO.MemoryStream StartMemoryStream = new System.IO.MemoryStream(),
                                NewMemoryStream = new System.IO.MemoryStream())
            {
                // write the string to the stream  
                StartMemoryStream.Write(PassedImage, 0, PassedImage.Length);

                // create the start Bitmap from the MemoryStream that contains the image  
                Bitmap startBitmap = new Bitmap(StartMemoryStream);

                // set thumbnail height and width proportional to the original image.  
                int newHeight;
                int newWidth;
                double HW_ratio;
                if (startBitmap.Height > startBitmap.Width)
                {
                    newHeight = LargestSide;
                    HW_ratio = (double)((double)LargestSide / (double)startBitmap.Height);
                    newWidth = (int)(HW_ratio * (double)startBitmap.Width);
                }
                else
                {
                    newWidth = LargestSide;
                    HW_ratio = (double)((double)LargestSide / (double)startBitmap.Width);
                    newHeight = (int)(HW_ratio * (double)startBitmap.Height);
                }

                // create a new Bitmap with dimensions for the thumbnail.  
                Bitmap newBitmap = new Bitmap(newWidth, newHeight);

                // Copy the image from the START Bitmap into the NEW Bitmap.  
                // This will create a thumnail size of the same image.  
                newBitmap = ResizeImage(startBitmap, newWidth, newHeight);

                // Save this image to the specified stream in the specified format.  
                newBitmap.Save(NewMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Fill the byte[] for the thumbnail from the new MemoryStream.  
                ReturnedThumbnail = NewMemoryStream.ToArray();
                width = newWidth;
            }

            // return the resized image as a string of bytes.  
            return ReturnedThumbnail;
        }

        // Resize a Bitmap  
        private static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            return resizedImage;
        }
    }
}