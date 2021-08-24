using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
        SvgViewbox dragObj = null;
        Point offset;
       
        private void CanvasImages_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            this.dragObj = null;
            this.canvasss.ReleaseMouseCapture();
           
        }
        private void CanvasImages_PreviewMouseMove(object sender, MouseEventArgs e)
        {

            if (this.dragObj == null)
            {
                return;
            }
            else
            {
                var position = e.GetPosition(sender as IInputElement);
                Canvas.SetTop(this.dragObj, position.Y - this.offset.Y);
                Canvas.SetLeft(this.dragObj, position.X - this.offset.X);
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

            SvgViewbox svg = new SvgViewbox();
            string path = "D:/Hannan/FlowChartUI/ZoomExample/Resources/magnet.svg";
            svg.Source = new System.Uri(path);
            svg.AutoSize = true;
            svg.AllowDrop = true;
            svg.OptimizePath = false;
            svg.TextAsGeometry = true;
            Canvas.SetTop(svg, 220);
            Canvas.SetLeft(svg, 469);
            // svg.MouseMove += OnMouseMove;
            svg.MouseLeftButtonDown += Img_MouseLeftButtonDown;
            //svg.PreviewMouseDown += Img_MouseLeftButtonDown;
            
            canvasss.Children.Add(svg);



        }
        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.dragObj = sender as SvgViewbox;
            this.offset = e.GetPosition(this.canvasss);
            this.offset.Y -= Canvas.GetTop(this.dragObj);
            this.offset.X -= Canvas.GetLeft(this.dragObj);
            this.canvasss.CaptureMouse();
        }


        void OnMouseMove(object sender, MouseEventArgs e)
        {
            /*if (lastDragPoint.HasValue)
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
        /*    var mousePos = e.GetPosition(scrollViewer);
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
    }
}