using Am.Api.Extensions;
using Am.Api.Filters;
using Am.Api.Helpers;
using Am.Repository.Ef;
using Am.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;


//builder.Services.AddDbContext<ApplicationDbContext>(options => {
//    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
//        b => b.MigrationsAssembly("Am.Api"));
//});
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(c => c.AddProfile<MappingProfile>(), typeof(Program));
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Agent Model HTTP Api" });
});
builder.Services.AddConfig();
builder.Services.AddAuthenticationConfig(configuration);
//builder.Host.UseSerilog((ctx, lc) => lc
//        //.WriteTo.Console(new JsonFormatter()))
//        .WriteTo.Seq("http://localhost:5341"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

JwtHelper.JWTClaimHelperConfigure(configuration);

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
