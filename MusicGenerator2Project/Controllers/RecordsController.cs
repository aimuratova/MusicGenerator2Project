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
        private readonly IConfiguration _configuration;
        public RecordsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                        (i + 1) + (musicParams.Page - 1) * musicParams.PageSize,
                        faker,
                        rng,
                        localeData,
                        musicParams.LikesMultiplier,
                        _configuration
                    )
                );

            var recordsViewList = records
                .Select(i => new RecordViewModel()
                {
                    Id = i.Result.Id,
                    Title = i.Result.Title,
                    Artist = i.Result.Artist,
                    Album = i.Result.Album,
                    Genre = i.Result.Genre,
                    Likes = i.Result.Likes,
                    ReleaseDate = i.Result.ReleaseDate,
                    ImageSrc = i.Result.ImageSrc,
                    DurationSeconds = i.Result.DurationSeconds
                })
                .OrderBy(i => i.Id)
                .ToList();

            return PartialView("_Records", recordsViewList);
        }
    }
}
