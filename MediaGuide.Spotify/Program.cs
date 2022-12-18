using SpotifyAPI.Web;
using MediaGuide.Data;
using MediaGuide.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.IO;

namespace MediaGuide.Spotify
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json");

            var config = configuration.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var clientId = config.GetSection("Spotify:SpotifyClientId")?.Value;
            var clientSecret = config.GetSection("Spotify:SpotifyClientSecret")?.Value;

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                var spotifyConfig = SpotifyClientConfig.CreateDefault();
                var request = new ClientCredentialsRequest(clientId, clientSecret);
                var response = await new OAuthClient(spotifyConfig).RequestToken(request);

                var spotify = new SpotifyClient(spotifyConfig.WithToken(response.AccessToken));
                var armChairShow = context.Shows.Where(x => x.Title.ToUpper() == "ARMCHAIR EXPERT").FirstOrDefault();
                if (armChairShow != null)
                {
                    var episodeList = new List<Episode>();
                    var existingEpisodes = context.Episodes.Where(x => x.ShowId == armChairShow.Id).ToList();

                    await foreach (
                      var spotifyEpisode in spotify.Paginate(await spotify.Shows.GetEpisodes(armChairShow.SpotifyShowId, new ShowEpisodesRequest()
                      {
                          Market = "US"
                      }))
                    )
                    {
                        var existingEpisode = existingEpisodes.Where(x => x.Name == spotifyEpisode.Name).FirstOrDefault();
                        if (existingEpisode != null)
                        {
                            existingEpisode.Name = spotifyEpisode.Name;
                            existingEpisode.Description = spotifyEpisode.Description;
                            existingEpisode.Url = $"https://open.spotify.com/episode/{spotifyEpisode.Id}";
                            existingEpisode.ImageUrl = spotifyEpisode.Images.FirstOrDefault().Url;
                            existingEpisode.EpisodeDate = spotifyEpisode.ReleaseDate;

                        }
                        else
                        {
                            context.Add(new Episode()
                            {
                                ShowId = armChairShow.Id,
                                Name = spotifyEpisode.Name,
                                Description = spotifyEpisode.Description,
                                Url = $"https://open.spotify.com/episode/{spotifyEpisode.Id}",
                                ImageUrl = spotifyEpisode.Images.FirstOrDefault().Url,
                                EpisodeDate = spotifyEpisode.ReleaseDate,
                                CreateDate = DateTime.Now
                            });
                        }

                    }

                    context.SaveChanges();
                }
            }
        }
    }
}