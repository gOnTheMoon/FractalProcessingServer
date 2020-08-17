using NotificationServer;

namespace FractalProcessingServer
{
    public class MandelbrotRule : IRule
    {
        public bool IsActivated(IEvent eventToCheck)
        {
            PixelEvent pixelEvent = eventToCheck as PixelEvent;

            if (pixelEvent != null)
            {
                return pixelEvent.Request is MandelbrotRequestEvent;
            }

            return false;
        }
    }

    public class JuilaRule : IRule
    {
        public bool IsActivated(IEvent eventToCheck)
        {
            PixelEvent pixelEvent = eventToCheck as PixelEvent;
    
            if (pixelEvent != null)
            {
                return pixelEvent.Request is JuliaRequestEvent;
            }
    
            return false;
        }
    }
}