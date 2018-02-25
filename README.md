# StoredProcedurePlus.Net
A Stored procedure manager project written in c# to manage stored procedure driven database operations easily.

# Motivation
In everyday development of .Net projects/Application average developer like me used to face same kind of ADO.Net codes and almost every project's DAL layer. WRiting such code every time is dumb, error prone, and absolutely not efficient in terms of productivity.

Could use Entity Framework but its a huge overhead, bulky and slow for the countless features that wont even required operating through just stored procedures.  

Next is going back to ADO.NET. It's the most mature light weight and stable library to communicate with SQL server objects out there. but very generic thus offers speciality or advantage for Stored procedures. So how about mixing some qualities of Entity framework and ADO.NET to just enable some smart coding, dedicated to make life easy working with stored procedures.   

So, I have started working on StoredPrecurePlus.Net. Its uses ADO.NET inside and have some very attractive Entity framework like features minus the bulk of it. 

The concept is every class will be representing the stored procedures in the database and just create an object of it and fire to actually trigger the stored procedures in database. It will manages the rest if configured properly. 


-------------------------------------------------------------------------------------------------------------------------
The README is not fully ready yet and we are working on it. Please read the GIT WIKI as we have put some content into it. 
Also, contact at sknath25@gmail.com for any further help or explanations. 
This light weight high performance library wont be getting its maturity without usages and their feedbacks. 
-------------------------------------------------------------------------------------------------------------------------

 

