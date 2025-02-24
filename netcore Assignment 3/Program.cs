using netcore_Assignment3.GuidServices;
using netcore_Assignment3.Middlewares;
using netcore_Assignment3.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();

builder.Services.AddSingleton<SingletonGuidService>();
builder.Services.AddScoped<ScopedGuidService>();
builder.Services.AddTransient<TransientGuidService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Console.WriteLine("Hello World");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandling();
app.UseRequestResponseLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
