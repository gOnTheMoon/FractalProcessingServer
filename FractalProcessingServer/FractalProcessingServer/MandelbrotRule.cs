using NotificationServer;

namespace FractalProcessingServer
{
    public class MandelbrotRule : IRule
    {
        public bool IsActivated(IEvent eventToCheck)
        {
            ComplexPixelEvent pixelEvent = eventToCheck as ComplexPixelEvent;

            if (pixelEvent != null)
            {
                return pixelEvent.Pixel.Request is MandelbrotRequestEvent;
            }

            return false;
        }
    }

    public class JuilaRule : IRule
    {
        public bool IsActivated(IEvent eventToCheck)
        {
            ComplexPixelEvent pixelEvent = eventToCheck as ComplexPixelEvent;
    
            if (pixelEvent != null)
            {
                return pixelEvent.Pixel.Request is JuliaRequestEvent;
            }
    
            return false;
        }
    }
}