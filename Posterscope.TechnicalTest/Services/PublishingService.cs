using System;
using System.Configuration;
using Posterscope.TechnicalTest.Data;
using Posterscope.TechnicalTest.Types;

namespace Posterscope.TechnicalTest.Services
{
    public class PublishingService : IPublishingService
    {
        public PublishPosterResult PublishPosterToScreen(PublishPosterRequest publishPosterRequest)
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"]; 

            Campaign campaign = null;

            if (dataStoreType == "Backup")
            {
                var campaignDataStore = new BackupCampaignDataStore();
                campaign = campaignDataStore.GetCampaign(publishPosterRequest.CampaignId);
            }
            else
            {
                var campaignDataStore = new CampaignDataStore();
                campaign = campaignDataStore.GetCampaign(publishPosterRequest.CampaignId);
            }
            

            var result = new PublishPosterResult();

            switch (campaign.CampaignStatus)
            {
                case CampaignStatus.Active:
                    if (campaign.Screens == null)
                    {
                        result.Success = false;
                    }
                    else
                    {
                        var flag = false; 
                        Screen screenToPublish = null;
                        foreach (var s in campaign.Screens)
                        {
                            if (s.Id == publishPosterRequest.ScreenId)
                            {
                                flag = true;
                                screenToPublish = s;
                            }
                                
                        }
                        if (flag)
                        {
                            var fileUploadService = new FileUploadService();
                            switch (screenToPublish.PublishType)
                            {
                                case PublishTypes.Ftp:
                                    result.Success = fileUploadService.UploadFileToFTPServer(publishPosterRequest.PosterBytes, publishPosterRequest.ScreenContentPath);
                                    break;
                                case PublishTypes.AzureBlobStorage:
                                    result.Success = fileUploadService.UploadFileToAzureBlobStorage(publishPosterRequest.PosterBytes, publishPosterRequest.ScreenContentPath);
                                    break;
                            }
                        }
                        else result.Success = false;
                    }
                    break;

                case CampaignStatus.Archived:
                case CampaignStatus.Inactive:
                    if (campaign.Screens == null)
                    {
                        result.Success = false;
                    }
                    else result.Success = false;
                    break;
            }
            
            if (result.Success)
            {
                foreach (var x in campaign.Screens)
                {
                    if (x.Id == publishPosterRequest.ScreenId)
                    {
                        x.LastPublishDateTime = DateTime.Now;
                    }
                }

                if (dataStoreType == "Backup")
                {
                    var campaignDataStore = new BackupCampaignDataStore();
                    campaignDataStore.UpdateCampaign(campaign);
                }
                else
                {
                    var campaignDataStore = new CampaignDataStore();
                    campaignDataStore.UpdateCampaign(campaign);
                }
            }

            return result;
        }
    }
}
