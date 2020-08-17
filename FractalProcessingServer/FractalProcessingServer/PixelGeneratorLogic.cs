using NotificationServer;

namespace FractalProcessingServer
{
    public class PixelGeneratorLogic : Logic
    {
        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            FractalRequestEvent request = eventToProcess as FractalRequestEvent;

            EventGroup result = new EventGroup();

            for (int x = 0; x < request.Width; x++)
            {
                for (int y = 0; y < request.Height; y++)
                {
                    var currentPixel = new PixelEvent()
                                       {
                                           Request = request,
                                           X = x,
                                           Y = y
                                       };

                    result.Add(currentPixel);
                }
            }

            return result;
        }
    }
}