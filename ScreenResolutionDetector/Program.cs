using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScreenResolutionDetector {
    class Program {
        static void Main(string[] args) {
            Size maxDimension = GetMaximumScreenSizePrimary();
            Console.WriteLine("Max. resolution: " + maxDimension.Width + " x " + maxDimension.Height + "\r\n");
            Console.WriteLine("Stereo available: " + IsStereoAvailable()+"\r\n");
            Console.WriteLine("Available resolutions:");
            PrintResolutions();
            Console.Read();
        }

        public static Size GetMaximumScreenSizePrimary(){
            var scope = new System.Management.ManagementScope();
            var q = new System.Management.ObjectQuery("SELECT * FROM CIM_VideoControllerResolution");
            
            UInt32 maxHResolution;
            UInt32 maxVResolution;

            using (var searcher = new System.Management.ManagementObjectSearcher(scope, q))
            {
                var results = searcher.Get();
                maxHResolution = 0;
                maxVResolution = 0;

                foreach (var item in results)
                {
                    if ((UInt32)item["HorizontalResolution"] > maxHResolution)
                        maxHResolution = (UInt32)item["HorizontalResolution"];

                    if ((UInt32)item["VerticalResolution"] > maxVResolution)
                        maxVResolution = (UInt32)item["VerticalResolution"];
                }
            }

            return new Size((int)maxHResolution, (int)maxVResolution);
        }

        /// <summary>
        /// Looks for the resolution 5760 x 1080. If the resolution is available, stereo mode is available.
        /// (Looking for the max resolution didn't work because it returned 5760 x 1200.)
        /// </summary>
        /// <returns></returns>
        public static bool IsStereoAvailable() {
            bool isStereoModeAvailable = false;
            var scope = new System.Management.ManagementScope();
            var q = new System.Management.ObjectQuery("SELECT * FROM CIM_VideoControllerResolution");

            using(var searcher = new System.Management.ManagementObjectSearcher(scope, q)) {
                var results = searcher.Get();

                foreach(var item in results) {
                    if((UInt32)item["HorizontalResolution"] == 5760 && (UInt32)item["VerticalResolution"] == 1080)
                        isStereoModeAvailable = true;
                }
            }

            return isStereoModeAvailable;
        }

        /// <summary>
        /// Looks for the resolution 5760 x 1080. If the resolution is available, stereo mode is available.
        /// (Looking for the max resolution didn't work because it returned 5760 x 1200.)
        /// </summary>
        /// <returns></returns>
        public static void PrintResolutions() {
            var scope = new System.Management.ManagementScope();
            var q = new System.Management.ObjectQuery("SELECT * FROM CIM_VideoControllerResolution");

            using(var searcher = new System.Management.ManagementObjectSearcher(scope, q)) {
                var results = searcher.Get();

                foreach(var item in results) {
                    Console.WriteLine((UInt32)item["HorizontalResolution"] + " x " + (UInt32)item["VerticalResolution"]);
                }
            }
        }
    }
}
