using _netcore_Assignment2.Middlewares;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

 //Safely get configuration values
var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>() ?? new string[0];

var mySettings = builder.Configuration.GetSection("MySettings").Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();

string id1 = mySettings.ContainsKey("ID1") ? mySettings["ID1"] : "DefaultID1";
string id2 = mySettings.ContainsKey("ID2") ? mySettings["ID2"] : "DefaultID2";
string id3 = mySettings.ContainsKey("ID3") ? mySettings["ID3"] : "DefaultID3";

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure Middleware Order


app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Custom-ID1", id1);
    context.Response.Headers.Add("X-Custom-ID2", id2);
    context.Response.Headers.Add("X-Custom-ID3", id3);

    await context.Response.WriteAsync($"Executing Middleware...\n  ID1: {id1}\n, ID2: {id2}\n, ID3: {id3}\n");
    await next();
});

app.UseLogRequest(); // Logging Middleware
app.UseExceptionHandling(); // Exception Handling Middleware
app.UseHttpsRedirection();
// Authorization & Controllers mapping (must be before `app.Run()`)
app.UseAuthorization();
app.MapControllers();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Middleware - Token Key: ");
    await context.Response.WriteAsync("use method middleware\n");
    await next();
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("run method middleware\n");
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
