using DetailingApi.Models;
using DetailingApi.Services;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCors();
builder.Services.AddCors(options => {
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy => {
            // policy.WithOrigins("http://localhost:4200")
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
         );  
    }
    );

// Add services to the container.
builder.Services.Configure<DetailingDatabaseSettings>(
    builder.Configuration.GetSection("DetailingDatabase"));

builder.Services.AddSingleton<ContactService>();
builder.Services.AddSingleton<DealsService>();
builder.Services.AddSingleton<FeaturesService>();
builder.Services.AddSingleton<PhotosService>();
builder.Services.AddSingleton<ReviewsService>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
// app.UseCors(options => options.AllowAnyOrigin());yy

// app.UseCors(options => 
//                 options.WithOrigins("http://localhost:4200")
//                 .AllowAnyMethod()
//                 .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
