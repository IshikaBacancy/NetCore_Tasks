using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using netcore__Assignment_4.Interfaces;
using netcore__Assignment_4.Middlewares;
using netcore__Assignment_4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register TimeService as a Singleton
builder.Services.AddSingleton<ITimeService, TimeService>();
builder.Services.AddSingleton<IStudentService, StudentService>();

var app = builder.Build();

app.UseRequestResponseDateLogging();
app.UseUserRestrictionmiddleware();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Uncomment only if authentication/authorization is set up
// app.UseAuthorization();

app.MapControllers();
app.Run();
