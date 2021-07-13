using System;
using System.Collections.Generic;

public class Customer
{
    public long id { get; set; }
    public int user_id { get; set; }
    public DateTime customer_creation_date { get; set; }
    public string company_name { get; set; }
    public string headquarters_address { get; set; }
    public string company_contact_full_name { get; set; }
    public string company_contact_phone { get; set; }
    public string company_contact_email { get; set; }
    public string company_description { get; set; }
    public string service_tech_authority_full_name { get; set; }
    public string technical_authority_for_service_phone { get; set; }
    public string technical_manager_email_for_service { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}