using System;
using System.Threading.Tasks;
using RestSharp;
using System.Text.Json;
using Microsoft.Extensions.Configuration;


namespace CanWeGoToSchool.WebApi.Services
{
    public class RkiDataService : IRkiDataService
    {
        private readonly IConfiguration _configuration;

        public RkiDataService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public bool CanWeGoToSchool(int incidence)
        {
            if(incidence >= 165)
            {
                return false;
            }
            else
            {
                return true; 
            }

        }

        public async Task<Root> GetRkiCoronaData()
        {
            var uri = _configuration.GetSection("Urls:RkiCoronaSevenDayIncidence").Value;

            var client = new RestClient(uri)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            var jsonString = response.Content.ToString();

            Root result =  JsonSerializer.Deserialize<Root>(jsonString);

            return result;

        }
    }
}
