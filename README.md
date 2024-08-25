## Project Setup and Requirements

This project was developed using **Visual Studio 2022** with a **C# backend**. It uses the **Entity Framework** for database management, and **SQL Server** as the database. Below are the steps and requirements to set up and run the project locally.

### Prerequisites

Before running the project, ensure that you have the following installed:

- **Visual Studio 2022**
- **.NET Framework** (the version required by your project)
- **Entity Framework**
- **SQL Server** (or SQL Server Express for development)

### Setup Instructions

1. **Clone the Repository**:  
   Clone the project repository to your local machine.

2. **Open the Project in Visual Studio 2022**:
Launch Visual Studio 2022 and open the project by navigating to File > Open > Project/Solution and selecting the .sln file.

### Install Dependencies:
Make sure all necessary NuGet packages are installed. You can restore the packages by right-clicking on the solution in the Solution Explorer and selecting "Restore NuGet Packages".

### Update the Database:
Before running the project, update the database with the latest migrations. Open the Package Manager Console in Visual Studio and run the following command:

  ```Update-Database```
**This command will apply all pending migrations and create or update the database schema in your SQL Server.**

### Run the Project Locally:
To run the project on your local machine, press F5 or click the "Start Debugging" button in Visual Studio. The site will be available at http://localhost:5086.

### Additional Notes
- Make sure your SQL Server instance is running and accessible.
- You might need to adjust firewall settings to allow traffic on port 5086.
- By following these steps, you should be able to set up and run the project locally without any issues.
