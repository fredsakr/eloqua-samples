using System;
using System.Collections.Generic;
using EmailSample.Models;
using RestSharp;

namespace EmailSample
{
    public class EmailClient
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
        public EmailClient(string instance, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl) { Authenticator = new HttpBasicAuthenticator(instance + "\\" + user, password) };
        }

        #endregion

        #region Email CRUD Operations

        /// <summary>
        /// Invoke an HTTP GET request to retrieve an <see cref="Email"/> by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Email GetEmail(int id)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          Resource = "/assets/email/" + id,
                                          RootElement = "Email",
                                          RequestFormat = DataFormat.Json
                                      };

            IRestResponse<Email> response = _client.Execute<Email>(request);
            return response.Data;
        }

        /// <summary>
        /// Invoke an HTTP GET request to retrieve a list of <see cref="Email"/> by search term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Email> GetEmail(string searchTerm, int page, int pageSize, string orderBy, Direction direction)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          Resource =
                                              string.Format("/assets/emails?search={0}&page={1}&count={2}&orderBy=createdAt%20{3}", searchTerm,
                                                            page, pageSize, direction),
                                          RequestFormat = DataFormat.Json
                                      };

            IRestResponse<RequestObjectList<Email>> response = _client.Execute<RequestObjectList<Email>>(request);

            Console.WriteLine("Total :" + response.Data.total);

            return response.Data.elements;
        }

        /// <summary>
        /// Create a new Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The email as returned from the API</returns>
        public Email CreateEmail(Email email)
        {

            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          Resource = "/assets/email",
                                          RequestFormat = DataFormat.Json
                                      };
            request.AddBody(email);

            IRestResponse<Email> response = _client.Execute<Email>(request);

            return response.Data;
        }

        /// <summary>
        /// Update an existnig Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The email as returned from the API</returns>
        public Email UpdateEmail(Email email)
        {
            RestRequest request = new RestRequest(Method.PUT)
            {
                Resource = "/assets/email/" + email.id,
                RequestFormat = DataFormat.Json
            };
            request.AddBody(email);

            IRestResponse<Email> response = _client.Execute<Email>(request);

            return response.Data;
        }

        /// <summary>
        /// Delete an <see cref="Email"/>
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEmail(int id)
        {
            RestRequest request = new RestRequest(Method.DELETE) { Resource = "/assets/email/" + id, RequestFormat = DataFormat.Json };

            _client.Execute<Email>(request);
        }

        #endregion

        #region send saved email to a contact

        /// <summary>
        /// Sending an Email is also known as an Email Deployment
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public EmailDeployment SendEmailToContact(int contactId, Email email)
        {
            EmailTestDeployment deployment = new EmailTestDeployment
                                        {
                                            contactId = contactId,
                                            email = email,
                                            name = "sample deployment",
                                            type = "EmailTestDeployment"
                                        };

            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          Resource = "/assets/email/deployment",
                                          RequestFormat = DataFormat.Json
                                      };
            request.AddBody(deployment);

            IRestResponse<EmailDeployment> response = _client.Execute<EmailDeployment>(request);

            return response.Data;
        }

        #endregion

        #region send custom email to a list of contacts

        /// <summary>
        /// Sending an Email is also known as an Email Deployment
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public EmailDeployment SendCustomEmailToAddress(List<Contact> contacts, Email email)
        {
            EmailInlineDeployment deployment = new EmailInlineDeployment()
            {
                contacts = contacts,
                email = email,
                name = "sample deployment",
                type = "EmailInlineDeployment"
            };

            RestRequest request = new RestRequest(Method.POST)
            {
                Resource = "/assets/email/deployment",
                RequestFormat = DataFormat.Json
            };
            request.AddBody(deployment);

            IRestResponse<EmailDeployment> response = _client.Execute<EmailDeployment>(request);

            return response.Data;
        }

        #endregion

        #region deployments

        public EmailInlineDeployment GetDeployment(int deploymentId)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource = "/assets/email/deployment/" + deploymentId
                                      };

            IRestResponse<EmailInlineDeployment> response = _client.Execute<EmailInlineDeployment>(request);
            return response.Data;
        }

        public List<EmailDeployment> GetDeployments(string searchTerm, int page, int pageSize)
        {
            var request = new RestRequest(Method.GET)
                              {
                                  Resource =
                                      string.Format("/assets/email/deployments?search={0}&page={1}&count={2}",
                                                    searchTerm, page, pageSize)
                              };
            var response = _client.Execute<RequestObjectList<EmailDeployment>>(request);
            return response.Data.elements;
        } 

        #endregion
    }
}
