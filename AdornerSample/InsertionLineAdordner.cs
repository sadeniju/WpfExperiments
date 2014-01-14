using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdornerSample {
    public enum InsertionMode {
        Top,
        Bottom,
        Middle
    }

    /// <summary>
    /// Represents an insertion line, giving the user visual clues.
    /// </summary>
    public class InsertionLineAdordner : ElementAdorner {
        #region Fields
        protected bool insertAboveItem;
        protected Pen renderPen;
        protected double lineWidth;
        #endregion

        #region Construction
        /// <summary>
        /// Constructs an instance.
        /// </summary>
        /// <param name="adornedElement"></param>
        public InsertionLineAdordner(UIElement adornedElement, bool insertAboveItem) : this(adornedElement, insertAboveItem, adornedElement.DesiredSize.Width){}

        /// <summary>
        /// Constructs an instance with a specific line width.
        /// </summary>
        /// <param name="adornedElement"></param>
        /// <param name="insertAboveItem"></param>
        public InsertionLineAdordner(UIElement adornedElement, bool insertAboveItem, double lineWidth): base(adornedElement) {
            this.insertAboveItem = insertAboveItem;
            this.lineWidth = lineWidth;
            renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1);
        }
        #endregion

        /// <summary>
        /// The adorner's rendering behavior, called by the layout system as part of a rendering pass.
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext){
            Point firstPoint;
            Point secondPoint;

            if(insertAboveItem) {
                firstPoint = new Point(0, 0);
                secondPoint = new Point(lineWidth, 0);
            }
            else{
                firstPoint = new Point(0, AdornedElement.DesiredSize.Height);
                secondPoint = new Point(lineWidth, AdornedElement.DesiredSize.Height);
            }

            drawingContext.DrawLine(renderPen, firstPoint, secondPoint);
        }

        /// <summary>
        /// Updates the adorner's position and redraws it. 
        /// </summary>
        /// <param name="insertAboveItem"></param>
        public void UpdatePosition(bool insertAboveItem) {
            if(this.insertAboveItem != insertAboveItem) {
                this.insertAboveItem = insertAboveItem;
                this.InvalidateVisual();    // force redraw
            }
        }
    }
}
