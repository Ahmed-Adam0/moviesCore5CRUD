using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using moviesCore5CRUD.Models;
using moviesCore5CRUD.VMOdel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace moviesCore5CRUD.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDBcontecst _context;
        public MoviesController(ApplicationDBcontecst contecst)
        {
            _context =contecst;
        }

        public async Task<IActionResult> Index()
        {
            var mov =await _context.movies.OrderBy(m=>m.Rate).ToListAsync();
            return View( mov);
        }
        public async Task<IActionResult> Create()
        {
            var viewmodel = new movieform
            {
                Generas = await _context.generas.OrderBy(m=>m.name).ToListAsync()
            };
            return View("MovieForm", viewmodel);
        }
        [HttpPost]
   
        public async Task<IActionResult> Create(movieform movieform)
        {
           if(!ModelState.IsValid)
            {
                movieform.Generas = await _context.generas.OrderBy(m => m.name).ToListAsync();
                return View("MovieForm", movieform);
            }
            var file = Request.Form.Files;
           
            if(!file.Any()) 
            {
                movieform.Generas = await _context.generas.OrderBy(m => m.name).ToListAsync();
                ModelState.AddModelError("poster", "please select poster");
                return View("MovieForm", movieform);
            }
            var poster = file.FirstOrDefault();
            using var datastream = new MemoryStream();
            await poster.CopyToAsync(datastream);

            var movie = new movie
            {
                Title = movieform.Title,
                GeneraId = movieform.GeneraId,
                year = movieform.year,
                Rate = movieform.Rate,
                StoreLine = movieform.StoreLine,
                poster = datastream.ToArray(),
            };
            _context.movies.Add(movie);
            _context.SaveChanges();
            return Redirect(nameof(Index));
        }
        public async Task<IActionResult> Edite(int? id)
        {
            if (id == null)
                return BadRequest();
            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
                return NotFound();
            var viewmodle = new movieform
            {
                Id = movie.Id,
                Title = movie.Title,
                GeneraId = movie.GeneraId,
                Rate = movie.Rate,
                year = movie.year,
                StoreLine = movie.StoreLine,
                poster = movie.poster,
                Generas = await _context.generas.OrderBy(m => m.name).ToListAsync()
            };
            return View("MovieForm", viewmodle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edite(movieform movieform)
        {
            if (!ModelState.IsValid)
            {
                movieform.Generas = await _context.generas.OrderBy(m => m.name).ToListAsync();
                return View("MovieForm", movieform);

            }
            var movie = await _context.movies.FindAsync(movieform.Id);
            if (movie == null)
            {
                return NotFound();
            }


            movie.Title = movieform.Title;
            movie.StoreLine = movieform.StoreLine;
            movie.year = movieform.year;
            movie.GeneraId = movieform.GeneraId;
            movie.Rate = movieform.Rate;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        } 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();
            var move = await _context.movies.Include(m=>m.Genera).SingleOrDefaultAsync(m=>m.Id==id);
            if(move==null)
                return NotFound();

            return View(move); 
        }
        public IActionResult Delete(int? id)
        {
            var move = _context.movies.Find(id);
            if (id != null)
            {
                return BadRequest();
            }

            _context.movies.Remove(move);
            _context.SaveChanges();
            return View("Index");
        }
    }
}
