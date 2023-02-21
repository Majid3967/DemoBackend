using AutoMapper;
using DemoBackend.Data;
using DemoBackend.Helper;
using DemoBackend.Interfaces;
using DemoBackend.Repositories;
using DemoBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserInterface,UserRepository>();
builder.Services.AddScoped<IItemInterface, ItemRepository>();
builder.Services.AddScoped<ICategoryInterface, CategoryRepository>();
builder.Services.AddScoped<ICartInterface, CartRepository>();
builder.Services.AddScoped<IAuthServiceInterface,AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin 
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
