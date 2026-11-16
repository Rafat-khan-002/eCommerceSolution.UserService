using AutoMapper;
using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Core.Validator;
using eCommerce.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);


// Add Infrastracture services
builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add controllers to the service collection
builder.Services.AddControllers().AddJsonOptions
(options => {
    options.JsonSerializerOptions.Converters.Add
    (new JsonStringEnumConverter());
});

//add auto mapper
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(ApplicationUserMappingProfile)
);

//FluentValidation
builder.Services.AddFluentValidationAutoValidation();

//Add API Explorer service 
builder.Services.AddEndpointsApiExplorer();
//Add swagger generation services to create
//swagger specification
builder.Services.AddSwaggerGen();
// Configure CORS to allow the Angular application running at
// http://localhost:4200 to access the ASP.NET Core Web API.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    });
});


//build the web application
var app = builder.Build();
app.UseExceptionHandlingMiddlewares();
//Routing
app.UseRouting();
//add end points that can serve the Swagger.json
app.UseSwagger();
//add Swagger UI (Interactive page to explore and test api endpoints)
app.UseSwaggerUI();
app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();
//Controller Route
app.MapControllers();

app.Run();
