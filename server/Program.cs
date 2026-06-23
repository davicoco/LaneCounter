using server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var apiKey = builder.Configuration["RIOT_API_KEY"];
builder.Services.AddHttpClient<RiotApiClient>(client =>
{
    client.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
});
builder.Services.AddSingleton<PlayerStatsService>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://lanecounter.netlify.app")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.Run();

