using System;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;

namespace FlurlTesting
{
    public class ExternalApiCaller
    {
        private const string ApiUrl = "http://www.apiurl.com";

        public async Task<bool> MakeTheCall()
        {
            try
            {
                var result = await ApiUrl.GetAsync();
                return result.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}