using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WhatsappApi.Services.WhatsappCloud.SendMessage
{
    public class WhatsappCloudSendMessage : IWhatsappCloudSendMessage
    {
        public async Task<bool> Execute(object model)
        {
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string endponit = "https://graph.facebook.com";
                string phoneNumberId = "274993372355045";
                string accessToken = "EAAUK0WnwoEEBOw73etOFcstQYI1iEkgT2af3FlMDyMogZCzCcGyWBFqZAbjgnzlrsIsVbqoVobhYZBtBIb1fZC5QiDMqFKtFmSL3dfaVQOtKaozo51poYfUL1sUIpmQSCFdXC9fXYmqO3TmfHn6pU0rZAIEzqZAsEz0NO7ZCKQMV0wCjWEzMgRFIYZCRn28nwduxoIJhmvhK2EZB4AwAzWuA7jDMhkm0NNt5olhzXXcqXCZA9D9Kty5kT6";
                string uri = $"{endponit}/v18.0/{phoneNumberId}/messages";

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.PostAsync(uri, content);    

                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
