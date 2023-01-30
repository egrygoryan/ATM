using ATM.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddServices();

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

app.UseExceptionMiddleware();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

var minimal = app.Services.GetRequiredService<MinimalATM>();
minimal.Register(app);

app.Run();