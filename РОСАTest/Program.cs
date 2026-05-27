using FluentValidation;
using System.Reflection;
using ÐÎÑÀTest.Common.Extensions;
using ÐÎÑÀTest.Common.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.RegisterExecutingAsseblyServices();
builder.Services.UsePostgreSql(builder.Configuration);
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddAuthorization(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandling();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
