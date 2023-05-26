using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sd.Oms.Auth.Api.Middlewares;
using Sd.Oms.Auth.Core.Extensions;
using Sd.Oms.Auth.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(ConfigureOptions);
builder.Services.AddCore(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureOptions(JwtBearerOptions jwtBearerOptions)
{
    jwtBearerOptions.RequireHttpsMetadata = false;
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = "AuthService",
        ValidateAudience = true,
        ValidAudience = "OmsServices",
        ValidateLifetime = true,
        IssuerSigningKeyResolver = (string _, SecurityToken _, string _,
            TokenValidationParameters _) => new List<SecurityKey>()
        {
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("jwt-giga-mega-sigma-super-secret-key"))
        },
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
}