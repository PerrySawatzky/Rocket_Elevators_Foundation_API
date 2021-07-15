using System;
using System.Collections.Generic;

public class Intervention
{
    public long id { get; set; }
    public int author_id { get; set; }
    public int customer_id { get; set; }
    public int building_id { get; set; }
    public int? battery_id { get; set; }
    public int? column_id { get; set; }
    public int? elevator_id { get; set; }
    public int? employee_id { get; set; }
    public DateTime? start_of_intervention { get; set; }
    public DateTime? end_of_intervention { get; set; }
#nullable enable
    public string? result { get; set; }
#nullable enable
    public string? report { get; set; }
#nullable disable
    public string status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}