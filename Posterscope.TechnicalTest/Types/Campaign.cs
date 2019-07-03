using System.Collections.Generic;

namespace Posterscope.TechnicalTest.Types
{
    public class Campaign
    {
        public int Id { get; set; }

        public CampaignStatus CampaignStatus { get; set; }
        
        public IEnumerable<Screen> Screens { get; set; }

    }
}
