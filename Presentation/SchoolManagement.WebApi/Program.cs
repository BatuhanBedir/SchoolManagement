using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application;
using SchoolManagement.Application.Enums;
using SchoolManagement.Application.Validators;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.Filters;
using SchoolManagement.Persistence;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



//builder.Services.AddControllers().AddFluentValidation(options =>
//{
//    options.ImplicitlyValidateChildProperties = true;
// options.ImplicitlyValidateRootCollectionElements = true;
//});


// Add services to the container.

//ServiceRegistration of Persistence
builder.Services.AddPersistenceServices();
//ServiceRegistration of Application
builder.Services.AddApplicationServices();
//ServiceRegistration of Infrastructure
builder.Services.AddInfrastructureServices();

//ServiceRegistration of Infrastructure//Buradan hangi servisi inject edersem o file service ile çalýþýyor olacaðým
builder.Services.AddFileService(FileServiceType.Local);




//Tüm originlere, headerlara ve tüm metotlara izin verdik
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,    //tokeni kimler kullanacak
        ValidateIssuer = true,      //tokeni kim daðýtýyor
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,


        //nereden kontrol edeceðim kýsmý.
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
    };
});

builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<StudentCreateValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

//builder.Services.AddControllers()
//    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<StudentCreateValidator>())
//    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);



//claim authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerRequired", policy => policy.RequireClaim("role", "Manager"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//wwwroot kullanýmý için
app.UseStaticFiles();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
