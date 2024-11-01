using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dapper;
using Microsoft.Data.Sqlite;
using MinimalApiDapper.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using MinimalApiDapper.Services;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;

namespace MinimalApiDapper;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenTelemetry()
                .WithTracing(tracing =>
                {
                    tracing
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MinimalApiDapper"))
                        .AddAspNetCoreInstrumentation()
                        .AddSqlClientInstrumentation()
                        .AddConsoleExporter();
                });

        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.ParseStateValues = true;
            options.AddConsoleExporter(); 
        });

        builder.Services.AddAuthorization();
        builder.Services.AddCors();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();
        builder.Services.AddSingleton<DbConn>();
        builder.Services.AddScoped<IAlumnoService, AlumnoService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        var db = app.Services.GetRequiredService<DbConn>();
        db.Conn();

        app.MapControllers();

        app.Run();
    }
}
