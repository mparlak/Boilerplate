using Boilerplate.Api.Extensions;
using Boilerplate.Application.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfigurations();
// builder.Host.UseSerilog((_, config) =>
// {
//     config.WriteTo.Console()
//         .ReadFrom.Configuration(builder.Configuration);
// });

builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseInfrastructure(builder.Configuration);
app.MapControllers();

app.Run();