using System;
using System.Collections.Generic;

namespace UltimateBrain.Models;

public partial class Preguntum
{
    public string? TextoPregunta { get; set; }

    public string? Respuestas { get; set; }

    public string? Categoria { get; set; }

    public string? Dificultad { get; set; }

    public string? Tipo { get; set; }

    public int Id { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
