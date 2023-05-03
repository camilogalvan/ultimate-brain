using System;
using System.Collections.Generic;

namespace UltimateBrain.Models;

public partial class Partidum
{
    public int? ParticipanteId { get; set; }

    public int? Puntaje { get; set; }

    public int? Tiempo { get; set; }

    public int Id { get; set; }

    public virtual Participante? Participante { get; set; }
}
