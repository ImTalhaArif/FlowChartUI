using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using SharpVectors;
using SharpVectors.Converters;


namespace ZoomExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;

        public MainWindow()
        {
            InitializeComponent();

            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;

            scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            scrollViewer.MouseMove += OnMouseMove;

            slider.ValueChanged += OnSliderValueChanged;
        }
        Viewbox dragObj = null;
        Path linePath = null;
        Point offset;
        Point offsetForLine;
       


        private void CanvasImages_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            dragObj = null;
            this.canvasss.ReleaseMouseCapture();
           
        }
        private void CanvasImages_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            /*if(linePath != null)
            {
               // linePath.Data.
                var linePosition = e.GetPosition(sender as Path);
                LineGeometry myLineGeometry = new LineGeometry();
                myLineGeometry.StartPoint = new Point(linePosition.X, linePosition.Y);
                myLineGeometry.EndPoint = new Point(position.X + 10, position.Y);
            }*/
            if (dragObj == null)
            {
                return;
            }
            else
            {
                var position = e.GetPosition(sender as IInputElement);
               // MessageBox.Show(position.X + " " + position.Y);
                Canvas.SetTop(dragObj, position.Y - this.offset.Y);
                Canvas.SetLeft(dragObj, position.X - this.offset.X);
            }

            
        }



        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void UserControl_PreviewGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
       
        }

        private void OPEN_Click(object sender, RoutedEventArgs e)
        {

            Viewbox dynamicViewbox = new Viewbox();
            // Set StretchDirection and Stretch properties  
            dynamicViewbox.StretchDirection = StretchDirection.Both;
            dynamicViewbox.Stretch = Stretch.Uniform;
            dynamicViewbox.Width = 30;
            dynamicViewbox.Height = 20;

            Button magnetSvg = new Button();
            magnetSvg.Width = 30;
            magnetSvg.Height = 20;
           


            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/magnet.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
           
            // svg.MouseMove += OnMouseMove;
            dynamicViewbox.MouseLeftButtonDown += Img_MouseLeftButtonDown;
            dynamicViewbox.MouseRightButtonDown += Img_MouseRightButtonDown;
            
            //svg.PreviewMouseDown += Img_MouseLeftButtonDown;
            svg.AutoSize = true;
          
            
            //connectionPointRight.RenderTransformOrigin= "1.292,0.686";
            // connectionPointRight.Height



            // svg.Child = thumb;
          
            dynamicViewbox.Child = svg;
            //dynamicViewbox.Child = magnetSvg;
            
          
           // dynamicViewbox.Child = thumb;


            // addSvgHere.Content = svg;

            //Thumb thumb = new Thumb();
           // thumb.conte

            Canvas.SetTop(dynamicViewbox, 220);
            Canvas.SetLeft(dynamicViewbox, 469);
            canvasss.Children.Add(dynamicViewbox);



        }
        

        private void Img_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Viewbox svgLine = sender as Viewbox;
            var position = e.GetPosition(svgLine);
            //  double x= Convert.ToDouble(position.X);
            // double y = Convert.ToDouble(position.Y);
            //MessageBox.Show(x + " " + y);
            /*      Path myPath = new Path();
                  myPath.Stroke = System.Windows.Media.Brushes.Black;
                  myPath.Fill = System.Windows.Media.Brushes.MediumSlateBlue;
                  myPath.StrokeThickness = 3;


                  //myPath.HorizontalAlignment = HorizontalAlignment.Center;
                 // myPath.VerticalAlignment = VerticalAlignment.Center;
                  LineGeometry myLineGeometry = new LineGeometry();
                  myLineGeometry.StartPoint = new Point(position.X, position.Y);
                  myLineGeometry.EndPoint = new Point(position.X+10, position.Y);
                  myPath.Data = myLineGeometry;
                  myPath.MouseLeftButtonDown += myPath_MouseLeftButtonDown;*/

            /*    Line line = new Line();
                line.X1 = position.X;
                line.Y1 = position.Y;
                line.X2 = position.X;
                line.Y2 = position.Y;
                line.StrokeThickness = 1;*/

            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(position.X, position.Y);
            PointCollection myPointCollection = new PointCollection(6);
            myPointCollection.Add(new Point(10, 100));
            
            myPointCollection.Add(new Point(20, 200));
            myPointCollection.Add(new Point(30, 250));
            myPointCollection.Add(new Point(40, 300));


            PolyBezierSegment myBezierSegment = new PolyBezierSegment();
            myBezierSegment.Points = myPointCollection;

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myBezierSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;

            myPath.Data = myPathGeometry;

            Canvas.SetTop(myPath, Canvas.GetTop(svgLine));
            Canvas.SetLeft(myPath, Canvas.GetLeft(svgLine));
            canvasss.Children.Add(myPath);


        }
     
        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

      
            dragObj = sender as Viewbox;
            var pos = e.GetPosition(dragObj);
            this.offset = e.GetPosition(this.canvasss);
            this.offset.Y -= Canvas.GetTop(dragObj);
            this.offset.X -= Canvas.GetLeft(dragObj);
            

         
            this.canvasss.CaptureMouse();
        }
        private void myPath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            linePath = sender as Path;
           // var pos = e.GetPosition(linePath);
         
            this.canvasss.CaptureMouse();
        }
        public void Draw_Click(object sender, RoutedEventArgs e)
        {

            //  Path myPath = new Path();
            //  myPath.Stroke = System.Windows.Media.Brushes.Black;
            //  myPath.Fill = System.Windows.Media.Brushes.MediumSlateBlue;
            // myPath.StrokeThickness = 4;

            // myPath.HorizontalAlignment = HorizontalAlignment.Left;
            // myPath.VerticalAlignment = VerticalAlignment.Center;
            // LineGeometry myLineGeometry = new LineGeometry();
            // myLineGeometry.StartPoint = new Point(x, y);
            // myLineGeometry.EndPoint = new Point(x+10.2, y+10.3);
            // myPath.Data = myLineGeometry;

            Line line = new Line();
            //line.X1
            

           // Canvas.SetTop(myPath, 220);
          //  Canvas.SetLeft(myPath, 469);
           // canvasss.Children.Add(myPath);
            
        }
        private void OPEN_Flag(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/flag.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);

 

        }

        private void OPEN_Treasure(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/treasure.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);



        }

        private void OPEN_Speaker(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/treasure.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);



        }


        private void OPEN_Backtrack(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/treasure.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);



        }

        private void OPEN_Output(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/treasure.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);



        }

        private void OPEN_Homophone(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/treasure.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);



        }

        private void OPEN_Bucket(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/treasure.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);



        }

        private void OPEN_Scale(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/treasure.svggity";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);



        }


        void OnMouseMove(object sender, MouseEventArgs e)
        {
           /* if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
            }*/
        }

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          /*  var mousePos = e.GetPosition(scrollViewer);
            if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y < scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                scrollViewer.Cursor = Cursors.SizeAll;
                lastDragPoint = mousePos;
                Mouse.Capture(scrollViewer);
            }*/
        }

        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            if (e.Delta > 0)
            {
                slider.Value += 1;
            }
            if (e.Delta < 0)
            {
                slider.Value -= 1;
            }

            e.Handled = true;
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.Cursor = Cursors.Arrow;
            scrollViewer.ReleaseMouseCapture();
            lastDragPoint = null;
        }

        void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            scaleTransform.ScaleX = e.NewValue;
            scaleTransform.ScaleY = e.NewValue;

            var centerOfViewport = new Point(scrollViewer.ViewportWidth/2, scrollViewer.ViewportHeight/2);
            lastCenterPositionOnTarget = scrollViewer.TranslatePoint(centerOfViewport, grid);
        }

        void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(scrollViewer.ViewportWidth/2, scrollViewer.ViewportHeight/2);
                        Point centerOfTargetNow = scrollViewer.TranslatePoint(centerOfViewport, grid);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(grid);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth/grid.Width;
                    double multiplicatorY = e.ExtentHeight/grid.Height;

                    double newOffsetX = scrollViewer.HorizontalOffset - dXInTargetPixels*multiplicatorX;
                    double newOffsetY = scrollViewer.VerticalOffset - dYInTargetPixels*multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    scrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    scrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}