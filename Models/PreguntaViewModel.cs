namespace UltimateBrain.Models
{
    public class PreguntaViewModel
    {
        public Preguntum Pregunta { get; set; }
        public List<Respuestum> Opciones { get; set; }
        public int TiempoRestante { get; set; }

        public Participante participante { get; set; }
    }
}
