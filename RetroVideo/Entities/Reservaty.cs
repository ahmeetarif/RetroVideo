using System;
using System.Collections.Generic;

namespace RetroVideo.Entities;

public partial class Reservaty
{
    public long KlantId { get; set; }

    public long FilmId { get; set; }

    public DateTime Reservatie { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual Klanten Klant { get; set; } = null!;
}
