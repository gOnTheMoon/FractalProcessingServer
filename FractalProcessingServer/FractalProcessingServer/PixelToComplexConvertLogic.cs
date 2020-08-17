using System.Numerics;
using NotificationServer;

namespace FractalProcessingServer
{
    public class PixelToComplexConvertLogic : Logic
    {
        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            PixelEvent pixelEvent = eventToProcess as PixelEvent;

            ComplexPixelEvent result = new ComplexPixelEvent()
                                       {
                                           Pixel = pixelEvent,
                                           ComplexNumber = ComputeComplexNumber(pixelEvent)
                                       };

            return result;
        }

        private Complex ComputeComplexNumber(PixelEvent pixelEvent)
        {
            FractalRequestEvent request = pixelEvent.Request;

            double x = ComputeCoordinate(pixelEvent.X, request.Width, request.Left, request.Right);
            double y = ComputeCoordinate(pixelEvent.Y, request.Height, request.Top, request.Bottom);

            return new Complex(x, y);

            double ComputeCoordinate(int pixelCoordinate, int pixelMax, double start, double end)
            {
                return start + ((double) pixelCoordinate) / (pixelMax - 1) * (end - start);
            }
        }
    }
}