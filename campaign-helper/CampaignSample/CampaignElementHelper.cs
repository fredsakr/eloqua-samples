using System.Collections.Generic;
using CampaignSample.Models;

namespace CampaignSample
{
    public class CampaignElementHelper
    {
        /// <summary>
        /// Construct a new Campaign Element of type Email
        /// </summary>
        /// <param name="emailId">Unique identifier of the email</param>
        /// <param name="referenceId">An ID used to map/reference this object within the Request.</param>
        /// <returns></returns>
        public CampaignEmail GetCampaignEmail(int emailId, int referenceId)
        {
            return new CampaignEmail
                       {
                           id = referenceId,
                           type = "CampaignEmail",
                           emailId = emailId,
                           sendTimePeriod = "sendAllEmailAtOnce",
                           position = new Position
                                          {
                                              x = 100,
                                              y = 100
                                          }
                       };
        }

        /// <summary>
        /// Construct a new Campaign Element of type Segment
        /// </summary>
        /// <param name="segmentId"></param>
        /// <param name="referenceId">An ID used to map/reference this object within the Request</param>
        /// <param name="outputElementId">The ID of the Campaign Element that this Outputs to</param>
        /// <returns></returns>
        public CampaignSegment GetCampaignSegment(int segmentId, int referenceId, int outputElementId)
        {
            return new CampaignSegment
                       {
                           id = referenceId,
                           type = "CampaignSegment",
                           segmentId = segmentId,
                           isRecurring = false,
                           position = new Position
                                          {
                                              x = 10,
                                              y = 10
                                          },
                           outputTerminals = new List<CampaignOutputTerminal>
                                                 {
                                                     new CampaignOutputTerminal
                                                         {
                                                             connectedId = outputElementId,
                                                             connectedType = "CampaignEmail",
                                                             terminalType = "out"
                                                         }
                                                 }

                       };
        }
    }
}
