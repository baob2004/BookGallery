using System.Text;
using System.Text.Json.Serialization;
using BookGallery.API.Middlewares;
using BookGallery.Application.Interfaces;
using BookGallery.Application.Mappings;
using BookGallery.Application.Services;
using BookGallery.Domain.Entities;
using BookGallery.Domain.Interfaces;
using BookGallery.Infrastructure.Data;
using BookGallery.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ========== Controllers ==========
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// ========== AutoMapper ==========
builder.Services.AddAutoMapper(typeof(MappingInitializer));

// ========== OpenAPI ==========
builder.Services.AddOpenApi();

// ========== DbContext ==========
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ========== DI ==========
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenericRepository<AuthorBook>, GenericRepository<AuthorBook>>();
builder.Services.AddScoped<IGenericRepository<BookImage>, GenericRepository<BookImage>>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

// ========== CORS ==========
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ========== Authentication (JWT) ==========
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(opt =>
//     {
//         opt.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             ValidAudience = builder.Configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
//             )
//         };
//     });

// builder.Services.AddAuthorization();

var app = builder.Build();

// ========== OpenAPI in Dev ==========
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// ========== Middleware pipeline ==========
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");
// app.UseAuthentication();
// app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
