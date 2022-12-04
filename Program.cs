using ATM.Services;
using ATM.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//added singleton service just for this iteration. 
//we don't inject any other dependencies in CardService
builder.Services.AddSingleton<ICardService, CardService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
