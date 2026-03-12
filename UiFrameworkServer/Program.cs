using UiFrameworkServer.Contract.Models.ComponentManifests;
using UiFrameworkServer.Databases.Utils;
using UiFrameworkServer.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddMongoContext(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<EnumNameSchemaFilter>();
    options.SchemaFilter<ComponentManifestPolymorphismSchemaFilter>();
    options.UseOneOfForPolymorphism();
    options.SelectSubTypesUsing(baseType =>
        baseType == typeof(ComponentManifestPropsDefinition)
            ?
            [
                typeof(ComponentManifestPropsStylePropertyDefinition),
                typeof(ComponentManifestPropsBehaviorPropertyDefinition),
            ]
        : baseType == typeof(ComponentManifestProps)
            ?
            [
                typeof(ComponentManifestPropsStyleProperty),
                typeof(ComponentManifestPropsBehaviorProperty),
            ]
        : []
    );
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options => options.EnablePersistAuthorization());

app.Run();
