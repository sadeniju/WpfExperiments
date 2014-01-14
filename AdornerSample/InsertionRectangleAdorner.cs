using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdornerSample {
    /// <summary>
    /// Represents an insertion rectangle to give the user visual feedback.
    /// </summary>
    public class InsertionRectangleAdordner : ElementAdorner {
        #region Fields
        protected Size rectangleSize;
        protected SolidColorBrush renderBrush;
        protected Pen renderPen;
        #endregion

        #region Construction
        /// <summary>
        /// Constructs an instance.
        /// </summary>
        /// <param name="adornedElement"></param>
        public InsertionRectangleAdordner(UIElement adornedElement)
            : this(adornedElement, new Size(adornedElement.DesiredSize.Width, adornedElement.DesiredSize.Height)) {
        }

        /// <summary>
        /// Constructs an instance with a rectangle size.
        /// </summary>
        /// <param name="adornedElement"></param>
        public InsertionRectangleAdordner(UIElement adornedElement, Size rectangleSize)
            : base(adornedElement) {
            this.rectangleSize = rectangleSize;

            renderBrush = new SolidColorBrush(Colors.Blue);
            renderBrush.Opacity = 0.5;
            renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
        }
        #endregion

        /// <summary>
        /// The adorner's rendering behavior, called by the layout system as part of a rendering pass.
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext) {
            Rect rect = new Rect(rectangleSize);
            drawingContext.DrawRectangle(renderBrush, renderPen, rect);
            Console.WriteLine("draaawing rect");
        }
    }
}
