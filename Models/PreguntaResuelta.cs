using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UltimateBrain.Models;

public partial class PreguntaResuelta
{
    public int? IdPregunta { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? ParticipanteId { get; set; }

    public virtual Participante? Participante { get; set; }

}
