using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;

namespace AdornerSample {
    /// <summary>
    /// Adorner which has the ability to detach itself from its adorned element.
    /// </summary>
    public class ElementAdorner: Adorner {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="adornedElement"></param>
        public ElementAdorner(UIElement adornedElement): base(adornedElement) {
            AdornerLayer.GetAdornerLayer(adornedElement).Add(this);
            IsHitTestVisible = false;   // prevent the adorner layer from flickering
        }

        /// <summary>
        /// Removes this adorner from its element.
        /// </summary>
        public void Detach() {
            AdornerLayer.GetAdornerLayer(AdornedElement).Remove(this);
        }
    }
}
