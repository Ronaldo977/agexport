using CrudAgexport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace CrudAgexport.Controllers
{
    public class ProductController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44306/api");
        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/product/Get").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                productList = JsonConverter.DeserializeObject<List<ProductViewModel>>(data);

            }
            return View();
        }



    }
}
