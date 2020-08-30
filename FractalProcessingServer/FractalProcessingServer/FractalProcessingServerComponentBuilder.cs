using NotificationServer;

namespace FractalProcessingServer
{
    public class FractalProcessingServerComponentBuilder : IComponentBuilder
    {
        public void BuildComponents(IRuntimeContext context)
        {
            // Create components
            RequestListener requestListener = new RequestListener();
            PixelGeneratorLogic pixelGeneratorLogic = new PixelGeneratorLogic();
            PixelToComplexConvertLogic pixelToComplexConvertLogic = new PixelToComplexConvertLogic();
            MandelbrotLogic mandelbrotLogic = new MandelbrotLogic();
            JuliaLogic juliaLogic = new JuliaLogic();
            ColorConvertLogic colorConvertLogic = new ColorConvertLogic();
            PixelStoreLogic pixelStoreLogic = new PixelStoreLogic();
            BitmapConvertLogic bitmapConvertLogic = new BitmapConvertLogic();
            FileSystemDispatcher fileSystemDispatcher = new FileSystemDispatcher();

            // Add components to component container
            IComponentContainer componentContainer = context.Components;
            componentContainer.Add(requestListener);
            componentContainer.Add(pixelGeneratorLogic);
            componentContainer.Add(pixelToComplexConvertLogic);
            componentContainer.Add(mandelbrotLogic);
            componentContainer.Add(juliaLogic);
            componentContainer.Add(colorConvertLogic);
            componentContainer.Add(pixelStoreLogic);
            componentContainer.Add(bitmapConvertLogic);
            componentContainer.Add(fileSystemDispatcher);

            // Link components
            // Lower chain:
            requestListener.Subscribe(pixelStoreLogic, new ActivateAllRule());
            colorConvertLogic.Subscribe(pixelStoreLogic, new ActivateAllRule());
            pixelStoreLogic.Subscribe(bitmapConvertLogic, new TypeRule(typeof(ImageEvent)));
            bitmapConvertLogic.Subscribe(fileSystemDispatcher, new ActivateAllRule());

            // Upper chain
            pixelStoreLogic.Subscribe(pixelGeneratorLogic, new TypeRule(typeof(FractalRequestEvent)));
            pixelGeneratorLogic.Subscribe(pixelToComplexConvertLogic, new ActivateAllRule());
            pixelToComplexConvertLogic.Subscribe(mandelbrotLogic, new MandelbrotRule());
            pixelToComplexConvertLogic.Subscribe(juliaLogic, new JuilaRule());
            mandelbrotLogic.Subscribe(colorConvertLogic, new ActivateAllRule());
            juliaLogic.Subscribe(colorConvertLogic, new ActivateAllRule());
        }
    }
}