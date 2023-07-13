using Serilog;

Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Debug()
                  .WriteTo.Console()
                  .WriteTo.File("logs/productinfo.txt", rollingInterval: RollingInterval.Day)
                  .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

#region Containers

builder.Host.UseSerilog();

builder.Services.AddControllers(options =>
{
  options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

#endregion

//build application

var app = builder.Build();

#region Pipelines

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
