using MediaGuide.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MediaGuide.Web.ViewModels;

namespace MediaGuide.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;

    public List<EpisodeListing> ListOfEpisodes { get; private set; }
    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> OnGet()
    {
        var episodeListings = new List<EpisodeListing>();

        var episodes = await _context
            .Episodes
            .Include(x => x.Medias)
            .Where(x => x.Show.Title == "Armchair Expert" && x.Display == true)
            .OrderByDescending(x => x.EpisodeDate)
            .ToListAsync();

        foreach (var episode in episodes)
        {
            var episodeListing = new EpisodeListing();
            episodeListing.Name = episode.Name;
            episodeListing.Description = episode.Description;
            episodeListing.Url = episode.Url;
            episodeListing.ImageUrl = episode.ImageUrl;
            episodeListing.HasMedia = episode.Medias?.Count > 0;

            var articles = episode.Medias?.Where(x => x.MediaType.ToUpper() == "ARTICLE").Select(x => x.Title).ToList();
            if (articles != null)
            {
                episodeListing.Articles = string.Join(", ", articles);
            }

            var books = episode.Medias?.Where(x => x.MediaType.ToUpper() == "BOOK").Select(x => x.Title).ToList();
            if (books != null)
            {
                episodeListing.Books = string.Join(", ", books);
            }

            var documentaries = episode.Medias?.Where(x => x.MediaType.ToUpper() == "DOCUMENTARY").Select(x => x.Title).ToList();
            if (documentaries != null)
            {
                episodeListing.Documentaries = string.Join(", ", documentaries);
            }

            var movies = episode.Medias?.Where(x => x.MediaType.ToUpper() == "MOVIE").Select(x => x.Title).ToList();
            if (movies != null)
            {
                episodeListing.Movies = string.Join(", ", movies);
            }

            var music = episode.Medias?.Where(x => x.MediaType.ToUpper() == "MUSIC").Select(x => x.Title).ToList();
            if (music != null)
            {
                episodeListing.Music = string.Join(", ", music);
            }

            var podcasts = episode.Medias?.Where(x => x.MediaType.ToUpper() == "PODCAST").Select(x => x.Title).ToList();
            if (podcasts != null)
            {
                episodeListing.Podcasts = string.Join(", ", podcasts);
            }

            var shows = episode.Medias?.Where(x => x.MediaType.ToUpper() == "SHOW").Select(x => x.Title).ToList();
            if (shows != null)
            {
                episodeListing.Shows = string.Join(", ", shows);
            }

            episodeListings.Add(episodeListing);
        }

        ListOfEpisodes = episodeListings;
        return Page();
    }
}
