
using System.Collections.Generic;

namespace Posterscope.TechnicalTest.Services
{
    public class FileUploadService
    {
        public bool UploadFileToFTPServer(IEnumerable<byte> posterBytes, string screenContentPath)
        {
            //get credentials and upload file to an FTP server and return true if file has been correctly uploaded, code removed for brevity

            return false;
        }

        public bool UploadFileToAzureBlobStorage(IEnumerable<byte> posterBytes, string screenContentPath)
        {
            //get credentials and upload file to an Azure blob storage account and return true if file has been correctly uploaded, code removed for brevity

            return false;
        }
    }
}
