using System.Drawing;
using System.Numerics;
using NotificationServer;

namespace FractalProcessingServer
{
    public class PixelEvent : IEvent
    {
        // The coordinates of the pixel in the bitmap
        public int X { get; set; }
        public int Y { get; set; }

        // The request associated with this pixel
        public FractalRequestEvent Request { get; set; }
    }
    
    public class ComplexPixelEvent : IEvent
    {
        // The number in the complex plane represented by this pixel
        public Complex ComplexNumber { get; set; }

        // The original pixel
        public PixelEvent Pixel { get; set; }
    }

    public class IteratedPixelEvent : IEvent
    {
        // Number of iterations done on this pixel
        public int Iterations { get; set; }

        // The original pixel
        public PixelEvent Pixel { get; set; }        
    }

    public class ColoredPixelEvent : IEvent
    {
        // The pixel's color
        public Color Color { get; set; }

        // The original pixel
        public PixelEvent Pixel { get; set; }
    }
}