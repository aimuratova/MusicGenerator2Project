using Bogus;
using Microsoft.AspNetCore.Mvc;
using MusicGenerator.BLL;
using MusicGenerator.BLL.Localization;
using MusicGenerator.BLL.Models;
using MusicGenerator.BLL.Rng;
using MusicGenerator2Project.Models;

namespace MusicGenerator2Project.Controllers
{
    public class RecordsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]MusicParams musicParams)
        {
            var composedSeed = SeedComposer.Compose(musicParams.Seed, musicParams.Page);
            var rng = new SeededRandom(composedSeed);
            var localeData = LocaleLoader.Load(musicParams.Locale);

            Randomizer.Seed = new Random(composedSeed);
            var faker = new Faker();

            var records = Enumerable
                .Range(0, musicParams.PageSize)
                .Select(i =>
                    MusicGeneratorService.Generate(
                        i + musicParams.Page * musicParams.PageSize,
                        faker,
                        rng,
                        localeData,
                        musicParams.LikesMultiplier
                    )
                );

            var recordsViewList = records
                .Select(i => new RecordViewModel()
                {
                    Id = i.Id,
                    Title = i.Title,
                    Artist = i.Artist,
                    Album = i.Album,
                    Genre = i.Genre,
                    Likes = i.Likes,
                    ReleaseDate = i.ReleaseDate
                })
                .ToList();

            return PartialView("_Records", recordsViewList);
        }
    }
}
