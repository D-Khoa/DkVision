using DkVision.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DkVision.UserControls
{
    public class DkShape : INotifyPropertyChanged
    {
        private float _Width = 0;
        private float _Height = 0;
        private float _CenterX = 0;
        private float _CenterY = 0;
        private float _AngleX = 0;
        private float _AngleY = 0;
        private float _RotateAngle = 0;
        private GraphicsPath _Path = null;
        private PointF[] _Points = new PointF[0];
        private RectangleF _Rect = RectangleF.Empty;
        private ShapeTypes _ShapeType = ShapeTypes.Polygon;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text { get; set; }
        public float RotateAngle
        {
            get => _RotateAngle;
            set => SetAngle(value);
        }
        public float Width => _Width;
        public float Height => _Height;
        public float AngleX => _AngleX;
        public float AngleY => _AngleY;
        public float CenterX => _CenterX;
        public float CenterY => _CenterY;
        public RectangleF Rect => _Rect;
        public GraphicsPath Path => _Path;
        public PointF[] Points => _Points;
        public ShapeTypes ShapeType => _ShapeType;
        public bool IsVisible { get; set; } = true;
        public bool IsSelected { get; set; } = false;
        public BindingList<DkFilter> Filters { get; } = new BindingList<DkFilter>();

        public DkShape(ShapeTypes type, params PointF[] points)
        {
            _Points = points;
            _ShapeType = type;
            if (_Points.Length > 0)
            {
                float xMin = Points.Min(p => p.X);
                float yMin = Points.Min(p => p.Y);
                float xMax = Points.Max(p => p.X);
                float yMax = Points.Max(p => p.Y);
                _Width = (xMax - xMin);
                _Height = (yMax - yMin);
                _CenterX = xMin + _Width / 2;
                _CenterY = yMin + _Height / 2;
                _Rect = new RectangleF(xMin - _CenterX, yMin - _CenterY, _Width, _Height);
                RotateAngle = 0;
                UpdatePath();
            }
        }

        public void ToCircle()
        {
            _ShapeType = ShapeTypes.Circle;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShapeType"));
        }
        public void ToPolygon()
        {
            _ShapeType = ShapeTypes.Polygon;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShapeType"));
        }
        public void ToRectangle()
        {
            _ShapeType = ShapeTypes.Rectangle;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShapeType"));
        }
        public void UpdatePath()
        {
            _Path = new GraphicsPath();
            _Path.AddLines(ValidatePoints().ToArray());
            _Path.CloseFigure();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path"));
        }
        private void SetAngle(float value)
        {
            if (value > 359)
            {
                value = 0;
            }
            if (value < 0)
            {
                value = 359;
            }
            _RotateAngle = value;
            _AngleX = (float)(_Width * Math.Cos(_RotateAngle));
            _AngleY = (float)(_Height * Math.Sin(_RotateAngle));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AngleX"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AngleY"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RotateAngle"));
        }
        private IEnumerable<PointF> ValidatePoints()
        {
            foreach (PointF point in Points)
            {
                yield return new PointF(point.X - _CenterX, point.Y - _CenterY);
            }
        }
    }

    public enum ShapeTypes
    {
        Rectangle = 0,
        Circle = 1,
        Polygon = 2
    }
}
