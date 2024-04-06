using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KeyCloakAuthenticate
{
    public class KeycloakUserManagement : IKeycloakUserManagement
    {
        private readonly string _url = "http://87.248.139.125:8080/";
        private readonly string _realm = "myapp";
        private readonly string _clientId = "dotnet";
        private readonly string _clientSecret = "BtQCva2tqU4VipO52ioFS338AGCZqX0T";

        

        public async Task<UserProfile>  GetUserProfileAsync(string username)
        {
            var client = new HttpClient();
            var response = await GetAsync($"{_url}/{_realm}/accounts/{username}");
            response.EnsureSuccessStatusCode();

            var userJson = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserProfile>(userJson);

            return user;
        }
        private async Task<HttpResponseMessage> GetAsync(string path)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _clientId);

            return await client.GetAsync(path);
        }
    }
}
