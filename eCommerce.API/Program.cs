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
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

//build the web application
var app = builder.Build();
app.UseExceptionHandlingMiddlewares();
//Routing
app.UseRouting();
//Auth
app.UseAuthorization();
app.UseAuthorization();
//Controller Route
app.MapControllers();

app.Run();
