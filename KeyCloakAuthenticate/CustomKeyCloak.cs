
using Newtonsoft.Json;

namespace KeyCloakAuthenticate;

    public class CustomKeyCloak
    {

    public async Task<dynamic> GetTokenAsync(string username , string password)
        {
            using (var client = new HttpClient())
            {
                var requestData = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", "dotnet"),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("client_secret", "BtQCva2tqU4VipO52ioFS338AGCZqX0T")
        });

                var response = await client.PostAsync("https://server.easyportal.dev:8443/realms/myapp/protocol/openid-connect/token", requestData);

                if (response.IsSuccessStatusCode)
                {
                //var content =  await response.Content.ReadAsStringAsync();
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AuthTokenResponse>(jsonString);
                string accessToken = "Bearer " +  data.access_token;
                    return accessToken;
                }
            else
            {
                throw new Exception();
            }
            }
    }
}
