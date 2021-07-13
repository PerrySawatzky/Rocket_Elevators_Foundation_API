using System;
using System.Collections.Generic;

public class Battery
{
    public long id { get; set; }
    public string building_id { get; set; }
    public string battery_type { get; set; }
    public string status { get; set; }
    public string employee_id { get; set; }
    public DateTime commissioned_date { get; set; }
    public DateTime last_inspection_date { get; set; }
    public string certificate_of_operations { get; set; }
    public string information { get; set; }
    public string notes { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}