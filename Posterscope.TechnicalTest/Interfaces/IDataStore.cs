using Posterscope.TechnicalTest.Types;

namespace Posterscope.TechnicalTest.Interfaces
{
    public interface IDataStore
    {
        Campaign GetCampaign(int id);

        void UpdateCampaign(Campaign campaign);
    }
}