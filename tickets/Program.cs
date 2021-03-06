using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using Swashbuckle.AspNetCore.Filters;
using tickets;
using tickets.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetSection("ConnectionStrings:Tickets").Value; //getting connection string
builder.Services.AddSqlServer<TicketsContext>(connectionString);  //add db context
builder.Services.AddScoped<IEventsService, EventsService>(); //eventsService dependency injection
builder.Services.AddScoped<IAuthService, AuthService>(); //authService dependency injection
builder.Services.AddScoped<IUserService, UserService>(); //userService dependency injection
builder.Services.AddScoped<ITicketsService, TicketsService>(); //ticketsService dependency injection
builder.Services.AddScoped<IAdminService, AdminService>(); //adminService dependency injection
builder.Services.AddScoped<IArtistsService,ArtistsService>(); //artistsService dependency injection
builder.Services.AddScoped<IGenresService,GenresService>(); //genresService dependency injection
builder.Services.AddScoped<IPaymentService,PaymentService>(); //paymentService dependency injection
//configuring stripe secret key
StripeConfiguration.ApiKey = builder.Configuration.GetSection("AppSettings:StripeKey").Value;

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();