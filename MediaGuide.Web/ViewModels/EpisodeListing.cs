using System.ComponentModel.DataAnnotations;

namespace MediaGuide.Web.ViewModels;

public class EpisodeListing
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
    public string? Articles { get; set; }
    public string? Books { get; set; }
    public string? Documentaries { get; set; }
    public string? Movies { get; set; }
    public string? Music { get; set; }
    public string? Podcasts { get; set; }
    public string? Shows { get; set; }
    public bool HasMedia { get; set; }

}