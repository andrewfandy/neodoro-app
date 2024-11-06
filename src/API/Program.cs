using API.Databases;
using API.Repositories;
using API.Services.Auth;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database and Repositories
builder.Services.AddScoped<ApplicationDbConnection>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ScheduleRepository>();
builder.Services.AddScoped<ActivityRepository>();
builder.Services.AddScoped<TimerSessionRepository>();

// Common and Utility services
builder.Services.AddScoped<UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();