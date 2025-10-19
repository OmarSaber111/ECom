using Ecom.Api.Hubs;
using Ecom.Api.MiddleWare;
using Ecom.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(op =>
{
    op.AddPolicy("CORSPolicy", builder =>
    {
        builder
            .WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RepositoryDI(builder.Configuration);
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CORSPolicy");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseStaticFiles();
app.UseHttpsRedirection();


app.MapHub<ProductHub>("/hubs/product");
app.MapControllers();

app.Run();
