using System.Text.Json.Serialization;
using EnsitechLibrary;
using EnsitechLibrary.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using server.Models;
using server.Services;

// using server.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(); 
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
 
builder.Services.AddSwaggerGen();
 
var conStr = builder.Configuration.GetConnectionString("TestDb");
if(conStr != null){ 
    builder.Services.AddDbContext<BookDbContext>(o =>
    {
        o.UseInMemoryDatabase(conStr); 
    });
}

// Adding Authentication  
 
builder.Services
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
    .AddBearerToken()
    .AddJwtBearer(options => 
        {
            options.TokenValidationParameters = new TokenValidationParameters 
            {  
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        }
); 

builder.Services.AddScoped<IBookRepository, BookRepository>(); 

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
app.UseAuthentication();
app.UseAuthorization();
 
 
app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});
 
app.Run();