using Manufacturing.Api;
using Manufacturing.Api.Middlewares;
using Manufacturing.Api.Services;
using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Infrastructure;
using Manufacturing.Application;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddOpenIdDict(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwagger();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

// Register the worker responsible for creating and seeding the SQL database.
// Note: in a real world application, this step should be part of a setup script.
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
