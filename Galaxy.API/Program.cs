using Galaxy.Application.Extention;
using Galaxy.Infrastructure.Extention;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pharamcy.Presentation.Middleware;
using Galaxy.Presistance.Extention;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Galaxy.Presistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Galaxy.Domain.Identity;
using Galaxy.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddInfrastructure()
                .AddApplication()
                .AddPresistance(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Galaxy API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecureKey"]!)),
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    using (var galaxyDb = scope.ServiceProvider.GetRequiredService<GalaxyDbContext>())
    {
        if (galaxyDb.Database.GetPendingMigrations().Any())
        {
            galaxyDb.Database.Migrate();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.OWNER));
                await roleManager.CreateAsync(new IdentityRole(Roles.Manager));
                await roleManager.CreateAsync(new IdentityRole(Roles.SALE));
            }
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    Email = "Galaxy@gmail.com",
                    Name = "SystemAdmin",
                    PhoneNumber = "111111111111",
                    Gander = Gander.Male,
                    EmployeeId = "0",
                    UserName = "SystemAdmin"
                };

                await userManager.CreateAsync(user, "123@Abc");
                await userManager.AddToRoleAsync(user, Roles.OWNER);
            }
        }
    }

}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var supportedCultures = new[] { "de", "ar" };

var localizationOptions = new RequestLocalizationOptions()
    .AddSupportedCultures(supportedCultures)
    .SetDefaultCulture(supportedCultures[0]);

app.UseRequestLocalization(localizationOptions);

app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseRouting();

app.UseMiddleware<GlobalExceptionHanlderMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
