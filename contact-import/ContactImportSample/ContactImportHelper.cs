using System;
using System.Collections.Generic;
using System.IO;
using ContactImportHelper.Models;
using ContactImportSample.RequestObjects;
using RestSharp;

namespace ContactImportSample
{
    public class ContactImportHelper
    {
        #region properties 

        private readonly RestClient _client;

        #endregion 

        #region constructors
        
        /// <summary>
        /// Tbe constructor is responsible for setting up the Client
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="baseUrl"></param>
        public ContactImportHelper(string instance, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {Authenticator = new HttpBasicAuthenticator(instance + "\\" + user, password)};
        }

        #endregion

        #region Step 2 : Configure the Import

        /// <summary>
        /// The <see cref="Import"/> definition contains references to the fields being imported
        /// as well as other metadata used in the import. 
        /// </summary>
        /// <param name="fields"></param>
        /// <returns>The URI of the import</returns>
        public string CreateImport(Dictionary<string, string> fields)
        {
            Import import = new Import()
            {
                name = "sample import",
                fields = fields,
                updateRule = RuleType.always,
                identifierFieldName = "C_EmailAddress",
                secondsToRetainData = 3600,
                isSyncTriggeredOnImport = true,
                syncActions = new List<SyncAction>()
                                                   {
                                                       new SyncAction()
                                                           {
                                                               action = SyncActionType.add,
                                                               destinationUri = "/contact/list/1"
                                                           }
                                                   }
            };

            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          Resource = "/contact/import", 
                                          RequestFormat = DataFormat.Json, 
                                          RootElement = "import"
                                      };
            request.AddBody(import);

            IRestResponse<Import> response = _client.Execute<Import>(request);
            Import returnedImport = response.Data;
            return returnedImport.uri;
        }

        #endregion

        #region Step 3 : Data Import

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

        public Sync ImportDataFromFile(string importUri, string fileName, string pathToFile)
        {
            RestRequest request = new RestRequest(Method.POST)
            {
                Resource = importUri + "/data",
                RequestFormat = DataFormat.Json
            };
            request.AddFile(fileName, pathToFile);

            IRestResponse<Sync> response = _client.Execute<Sync>(request);
            Sync sync = response.Data;

            return sync;

        }

        #endregion

        #region Step 4 : Check the SyncResult

        /// <summary>
        /// Check the <see cref="Sync"/> result Status
        /// </summary>
        /// <param name="syncUri"></param>
        /// <returns></returns>
        public RequestObjectList<SyncResult> CheckSyncResult(string syncUri)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          Resource = syncUri + "/results", 
                                          RequestFormat = DataFormat.Json
                                      };

            IRestResponse<RequestObjectList<SyncResult>> response = _client.Execute<RequestObjectList<SyncResult>>(request);
            return response.Data;
        }

        #endregion

        #region Step 1 : Data Helpers

        /// <summary>
        /// Invoke a REST Request to retrieve a list of <see cref="Field"/> from the API
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Field> GetFields(string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
                              {
                                  Resource = string.Format("/contact/fields?search={0}&page={1}&pageSize={2}", searchTerm, page, pageSize)
                              };

            IRestResponse<RequestObjectList<Field>> response = _client.Get<RequestObjectList<Field>>(request);
            List<Field> fields = response.Data.elements;

            foreach (var field in fields)
            {
                Console.WriteLine("Name: " + field.name);
                Console.WriteLine("Internal Name: " + field.internalName);
                // do something...
            }

            return fields;
        }

        /// <summary>
        /// Invoke a REST Request to retrieve a list of <see cref="Field"/> from the API
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ContactList> GetContactLists(string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          Resource = string.Format("/contact/lists?search={0}&page={1}&pageSize={2}", searchTerm, page, pageSize)
                                      };

            IRestResponse<RequestObjectList<ContactList>> response = _client.Get<RequestObjectList<ContactList>>(request);
            List<ContactList> contactLists = response.Data.elements;

            foreach (var item in contactLists)
            {
                Console.WriteLine("Name: " + item.name);
                // do something...
            }

            return contactLists;
        }
        #endregion
    }
}