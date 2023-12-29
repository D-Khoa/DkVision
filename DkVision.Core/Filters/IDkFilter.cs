using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public interface IDkFilter
    {
        Bitmap UpdateFrame(ref Image<Bgr, byte> frame);
    }
}
