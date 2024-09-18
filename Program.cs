using Microsoft.EntityFrameworkCore;
using APIPOC.DbContexts;
using APIPOC.Extensions;
using System.Net;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext para usar SQLite
builder.Services.AddDbContext<RangoDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:RangoDbConStr"])
);

builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<RangoDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder().AddPolicy("requiredBrazil", policy => 
    policy
        .RequireRole("admin")
        .RequireClaim("country", "Brazil"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuthRango",
        new()
        {
            Name = "Authorization",
            Description = "Token baseado em Autenticação e Autorização",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = ParameterLocation.Header
        }
    );
    options.AddSecurityRequirement(new()
            {
                {
                    new ()
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "TokenAuthRango" 
                        }
                    }, 
                    new List<string>()
                }
            }
    );
});

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

app.UseAuthentication();
app.UseAuthorization();

if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterIngredientesEndpoints();
app.RegisterRangosEndpoints();

app.Run();
