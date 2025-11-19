# iChoosr Home Assessment - SpaceX Payloads API

A .NET 9.0 Web API that provides access to SpaceX payload data from the SpaceX public API.

## Features

- Retrieve all SpaceX payloads
- Get specific payload by ID
- API Key authentication
- Swagger/OpenAPI documentation
- Error handling with appropriate HTTP status codes

## Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or later (optional)

## Getting Started

### Running the Application

1. Clone the repository
2. Navigate to the project directory:
   ```bash
   cd iChoosr-home-assessment/iChoosr-home-assessment
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. The API will be available at `http://localhost:5106`

### Using Swagger UI

Once the application is running, navigate to:
```
http://localhost:5106/swagger
```

## API Endpoints

### GET /payloads
Retrieves all SpaceX payloads.

**Headers:**
- `X-Api-Key`: `api-key-123`

**Response:** `200 OK` with array of payload objects

### GET /payloads/{id}
Retrieves a specific payload by ID.

**Headers:**
- `X-Api-Key`: `api-key-123`

**Parameters:**
- `id` (string): The payload ID

**Responses:**
- `200 OK`: Payload found
- `404 Not Found`: Payload not found
- `400 Bad Request`: Invalid ID

## Authentication

All endpoints require an API key to be passed in the request header:

```
X-Api-Key: api-key-123
```

The API key can be configured in `appsettings.json`:

```json
{
  "ApiSettings": {
    "ApiKey": "api-key-123"
  }
}
```

## Example Usage

### Using cURL

```bash
curl -X GET "http://localhost:5106/payloads" -H "X-Api-Key: api-key-123"
```

```bash
curl -X GET "http://localhost:5106/payloads/{payload-id}" -H "X-Api-Key: api-key-123"
```

### Using PowerShell

```powershell
Invoke-RestMethod -Uri "http://localhost:5106/payloads" -Headers @{"X-Api-Key"="api-key-123"}
```

## Project Structure

```
iChoosr-home-assessment/
├── Services/
│   ├── ISpaceXService.cs
│   └── SpaceXService.cs
├── PayloadModels/
│   └── Payload.cs
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

## Technologies Used

- .NET 9.0
- ASP.NET Core Minimal APIs
- Swagger/OpenAPI (Swashbuckle)
- HttpClient for external API calls

## Error Handling

The API provides appropriate error responses:

- `401 Unauthorized`: Missing or invalid API key
- `400 Bad Request`: Invalid request parameters
- `404 Not Found`: Resource not found
- `502 Bad Gateway`: Error communicating with SpaceX API
- `500 Internal Server Error`: Unexpected server errors
