using MissingHistoricalRecords.WebApi;
using MissingHistoricalRecords.WebApi.Middleware;
using MissingHistoricalRecords.WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddScoped<EfCoreRepository>();
builder.Services.AddScoped<DapperRepository>();
builder.Services.AddScoped<AdoNetRepository>();
builder.Services.AddTransient<GlobalExceptionMiddleware>();
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

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
