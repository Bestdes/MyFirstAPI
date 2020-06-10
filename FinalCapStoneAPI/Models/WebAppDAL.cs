using FinalCapStoneAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinalCapStoneAPI.Models
{
    public class WebAppDAL
    {

        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44302/");
            return client;
        }



        //install-package Microsoft.AspNet.WebAPI.Client
        public async Task<ListOfCars> GetListOfAllCars()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/Car");
            ListOfCars listOfCars = await response.Content.ReadAsAsync<ListOfCars>();
            return listOfCars;
        }

        public async Task<ListOfCars> GetSpecificMake()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/Car");
            ListOfCars listOfCars = await response.Content.ReadAsAsync<ListOfCars>();

            return listOfCars;
        }
    }
}
