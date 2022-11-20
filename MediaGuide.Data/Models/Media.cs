using System.ComponentModel.DataAnnotations;

namespace MediaGuide.Data.Models;

public class Media
{

    [Required]
    public int Id { get; set; }

    [Required]
    public string? MediaType { get; set; }

    [Required]
    public string? Title { get; set; }

    public string? Url { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual List<Episode>? Episodes { get; set; }

}