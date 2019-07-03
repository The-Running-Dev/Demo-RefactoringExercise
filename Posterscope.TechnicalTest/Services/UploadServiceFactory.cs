using System;

using Posterscope.TechnicalTest.Types;
using Posterscope.TechnicalTest.Interfaces;

namespace Posterscope.TechnicalTest.Services
{
    public class UploadServiceFactory : IUploadServiceFactory
    {
        public IUploadService GetService(PublishTypes publishType)
        {
            switch (publishType)
            {
                case PublishTypes.Ftp:
                    return new FtpUploadService();
                case PublishTypes.AzureBlobStorage:
                    return new AzureUploadService();
                case PublishTypes.Unknown:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}