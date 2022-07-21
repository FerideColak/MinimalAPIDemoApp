using DataAccess.DbAccess;
using MinimalAPIDemo;

var builder = WebApplication.CreateBuilder(args); //web application configure edilir

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();  /* Dependency Injection */
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IUserData, UserData>();

var app = builder.Build(); //app'in baþlamasý için Build edilir, bu sayede configuration bilgisine eriþilir

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //Development modda olup olmadýðý kontrol edilir
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureApi();

app.Run();