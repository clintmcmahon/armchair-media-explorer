using System.ComponentModel.DataAnnotations;

namespace MediaGuide.Data.Models;

public class Show
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public DateTime CreateDate { get; set; }
    public string? SpotifyShowId { get; set; }
    public virtual List<Episode>? Episodes { get; set; }

}