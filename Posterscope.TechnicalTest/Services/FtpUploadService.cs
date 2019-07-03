using System.Collections.Generic;

namespace Posterscope.TechnicalTest.Services
{
    public class FtpUploadService : IUploadService
    {
        /// <summary>
        /// Upload file to a FTP server and
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