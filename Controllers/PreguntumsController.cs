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

        public ActionResult MostrarPregunta(int participanteId, int tiempoRestante = 30)
        {
            var participante = _context.Participantes.FirstOrDefault(p => p.Id == participanteId);
            // Obtener una pregunta aleatoria del contexto de base de datos con sus respuestas
            var pregunta = _context.Pregunta.Include(p => p.Respuesta).OrderBy(p => Guid.NewGuid()).FirstOrDefault();

            // Crear un modelo de vista de pregunta y asignar la pregunta, las opciones de respuesta y el tiempo restante
            var modelo = new PreguntaViewModel
            {
                Pregunta = pregunta,
                Opciones = pregunta.Respuesta.Select(r => r).ToList(),
                TiempoRestante = tiempoRestante,
                participante = participante
            };

            // Mostrar la vista de pregunta con el modelo de vista creado
            return View("Pregunta", modelo);
        }

        public ActionResult ValidarRespuesta(int respuestaId, int participanteId)
        {
            
            var respuestaSeleccionada = _context.Respuesta.Find(respuestaId);


            var pregunta = _context.Pregunta.Find(respuestaSeleccionada.PreguntaId);

            
            var participante = _context.Participantes.Find(participanteId);

            
            if (respuestaSeleccionada.IsCorrect==true)
            {
                
                participante.Puntaje++;

                
                _context.SaveChanges();

                // Obtener la siguiente pregunta aleatoria del contexto de base de datos con sus respuestas
                var siguientePregunta = _context.Pregunta.Include(p => p.Respuesta).OrderBy(p => Guid.NewGuid()).FirstOrDefault();

                // Crear un modelo de vista de pregunta y asignar la pregunta, las opciones de respuesta y el tiempo restante
                var modelo = new PreguntaViewModel
                {
                    Pregunta = siguientePregunta,
                    Opciones = siguientePregunta.Respuesta.Select(r => r).ToList(),
                    TiempoRestante = 30,
                    participante = participante
                };

                // Mostrar la vista de pregunta con el modelo de vista creado
                return View("Pregunta", modelo);
            }
            else
            {
                // El participante ha perdido el juego, mostrar un mensaje de fin de juego
                var modelo = new FinJuegoViewModel
                {
                    Puntaje = participante.Puntaje,
                    ParticipanteId = participanteId
                };

                // Mostrar la vista de fin de juego con el modelo de vista creado
                return View("FinJuego", modelo);
            }
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
