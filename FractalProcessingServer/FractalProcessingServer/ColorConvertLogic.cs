using System.Drawing;
using NotificationServer;

namespace FractalProcessingServer
{
    public class ColorConvertLogic : Logic
    {
        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            IteratedPixelEvent iteratedEvent = eventToProcess as IteratedPixelEvent;

            PixelEvent pixel = iteratedEvent.Pixel;

            return new ColoredPixelEvent()
                   {
                       Pixel = pixel,
                       Color = Grayscale(iteratedEvent.Iterations,
                                              pixel.Request.MaxNumberOfIterations)
                   };
        }

        private static Color Grayscale(int iterations, int maxIterations)
        {
            int value = ((maxIterations - iterations) * 255) / maxIterations;
            Color result = Color.FromArgb(value, value, value);
            return result;
        }
    }
}