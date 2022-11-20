using System.ComponentModel.DataAnnotations;

namespace MediaGuide.Data.Models;

public class Episode
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? HtmlDescription { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public int ShowId { get; set; }
    public string? EpisodeDate { get; set; }
    public DateTime CreateDate { get; set; }
    public bool Display { get; set; }
    public virtual List<Media>? Medias { get; set; }
    public virtual Show? Show { get; set; }

    public Episode()
    {
        Medias = new List<Media>();
    }
}