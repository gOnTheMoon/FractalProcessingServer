using System.Numerics;
using NotificationServer;

namespace FractalProcessingServer
{
    public class MandelbrotLogic : Logic
    {
        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            ComplexPixelEvent complexPixel = eventToProcess as ComplexPixelEvent;

            Complex number = complexPixel.ComplexNumber;

            int maxNumberOfIterations = complexPixel.Pixel.Request.MaxNumberOfIterations;

            return new IteratedPixelEvent()
                   {
                       Pixel = complexPixel.Pixel,
                       Iterations = MathAlgorithms.QuadraticPolynomialEvaluation(number, number, maxNumberOfIterations)
                   };
        }
    }

    public class JuliaLogic : Logic
    {
        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            ComplexPixelEvent complexPixel = eventToProcess as ComplexPixelEvent;
    
            Complex number = complexPixel.ComplexNumber;

            JuliaRequestEvent juliaRequest = complexPixel.Pixel.Request as JuliaRequestEvent;

            int maxNumberOfIterations = juliaRequest.MaxNumberOfIterations;
            Complex c = juliaRequest.C;

            return new IteratedPixelEvent()
                   {
                       Pixel = complexPixel.Pixel,
                       Iterations = MathAlgorithms.QuadraticPolynomialEvaluation(number, c, maxNumberOfIterations)
                   };
        }
    }


    internal static class MathAlgorithms
    {
        public static int QuadraticPolynomialEvaluation(Complex number, Complex c, int maxNumberOfIterations)
        {
            int i = 0;

            Complex currentValue = number;

            while (currentValue.Magnitude < 2 && i < maxNumberOfIterations)
            {
                currentValue = currentValue * currentValue + c;
                i++;
            }

            return i;
        }        
    }
}