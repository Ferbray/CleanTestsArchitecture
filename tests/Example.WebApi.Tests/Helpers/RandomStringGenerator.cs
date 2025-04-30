using System.Text;

namespace Example.WebApi.Tests.Helpers;

public static class RandomStringGenerator
{
    private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const string Numbers = "0123456789";
    private const string LettersWithNumbers = Letters + Numbers;

    private static readonly Random Random = new();

    public static string Generate(int length, bool numbers = false)
    {
        if (length <= 0)
        {
            throw new ArgumentException("Длина строки должна быть больше 0", nameof(length));
        }

        var letters = numbers ? LettersWithNumbers : Letters;

        var result = new StringBuilder(length);
        for (var i = 0; i < length; i++)
        {
            result.Append(letters[Random.Next(letters.Length)]);
        }

        return result.ToString();
    }

    public static string GenerateEmail(int length, bool numbers = false)
    {
        return Generate(length, numbers).ToLower() + "@test.com";
    }
}
