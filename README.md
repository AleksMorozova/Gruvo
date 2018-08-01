# Gruvo
This is an educational project - a twitter replica. 

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites
1. .Net core sdk
2. Either VSCode with C# extension OR Visual studio 2017
3. Nodejs

### Installing
1. Clone the repo
    git clone https://github.com/AleksMorozova/Gruvo.git
2. Create database 
    Execute script from "script for creation of DB" folder on sql server
3. Add connection string to appsetings.json
    For instance, 
```{ 
    "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=mobilesdb;Trusted_Connection=True;"
       }, 
   }
```
3. Run Server
    F5 from either [VScode] (https://code.visualstudio.com/) or [Visual Studio IDE](https://www.visualstudio.com/)
    
## Built with
* ASP.NET Core 2.0
* Angular
* Angular CLI

    

