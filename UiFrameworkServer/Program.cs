using UiFrameworkServer.Databases;
using UiFrameworkServer.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddMongoContext(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options => options.EnablePersistAuthorization());

app.Run();
