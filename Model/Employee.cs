using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Foundation_API.Models
{
    public class Employee
    {
        public long id { get; set; }
        public int user_id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}