using CsvHelper;
using CsvHelper.Configuration;
using MediaGuide.Data;
using MediaGuide.Data.Models;
using Microsoft.EntityFrameworkCore;

using System.Globalization;
using System.IO;
using MediaGuide.DataLoader.Data;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
     .AddJsonFile($"appsettings.json");

var config = configuration.Build();
var connectionString = config.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseMySql(connectionString,
ServerVersion.AutoDetect(connectionString));

using (var context = new ApplicationDbContext(optionsBuilder.Options))
{
    var armChairShow = context.Shows.Include(x => x.Episodes).ThenInclude(x => x.Medias).Where(x => x.Title.ToUpper() == "ARMCHAIR EXPERT").FirstOrDefault();
    if (armChairShow != null && armChairShow.Episodes != null)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
        };

        var fileLocation = $"{Directory.GetCurrentDirectory()}/Data/armchairdata.csv";

        using (var reader = new StreamReader(fileLocation))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            var records = csv.GetRecords<ArmchairDataFile>();
            foreach (var record in records)
            {
                if (!string.IsNullOrEmpty(record.Episode))
                {
                    var episode = armChairShow
                                    .Episodes
                                    .Where(x =>
                                        x.Name.ToLower()
                                        .Contains(record.Episode.ToLower()))
                                        .FirstOrDefault();

                    if (episode != null)
                    {
                        var movies = record.Movies;
                        var documentaries = record.Documentaries;
                        var shows = record.Shows;
                        var books = record.Books;
                        var podcasts = record.Podcasts;
                        var music = record.Music;
                        var articles = record.Articles;

                        //Movies
                        if (!string.IsNullOrEmpty(record.Movies))
                        {
                            var existingMovie = context.Media.Where(x => x.Title.ToLower() == record.Movies.ToLower() && x.MediaType == "SHOW").FirstOrDefault();
                            if (existingMovie != null)
                            {
                                var existingMovieEpisode = episode.Medias?.Where(x => x.Id == existingMovie.Id).FirstOrDefault();
                                if (existingMovieEpisode == null)
                                {
                                    episode.Medias?.Add(existingMovie);
                                }
                            }
                            else
                            {
                                episode.Medias?.Add(new Media()
                                {
                                    CreateDate = DateTime.Now,
                                    MediaType = "MOVIE",
                                    Title = record.Movies
                                });
                            }
                        }

                        //Documentaries
                        if (!string.IsNullOrEmpty(record.Documentaries))
                        {
                            var existingDocumentary = context.Media.Where(x => x.Title.ToLower() == record.Documentaries.ToLower() && x.MediaType == "DOCUMENTARY").FirstOrDefault();

                            if (existingDocumentary != null)
                            {
                                var existingDocumentaryEpisode = episode.Medias?.Where(x => x.Id == existingDocumentary.Id).FirstOrDefault();

                                if (existingDocumentaryEpisode == null)
                                {

                                    episode.Medias?.Add(existingDocumentary);
                                }
                            }
                            else
                            {
                                episode.Medias?.Add(new Media()
                                {
                                    CreateDate = DateTime.Now,
                                    MediaType = "DOCUMENTARY",
                                    Title = record.Documentaries
                                });
                            }
                        }

                        //Shows
                        if (!string.IsNullOrEmpty(record.Shows))
                        {
                            var existingShow = context.Media.Where(x => x.Title.ToLower() == record.Shows.ToLower() && x.MediaType == "SHOW").FirstOrDefault();
                            if (existingShow != null)
                            {
                                var existingShowEpisode = episode.Medias?.Where(x => x.Id == existingShow.Id).FirstOrDefault();
                                if (existingShowEpisode == null)
                                {
                                    episode.Medias?.Add(existingShow);
                                }
                            }
                            else
                            {
                                episode.Medias?.Add(new Media()
                                {
                                    CreateDate = DateTime.Now,
                                    MediaType = "SHOW",
                                    Title = record.Shows
                                });
                            }
                        }

                        //Books
                        if (!string.IsNullOrEmpty(record.Books))
                        {
                            var existingBook = context.Media.Where(x => x.Title.ToLower() == record.Books.ToLower() && x.MediaType == "BOOK").FirstOrDefault();
                            if (existingBook != null)
                            {
                                var existingBookEpisode = episode.Medias?.Where(x => x.Id == existingBook.Id).FirstOrDefault();
                                if (existingBookEpisode == null)
                                {
                                    episode.Medias?.Add(existingBook);
                                }
                            }
                            else
                            {
                                episode.Medias?.Add(new Media()
                                {
                                    CreateDate = DateTime.Now,
                                    MediaType = "BOOK",
                                    Title = record.Books
                                });
                            }
                        }

                        //Podcasts
                        if (!string.IsNullOrEmpty(record.Podcasts))
                        {
                            var existingPodcast = context.Media.Where(x => x.Title.ToLower() == record.Podcasts.ToLower() && x.MediaType == "PODCAST").FirstOrDefault();
                            if (existingPodcast != null)
                            {
                                var existingPodcastEpisode = episode.Medias?.Where(x => x.Id == existingPodcast.Id).FirstOrDefault();
                                if (existingPodcastEpisode == null)
                                {
                                    episode.Medias?.Add(existingPodcast);
                                }
                            }
                            else
                            {
                                episode.Medias?.Add(new Media()
                                {
                                    CreateDate = DateTime.Now,
                                    MediaType = "PODCAST",
                                    Title = record.Podcasts
                                });
                            }
                        }

                        //Music
                        if (!string.IsNullOrEmpty(record.Music))
                        {
                            var existingMusic = context.Media.Where(x => x.Title.ToLower() == record.Music.ToLower() && x.MediaType == "MUSIC").FirstOrDefault();
                            if (existingMusic != null)
                            {
                                var existingMusicEpisode = episode.Medias?.Where(x => x.Id == existingMusic.Id).FirstOrDefault();
                                if (existingMusicEpisode == null)
                                {
                                    episode.Medias?.Add(existingMusic);
                                }
                            }
                            else
                            {
                                episode.Medias?.Add(new Media()
                                {
                                    CreateDate = DateTime.Now,
                                    MediaType = "MUSIC",
                                    Title = record.Music
                                });
                            }
                        }

                        //Articles
                        if (!string.IsNullOrEmpty(record.Articles))
                        {
                            var existingArticle = context.Media.Where(x => x.Title.ToLower() == record.Articles.ToLower() && x.MediaType == "ARTICLE").FirstOrDefault();
                            if (existingArticle != null)
                            {
                                var existingArticleEpisode = episode.Medias?.Where(x => x.Id == existingArticle.Id).FirstOrDefault();
                                if (existingArticleEpisode == null)
                                {
                                    episode.Medias?.Add(existingArticle);
                                }
                            }
                            else
                            {
                                episode.Medias?.Add(new Media()
                                {
                                    CreateDate = DateTime.Now,
                                    MediaType = "ARTICLE",
                                    Title = record.Articles
                                });
                            }
                        }


                    }
                    else
                    {
                        Console.WriteLine($"Could not find {record.Episode}!");

                    }

                    context.SaveChanges();
                }
            }


        }
    }
}