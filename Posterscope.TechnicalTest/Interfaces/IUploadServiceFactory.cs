using Posterscope.TechnicalTest.Types;
using Posterscope.TechnicalTest.Services;

namespace Posterscope.TechnicalTest.Interfaces
{
    public interface IUploadServiceFactory
    {
        IUploadService GetService(PublishTypes publishType);
    }
}