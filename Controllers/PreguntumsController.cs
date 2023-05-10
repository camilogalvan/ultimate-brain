using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UltimateBrain.Models;

namespace UltimateBrain.Controllers
{
    public class PreguntumsController : Controller
    {
        private readonly UltimateBrainContext _context;

        public PreguntumsController(UltimateBrainContext context)
        {
            _context = context;
        }

        // GET: Preguntums
        public async Task<IActionResult> Index()
        {
            return _context.Pregunta != null ?
                        View(await _context.Pregunta.ToListAsync()) :
                        Problem("Entity set 'UltimateBrainContext.Pregunta'  is null.");
        }

        // GET: Preguntums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pregunta == null)
            {
                return NotFound();
            }

            var preguntum = await _context.Pregunta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preguntum == null)
            {
                return NotFound();
            }

            return View(preguntum);
        }

        // GET: Preguntums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Preguntums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TextoPregunta,Respuestas,Categoria,Dificultad,Tipo,Id")] Preguntum preguntum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preguntum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preguntum);
        }

        // GET: Preguntums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pregunta == null)
            {
                return NotFound();
            }

            var preguntum = await _context.Pregunta.FindAsync(id);
            if (preguntum == null)
            {
                return NotFound();
            }
            return View(preguntum);
        }

        // POST: Preguntums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TextoPregunta,Respuestas,Categoria,Dificultad,Tipo,Id")] Preguntum preguntum)
        {
            if (id != preguntum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preguntum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreguntumExists(preguntum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(preguntum);
        }

        // GET: Preguntums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pregunta == null)
            {
                return NotFound();
            }

            var preguntum = await _context.Pregunta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preguntum == null)
            {
                return NotFound();
            }

            return View(preguntum);
        }

        public ActionResult MostrarPregunta(int participanteId)
        {
            int tiempoRestante = 30;

            var participante = _context.Participantes.FirstOrDefault(p => p.Id == participanteId);

            var pregunta = _context.Pregunta.Include(p => p.Respuesta).OrderBy(p => Guid.NewGuid()).FirstOrDefault();


            var modelo = new PreguntaViewModel
            {
                Pregunta = pregunta,
                Opciones = pregunta.Respuesta.Select(r => r).ToList(),
                TiempoRestante = tiempoRestante,
                participante = participante
            };

            return View("Pregunta", modelo);
        }

        public ActionResult ValidarRespuesta(int respuestaId, int participanteId)
        {

            var respuestaSeleccionada = _context.Respuesta.Find(respuestaId);


            var pregunta = _context.Pregunta.Find(respuestaSeleccionada.PreguntaId);


            var participante = _context.Participantes.Find(participanteId);
            string combinedId = participanteId.ToString() + pregunta.Id.ToString();
            int finalId = int.Parse(combinedId);

            if (respuestaSeleccionada.IsCorrect == true)
            {
                var preguntaResuelta = new PreguntaResuelta
                {
                    Id = finalId,
                    IdPregunta = pregunta.Id,
                    ParticipanteId = participante.Id
                };

                _context.PreguntaResueltas.Add(preguntaResuelta);
                participante.PreguntasResueltas.Add(preguntaResuelta);

                participante.Puntaje++;
                _context.SaveChanges();

                var preguntasDisponibles = _context.Pregunta.ToList();
                var siguientePregunta = new Preguntum();
                bool encontrada = false;

                foreach (var preguntum in preguntasDisponibles)
                {
                    combinedId = participanteId.ToString() + preguntum.Id.ToString();
                    finalId = int.Parse(combinedId);

                    if (!_context.PreguntaResueltas.Any(pr => pr.Id == finalId))
                    {
                        siguientePregunta = preguntum;
                        encontrada = true;
                        break; 
                    }
                }

                if (encontrada)
                {
                    var opciones = _context.Respuesta
                        .Where(r => r.PreguntaId == siguientePregunta.Id)
                        .Select(r => r)
                        .ToList();

                    var modelo = new PreguntaViewModel
                    {
                        Pregunta = siguientePregunta,
                        Opciones = opciones,
                        TiempoRestante = 30,
                        participante = participante
                    };

                    return View("Pregunta", modelo);
                }
                else
                {
                    return View("Ganador", participante);
                }
            }
            else
            {
                participante.PreguntasResueltas.Clear();
                _context.SaveChanges();
                var modelo = new FinJuegoViewModel
                {
                    Puntaje = participante.Puntaje,
                    ParticipanteId = participanteId
                };

                return View("FinJuego", modelo);
            }
        }

        public IActionResult FinJuegoTiempo()
        {
            return View("FinJuegoTiempo");
        }

        // POST: Preguntums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pregunta == null)
            {
                return Problem("Entity set 'UltimateBrainContext.Pregunta'  is null.");
            }
            var preguntum = await _context.Pregunta.FindAsync(id);
            if (preguntum != null)
            {
                _context.Pregunta.Remove(preguntum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreguntumExists(int id)
        {
            return (_context.Pregunta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
