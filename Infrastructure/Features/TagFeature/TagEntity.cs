using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Features.ArticleFeature;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.TagFeature;

[Table("Tag")]
[Index(nameof(Name), IsUnique = true)]
public class TagEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<ArticleEntity> Articles { get; set; }
}