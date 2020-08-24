using System.Drawing.Imaging;
using System.Linq;
using NotificationServer;

namespace FractalProcessingServer
{
    public class FileSystemDispatcher : Dispatcher, IComponent, IRecipient
    {
        public override void DoDispatch(EventGroup eventGroup)
        {
            foreach (BitmapEvent bitmapEvent in eventGroup.OfType<BitmapEvent>())
            {
                bitmapEvent.Bitmap.Save(bitmapEvent.Request.FilePath, ImageFormat.Png);
            }
        }

        public void HandleNotification(Notification notification)
        {
            throw new System.NotImplementedException();
        }
    }
}