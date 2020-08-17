using NotificationServer;

namespace FractalProcessingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MandelbrotRequestEvent request =
                new MandelbrotRequestEvent()
                {
                    Bottom = -1.5,
                    Top = 1.5,
                    Left = -2,
                    Right = 2,
                    Width = 640,
                    Height = 480,
                    FilePath = @"mandelbrot.png",
                    MaxNumberOfIterations = 100
                };

            PixelGeneratorLogic pixelGeneratorLogic = new PixelGeneratorLogic();
            PixelToComplexConvertLogic pixelToComplexConvertLogic = new PixelToComplexConvertLogic();
            MandelbrotLogic mandelbrotLogic = new MandelbrotLogic();
            ColorConvertLogic colorConvertLogic = new ColorConvertLogic();
            PixelStoreLogic pixelStoreLogic = new PixelStoreLogic();
            BitmapConvertLogic bitmapConvertLogic = new BitmapConvertLogic();
            FileSystemDispatcher fileSystemDispatcher = new FileSystemDispatcher();

            pixelStoreLogic.ProcessEvent(request);

            IEvent pixels = pixelGeneratorLogic.ProcessEvent(request);

            foreach (var pixel in (EventGroup)pixels)
            {
                IEvent processedEvent = pixelStoreLogic.ProcessEvent(colorConvertLogic.ProcessEvent(mandelbrotLogic
                                                                         .ProcessEvent(pixelToComplexConvertLogic
                                                                             .ProcessEvent(pixel))));

                if (processedEvent != null)
                {
                    fileSystemDispatcher.DoDispatch(new EventGroup() { bitmapConvertLogic.ProcessEvent(processedEvent) });
                }
            }
        }
    }
}
