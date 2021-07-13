Open terminal and run command:
```
dotnet run
```
 open Firefox to 
 ```
 https://localhost:5001/
```
Retrieving the current status of a specific Battery = api/Batteries/5

Changing the status of a specific Battery = api/Batteries/5

Retrieving the current status of a specific Column = api/Columns/5

Changing the status of a specific Column = api/Columns/5

Retrieving the current status of a specific Elevator = api/Elevators/5

Changing the status of a specific Elevator = api/Elevators/5

Retrieving a list of Elevators that are not in operation at the time of the request = api/Elevators/DisplayAllInoperational

Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention = api/Buildings/ImperfectBuildings

Retrieving a list of Leads created in the last 30 days who have not yet become customers = api/leads/Get30DayLeads
Get by lead id api/leads/GetLead/4
