# 🌍 Country Info API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green)
![API](https://img.shields.io/badge/API-RESTful-blue)
![Swagger](https://img.shields.io/badge/docs-Swagger-85EA2D?logo=swagger)

A modern .NET 8 Web API that provides comprehensive country information by consuming data from the [REST Countries API](https://restcountries.com/). This API serves as a clean, well-structured wrapper with simplified endpoints for retrieving country data.

## 📋 Table of Contents

- [Features](#-features)
- [Prerequisites](#-prerequisites)
- [Quick Start](#-quick-start)
- [API Documentation](#-api-documentation)
- [Usage Examples](#-usage-examples)
- [Architecture](#-architecture)
- [Development](#-development)
- [Deployment](#-deployment)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features

- **🚀 Modern .NET 8** - Built with the latest .NET framework
- **📖 Swagger/OpenAPI** - Interactive API documentation
- **🏗️ Clean Architecture** - Separation of concerns with interfaces and services
- **💉 Dependency Injection** - Built-in DI container for better testability
- **🌐 RESTful Design** - Following REST best practices
- **⚡ Async/Await** - Fully asynchronous operations
- **🔍 Multiple Endpoints** - Get countries by region, name, or detailed information
- **📊 Structured Data** - Well-defined models for country information
- **🛡️ Error Handling** - Comprehensive error responses

## 📦 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022, Visual Studio Code, or JetBrains Rider
- Internet connection (to fetch data from REST Countries API)

## 🚀 Quick Start

### Clone the Repository

```bash
git clone https://github.com/yourusername/CountryInfoAPI.git
cd CountryInfoAPI
```

### Build and Run

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

The API will be available at:
- `https://localhost:7XXX` (HTTPS)
- `http://localhost:5XXX` (HTTP)

### Access Swagger UI

Navigate to `https://localhost:7XXX/swagger` to explore and test the API endpoints interactively.

## 📚 API Documentation

### Base URL
```
https://localhost:7XXX/api
```

### Endpoints

#### 1️⃣ Get Countries by Region
Returns a dictionary of regions with their associated country names.

```http
GET /api/countries/regions
```

<details>
<summary>Response Example</summary>

```json
{
  "Europe": ["Albania", "Andorra", "Austria", "Belgium", ...],
  "Asia": ["Afghanistan", "Armenia", "Azerbaijan", "Bangladesh", ...],
  "Africa": ["Algeria", "Angola", "Benin", "Botswana", ...],
  "Americas": ["Argentina", "Belize", "Bolivia", "Brazil", ...],
  "Oceania": ["Australia", "Fiji", "Kiribati", "Marshall Islands", ...]
}
```
</details>

#### 2️⃣ Get All Country Names
Returns a sorted list of all country names.

```http
GET /api/countries/names
```

<details>
<summary>Response Example</summary>

```json
[
  "Afghanistan",
  "Albania",
  "Algeria",
  "Andorra",
  "Angola",
  ...
]
```
</details>

#### 3️⃣ Get Country by Name
Returns detailed information about a specific country.

```http
GET /api/countries/{name}
```

**Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| name | string | Yes | The name of the country |

<details>
<summary>Response Example</summary>

```json
{
  "name": {
    "common": "Spain",
    "official": "Kingdom of Spain",
    "nativeName": {
      "spa": {
        "official": "Reino de España",
        "common": "España"
      }
    }
  },
  "capital": ["Madrid"],
  "region": "Europe",
  "subregion": "Southern Europe",
  "population": 47351567,
  "currencies": {
    "EUR": {
      "name": "Euro",
      "symbol": "€"
    }
  },
  "languages": {
    "spa": "Spanish"
  }
}
```
</details>

### Response Codes

| Status Code | Description |
|-------------|-------------|
| 200 OK | Request successful |
| 404 Not Found | Country not found |
| 500 Internal Server Error | Server error occurred |

## 💻 Usage Examples

### Using cURL

```bash
# Get all regions with countries
curl https://localhost:7XXX/api/countries/regions

# Get all country names
curl https://localhost:7XXX/api/countries/names

# Get specific country details
curl https://localhost:7XXX/api/countries/spain
```

### Using PowerShell

```powershell
# Get regions
Invoke-RestMethod -Uri "https://localhost:7XXX/api/countries/regions" -Method Get

# Get country names
Invoke-RestMethod -Uri "https://localhost:7XXX/api/countries/names" -Method Get

# Get country details
Invoke-RestMethod -Uri "https://localhost:7XXX/api/countries/spain" -Method Get
```

### Using JavaScript (Fetch API)

```javascript
// Get country details
fetch('https://localhost:7XXX/api/countries/spain')
  .then(response => response.json())
  .then(data => console.log(data))
  .catch(error => console.error('Error:', error));
```

### Using C# (HttpClient)

```csharp
using var client = new HttpClient();
var response = await client.GetAsync("https://localhost:7XXX/api/countries/spain");
var content = await response.Content.ReadAsStringAsync();
```

## 🏗️ Architecture

### Project Structure

```
CountryInfoAPI/
├── 📁 Controllers/
│   └── CountriesController.cs      # API endpoints
├── 📁 Interfaces/
│   └── ICountryService.cs          # Service contracts
├── 📁 Models/
│   └── Country.cs                  # Data models
├── 📁 Services/
│   └── CountryService.cs           # Business logic
├── 📁 Properties/
│   └── launchSettings.json         # Launch configurations
├── Program.cs                       # Application entry point
├── appsettings.json                 # Configuration
├── appsettings.Development.json     # Development config
└── CountryInfoAPI.csproj           # Project file
```

### Key Components

| Component | Description |
|-----------|-------------|
| **CountriesController** | Handles HTTP requests and maps them to service methods |
| **ICountryService** | Defines the contract for country data operations |
| **CountryService** | Implements the business logic and REST Countries API integration |
| **Country Model** | Data transfer objects representing country information |

### Design Patterns

- **Dependency Injection** - Services are injected via constructor
- **Repository Pattern** - Abstraction over data access
- **Async/Await Pattern** - Non-blocking I/O operations

## 🛠️ Development

### Configuration

The API uses the REST Countries API v3.1. The base URL is configured in `CountryService.cs`:

```csharp
private const string BaseUrl = "https://restcountries.com/v3.1/";
```

### Adding New Features

1. **Update the Interface** - Add method signature to `ICountryService`
2. **Implement the Method** - Add implementation in `CountryService`
3. **Add Controller Endpoint** - Create endpoint in `CountriesController`
4. **Update Documentation** - Add endpoint details to this README
5. **Write Tests** - Add unit and integration tests

### Running Tests

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Code Quality

```bash
# Format code
dotnet format

# Run code analysis
dotnet build -warnaserror
```

## 🐳 Deployment

### Docker

Create a `Dockerfile` in the project root:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CountryInfoAPI.csproj", "."]
RUN dotnet restore "CountryInfoAPI.csproj"
COPY . .
RUN dotnet build "CountryInfoAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountryInfoAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountryInfoAPI.dll"]
```

Build and run the Docker container:

```bash
# Build the image
docker build -t country-info-api .

# Run the container
docker run -d -p 8080:80 -p 8443:443 --name country-api country-info-api
```

### Azure App Service

```bash
# Create a resource group
az group create --name CountryApiRG --location eastus

# Create an App Service plan
az appservice plan create --name CountryApiPlan --resource-group CountryApiRG --sku B1

# Create a Web App
az webapp create --resource-group CountryApiRG --plan CountryApiPlan --name country-info-api

# Deploy the application
az webapp deployment source config-zip --resource-group CountryApiRG --name country-info-api --src publish.zip
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Guidelines

- Follow C# coding conventions
- Write unit tests for new features
- Update documentation as needed
- Ensure all tests pass before submitting PR

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **[REST Countries API](https://restcountries.com/)** - For providing comprehensive country data
- **[.NET Team](https://dotnet.microsoft.com/)** - For the excellent framework
- **[Swagger/OpenAPI](https://swagger.io/)** - For API documentation tools
- **Contributors** - Thanks to all contributors who help improve this project

## 📧 Contact

GitHub - [@vchatzis4]

Project Link: [https://github.com/vchatzis4/CountryInfoAPI](https://github.com/vchatzis4/CountryInfoAPI)

---

<p align="center">Made with ❤️ using .NET 8</p>