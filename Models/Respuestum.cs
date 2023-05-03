using System;
using System.Collections.Generic;

namespace UltimateBrain.Models;

public partial class Respuestum
{
    public string? TextoRespuesta { get; set; }

    public bool? IsCorrect { get; set; }

    public int? PreguntaId { get; set; }

    public int Id { get; set; }

    public virtual Preguntum? Pregunta { get; set; }
}
