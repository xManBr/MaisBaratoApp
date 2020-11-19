using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;

namespace Mercoplano.Maisbarato.Server.RESTful.Util
{
    public class ExifStuff
    {
        // Orientations.
        public const int OrientationId = 0x0112;
        public enum ExifOrientations
        {
            Unknown = 0,
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 3,
            BottomLeft = 4,
            LeftTop = 5,
            RightTop = 6,
            RightBottom = 7,
            LeftBottom = 8,
        }

        public static ExifOrientations ImageOrientationFromArray(byte[] PassedImage)
        {
            MemoryStream bitmapDataStream = new MemoryStream(PassedImage);
            Bitmap bitmap = new Bitmap(bitmapDataStream);
            return ImageOrientation(bitmap);
        }

        // Return the image's orientation.
        public static ExifOrientations ImageOrientation(Image img)
        {
            // Get the index of the orientation property.
            int orientation_index =
                Array.IndexOf(img.PropertyIdList, OrientationId);

            // If there is no such property, return Unknown.
            if (orientation_index < 0) return ExifOrientations.Unknown;

            // Return the orientation value.
            return (ExifOrientations)
                img.GetPropertyItem(OrientationId).Value[0];
        }

        // Make an image to demonstrate orientations.
        public static Image OrientationImage(ExifOrientations orientation)
        {
            const int size = 64;
            Bitmap bm = new Bitmap(size, size);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.White);
                gr.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                // Orient the result.
                switch (orientation)
                {
                    case ExifOrientations.TopLeft:
                        break;
                    case ExifOrientations.TopRight:
                        gr.ScaleTransform(-1, 1);
                        break;
                    case ExifOrientations.BottomRight:
                        gr.RotateTransform(180);
                        break;
                    case ExifOrientations.BottomLeft:
                        gr.ScaleTransform(1, -1);
                        break;
                    case ExifOrientations.LeftTop:
                        gr.RotateTransform(90);
                        gr.ScaleTransform(-1, 1, MatrixOrder.Append);
                        break;
                    case ExifOrientations.RightTop:
                        gr.RotateTransform(-90);
                        break;
                    case ExifOrientations.RightBottom:
                        gr.RotateTransform(90);
                        gr.ScaleTransform(1, -1, MatrixOrder.Append);
                        break;
                    case ExifOrientations.LeftBottom:
                        gr.RotateTransform(90);
                        break;
                }

                // Translate the result to the center of the bitmap.
                gr.TranslateTransform(
                    size / 2, size / 2, MatrixOrder.Append);

                using (StringFormat string_format = new StringFormat())
                {
                    string_format.LineAlignment = StringAlignment.Center;
                    string_format.Alignment = StringAlignment.Center;
                    using (Font font = new Font("Times New Roman", 40,
                        GraphicsUnit.Point))
                    {
                        if (orientation == ExifOrientations.Unknown)
                        {
                            gr.DrawString("?", font, Brushes.Black,
                                0, 0, string_format);
                        }
                        else
                        {
                            gr.DrawString("F", font, Brushes.Black,
                                0, 0, string_format);
                        }
                    }
                }
            }

            return bm;
        }

        /*

        // Open the file and read its orientation information.
        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Open the file.
            Bitmap bm = new Bitmap(txtFile.Text);
            picOriginal.Image = bm;

            // Get the PropertyItems property from image.
            ExifStuff.ExifOrientations orientation =
                ExifStuff.ImageOrientation(bm);
            lblOrientation.Text = orientation.ToString();
            picOrientation.Image = ExifStuff.OrientationImage(orientation);
        }
        */
    }
}