# BlackJackAPI

BlackJackAPI is a RESTful ASP.NET Core Web API for managing player accounts and authentication in a Blackjack game environment.
It leverages Entity Framework Core with SQL Server for data persistence and integrates ASP.NET Identity for user management.

Features

	•	Player Management:
	  
	  •	Create, read, update, and delete player accounts.
	  •	Soft-delete support (players marked inactive).
	 
	•	Authentication:
	  
	  •	Login endpoint for player authentication.
	  •	Password and email validation.
	  
	•	AutoMapper Integration:
	  
		•	DTO mapping for clean API responses.
  
	•	Error Handling:
  
		•	Custom middleware for consistent error responses.
  
	•	Swagger/OpenAPI:
  
		•	Interactive API documentation.
  
	•	CORS Support:
  
		•	Configured for local frontend development (e.g., Vite).
  
	•	Unit Testing:
  
		•	xUnit and Moq-based tests for controller logic.
  
  
Technologies

  	•	ASP.NET Core (.NET 8)
  	•	Entity Framework Core 9
  	•	SQL Server
  	•	ASP.NET Identity
  	•	AutoMapper
  	•	Swagger (Swashbuckle)
  	•	xUnit, Moq (testing)

Getting Started

	1. Clane the repsitory
 
 	2. Configure your connection string in appsettings.json:
		"ConnectionStrings": {
      "BlackJackAPIDbContextConnection": "Server=.\\SQLEXPRESS;Database=BlackJackDB;Trusted_Connection=True;..."
    }
		
	3.	Apply migrations and update the database:
 		dotnet ef database update
	 
 	4.	Run the API:
		dotnet run
	
	5.	Access Swagger UI at http://localhost:<port>/swagger for API exploration.

	 ---
		Note:
		This API is designed for integration with a frontend client and can be extended for additional game logic or features.
	---
