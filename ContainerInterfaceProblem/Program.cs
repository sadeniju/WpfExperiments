using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerInterfaceSolution {
    class Program {
        static void Main(string[] args) {
            // Construct a presentation for the test output
            Presentation presentation = new Presentation("Presentation");
            presentation.AddContent(new Page("1. Page"));

            Page secondPage = new Page("2. Page");
            secondPage.AddContent(new ImageContent("2.1 Image Content"));
            secondPage.AddContent(new Page("2.2 Page"));

            Gallery gallery = new Gallery("2.3 Gallery");
            gallery.AddContent(new ImageContent("2.3.1 Galleryimage 1"));
            gallery.AddContent(new ImageContent("2.3.2 Galleryimage 2"));

            secondPage.AddContent(gallery);
            presentation.AddContent(secondPage);

            // Print information about its contents
            PrintSubContents(presentation.Contents);
            Console.Read();
        }

        /// <summary>
        /// Recursively prints information about the given contents and their subcontents.
        /// </summary>
        /// <param name="contents"></param>
        static void PrintSubContents(List<Content> contents) {
            foreach(Content c in contents){
                Console.WriteLine((c.Parent as IPresentationModule).Title+" contains "+c.Title);
                if (c is IPresentationContainer) {
                    PrintSubContents((c as IPresentationContainer).Contents);
                }
            }
        }
    }
}
