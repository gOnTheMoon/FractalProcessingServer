using NotificationServer;
using System.Drawing;

namespace FractalProcessingServer
{
    public class BitmapEvent : IEvent
    {
        public Bitmap Bitmap { get; set; }
        public FractalRequestEvent Request { get; set; }
    }

    public class BitmapConvertLogic : Logic
    {
        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            ImageEvent imageEvent = eventToProcess as ImageEvent;

            FractalRequestEvent request = imageEvent.Request;

            Bitmap bitmap = new Bitmap(request.Width, request.Height);

            foreach (ColoredPixelEvent coloredPixel in imageEvent.Pixels)
            {
                PixelEvent pixel = coloredPixel.Pixel;
                bitmap.SetPixel(pixel.X, pixel.Y, coloredPixel.Color);
            }

            return new BitmapEvent
                   {
                       Bitmap = bitmap,
                       Request = request
                   };
        }
    }
}