using Add_Database_Model.Dtos;
using Add_Database_Model.Models;
using Add_Database_Model.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Add_Database_Model.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }


       

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genreService.GetAllGenreAsync();

            return Ok(genres);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetGenreAsync(byte id)
        {
            var genres = await _genreService.GetGenrebyidAsync(id);

            if(genres == null)
            {
                return BadRequest("not found genre");
            }
           
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResult(GenresDto creatGenresDto)
        {
            var genre = new Genre
            {
                Name = creatGenresDto.Name,
                Description = creatGenresDto.Description,
            };
            var addGenree = await _genreService.PostGenreAsync(genre);

            return Ok(addGenree);
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] Genre genre)
        {
            //استخدمنا ال SingleOrDefaultAsync لانو احنا هون مرفين لبرايمري كي بالباييت
            //المفروض نحكي زfindasync
            var genres = await _genreService.GetGenrebyidAsync(id);

            if(genres is null)
            {
                return NotFound($"Not Found Geners on id = {id} ");
            }
            
            genres.Name = genre.Name;
            genres.Description = genre.Description;

            _genreService.UpdateGenreAsync(genre);

            return Ok(genres);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genres = await _genreService.GetGenrebyidAsync(id);

            if(genres is null)
            {
                return NotFound($"Not Found Geners where Id = {id}");
            }

            _genreService.DeleteGenreAsync(genres);

            return Ok(genres);
        }
    }
}