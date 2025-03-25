using HogwartsAPI.Web.Api.Extensions;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddHttpClient()
        .AddGlobalErrorHandling()
        .AddOpenApiDocument()
        .AddServices()
        .AddDbContext(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddControllers();
}
var app = builder.Build();
{
    app.UseGlobalErrorHandling();
    app.UseSwagger();
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }
        await next();
    });
    app.MapControllers();
}
app.Run();
