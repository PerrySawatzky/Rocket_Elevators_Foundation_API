Open terminal and run command:
```
dotnet run
```
 open Postman (preferably) or Firefox (for get requests) to 
 ```
 https://localhost:5001/
```
Changing the status of an intervention request to Completed = api/Interventions/{id}/Completed

Changing the status of an intervention request to InProgress = api/Interventions/{id}/InProgress

Retrieveing list of intervention requests with a status of pending and no start time = api/Interventions/PendingInterventions

Other functionality exists, look through the code comments for each command. Use postman for the put requests, get requests can be done in browser
