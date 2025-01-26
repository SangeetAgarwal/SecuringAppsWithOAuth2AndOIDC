using MakeBitByte.IDP;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Fix for Heroku deployments in a .NET application: explicitly binding Kestrel to the runtime-provided port
    // 1. Kestrel will only listen on the port specified by the PORT environment variable (e.g., 12345).
    // 2. You avoid trying to bind to ports <1024 (such as 80 or 443), which is not allowed for non-root processes in Linux containers.
    // 3. It overrides any default or environment-based configurations that might be binding to privileged ports or conflicting addresses.
    builder.WebHost.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    SeedData.EnsureSeedData(app);
    app.Run();
}
// https://github.com/dotnet/runtime/issues/60600
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException")
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}