using System.Collections.Generic;

namespace Posterscope.TechnicalTest.Types
{
    public class PublishPosterRequest
    {
        public int CampaignId { get; set; }
        public int ScreenId { get; set; }
        public IEnumerable<byte> PosterBytes { get; set; }
        public string ScreenContentPath { get; set; }
    }
}
