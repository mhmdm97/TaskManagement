using Microsoft.EntityFrameworkCore;
using TaskManagementApi.DAL.EF;
using TaskManagementApi.DAL.IRepository;
using TaskManagementApi.DAL.Repository;
using TaskManagementApi.Services.Implementation;
using TaskManagementApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add repos via DI
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<ITaskGroupRepository, TaskGroupRepository>();

//Add services via DI
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<ITaskGroupService, TaskGroupService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TaskManagementContext>(opt => opt.UseNpgsql(Environment.GetEnvironmentVariable("DefaultConnectionString")));


builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
