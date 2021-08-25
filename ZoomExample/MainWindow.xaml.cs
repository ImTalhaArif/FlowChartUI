using System.Windows;
using System.Windows.Controls;
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

        private void OPEN_Click(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/magnet.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;
            
            canvasss.Children.Add(svg);



        }
        private void OPEN_Flag(object sender, RoutedEventArgs e)
        {

            SvgViewbox svg = new SvgViewbox();
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/flag.svg";
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
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/treasure.svg";
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
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/speaker.svg";
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
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/backtrack.svg";
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
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/output.svg";
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
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/homophone.svg";
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
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/bucket.svg";
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
            Line objLine = new Line();

            objLine.Stroke = System.Windows.Media.Brushes.Black;
            objLine.Fill = System.Windows.Media.Brushes.Black;

            //StartPoint
            objLine.X1 = aa.ActualWidth + aa.Margin.Left;
            objLine.Y1 = aa.ActualHeight + aa.Margin.Top;

            //EndPoint
            objLine.X2 = Application.Current.MainWindow.ActualWidth;
            objLine.Y2 = Application.Current.MainWindow.ActualHeight;

            grid.Children.Add(objLine);


            SvgViewbox svg = new SvgViewbox();
            string path = "C:/Users/Bytes-04/Documents/FlowChartUI/FlowChartUI-master (1)/FlowChartUI-master/ZoomExample/Resources/scale.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += OnMouseLeftButtonDown;

            canvasss.Children.Add(svg);



        }

        private void OPEN_Line(object sender, RoutedEventArgs e)
        {
            Line myLine;
            myLine = new Line();
            myLine.Stroke = Brushes.Black;
            myLine.X1 = 10;
            myLine.X2 = 1000;
            myLine.Y1 = 10;
            myLine.Y2 = 600;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 6;
            grid.Children.Add(myLine);
            
        }


        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
            }
        }

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(scrollViewer);
            if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y < scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                scrollViewer.Cursor = Cursors.SizeAll;
                lastDragPoint = mousePos;
                Mouse.Capture(scrollViewer);
            }
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
    }
}