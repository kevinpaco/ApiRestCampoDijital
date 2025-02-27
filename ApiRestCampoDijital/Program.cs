using ApiRestCampoDijital.Data;
using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Data.Repository;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements;
using System.Text;
using System.Text.Json.Serialization;
//librerias de JWT
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

//configuracion de JWT
builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("settings").GetSection("secretKey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey); //convierte el key en bytes

builder.Services.AddAuthentication(configureOptions =>
{
     configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey=new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,

    };
    });

// Add services to the container.

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
 policy =>
 {
     policy.WithOrigins("*") // Replace with specific origins if needed
           .WithMethods("*")
           .WithHeaders("*");
 });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EfContext>();

//repositories
builder.Services.AddScoped<IEmployerRepository,EmployerRepository>();
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IHistoryRepository,HistoryRepository>();
builder.Services.AddScoped<IPaymentAdvanceRepository,PaymentAdvanceRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();
builder.Services.AddScoped<IWorkingGroupRepository,WorkingGroupRepository>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<IWorkingTimeForHourRepository,WorkingTimeForHourRepository>();
builder.Services.AddScoped<IWorkingTimeForKilogramRepository,WorkingTimeForKilogramRepository>();
builder.Services.AddScoped<IWorkingTimeInGroupRepository,WorkingTimeInGroupRepository>();
builder.Services.AddScoped<IFarmSupervisorRepository, FarmSupervisorRepository>();

//services 
builder.Services.AddScoped<IEmployerService,EmployerService>();
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
builder.Services.AddScoped<IHistoryService,HistoryService>();
builder.Services.AddScoped<IPaymentAdvanceService,PaymentAdvanceService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IPaymentService,PaymentService>();
builder.Services.AddScoped<IWorkingGroupService,WorkingGroupService>();
builder.Services.AddScoped<IFarmService,FarmService>();
builder.Services.AddScoped<IWorkingTimeForHourService,WorkingTimeForHourService>();
builder.Services.AddScoped<IWorkingTimeForKilogramService, WorkingTimeForKilogramService>();
builder.Services.AddScoped<IWorkingTimeInGroupService, WorkingTimeInGroupService>();
builder.Services.AddScoped<IFarmSupervisorService , FarmSupervisorService>();

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    //para que la base de datos se ejecute al inicio de la app
    EfContext efContext = scope.ServiceProvider.GetRequiredService<EfContext>() ;
    efContext.Database.EnsureCreated();//para montar la base de datos rapidamente
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

//activar la autenticacion
app.UseAuthentication();
app.UseAuthorization(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
