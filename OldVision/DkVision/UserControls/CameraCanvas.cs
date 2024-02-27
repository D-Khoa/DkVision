using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DkVision.UserControls
{
    public class CameraCanvas : Canvas
    {
        public DependencyProperty FramePathProperty = DependencyProperty.Register("FramePath", typeof(string), typeof(CameraCanvas));

        public string FramePath
        {
            get => GetValue(FramePathProperty)?.ToString();
            set => SetValue(FramePathProperty, value);
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (File.Exists(FramePath))
            {
                BitmapImage img = new BitmapImage(new Uri(FramePath));
                double x = (this.ActualWidth - img.PixelWidth) / 2;
                double y = (this.ActualHeight - img.PixelHeight) / 2;
                dc.DrawImage(img, new Rect(x, y, img.PixelWidth, img.PixelHeight));
            }
        }
    }
}
