using System.Linq;
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
                    FilePath = @"mandelbrot2.png",
                    MaxNumberOfIterations = 100
                };

            JuliaRequestEvent request2 =
                new JuliaRequestEvent
                {
                    C = -1,
                    Bottom = -1.5,
                    Top = 1.5,
                    Left = -2,
                    Right = 2,
                    Width = 640,
                    Height = 480,
                    FilePath = @"julia2.png",
                    MaxNumberOfIterations = 100
                };

            FractalProcessingServerComponentBuilder componentBuilder =
                new FractalProcessingServerComponentBuilder();

            RuntimeContext runtimeContext = new RuntimeContext();
            componentBuilder.BuildComponents(runtimeContext);

            RequestListener listener =
                runtimeContext.Components.OfType<RequestListener>()
                              .FirstOrDefault();

            listener.OnNext(request);
            listener.OnNext(request2);
        }
    }
}
