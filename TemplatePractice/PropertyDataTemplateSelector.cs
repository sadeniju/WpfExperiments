using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using TemplatePractice;

namespace TemplatePractice
{
    class PropertyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate ImageSiteTemplate { get; set; }
        public DataTemplate TextSiteTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
                   DependencyObject container)
        {
            Site dpi = item as Site;
            if (dpi.Content is ContentImage)
            {
                return ImageSiteTemplate;
            }
            if (dpi.Content is Text)
            {
                return TextSiteTemplate;
            }

            return DefaultDataTemplate;
        }
    }
}
