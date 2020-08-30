using NotificationServer;
using System.Collections.Generic;

namespace FractalProcessingServer
{
    public class ImageEvent : IEvent
    {
        // The pixels of this image
        public ICollection<ColoredPixelEvent> Pixels { get; set; }

        // The request associated to this image
        public FractalRequestEvent Request { get; set; }
    }

    public class PixelStoreLogic : Logic
    {
        private readonly IDictionary<FractalRequestEvent, ICollection<ColoredPixelEvent>> 
            _requestToPixels = new Dictionary<FractalRequestEvent, ICollection<ColoredPixelEvent>>();

        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            switch (eventToProcess)
            {
                case FractalRequestEvent requestEvent:
                    return ProcessFractalRequest(requestEvent);
                case ColoredPixelEvent coloredPixel:
                    return ProcessColoredPixel(coloredPixel);
                    break;
            }

            return null;
        }

        private FractalRequestEvent ProcessFractalRequest(FractalRequestEvent requestEvent)
        {
            _requestToPixels[requestEvent] = new List<ColoredPixelEvent>();
            return requestEvent;
        }

        private IEvent ProcessColoredPixel(ColoredPixelEvent coloredPixel)
        {
            FractalRequestEvent request = coloredPixel.Pixel.Request;

            ICollection<ColoredPixelEvent> requestPixels = _requestToPixels[request];
            requestPixels.Add(coloredPixel);
            
            if (requestPixels.Count == request.Height * request.Width)
            {
                _requestToPixels.Remove(request);

                var eventToPublish =
                    new ImageEvent()
                    {
                        Pixels = requestPixels,
                        Request = request
                    };

                return eventToPublish;
            }

            return null;
        }
    }
}