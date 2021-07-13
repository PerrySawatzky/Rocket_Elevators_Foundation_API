using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Foundation_API.Models
{
    public class Column
    {
        public long id { get; set; }
        public int battery_id { get; set; }
        public string column_type { get; set; }
        public int num_floors_served { get; set; }
        public string status { get; set; }
        public string information { get; set; }
        public string notes { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}