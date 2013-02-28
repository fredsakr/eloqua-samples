using ContactSubscriptionSample.Models;
using RestSharp;

namespace ContactSubscriptionSample
{
    public class ContactSubscriptionHelper
    {
        private readonly RestClient _client;

        public ContactSubscriptionHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                {
                    Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                };
        }

        public SearchResponse<Subscription> ListContactSubscriptions(int contactId)
        {
            var request = new RestRequest(Method.GET)
                {
                    Resource = string.Format("/data/contact/{0}/email/groups/subscription", contactId)
                };

            var response = _client.Execute<SearchResponse<Subscription>>(request);

            return response.Data;
        }

        public Subscription UpdateContactSubscription(int contactId, Subscription subscription)
        {
            var request = new RestRequest(Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    Resource = string.Format("/data/contact/{0}/email/group/{1}/subscription", contactId, subscription.emailGroup.id)
                };

            request.AddBody(subscription);

            var response = _client.Execute<Subscription>(request);

            return response.Data;
        }
    }
}
