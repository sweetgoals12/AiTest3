using System;
using System.Collections.Generic;

namespace aitest3.Models;

public partial class NhlupperDeckMvp20252026
{
    public int Id { get; set; }

    public string SetName { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public string? PlayerName { get; set; }

    public string? TeamCity { get; set; }

    public string? TeamName { get; set; }

    public bool? Rookie { get; set; }

    public int? NumberedTo { get; set; }

    public string? Odds { get; set; }

    public string? Points { get; set; }
}
