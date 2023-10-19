using System;
using System.Collections.Generic;

namespace RetroVideo.Entities;

public partial class Film
{
    public long Id { get; set; }

    public long GenreId { get; set; }

    public string Titel { get; set; } = null!;

    public long Voorraad { get; set; }

    public long Gereserveerd { get; set; }

    public decimal Prijs { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Reservaty> Reservaties { get; set; } = new List<Reservaty>();
}
