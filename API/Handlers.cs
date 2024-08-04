using Microsoft.AspNetCore.Mvc;

public class Handlers
{
    public static async Task<IResult> GetShortLink(string url, int hours, HttpContext context, [FromServices]IStorage _storage) 
    {
        if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
        {
            var shortGuid = ShortGuid.GetShortGuid(16);
            await _storage.AddAsync(shortGuid, url, DateTime.Today.AddHours(hours));

            return Results.Ok(new { ShortUrl = $"{context.Request.Scheme}://{context.Request.Host}/gt/{shortGuid}"});
        }

        return Results.UnprocessableEntity("Url is not well formed");
    }

    public static async Task<IResult> GoTo(string shortId, [FromServices]IStorage _storage)
    {
        if (await _storage.ContainsKeyAsync(shortId))
            return Results.Redirect(await _storage.GetByKeyAsync(shortId), true, true);

        return Results.NotFound("No such url");

    }
}