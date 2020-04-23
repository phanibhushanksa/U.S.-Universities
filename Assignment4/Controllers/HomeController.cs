using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Assignment4; 

//using Assignment4.Models;
using static Assignment4.Models.EF_Models;
using Assignment4.Models;

namespace Assignment4
{
    public class HomeController : Controller
    {
        string BASE_URL = "https://api.data.gov/ed/collegescorecard/v1/schools.json?";
        HttpClient httpClient;

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpPost]
        public IActionResult GetUniversitiesByName(string universityName)
        {
            string apiExtension = "school.name=" + universityName;
            string apiFields = "&_fields=id,school.school_url,school.name,2018.student.size,school.zip,latest.cost.tuition.out_of_state,school.accreditor_code,";
            string apiKey = "&api_key=ck1LVrQunLhfsjSgoithxWggF6ZbNSp3SvalD4d4";
            string API_PATH = BASE_URL + apiExtension + apiFields + apiKey;

            string responseString = "";
            UniversityData data = null;

            // Connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(API_PATH).GetAwaiter().GetResult();

            // Read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                responseString = responseString.Replace("school.name", "schoolName");
                responseString = responseString.Replace("school.school_url", "schoolUrl");
                responseString = responseString.Replace("2018.student.size", "studentSize");
                responseString = responseString.Replace("school.zip", "schoolZip");
                responseString = responseString.Replace("latest.cost.tuition.out_of_state", "tuitionOutState");
                responseString = responseString.Replace("school.accreditor_code", "accCode");

            }

            // Parse the Json strings as C# objects
            if (!responseString.Equals(""))
            {
                data = System.Text.Json.JsonSerializer.Deserialize<UniversityData>(responseString);
                //data = JsonConvert.DeserializeObject<UniversityData>(responseString);
            }

            return View("Explore", data);
        }

        [HttpPost]
        public IActionResult GetUniversitiesByState(string state)
        {
            string apiExtension = "school.state=" + state;
            string apiFields = "&_fields=id,school.school_url,school.name,2018.student.size,school.zip,latest.cost.tuition.out_of_state,school.accreditor_code,";
            string apiKey = "&api_key=ck1LVrQunLhfsjSgoithxWggF6ZbNSp3SvalD4d4";
            string API_PATH = BASE_URL + apiExtension + apiFields + apiKey;

            string responseString = "";
            UniversityData data = null;

            // Connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(API_PATH).GetAwaiter().GetResult();

            // Read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                responseString = responseString.Replace("school.name", "schoolName");
                responseString = responseString.Replace("school.school_url", "schoolUrl");
                responseString = responseString.Replace("2018.student.size", "studentSize");
                responseString = responseString.Replace("school.zip", "schoolZip");
                responseString = responseString.Replace("latest.cost.tuition.out_of_state", "tuitionOutState");
                responseString = responseString.Replace("school.accreditor_code", "accCode");

            }

            // Parse the Json strings as C# objects
            if (!responseString.Equals(""))
            {
                data = System.Text.Json.JsonSerializer.Deserialize<UniversityData>(responseString);
                //data = JsonConvert.DeserializeObject<UniversityData>(responseString);
            }

            return View("Explore", data);
        }

        [HttpPost]
        public IActionResult Login(LogIn logIn)
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignUp(LogIn logIn)
        {
            return View("LogIn");
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ApplyForm()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Explore()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
