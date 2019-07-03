using System.Collections.Generic;

namespace Posterscope.TechnicalTest.Services
{
    public interface IUploadService
    {
        bool Upload(IEnumerable<byte> posterBytes, string screenContentPath);
    }
}