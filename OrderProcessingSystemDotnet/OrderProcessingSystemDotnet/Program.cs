﻿using Microsoft.EntityFrameworkCore;
using OrderProcessingSystemDotnet.Models;
using OrderProcessingSystemDotnet.Controllers;
using OrderProcessingSystemDotnet.Interfaces;
using OrderProcessingSystemDotnet.Repositories;

// Initialize the WebApplication builder.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the in-memory database for TaskManagerDbContext.
// The following line adds the TaskManagerDbContext to the dependency injection container
// and configures it to use an in-memory database named "OpsDb".
builder.Services.AddDbContext<TaskManagerDbContext>(options => options.UseInMemoryDatabase("OpsDb"));

// Add the transient dependency for IUserTaskService, using UserTaskRepository.
// This means a new instance of UserTaskRepository will be created for each request.
builder.Services.AddTransient<IUserTaskService, UserTaskRepository>();

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.

// If the application is in development mode, enable Swagger and SwaggerUI.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable authorization.
app.UseAuthorization();

// Map controllers.
app.MapControllers();

// Run the application.
app.Run();
