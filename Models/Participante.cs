using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UltimateBrain.Models;

public partial class Participante
{
    public string? NickName { get; set; }

    public int? Puntaje { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? PreguntaResuelta { get; set; }

    public virtual ICollection<PreguntaResuelta> PreguntasResueltas { get; set; } = new List<PreguntaResuelta>();
}
