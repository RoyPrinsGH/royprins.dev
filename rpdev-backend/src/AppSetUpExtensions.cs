namespace RPDev;

using Microsoft.EntityFrameworkCore;

using Services.Generic;

public static class AppSetupExtensions {
    public static IServiceCollection ConfigureGenericRPDevServices(this IServiceCollection services) {
        services.AddProblemDetails();
        services.AddExceptionHandler<RPDevExceptionHandler>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        string? connectionString = Environment.GetEnvironmentVariable("RPDEV_CONNECTION_STRING");
        services.AddDbContext<RPDevDataContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    public static IApplicationBuilder ConfigureRPDevMiddlewarePipeline(this IApplicationBuilder app, IWebHostEnvironment env) {
        app.UseRouting();

        app.UseStatusCodePages();

        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        } else {
            app.UseExceptionHandler();
        }

        return app;
    }

    public static IEndpointRouteBuilder ConfigureGenericRPDevEndpoints(this IEndpointRouteBuilder endpoints) {
        // We have no homepage yet. Redirect to about page.
        endpoints.MapGet("/", () => Results.Redirect("/about")).ExcludeFromDescription();

        // Exception and error testing
        endpoints.MapGet("/generate/exception", () => { 
            throw new Exception("This is an exception!");
        });

        endpoints.MapGet("/generate/code404", () => Results.NotFound());
        endpoints.MapGet("/generate/code400", () => Results.BadRequest());

        return endpoints;
    }
}