using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DbConnection");

// Add services to the container.
builder.Services.AddDbContext<BancoContexto>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// builder.Services.AddDbContext<BancoContexto>(options => options.UseInMemoryDatabase("DBMemoria"));

builder.Services.AddControllers();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
