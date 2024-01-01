using System.Drawing;

namespace DkVision.Core.Interfaces
{
    public interface IDkFilter
    {
        Bitmap Execute(Bitmap source);
    }
}
