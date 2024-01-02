using System.Drawing;

namespace DkVision.Core.Interfaces
{
    public interface IDkFilter
    {
        bool IsDebug { get; set; }
        Bitmap Execute(Bitmap source);
    }
}
