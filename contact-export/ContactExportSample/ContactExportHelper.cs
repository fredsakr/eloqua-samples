using System;
using System.Collections.Generic;
using ContactExportSample.Models;
using RestSharp;

namespace ContactExportSample
{
    public class ContactExportHelper
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
        public ContactExportHelper(string instance, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {Authenticator = new HttpBasicAuthenticator(instance + "\\" + user, password)};
        }

        #endregion

        #region Step 2 : Create Export

        /// <summary>
        /// Create an Export
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="destinationUri"></param>
        /// <param name="filter"> </param>
        /// <returns>The URI for the export</returns>
        public string CreateExport(Dictionary<string, string> fields, string destinationUri, ExportFilter filter)
        {
            Export export = new Export
                                {
                                    name = "sample export",
                                    fields = fields,
                                    filter = filter,
                                    secondsToAutoDelete = 3600,
                                    secondsToRetainData = 3600,
                                    syncActions = new List<SyncAction>
                                                      {
                                                          new SyncAction
                                                              {
                                                                  action = SyncActionType.add,
                                                                  destinationUri = destinationUri
                                                              }
                                                      }
                                };

            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          Resource = "/contact/export",
                                          RequestFormat = DataFormat.Json,
                                          RootElement = "export"
                                      };
            request.AddBody(export);

            IRestResponse<Export> response = _client.Execute<Export>(request);
            Export returnedExport = response.Data;

            return returnedExport.uri;
        }

        #endregion

        #region Step 3 : Check the Result

        /// <summary>
        /// Create a new instance of the Sync or Data Export
        /// Check the Sync.Status (poll until "active")
        /// </summary>
        /// <param name="exportUri">A reference to the export - created in Step 2</param>        
        /// <returns>The URI of the Sync</returns>
        public Sync CreateSync(string exportUri)
        {
            Sync sync = new Sync
                            {
                                status = SyncStatusType.pending,
                                syncedInstanceUri = exportUri
                            };

            RestRequest request = new RestRequest(Method.POST)
            {
                Resource = "/sync",
                RequestFormat = DataFormat.Json
            };
            request.AddBody(sync);

            IRestResponse<Sync> response = _client.Execute<Sync>(request);
            Sync returnedSync = response.Data;

            return returnedSync;
        }

        #endregion

        #region Step 4 : Get the Data

        public IRestResponse GetExportData(string exportUri)
        {
            RestRequest request = new RestRequest
                                      {
                                          Resource = exportUri + "/data",
                                          RequestFormat = DataFormat.Json
                                      };

            return _client.Execute<Export>(request);
        }



        #endregion

        #region Step 1 : Data Helpers

        /// <summary>
        /// Invoke a REST Request to retrieve a list of Contact Fields from the API
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Field> SearchContactFields(string searchTerm, int page, int pageSize)
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
        /// Invoke a REST Request to retrieve a list of Filters from the API
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ContactFilter> SearchContactFilters(string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
            {
                Resource = string.Format("/contact/filters?search={0}&page={1}&pageSize={2}", searchTerm, page, pageSize)
            };

            IRestResponse<RequestObjectList<ContactFilter>> response = _client.Get<RequestObjectList<ContactFilter>>(request);
            List<ContactFilter> contactFilters = response.Data.elements;

            foreach (var item in contactFilters)
            {
                Console.WriteLine("Name: " + item.name);
                // do something...
            }

            return contactFilters;
        }

        /// <summary>
        /// Invoke a REST Request to retrieve a list of Filters from the API
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ContactList> SearchContactLists(string searchTerm, int page, int pageSize)
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

        #region helpers 

        public List<Export> SearchExports(string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
            {
                Resource = string.Format("/contact/exports?search={0}&page={1}&pageSize={2}", searchTerm, page, pageSize)
            };

            IRestResponse<RequestObjectList<Export>> response = _client.Get<RequestObjectList<Export>>(request);
            List<Export> exports = response.Data.elements;

            foreach (var item in exports)
            {
                Console.WriteLine("URI: " + item.uri);
                // do something...
            }

            return exports;
        }

        public Sync GetSync(string uri)
        {
            RestRequest request = new RestRequest(Method.GET)
            {
                Resource = string.Format(uri)
            };

            IRestResponse<Sync> response = _client.Get<Sync>(request);
            Sync sync = response.Data;

            return sync;
        }

        #endregion
    }
}