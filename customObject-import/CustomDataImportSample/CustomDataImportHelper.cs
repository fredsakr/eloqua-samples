using System.Collections.Generic;
using System.Linq;
using CustomDataImportSample.Models;
using CustomDataImportSample.Models.CustomObjects;
using RestSharp;

namespace CustomDataImportSample
{
    public class CustomDataImportHelper
    {
        #region constructors

        public CustomDataImportHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                          };
        }

        #endregion

        #region properties

        private readonly RestClient _client;

        #endregion

        #region import methods

        /// <summary>
        /// Create the structure (or template) for the Import
        /// </summary>
        public Import CreateImportStructure(int id, Dictionary<string, string> fields)
        {
            Import import = new Import()
                                {
                                    name = "sample import",
                                    fields = fields,
                                    updateRule = RuleType.ifNewIsNotNull,
                                    identifierFieldName = "C_EmailAddress",
                                    secondsToRetainData = 3600,
                                    isSyncTriggeredOnImport = true,
                                    //syncActions = new List<SyncAction>()
                                    //                  {
                                    //                      new SyncAction()
                                    //                          {
                                    //                              action = SyncActionType.add,
                                    //                              destinationUri = ""
                                    //                          }
                                    //                  }
                                };

            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          Resource = string.Format("/customObjects/{0}/import", id)
                                      };

            IRestResponse<Import> response = _client.Execute<Import>(request);
            return response.Data;
        }

        /// <summary>
        /// Responsible for pushing the data to the API
        /// </summary>
        /// <param name="importUri"></param>
        /// <param name="data"></param>
        /// <returns>The URI of the Sync</returns>
        public Sync ImportData(string importUri, List<Dictionary<string, string>> data)
        {
            RestRequest request = new RestRequest(Method.POST)
            {
                Resource = importUri + "/data",
                RequestFormat = DataFormat.Json
            };
            request.AddBody(data);

            IRestResponse<Sync> response = _client.Execute<Sync>(request);
            Sync sync = response.Data;

            return sync;
        }

        /// <summary>
        /// Check the <see cref="Sync"/> result Status
        /// note : use polling until the sync is complete
        /// </summary>
        /// <param name="syncUri"></param>
        /// <returns></returns>
        public RestObjectList<SyncResult> CheckSyncResult(string syncUri)
        {
            RestRequest request = new RestRequest(Method.GET)
            {
                Resource = syncUri + "/results",
                RequestFormat = DataFormat.Json
            };

            IRestResponse<RestObjectList<SyncResult>> response = _client.Execute<RestObjectList<SyncResult>>(request);
            return response.Data;
        }

        #endregion

        #region field helpers


        public Dictionary<string, string> BuildFieldCollection(int customObjectId, Dictionary<string, string> customObjectFields, Dictionary<string, string> contactFields)
        {
            var cardFieldsForImport = customObjectFields.Keys.ToDictionary(key => key,
                                                                           key =>
                                                                           string.Format(
                                                                               "{{{{CustomObject[{0}].Field({1})}}}}",
                                                                               customObjectId, customObjectFields[key]));

            if (contactFields.Count > 0)
            {
                var contactFieldsForImport = contactFields.Keys.ToDictionary(key => key,
                                                                             key =>
                                                                             string.Format(
                                                                                 "{{{{CustomObject[{0}].Contact.Field({1})}}}}",
                                                                                 customObjectId, contactFields[key]));

                foreach (KeyValuePair<string, string> kvp in contactFieldsForImport)
                {
                    cardFieldsForImport.Add(kvp.Key, kvp.Value);
                }
            }
            return cardFieldsForImport;
        }

        #endregion

    }
}
