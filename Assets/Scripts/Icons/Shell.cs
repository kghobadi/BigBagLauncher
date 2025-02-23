using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace IconExtraction
{
    internal sealed class Shell : NativeMethods
    {
        #region OfExtension

        ///<summary>
        /// Get the icon of an extension
        ///</summary>
        ///<param name="filename">filename</param>
        ///<param name="overlay">bool symlink overlay</param>
        ///<returns>Icon</returns>
        public static Icon OfExtension(string filename, bool overlay = false)
        {
            string filepath;
            string[] extension = filename.Split('.');
            string dirpath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "cache");
            Directory.CreateDirectory(dirpath);
            if (String.IsNullOrEmpty(filename) || extension.Length == 1)
            {
                filepath = Path.Combine(dirpath, "dummy_file");
            }
            else
            {
                filepath = Path.Combine(dirpath, String.Join(".", "dummy", extension[extension.Length - 1]));
            }
            if (File.Exists(filepath) == false)
            {
                File.Create(filepath);
            }
            Icon icon = OfPath(filepath, true, true, overlay);
            return icon;
        }
        #endregion

        #region OfFolder

        ///<summary>
        /// Get the icon of a folder
        ///</summary>
        ///<returns>Icon</returns>
        ///<param name="overlay">bool symlink overlay</param>
        public static Icon OfFolder(bool overlay = false)
        {
            string dirpath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "cache", "dummy");
            Directory.CreateDirectory(dirpath);
            Icon icon = OfPath(dirpath, true, true, overlay);
            return icon;
        }
        #endregion

        #region OfPath

        /// <summary>
        /// Simple grab from path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Icon IconFromFilePath(string filePath)
        {
            var result = (Icon)null;

            try
            {
                result = Icon.ExtractAssociatedIcon(filePath);
            }
            catch (System.Exception)
            {
                // swallow and return nothing. You could supply a default Icon here as well
            }

            return result;
        }
        
        ///<summary>
        /// Get the normal,small assigned icon of the given path
        ///</summary>
        ///<param name="filepath">physical path</param>
        ///<param name="small">bool small icon</param>
        ///<param name="checkdisk">bool fileicon</param>
        ///<param name="overlay">bool symlink overlay</param>
        ///<returns>Icon</returns>
        public static Icon OfPath(string filepath, bool small = true, bool checkdisk = true, bool overlay = false)
        {
            Icon clone;
            SHGFI_Flag flags;
            SHFILEINFO shinfo = new SHFILEINFO();
            if (small)
            {
                flags = SHGFI_Flag.SHGFI_ICON | SHGFI_Flag.SHGFI_SMALLICON;
            }
            else
            {
                flags = SHGFI_Flag.SHGFI_ICON | SHGFI_Flag.SHGFI_LARGEICON;
            }
            if (checkdisk == false)
            {
                flags |= SHGFI_Flag.SHGFI_USEFILEATTRIBUTES;
            }
            if (overlay)
            {
                flags |= SHGFI_Flag.SHGFI_LINKOVERLAY;
            }
            if (SHGetFileInfo(filepath, 0, ref shinfo, Marshal.SizeOf(shinfo), flags) == 0)
            {
                throw (new FileNotFoundException());
            }
            Icon tmp = Icon.FromHandle(shinfo.hIcon);
            clone = (Icon)tmp.Clone();
            tmp.Dispose();
            if (DestroyIcon(shinfo.hIcon) != 0)
            {
                return clone;
            }
            return clone;
        }
        
        public static Texture2D GetTextureFromIcon(Icon icon)
        {
            Bitmap bmp = icon.ToBitmap();
            var bitmapData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var length = bitmapData.Stride * bitmapData.Height;
            byte[] bytes = new byte[length];
            // load image
            Texture2D texture = new Texture2D(bmp.Width, bmp.Height);
            texture.LoadImage(ImageToByte(bmp));
            bmp.UnlockBits(bitmapData);

            return texture;
        }
    
        public static byte[] ImageToByte(Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        #endregion
    }
}