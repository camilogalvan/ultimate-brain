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

    public virtual ICollection<Partidum> Partida { get; set; } = new List<Partidum>();
}
