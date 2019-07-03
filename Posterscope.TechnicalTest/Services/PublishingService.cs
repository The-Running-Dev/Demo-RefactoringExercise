using System;
using System.Linq;

using Posterscope.TechnicalTest.Types;
using Posterscope.TechnicalTest.Interfaces;

namespace Posterscope.TechnicalTest.Services
{
    public class PublishingService : IPublishingService
    {
        /// <summary>
        /// Creates a new instance of the service with
        /// dependencies provided by the dependency resolution container
        /// </summary>
        /// <param name="dataStore">An implementation of IDataStore</param>
        /// <param name="uploadServiceFactory">An implementation of IUploadServiceFactory</param>
        public PublishingService(IDataStore dataStore, IUploadServiceFactory uploadServiceFactory)
        {
            _dataStore = dataStore;
            _uploadServiceFactory = uploadServiceFactory;
        }

        public PublishPosterResult PublishPosterToScreen(PublishPosterRequest request)
        {
            var campaign = _dataStore.GetCampaign(request.CampaignId);
            var result = new PublishPosterResult();

            // If the campaign is not active or has no screens
            if (campaign.CampaignStatus != CampaignStatus.Active || campaign.Screens == null)
            {
                return result;
            }

            // Get the screen from the campaign
            var screenToPublish = campaign.Screens.FirstOrDefault(x => x.Id == request.ScreenId);

            // If the requested screen does not belong to the campaign
            if (screenToPublish == null) return result;

            // Get an instance of the IUploadService from the factory
            var uploadService = _uploadServiceFactory.GetService(screenToPublish.PublishType);

            // Upload the screen and get the result
            result.Success = uploadService.Upload(request.PosterBytes, request.ScreenContentPath);

            // If the uploading failed
            if (!result.Success) return result;

            // Update the publish date for the requested screen
            campaign.Screens.ToList().ForEach(x =>
            {
                if (x.Id == request.ScreenId)
                {
                    x.LastPublishDateTime = DateTime.Now;
                }
            });

            // Save the campaign to the data store
            _dataStore.UpdateCampaign(campaign);

            return result;
        }

        private readonly IDataStore _dataStore;

        private readonly IUploadServiceFactory _uploadServiceFactory;
    }
}
