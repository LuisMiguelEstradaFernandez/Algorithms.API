using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Algorithms.API.Domain.Persistence.Contexts;
using Algorithms.API.Domain.Persistence.Repositories;
using Algorithms.API.Domain.Services;
using Algorithms.API.Extensions;
using Algorithms.API.Persistence.Repositories;
using Algorithms.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(
    options => options.AddDefaultPolicy(b =>
        b.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .DisallowCredentials()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection"));
});


// Dependency Injection Configuration
// Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IAlgorithmRepository, AlgorithmRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<IAlgorithmService, AlgorithmService>();

//Endpoinst case conventions configurations
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//startup automapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

// documentation setup
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Algorithm.API", Version = "v1" });
    options.EnableAnnotations();
});

var app = builder.Build();

// Program
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context?.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors();
//
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context => {
        context.Response.Redirect("/swagger/index.html", false);
        return Task.FromResult(0);
    });
    endpoints.MapControllers();
});
app.MapControllers();
app.Run();
