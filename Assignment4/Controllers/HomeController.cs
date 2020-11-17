using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Assignment4; 
//using Assignment4.Models;
using static Assignment4.Models.EF_Models;
using Assignment4.Models;
using Assignment4.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Assignment4
{
    public class HomeController : Controller
    {
        //base url for the API call
        string BASE_URL = "https://api.data.gov/ed/collegescorecard/v1/schools.json?";
        //based on the API documentation, we are filtering out these fields.
        string apiFields = "&_fields=id,school.school_url,school.name,2018.student.size,school.city,latest.cost.tuition.out_of_state,school.accreditor_code,&per_page=30";
        //API key to access the API data
        string apiKey = "&api_key=ck1LVrQunLhfsjSgoithxWggF6ZbNSp3SvalD4d4";
        HttpClient httpClient;
       

        private readonly ILogger<HomeController> _logger;
         ApplicationDbContext applicationDbContext;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            _logger = logger;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

// method to convert he feild names and values that we recieve in the HTTP response from the API.
        public string replaceString(string responseString)
        {
            string responseStringModified = responseString;
            responseStringModified = responseStringModified.Replace("school.name", "schoolName");
            responseStringModified = responseStringModified.Replace("school.school_url", "schoolUrl");
            responseStringModified = responseStringModified.Replace("2018.student.size", "studentSize");
            responseStringModified = responseStringModified.Replace("school.city", "schoolCity");
            responseStringModified = responseStringModified.Replace("latest.cost.tuition.out_of_state", "tuitionOutState");
            responseStringModified = responseStringModified.Replace("school.accreditor_code", "accCode");
            responseStringModified = responseStringModified.Replace("id", "uId");
            responseStringModified = responseStringModified.Replace("FloruIda", "Florida");
            responseStringModified = responseStringModified.Replace("floruIda", "florida");

            return responseStringModified;
        }

        [HttpPost]
        public IActionResult GetUniversitiesByName(string universityName)
        {
            if(universityName!=null)
            universityName = universityName.Replace(" ", "%20");
            string apiExtension = "school.name=" + universityName;
           
            string API_PATH = BASE_URL + apiExtension + apiFields + apiKey;

            string responseString = "";
            UniversityData data = null;
      
            httpClient.BaseAddress = new Uri(API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(API_PATH).GetAwaiter().GetResult();

            // Read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                
                responseString = replaceString(responseString);
            }

            // Parse the Json strings as C# objects
            if (!responseString.Equals(""))
            {
                data = System.Text.Json.JsonSerializer.Deserialize<UniversityData>(responseString);

                //data = JsonConvert.DeserializeObject<UniversityData>(responseString);
            }
            ViewBag.search = "name";

            foreach (Results item in data.results)
            {
                applicationDbContext.Results.Add(item);
            }
          
            return View("Explore", data);
        }

        [HttpPost]
        public IActionResult GetUniversitiesByState(string state)
        {
            string apiExtension = "school.state=" + state;
          //  apiFields = "&_fields=id,school.school_url,school.name,2018.student.size,school.city,latest.cost.tuition.out_of_state,school.accreditor_code,&per_page=30";
            string API_PATH = BASE_URL + apiExtension + apiFields + apiKey;

            string responseString = "";
            UniversityData data = null;

            
            httpClient.BaseAddress = new Uri(API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(API_PATH).GetAwaiter().GetResult();

            // Read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                responseString = replaceString(responseString);

            }

            // Parse the Json strings as C# objects
            if (!responseString.Equals(""))
            {
                data = System.Text.Json.JsonSerializer.Deserialize<UniversityData>(responseString);                            
            }
           
            return View("Explore", data);
        }

        [HttpPost]
        public IActionResult GetUniversitiesByStateChart(string state)
        {
            string apiExtension = "school.state=" + state + "&latest.cost.tuition.out_of_state__range=100..";
            apiFields = "&_fields=id,school.school_url,school.name,2018.student.size,school.city,latest.cost.tuition.out_of_state,school.accreditor_code,&per_page=5";

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
                responseString = replaceString(responseString);
            }

            // Parse the Json strings as C# objects
            if (!responseString.Equals(""))
            {
                data = System.Text.Json.JsonSerializer.Deserialize<UniversityData>(responseString);               
            }
            ViewBag.State = state;
            return View("Charts", data);
        }

       

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Charts()
        {
            ViewBag.State = "AL";
            return GetUniversitiesByStateChart("AL");
        }

        
        public IActionResult Details(int id)
        {
            string apiExtension = "id=" + id;
            apiFields = "&_fields=id,school.school_url,school.name,2018.student.size,school.city,latest.cost.tuition.out_of_state,school.accreditor_code,&per_page=5";

            string API_PATH = BASE_URL + apiExtension + apiFields + apiKey;
            string responseString = "";
            UniversityData data = null;

            
            httpClient.BaseAddress = new Uri(API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(API_PATH).GetAwaiter().GetResult();

            
            if (response.IsSuccessStatusCode)
            {
                responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                responseString = replaceString(responseString);
            }

           
            if (!responseString.Equals(""))
            {
                data = System.Text.Json.JsonSerializer.Deserialize<UniversityData>(responseString);              
            }
            
            return View(data);
        }


        [HttpPost]
        public IActionResult SignUp(SignUp signUp)
        {
            if(ModelState.IsValid)
            {
                SignUp k = applicationDbContext.SignUp.Find(signUp.email);
                if (k == null)
                {
                    applicationDbContext.SignUp.Add(signUp);
                    applicationDbContext.SaveChanges();
                }
                else
                {
                    ViewBag.errorCode = 1;
                    ViewBag.errorMessage = "You have already registered, Thank you!";
                    return View();
                }
            }
            return View();
        }

     
        public IActionResult AboutUs()
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

        public IActionResult RegisteredUsers()
        {
            IEnumerable<SignUp> allUsers = applicationDbContext.SignUp;
            return View(allUsers);
        }

        public IActionResult DeleteUser(string email)
        {
            SignUp user = applicationDbContext.SignUp.Find(email);
            if (user != null)
            {
                applicationDbContext.SignUp.Remove(user);
                applicationDbContext.SaveChanges();
            }
            IEnumerable<SignUp> allUsers = applicationDbContext.SignUp;
            return View("RegisteredUsers",allUsers);
        }

        public IActionResult UpdateUser(string email)
        {
            ViewBag.email = email;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateUser(SignUp userChanges)
        {
            var user = applicationDbContext.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            applicationDbContext.SaveChanges();
            IEnumerable<SignUp> allUsers = applicationDbContext.SignUp;
            return View("RegisteredUsers", allUsers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
    
}
