using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Assignment4.DataAccess;
//using System.Data.Entity;

namespace Assignment4.Models
{
    public class EF_Models
    {
        public class Applications
        {
            [Key]

            public string StudentEmail { get; set; }
            //[Column(TypeName = "varchar(200)")]
            public string University { get; set; }

            //[Column(TypeName = "varchar(200)")]
            public string Major { get; set; }

        }

        public class LogIn
        {
            [Key]
            public string email { get; set; }
            public string password { get; set; }

        }
        public class Student
        {
            [Key]
            public string Name { get; set; }
            public string email { get; set; }
            public int contact { get; set; }
            public List<Applications> applications { get; set; }
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
            public string schoolZip { get; set; }
            public string schoolUrl { get; set; }
            public string accCode { get; set; }
            public string schoolName { get; set; }
            public int? studentSize { get; set; }
            [Key]
            public int id { get; set; }

        }
    }
}
