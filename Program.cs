using StudentsApi.Data;
using Microsoft.Extensions.Options;
using StudentsApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mongo DB
builder.Services.Configure<StudentDbSettings>(builder.Configuration.GetSection(nameof(StudentDbSettings)));
builder.Services.AddSingleton(s=>
{
    var settings = s.GetRequiredService<IOptions<StudentDbSettings>>().Value;
    return new StudentDbContext(settings.ConnectionString, settings.DatabaseName);
});

// Repository
builder.Services.AddScoped<StudentRepository>();

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
