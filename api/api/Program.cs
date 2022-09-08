global using Microsoft.EntityFrameworkCore;
global using api.Data;
using System;
using api.Services.UserService;
using api.Services.JwtService;
using api.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers();

//Adding The DbContext for entity framwork 6
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding this scope will tells asp .net that the class to use for the IUserService is UserService, NB : To change the implementation we just need to change this line.
builder.Services.AddScoped<IUserService, UserService>();

//Adding this scope will tells asp .net that the class to use for the IJwtService is JwtService, NB : To change the implementation we just need to change this line.
builder.Services.AddScoped<IJwtService, JwtService>();

//Adding this scope will tells asp .net that the class to use for the IEmailService is EmailService, NB : To change the implementation we just need to change this line.
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins(
                                    new[] { "http://localhost:3000", 
                                        "https://localhost:3000", 
                                        "http://localhost:3001", 
                                        "https://localhost:3001" }
                                )
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
