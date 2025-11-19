using iChoosr_home_assessment.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ISpaceXService, SpaceXService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints.",
        In = ParameterLocation.Header,
        Name = "X-Api-Key",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    };

    options.AddSecurityDefinition("ApiKey", securityScheme);

    options.AddSecurityRequirement(doc =>
    {
        var securityRequirement = new OpenApiSecuritySchemeReference("ApiKey", doc);
        return new OpenApiSecurityRequirement
        {
            { securityRequirement, new List<string>() }
        };
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    var requiredApiKey = app.Configuration["ApiSettings:ApiKey"];

    if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey) || extractedApiKey != requiredApiKey)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("Unauthorized. A valid 'X-Api-Key' is required.");
        return;
    }

    await next(context);
});

app.MapGet("/payloads", async (ISpaceXService spaceXService) =>
{
    try
    {
        var payloads = await spaceXService.GetAllPayloadsAsync();
        return Results.Ok(payloads);
    }
    catch (HttpRequestException ex)
    {
        return Results.Problem(
            detail: "Unable to fetch payloads from SpaceX API",
            statusCode: StatusCodes.Status502BadGateway,
            title: "Bad Gateway"
        );
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: "An unexpected error occurred",
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Internal Server Error"
        );
    }
});

app.MapGet("/payloads/{id}", async (string id, ISpaceXService spaceXService) =>
{
    try
    {

        if (string.IsNullOrWhiteSpace(id))
        {
            return Results.BadRequest(new { error = "Payload ID is required" });
        }

        var payload = await spaceXService.GetPayloadByIdAsync(id);

        if (payload == null)
        {
            return Results.NotFound(new { error = $"Payload with ID '{id}' not found" });
        }

        return Results.Ok(payload);
    }
    catch (HttpRequestException ex)
    {
        return Results.Problem(
            detail: "Unable to fetch payload from SpaceX API",
            statusCode: StatusCodes.Status502BadGateway,
            title: "Bad Gateway"
        );
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: "An unexpected error occurred",
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Internal Server Error"
        );
    }
});

app.Run();
