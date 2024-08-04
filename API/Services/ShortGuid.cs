using System.Text;

public class ShortGuid
{
    public const string salt = "abcdefghijklmnopqrstuvwxyz1234567890";

    public static string GetShortGuid(int length)
    {
        var random = new Random();
        var chars = DateTime.Now.Ticks + salt + DateTime.Now.Ticks;
        return new string(
            Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
    }
}