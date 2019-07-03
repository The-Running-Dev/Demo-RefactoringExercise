using Posterscope.TechnicalTest.Types;
using Posterscope.TechnicalTest.Interfaces;

namespace Posterscope.TechnicalTest.Data
{
    public class CampaignDataStore : IDataStore
    {
        public Campaign GetCampaign(int id)
        {
            // access database to retrieve account, code removed for brevity
            return new Campaign();
        }

        public void UpdateCampaign(Campaign campaign)
        {
            // updated campaign in database, code removed for brevity
        }
    }
}
