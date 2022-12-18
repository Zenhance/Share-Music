using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Share_Music.Data;
using Share_Music.Repositories;
using Share_Music.Services.Authentication;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var mappingConfig = TypeAdapterConfig.GlobalSettings;
mappingConfig.Scan(Assembly.GetExecutingAssembly());


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<MusicDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("musicDbDev"),
        providerOptions => providerOptions.EnableRetryOnFailure())
);


builder.Services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));

builder.Services.AddSingleton(mappingConfig);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});

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
