using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AdornerSample {
    public class InsertionLineAdordner : Adorner {
        bool lineOnTop;
        // Be sure to call the base class constructor. 
        public InsertionLineAdordner(UIElement adornedElement, bool lineOnTop) : base(adornedElement){
            this.lineOnTop = lineOnTop;
        }

        // A common way to implement an adorner's rendering behavior is to override the OnRender 
        // method, which is called by the layout system as part of a rendering pass. 
        protected override void OnRender(DrawingContext drawingContext){
            Point firstPoint;
            Point secondPoint;

            if(lineOnTop) {
                firstPoint = new Point(0, 0);
                secondPoint = new Point(100, 100);
            }
            else {
                firstPoint = new Point(0, AdornedElement.DesiredSize.Height);
                secondPoint = new Point(100, AdornedElement.DesiredSize.Height);
            }

            // Some arbitrary drawing implements.
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Blue);
            renderBrush.Opacity = 0.2;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
            drawingContext.DrawLine(renderPen, firstPoint, secondPoint);
            
            //double renderRadius = 5.0;

            //// Draw a circle at each corner.
            //drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
            //drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            //drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
            //drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
        }
    }
}
