using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace ClassLibrary1
{ 
    public class Class1
    {      
        public class readHeader
        {
            byte[] imageHeader = new byte[128];

            public readHeader() { }
            //讀PCX header
           
            public readHeader(byte[] FileData)
            {
                Array.Copy(FileData, imageHeader, 128);
            }

            public readHeader(String FilePath)
            {
                byte[] imageFile = File.ReadAllBytes(FilePath);
                Array.Copy(imageFile, imageHeader, 128);
            }

            //9/29
            public byte[] Headerbit { get { byte[] Headerbit = new byte[128]; Array.Copy(imageHeader, Headerbit, 128); return Headerbit; } }
            //The fixed header field valued 必須是10
            public byte Manufacturer { get { return imageHeader[0]; } }
            //version number，5代表後面有256色的調色盤
            public byte Version { get { return imageHeader[1]; } }
            //The method used for encoding the image data，1爲RLE壓縮法
            public byte Encoding { get { return imageHeader[2]; } }
            //The number of bits constituting one pixel in a plane
            public byte BitsPerPixel { get { return imageHeader[3]; } }
            //boundary 通常（0，0）開始到（X,X），真實長寬記得+1.
            public ushort Xmin { get { return BitConverter.ToUInt16(imageHeader, 4); } }
            public ushort Ymin { get { return BitConverter.ToUInt16(imageHeader, 6); } }
            public ushort Xmax { get { return BitConverter.ToUInt16(imageHeader, 8); } }
            public ushort Ymax { get { return BitConverter.ToUInt16(imageHeader, 10); } }
            //The horizontal image resolution in DPI.
            public ushort Hres { get { return BitConverter.ToUInt16(imageHeader, 12); } }
            //The vertical image resolution in DPI.
            public ushort Vres { get { return BitConverter.ToUInt16(imageHeader, 14); } }
            //The EGA palette for 16-color images.
            public byte[] Palette { get { byte[] palette = new byte[48]; Array.Copy(imageHeader, 16, palette, 0, 48); return palette; } }
            //The first reserved field
            public byte Reserve { get { return imageHeader[64]; } }
            //The number of color planes constituting the pixel data
            public byte ColorPlanes { get { return imageHeader[65]; } }
            //The number of bytes of one color plane representing a single scan line
            public ushort BytesPerLine { get { return BitConverter.ToUInt16(imageHeader, 66); } }
            //The mode in which to construe the palette，1：彩色或黑白，2：灰度
            public ushort PaletteType { get { return BitConverter.ToUInt16(imageHeader, 68); } }
            //The second reserved field
            public byte[] Filled
            {
                get { byte[] filled = new byte[58]; Array.Copy(imageHeader, 70, filled, 0, 58); return filled; }
            }
            public int Width { get { return Xmax - Xmin + 1; } }

            public int Height { get { return Ymax - Ymin + 1; } }
        }

        //read PCX pic
        public class readPic
        {
            public readPic(String FilePath)
            {
                if (File.Exists(FilePath))
                {
                    DecodetoPcx(File.ReadAllBytes(FilePath));
                }
                else
                {
                    return;
                }
            }

            private Bitmap DecodeImage;
            public Bitmap getBitmap { get { return DecodeImage; } }

            readHeader FileHead;
            int readIndex;

            private void DecodetoPcx(byte[] FileBytes)
            {
                readIndex = 128;
                FileHead = new readHeader(FileBytes);
                if (FileHead.Manufacturer != 10) return;
                PixelFormat pixelFormat = PixelFormat.Format24bppRgb;

                DecodeImage = new Bitmap(FileHead.Width, FileHead.Height, pixelFormat);

                if (FileHead.ColorPlanes == 3 && FileHead.BitsPerPixel == 8)
                {
                    BitmapData DecoImageData = DecodeImage.LockBits(new Rectangle(0, 0, FileHead.Width, FileHead.Height), ImageLockMode.ReadWrite, pixelFormat);
                    byte[] RGBData = new byte[DecoImageData.Height * DecoImageData.Stride];
                    byte[] RowRGBData = new byte[0];
                    for (int i = 0; i < DecoImageData.Height; i++)
                    {
                        RowRGBData = decode24(FileBytes);
                        Array.Copy(RowRGBData, 0, RGBData, i * DecoImageData.Stride, RowRGBData.Length);
                    }
                    Marshal.Copy(RGBData, 0, DecoImageData.Scan0, RGBData.Length);
                    DecodeImage.UnlockBits(DecoImageData);
                }
                else if (FileHead.ColorPlanes == 1 && FileHead.BitsPerPixel == 8)
                {
                    DecodeImage = decode8(FileBytes);
                }
            }
            private Bitmap decode8(byte[] FileBytes)
            {

                byte[] AllPixelData = new byte[FileHead.Height * FileHead.Width];
                int EndIndex = FileBytes.Length - 769;
                int HaveWriteTo = 0;
                readTailPalette rtp = new readTailPalette(FileBytes);
                Color[] palette = rtp.getPalette();
                Bitmap Image24bit = new Bitmap(FileHead.Width, FileHead.Height, PixelFormat.Format24bppRgb);
                int index = 0;

                while (true)
                {
                    if (readIndex >= EndIndex) break;
                    byte ByteValue = FileBytes[readIndex];
                    if (ByteValue > 0xC0)
                    {
                        int Count = ByteValue - 0xC0;
                        readIndex++;
                        for (int i = 0; i < Count; i++)
                        {
                            int RGBIndex = i + HaveWriteTo;
                            AllPixelData[RGBIndex] = FileBytes[readIndex];
                        }
                        HaveWriteTo += Count;
                        readIndex++;
                    }
                    else
                    {
                        int RGBIndex = HaveWriteTo;
                        AllPixelData[RGBIndex] = ByteValue;
                        readIndex++;
                        HaveWriteTo++;
                    }
                }
                for (int j = 0; j < Image24bit.Height; j++)
                {
                    for (int i = 0; i < Image24bit.Width; i++)
                    {
                        Image24bit.SetPixel(i, j, palette[AllPixelData[index]]);
                        index++;
                    }
                }
                return Image24bit;
            }

            //解析24位全真彩圖像
            private byte[] decode24(byte[] FileBytes)
            {
                int lineWidth = FileHead.BytesPerLine;
                byte[] RowRGBData = new byte[lineWidth * 3];
                int HaveWriteTo = 0;
                int WriteToRGB = 2;
                while (true)
                {
                    byte ByteValue = FileBytes[readIndex];
                    if (ByteValue > 0xC0)
                    {
                        int Count = ByteValue - 0xC0;
                        readIndex++;
                        for (int i = 0; i < Count; i++)
                        {
                            if (HaveWriteTo + i >= lineWidth)
                            {
                                i = 0;
                                HaveWriteTo = 0;
                                WriteToRGB--;
                                Count = Count - i;
                                if (WriteToRGB == -1) break;
                            }
                            int RGBIndex = (i + HaveWriteTo) * 3 + WriteToRGB;
                            RowRGBData[RGBIndex] = FileBytes[readIndex];
                        }
                        HaveWriteTo += Count;
                        readIndex++;
                    }
                    else
                    {
                        int RGBIndex = HaveWriteTo * 3 + WriteToRGB;
                        RowRGBData[RGBIndex] = ByteValue;
                        readIndex++;
                        HaveWriteTo++;
                    }
                    if (HaveWriteTo >= lineWidth)
                    {
                        HaveWriteTo = 0;
                        WriteToRGB--;
                    }
                    if (WriteToRGB == -1) break;

                }
                return RowRGBData;
            }
        }
        //read Tail Palette
        public class readTailPalette
        {
            byte[] imageTailPalette = new byte[768];

            public readTailPalette() { }

            public readTailPalette(String FilePath)
            {
                byte[] imageFile = File.ReadAllBytes(FilePath);
                Array.Copy(imageFile, imageFile.Length - 768, imageTailPalette, 0, 768);
            }

            public readTailPalette(byte[] FileBytes)
            {
                Array.Copy(FileBytes, FileBytes.Length - 768, imageTailPalette, 0, 768);
            }

            public byte[] TailPaletee { get { return imageTailPalette; } }

            //get Palette color
            public Color[] getPalette()
            {
                int k = 3;
                Color[] palette = new Color[256];
                Color RGB;
                for (int i = 0; i < 256; i++)
                {
                    RGB = Color.FromArgb(imageTailPalette[i * k], imageTailPalette[i * k + 1], imageTailPalette[i * k + 2]);
                    palette.SetValue(RGB, i);
                }
                return palette;
            }
        }
    }
    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                int PixelCount = Width * Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat);

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }
    }
}
