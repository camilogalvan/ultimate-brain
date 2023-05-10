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
    public class RespuestumsController : Controller
    {
        private readonly UltimateBrainContext _context;

        public RespuestumsController(UltimateBrainContext context)
        {
            _context = context;
        }

        // GET: Respuestums
        public async Task<IActionResult> Index()
        {
            var ultimateBrainContext = _context.Respuesta.Include(r => r.Pregunta);
            return View(await ultimateBrainContext.ToListAsync());
        }

        // GET: Respuestums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Respuesta == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta
                .Include(r => r.Pregunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respuestum == null)
            {
                return NotFound();
            }

            return View(respuestum);
        }

        // GET: Respuestums/Create
        public IActionResult Create()
        {
            ViewData["PreguntaId"] = new SelectList(_context.Pregunta, "Id", "Id");
            return View();
        }

        // POST: Respuestums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TextoRespuesta,IsCorrect,PreguntaId,Id")] Respuestum respuestum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(respuestum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PreguntaId"] = new SelectList(_context.Pregunta, "Id", "Id", respuestum.PreguntaId);
            return View(respuestum);
        }

        // GET: Respuestums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Respuesta == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta.FindAsync(id);
            if (respuestum == null)
            {
                return NotFound();
            }
            ViewData["PreguntaId"] = new SelectList(_context.Pregunta, "Id", "Id", respuestum.PreguntaId);
            return View(respuestum);
        }

        // POST: Respuestums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TextoRespuesta,IsCorrect,PreguntaId,Id")] Respuestum respuestum)
        {
            if (id != respuestum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(respuestum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespuestumExists(respuestum.Id))
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
            ViewData["PreguntaId"] = new SelectList(_context.Pregunta, "Id", "Id", respuestum.PreguntaId);
            return View(respuestum);
        }

        // GET: Respuestums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Respuesta == null)
            {
                return NotFound();
            }

            var respuestum = await _context.Respuesta
                .Include(r => r.Pregunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respuestum == null)
            {
                return NotFound();
            }

            return View(respuestum);
        }

        // POST: Respuestums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Respuesta == null)
            {
                return Problem("Entity set 'UltimateBrainContext.Respuesta'  is null.");
            }
            var respuestum = await _context.Respuesta.FindAsync(id);
            if (respuestum != null)
            {
                _context.Respuesta.Remove(respuestum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespuestumExists(int id)
        {
          return (_context.Respuesta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
