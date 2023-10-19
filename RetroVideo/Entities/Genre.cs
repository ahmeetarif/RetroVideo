using System;
using System.Collections.Generic;

namespace RetroVideo.Entities;

public partial class Genre
{
    public long Id { get; set; }

    public string Naam { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
