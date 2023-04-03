
using API.Middleware;
using Core.interfaces;
using Infrastructure.Data;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

///dotnet ef migrations add initialCreate -o Data/Migrations
/// 
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(opt=>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/// Add Exception Middleware   
app.UseMiddleware<ExceptionMiddleware>();   

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

/// Add Configuration Model Class 
using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
var context = Services.GetRequiredService<StoreContext>(); 
var Logger = Services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch(Exception ex)
{
    Logger.LogError(ex, "Error Occured While Migrating Process");
}


app.Run();
