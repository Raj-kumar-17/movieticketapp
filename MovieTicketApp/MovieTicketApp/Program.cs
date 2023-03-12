using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieTicketApp.Database;
using MovieTicketApp.JWT_Token_Manager;
using MovieTicketApp.Services;
using MovieTicketApp.Interface;
using MovieTicketApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var key = RandomStringGenerator.Random_String_Generator();

IMovieDatabaseSettings settings=new MovieDatabaseSettings();
settings.DatabaseName = builder.Configuration.GetValue<string>("MovieDatabaseSettings:DatabaseName");
settings.ConnectionString= builder.Configuration.GetValue<string>("MovieDatabaseSettings:ConnectionString");
settings.UserCollectionName= builder.Configuration.GetValue<string>("MovieDatabaseSettings:UserCollectionName");
settings.PassKey = builder.Configuration.GetValue<string>("MovieDatabaseSettings:PassKey");
builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(a =>
{
    a.RequireHttpsMetadata = false;
    a.SaveToken = true;
    a.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false       

    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Open",
        build => build.WithOrigins("*").AllowAnyOrigin().AllowAnyHeader());
});

builder.Services.AddControllersWithViews();
builder.Services.Configure<MovieDatabaseSettings>(builder.Configuration.GetSection(nameof(MovieDatabaseSettings)));
builder.Services.AddSingleton<IMovieDatabaseSettings>(sp =>sp.GetRequiredService<IOptions<MovieDatabaseSettings>>().Value);
builder.Services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(settings, key));
builder.Services.AddSingleton<IAdminApiServices,AdminApiServices>();
builder.Services.AddSingleton<IMovieApiServices,MovieApiServices>();
builder.Services.AddSingleton<IAdminModelRepository, AdminModelRepository>();
builder.Services.AddSingleton<IMovieModelRepository, MovieModelRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Open");
app.UseHttpsRedirection();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

