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
    public class ParticipantesController : Controller
    {
        private readonly UltimateBrainContext _context;

        public ParticipantesController(UltimateBrainContext context)
        {
            _context = context;
        }

        // GET: Participantes
        public async Task<IActionResult> Index()
        {
              return _context.Participantes != null ? 
                          View(await _context.Participantes.ToListAsync()) :
                          Problem("Entity set 'UltimateBrainContext.Participantes'  is null.");
        }

        // GET: Participantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // GET: Participantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Participantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NickName,Puntaje,Id")] Participante participante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participante);
        }

        // GET: Participantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }
            return View(participante);
        }

        // POST: Participantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NickName,Puntaje,Id")] Participante participante)
        {
            if (id != participante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipanteExists(participante.Id))
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
            return View(participante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistrarParticipante(Participante participante)
        {
            // Crea un nuevo participante con el nombre proporcionado y guárdalo en la base de datos
            var participante1 = new Participante { NickName = participante.NickName,
            Puntaje=0
            };
            _context.Participantes.Add(participante1);
            _context.SaveChanges();

            // Redirige al usuario a la acción mostrarPregunta con el ID del nuevo participante como parámetro
            return RedirectToAction("MostrarPregunta", "Preguntums", new { participanteId = participante1.Id });
        }

        public async Task<IActionResult> Registrar()
        {
            return View("/Views/Participantes/RegistrarParticipante.cshtml");
        }

            // GET: Participantes/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // POST: Participantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Participantes == null)
            {
                return Problem("Entity set 'UltimateBrainContext.Participantes'  is null.");
            }
            var participante = await _context.Participantes.FindAsync(id);
            if (participante != null)
            {
                _context.Participantes.Remove(participante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipanteExists(int id)
        {
          return (_context.Participantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
