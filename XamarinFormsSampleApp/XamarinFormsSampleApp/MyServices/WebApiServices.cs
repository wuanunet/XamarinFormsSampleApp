using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinFormsSampleApp.MyServices
{
    public class WebApiServices
    {
        private readonly HttpClient _httpClient;

        public WebApiServices()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataAsync(string city, string area)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Referer", "http://www.family.com.tw/marketing/inquiry.aspx");
            _httpClient.DefaultRequestHeaders.Add("Host", "api.map.com.tw");

            var apiUri = string.Format("http://api.map.com.tw/net/familyShop.aspx?searchType=ShopList&type=&city={0}&area={1}&road=&fun=showStoreList&key=6F30E8BF706D653965BDE302661D1241F8BE9EBC", city, area);

            var response = await _httpClient.GetAsync(apiUri);

            var responseAsString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseAsString) == false)
            {
                responseAsString = responseAsString.Remove(0, 14);
                responseAsString = responseAsString.Substring(0, responseAsString.Length - 1);
            }

            Debug.WriteLine(responseAsString);

            return responseAsString;
        }
    }
}