using System;
using System.Linq;
using System.Collections.Generic;

using Moq;
using Should;
using NUnit.Framework;

using Posterscope.TechnicalTest.Services;
using Posterscope.TechnicalTest.Types;

namespace Posterscope.TechnicalTest.Test.Services
{
    [TestFixture]
    public class PublishingServiceTests : TestsBase
    {
        [Test]
        public void Publishing_Should_Succeed_For_Campaign_With_Screens()
        {
            SetupActiveCampaignWithScreens();

            PublishingService.PublishPosterToScreen(new PublishPosterRequest()).Success.ShouldBeTrue();
        }

        [Test]
        public void Campaign_Should_Be_Updated_If_Upload_Succeeds()
        {
            SetupActiveCampaignWithScreens();
            var defaultLastPublishDateTime = Campaigns.FirstOrDefault().Screens.FirstOrDefault().LastPublishDateTime;

            PublishingService.PublishPosterToScreen(new PublishPosterRequest()).Success.ShouldBeTrue();
            var lastPublishDateTime = Campaigns.FirstOrDefault().Screens.FirstOrDefault().LastPublishDateTime;

            // The last publish date/time should be greater than the default
            lastPublishDateTime.ShouldBeGreaterThan(defaultLastPublishDateTime);

            // The UpdateCampaign method should have been called exactly 1 time
            DataStoreMockObject.Verify(x => x.UpdateCampaign(Campaigns.FirstOrDefault()), Times.Exactly(1));
        }

        [Test]
        public void Publishing_Should_Fail_For_Campaign_With_No_Screens()
        {
            SetupActiveCampaign();

            PublishingService.PublishPosterToScreen(new PublishPosterRequest()).Success.ShouldBeFalse();
        }

        [Test]
        public void Publishing_Should_Fail_For_Campaign_With_No_Matching_Screens()
        {
            var campaign = Campaigns.FirstOrDefault();
            campaign.Screens = new List<Screen> { new Screen() { Id = 1 } };

            // Setup the mock data store to return our campaign
            DataStoreMockObject.Setup(x => x.GetCampaign(It.IsAny<int>())).Returns(campaign);

            SetupService();

            PublishingService.PublishPosterToScreen(new PublishPosterRequest()).Success.ShouldBeFalse();
        }

        [Test]
        public void Publishing_Should_Fail_If_Upload_Fails()
        {
            var campaign = Campaigns.FirstOrDefault();
            campaign.Screens = new List<Screen> { new Screen() };

            // Setup the mock data store to return our campaign
            DataStoreMockObject.Setup(x => x.GetCampaign(It.IsAny<int>())).Returns(campaign);

            PublishingService = new PublishingService(DataStoreMock, new UploadServiceFactory());

            PublishingService.PublishPosterToScreen(new PublishPosterRequest()).Success.ShouldBeFalse();
        }

        [Test]
        public void Publishing_Should_Throw_Exception_On_Unknown_Publish_Type()
        {
            var campaign = Campaigns.FirstOrDefault();
            campaign.Screens = new List<Screen> { new Screen { PublishType = PublishTypes.Unknown } };

            // Setup the mock data store to return our campaign
            DataStoreMockObject.Setup(x => x.GetCampaign(It.IsAny<int>())).Returns(campaign);

            PublishingService = new PublishingService(DataStoreMock, new UploadServiceFactory());

            Assert.Throws<ArgumentOutOfRangeException>(() => PublishingService.PublishPosterToScreen(new PublishPosterRequest()));
        }

        [Test]
        public void Campaign_Should_Not_Be_Updated_If_Upload_Fails()
        {
            var campaign = Campaigns.FirstOrDefault();
            campaign.Screens = new List<Screen> { new Screen() };
            var defaultLastPublishDateTime = campaign.Screens.FirstOrDefault().LastPublishDateTime;

            // Setup the mock data store to return our campaign
            DataStoreMockObject.Setup(x => x.GetCampaign(It.IsAny<int>())).Returns(campaign);

            var service = new PublishingService(DataStoreMock, new UploadServiceFactory());
            var lastPublishDateTime = campaign.Screens.FirstOrDefault().LastPublishDateTime;

            // Success should be false
            service.PublishPosterToScreen(new PublishPosterRequest()).Success.ShouldBeFalse();

            // The last publish date/time should be the default
            lastPublishDateTime.ShouldEqual(defaultLastPublishDateTime);

            // The UpdateCampaign method should have never been called
            DataStoreMockObject.Verify(x => x.UpdateCampaign(new Campaign()), Times.Never());
        }

        [Test]
        public void Publishing_Should_Fail_For_Inactive_Campaign()
        {
            SetupInactiveCampaign();

            PublishingService.PublishPosterToScreen(new PublishPosterRequest()).Success.ShouldBeFalse();
        }

        [Test]
        public void Publishing_Should_Fail_For_Archived_Campaign()
        {
            SetupArchivedCampaign();

            PublishingService.PublishPosterToScreen(new PublishPosterRequest()).Success.ShouldBeFalse();
        }
    }
}