using System;
using System.Collections.Generic;

namespace aitest3.Models;

public partial class CardSet
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public int? ReleaseYear { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
