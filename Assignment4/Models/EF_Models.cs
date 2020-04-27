using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Assignment4.DataAccess;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity;

namespace Assignment4.Models
{
    public class EF_Models
    {

        public class SignUp
        {
            [Key]

            [EmailAddress]
            [Display(Name = "Email Address")]
            public string email { get; set; }
            public string name { get; set; }

            public string cNumber { get; set; }

            public string university { get; set; }

            public string major { get; set; }

        }


        public class UniversityData
        {            
            public Metadata metadata { get; set; }
            [JsonProperty]
            public Results[] results { get; set; }
        }

        public class Metadata
        {
            
            public int total { get; set; }
            [Key]
            public int page { get; set; }
            public int per_page { get; set; }
        }

        public class Results
        {
            public int? tuitionOutState { get; set; }
            public string schoolCity { get; set; }
            public string schoolUrl { get; set; }
            public string accCode { get; set; }
            public string schoolName { get; set; }
            public int? studentSize { get; set; }

           
            [Key]
            public int uId { get; set; }
            public int likesCount { get; set; }

        }
    }

  
}
