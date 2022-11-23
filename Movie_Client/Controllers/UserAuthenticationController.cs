using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Movie_Client.Models;
using Movie_Core.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Movie_Client.Controllers
{
    public class UserAuthenticationController : BaseController
    {
        private readonly ApiRequestUri _apiRequestUri;
        private readonly IHttpContextAccessor _httpContext;
        public UserAuthenticationController(IOptionsSnapshot <ApiRequestUri> options, IHttpContextAccessor httpcontext) : base(httpcontext.HttpContext)
        {
            _apiRequestUri = options.Value;
            _httpContext = httpcontext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDtoModel model) 
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_apiRequestUri.BaseUri);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var uri = string.Format(_apiRequestUri.Register, model);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model));

                    HttpResponseMessage response = (HttpResponseMessage)null;

                    response = await httpClient.PostAsJsonAsync(uri, model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");

                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("",await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDtoModel model)
        {
            try
            {

            
                if (!ModelState.IsValid)
                {
                    return View();
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_apiRequestUri.BaseUri);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var uri = string.Format(_apiRequestUri.Login, model);

                    StringContent content = new StringContent(JsonConvert.SerializeObject(model));

                    HttpResponseMessage response = (HttpResponseMessage)null;

                    response = await httpClient.PostAsJsonAsync(uri, model);

                    if (response.IsSuccessStatusCode)
                    {
                        string stringJWT = response.Content.ReadAsStringAsync().Result;
                        JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

                        HttpContext.Session.SetString("token", jwt.Token);

                        return RedirectToAction("AddGenre","Genre");
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
    }
}
