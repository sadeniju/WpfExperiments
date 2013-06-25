using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ErrorDialogSample {
    public class ImageGallery {
        public List<string> ImageFileNames { get; set; }
        public string GalleryDirectory { get; set; }

        public ImageGallery() {
            ImageFileNames = new List<string> { "Logo.png", "Product.jpg" };
            GalleryDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Image Gallery");
            Console.WriteLine(GalleryDirectory);
        }
    }
}
