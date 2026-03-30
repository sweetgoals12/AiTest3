using System;
using System.Collections.Generic;

namespace aitest3.Models;

public partial class UserCard
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public int CardSetId { get; set; }

    public int CardId { get; set; }

    public string CardDataJson { get; set; } = null!;

    public DateTime AddedAt { get; set; }

    public int Count { get; set; }
}
