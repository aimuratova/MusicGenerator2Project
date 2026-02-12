using Bogus;
using Microsoft.Extensions.Configuration;
using MusicGenerator.BLL.Models;
using MusicGenerator.BLL.Rng;
using System.Text.Json;

namespace MusicGenerator.BLL
{
    public static class MusicGeneratorService
    {
        public static async Task<MusicItem> Generate(int index, Faker faker, SeededRandom rng, JsonElement locale,
            double likesMultiplier, IConfiguration configuration)
        {
            var music = locale.GetProperty("music");

            var adjectives = music.GetProperty("adjectives").EnumerateArray().Select(x => x.GetString()).ToArray();
            var nouns = music.GetProperty("nouns").EnumerateArray().Select(x => x.GetString()).ToArray();
            var genres = music.GetProperty("genres").EnumerateArray().Select(x => x.GetString()).ToArray();

            var title = $"{faker.PickRandom(adjectives)} {faker.PickRandom(nouns)}";
            var baseLikes = rng.NextInt(10);

            var result = new MusicItem();

            result = new MusicItem()
            {
                Id = index,
                Title = title,
                Artist = faker.Person.FullName,
                Genre = faker.PickRandom(genres),
                Likes = (int)(baseLikes * likesMultiplier),
                Album = faker.Company.CompanyName(),
                DurationSeconds = rng.NextInt(300) + 60, // 1 to 6 minutes
                ReleaseDate = faker.Date.Past(30), // Released within the last 30 years
            };            

            result.ImageSrc = await ImageGeneratorService.GenerateImageAsync(result, configuration);

            return result;
        }
    }
}
