using Microsoft.EntityFrameworkCore;
using APIPOC.DbContexts;
using APIPOC.Extensions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext para usar SQLite
builder.Services.AddDbContext<RangoDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:RangoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

var app = builder.Build();

if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler();
    // app.UseExceptionHandler(configurationApplicationBuilder => {
    //     configurationApplicationBuilder.Run(
    //         async context => {
    //             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //             context.Response.ContentType = "text/html";
    //             await context.Response.WriteAsync("An unexpected problem happened");
    //         }
    //     );
    // });
}

app.RegisterIngredientesEndpoints();
app.RegisterRangosEndpoints();

app.Run();
