using Configuration.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infrastructure.Persistence.Entities;

public class SiteContent : EntityAuditBase<long>
{
    [MaxLength(200)]
    public string ContentType { get; set; }

    [MaxLength(500)]
    public string Title { get; set; }

    [MaxLength(200)]
    public string ImageUrl { get; set; }

    [MaxLength(200)]
    public string Slug { get; set; }

    [MaxLength(4000)]
    public string Summary { get; set; }

    [Column(TypeName = "ntext")]
    public string Content { get; set; }

    public int Order { get; set; }
}
