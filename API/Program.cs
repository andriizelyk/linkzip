var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IStorage, MemoryStorage>();

var app = builder.Build();
app.Urls.Add("http://localhost:3000");

app.UseDefaultFiles(new DefaultFilesOptions 
{ 
    DefaultFileNames = new 
     List<string> { "index.html" } 
});

app.UseStaticFiles();

app.MapGet("api/getshortlink", Handlers.GetShortLink);
app.MapGet("gt/{shortId}", Handlers.GoTo);

app.Run();
