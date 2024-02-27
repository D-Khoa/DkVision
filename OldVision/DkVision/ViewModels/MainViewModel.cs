using System;
using DevExpress.Mvvm;

namespace DkVision.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _FramePath= @"D:\05.Textures\03.Images\JPG\j900x600_bubbles_colourful.jpg";

        public string FramePath
        {
            get => _FramePath;
            set => SetProperty(ref _FramePath, value, nameof(FramePath));
        }
    }
}