using Posterscope.TechnicalTest.Types;

namespace Posterscope.TechnicalTest.Services
{
    public interface IPublishingService
    {
        PublishPosterResult PublishPosterToScreen(PublishPosterRequest request);
    }
}