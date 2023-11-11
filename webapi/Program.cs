using Newtonsoft.Json;
using WebApi.Providers;
using Notes.WebApi.Providers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;


IServiceCollection services = builder.Services;

services.AddLogging();
services.AddMSSQLServer(config);
services.AddIdenityModels();

services.AddJWTAuthentication(config);
services.AddHttpContextAccessor();

services.AddUserServices();
services.AddAccountServices();
services.AddNotesModelServices();

services.AddControllers();
    //.AddJsonOptions(options =>
    //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    //).
    //AddNewtonsoftJson(options => 
    //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    //);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseRouting();

await app.InitializeDataAsync(config);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();