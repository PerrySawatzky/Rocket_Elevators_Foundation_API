using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Foundation_API.Models
{
    public class Quote
    {
        public long id { get; set; }
        public string building_type { get; set; }
        public string product_line { get; set; }
        public int? apartments { get; set; }
        public int? floors { get; set; }
        public int? basements { get; set; }
        public int? elevators { get; set; }
        public int? companies { get; set; }
        public int? parking_spots { get; set; }
        public int? max_occupancy_per_floor { get; set; }
        public int? corporations { get; set; }
        public string business_hours { get; set; }
    #nullable enable
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int? elevator_amount { get; set; }
        public int? unit_price { get; set; }
        public int? total_price { get; set; }
        public int? install_fees { get; set; }
        public int? final_price { get; set; }
        public string? company_name { get; set; }
        public string? email { get; set; }
    }
}