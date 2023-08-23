using ContactList.Infrastructure; 
using ContactList.Application;
using ContactList.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Create an IConfiguration instance and configure it
var contentRootPath = builder.Environment.ContentRootPath;
Console.WriteLine($"ContentRootPath: {contentRootPath}");

//var appSettingsPath = Path.Combine(contentRootPath, "Backend", "ContactListApi", "appsettings.json");
var appSettingsPath = Path.Combine(contentRootPath, "appsettings.json");
Console.WriteLine($"appSettingsPath: {appSettingsPath}");

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile(appSettingsPath, optional: true, reloadOnChange: true)
    .Build();

IConfiguration cfgsection = configuration.GetSection("Redis");
string redisString = cfgsection["ConnectionString"];
Console.WriteLine($"redisString: {redisString}");
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers();

builder.Services.AddTransient<IRedisService, RedisService>();
//builder.Services.AddScoped<IMongoDbContext>(_ => new MongoDbContext(configuration)); 
       
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonAppService, PersonAppService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var alowedOrigins = configuration.GetSection("AlowedOrigins").Get<string[]>();
 builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost4200", builder =>
        {
            builder.WithOrigins(alowedOrigins)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(3000);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors("AllowLocalhost4200");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
