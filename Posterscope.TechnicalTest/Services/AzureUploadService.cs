using System.Collections.Generic;

namespace Posterscope.TechnicalTest.Services
{
    public class AzureUploadService : IUploadService
    {
        /// <summary>
        /// Upload file to an  an Azure blob storage and
        /// return true if file has been correctly uploaded
        /// </summary>
        /// <param name="posterBytes"></param>
        /// <param name="screenContentPath"></param>
        /// <returns></returns>
        public bool Upload(IEnumerable<byte> posterBytes, string screenContentPath)
        {
            return false;
        }
    }
}
