using Posterscope.TechnicalTest.Types;
using Posterscope.TechnicalTest.Interfaces;

namespace Posterscope.TechnicalTest.Data
{
    public class BackupCampaignDataStore: IDataStore
    {
        public Campaign GetCampaign(int id)
        {
            // access database to trivet account, code removed for brevity
            return new Campaign();
        }

        public void UpdateCampaign(Campaign campaign)
        {
            // updated campaign in database, code removed for brevity
        }
    }
}
