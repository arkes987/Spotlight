using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Spotlight.Models
{
    public class Directory
    {

        public Directory()
        {
            TrackingPathes = new List<string>
            {
                @"C:\Users\arkes\Desktop",
                @"C:\Users\Public\Desktop"

            };
        }
        public List<string> TrackingPathes { get; set; }

        public List<Item> Retrack(string searchPhase)
        {
            var temp = new List<Item>();

            if (string.IsNullOrEmpty(searchPhase)) return temp;

            foreach (var path in TrackingPathes)
            {
                foreach (var item in System.IO.Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
                {
                    var fileName = Path.GetFileName(item);

                    if (fileName.ToLower().StartsWith(searchPhase.ToLower()))
                    {
                        temp.Add(new Item()
                        {
                            Name = fileName,
                            Priority = 0,
                            Path = Path.GetFullPath(item),
                            Icon = Imaging.CreateBitmapSourceFromHBitmap(
Icon.ExtractAssociatedIcon(item).ToBitmap().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
BitmapSizeOptions.FromEmptyOptions())
                        });
                    }
                }
            }
            return temp;
        }
    }
}
