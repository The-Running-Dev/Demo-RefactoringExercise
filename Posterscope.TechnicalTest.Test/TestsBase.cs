using System.Linq;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using Posterscope.TechnicalTest.Interfaces;
using Posterscope.TechnicalTest.Services;
using Posterscope.TechnicalTest.Types;

namespace Posterscope.TechnicalTest.Test
{
    public class TestsBase
    {
        protected IPublishingService PublishingService;
        protected IUploadService UploadService;

        protected Mock<IDataStore> DataStoreMockObject;
        protected IDataStore DataStoreMock => DataStoreMockObject.Object;

        protected Mock<IUploadServiceFactory> UploadServiceFactoryMockObject;
        protected IUploadServiceFactory UploadServiceFactoryMock => UploadServiceFactoryMockObject.Object;

        protected List<Campaign> Campaigns = new List<Campaign>
        {
            new Campaign { Id = 1, CampaignStatus = CampaignStatus.Active },
            new Campaign { Id = 2, CampaignStatus = CampaignStatus.Inactive },
            new Campaign { Id = 3, CampaignStatus = CampaignStatus.Archived }
        };

        /// <summary>
        /// One time setup for all test execution
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DataStoreMockObject = new Mock<IDataStore>();
            UploadServiceFactoryMockObject = new Mock<IUploadServiceFactory>();

            var uploadServiceMockObject = new Mock<IUploadService>();
            uploadServiceMockObject.Setup(x => x.Upload(It.IsAny<IEnumerable<byte>>(), It.IsAny<string>())).Returns(true);

            UploadServiceFactoryMockObject
                .Setup(x => x.GetService(It.IsAny<PublishTypes>()))
                .Returns(uploadServiceMockObject.Object);

            // Mock the data store methods
            DataStoreMockObject.Setup(x => x.UpdateCampaign(It.IsAny<Campaign>())).Verifiable();
        }

        protected void SetupService()
        {
            PublishingService = new PublishingService(DataStoreMock, UploadServiceFactoryMock);
        }

        protected void SetupActiveCampaign()
        {
            DataStoreMockObject.Setup(x => x.GetCampaign(It.IsAny<int>())).Returns(Campaigns.FirstOrDefault());
            SetupService();
        }

        protected void SetupActiveCampaignWithScreens()
        {
            var campaign = Campaigns.FirstOrDefault();
            campaign.Screens = new List<Screen> { new Screen() };

            DataStoreMockObject.Setup(x => x.GetCampaign(It.IsAny<int>())).Returns(campaign);

            SetupService();
        }

        protected void SetupInactiveCampaign()
        {
            DataStoreMockObject.Setup(x => x.GetCampaign(It.IsAny<int>())).Returns(Campaigns.Skip(1).FirstOrDefault());
            SetupService();
        }

        protected void SetupArchivedCampaign()
        {
            DataStoreMockObject.Setup(x => x.GetCampaign(It.IsAny<int>())).Returns(Campaigns.Skip(2).FirstOrDefault());
            SetupService();
        }
    }
}