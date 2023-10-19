using System;
using System.Collections.Generic;

namespace RetroVideo.Entities;

public partial class Klanten
{
    public long Id { get; set; }

    public string Familienaam { get; set; } = null!;

    public string Voornaam { get; set; } = null!;

    public string Straat { get; set; } = null!;

    public string Huisnummer { get; set; } = null!;

    public string? Bus { get; set; }

    public string Postcode { get; set; } = null!;

    public string Gemeente { get; set; } = null!;

    public virtual ICollection<Reservaty> Reservaties { get; set; } = new List<Reservaty>();
}
