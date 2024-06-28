using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAutomovil.Models;
using WebAutomovil.Models.DTO;

namespace WebAutomovil.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AutomovilContext _context;
        private readonly IMapper _mapper;

        public ClientesController(AutomovilContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Documento,Nombres,Estado")] ClienteSetDTO clienteDTO)
        {
            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                cliente.FechaCreacion = DateTime.Now;
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Documento,Nombres,FechaCreacion,Estado")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: Clientes/AsignarCarro
        public IActionResult AsignarCarro()
        {
            //ViewData["Title"] = "AsignarCarro";
            ViewData["Cliente"] = new SelectList(_context.Clientes, "Id", "Nombres");
            ViewData["Carro"] = new SelectList(_context.Carros, "Id", "Descripcion");
            return View();
        }

        // POST: Clientes/AsignarCarro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarCarro([Bind("Id,Cliente,Carro,Estado")] ClienteCarroDTO clienteCarroDTO)
        {
            if (ModelState.IsValid)
            {
                var clienteCarro = _mapper.Map<ClienteCarro>(clienteCarroDTO);
                _context.Add(clienteCarro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListarAsignacion));
            }
            return View();
        }

        public async Task<IActionResult> ListarAsignacion()
        {
            var result = _context.ClienteCarros
                    .Include(x => x.ClienteNavigation)
                    .Include(x => x.CarroNavigation)
                    .ToListAsync()
                    ;
            return View(await result);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
