using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Movie_Client.Models;
using Movie_Core.Dtos;
using Movie_Core.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;

namespace Movie_Client.Controllers
{
    //[Authorize]
    public class GenreController : Controller
    {
        private readonly ApiRequestUri _apiRequestUri;
       
        public GenreController(IOptionsSnapshot<ApiRequestUri> options) 
        {
            _apiRequestUri = options.Value;
           
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreDtoModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_apiRequestUri.BaseUri);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var uri = string.Format(_apiRequestUri.AddGenre, model);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model));

                    HttpResponseMessage response = (HttpResponseMessage)null;

                    response = await httpClient.PostAsJsonAsync(uri, model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");

                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("", await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(model);
        }

      
        
        public async Task<IActionResult> GetAll()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_apiRequestUri.BaseUri);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("token"));

                    var uri = string.Format(_apiRequestUri.GetAllGenre);

                    HttpResponseMessage res = await httpClient.GetAsync(uri);

                    if (res.IsSuccessStatusCode)
                    {
                        var apiTask = res.Content.ReadAsStringAsync();
                        var responseString = apiTask.Result;
                        var model = JsonConvert.DeserializeObject<List<GenreDtoModel>>(responseString);
                        if (res.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            ViewBag.Message = "Unauthorized!";
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }
    }
}
