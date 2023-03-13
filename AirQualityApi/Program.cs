using AirQualityApi.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient("stationsclient", client =>
{
    client.BaseAddress = new Uri("https://api.gios.gov.pl/pjp-api/rest/");
});
builder.Services.AddScoped<IStationsService, StationsService>();

HttpClient.DefaultProxy.Credentials = CredentialCache.DefaultCredentials;

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
