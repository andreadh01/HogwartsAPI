namespace HogwartsAPI.Web.Api.Extensions
{

    public static class WebApplicationExtensions
    {
        public static WebApplication UseSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi();
            }
            return app;
        }
        public static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseExceptionHandler(_ => { });
            return app;
        }
    }
}