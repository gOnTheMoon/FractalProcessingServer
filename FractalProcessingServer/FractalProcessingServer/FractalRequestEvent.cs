using System.Numerics;
using NotificationServer;

namespace FractalProcessingServer
{
    public class FractalRequestEvent : IEvent
    {
        // Dimensions of the produced bitmap
        public int Width { get; set; }
        public int Height { get; set; }

        // The following four parameters describe the rectangular domain in the complex plane
        // we are interested in plotting
    
        // Left <= x <= Right
        public double Left { get; set; }
        public double Right { get; set; }

        // Bottom <= y <= Top
        public double Top { get; set; }
        public double Bottom { get; set; }

        // The maximum number of iterations to run the algorithm per point
        public int MaxNumberOfIterations { get; set; }

        // The file path to save the generated image to
        public string FilePath { get; set; }
    }

    public class MandelbrotRequestEvent : FractalRequestEvent
    {
    }

    public class JuliaRequestEvent : FractalRequestEvent
    {
        public Complex C { get; set; }
    }
}