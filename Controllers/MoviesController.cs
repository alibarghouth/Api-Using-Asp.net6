using Add_Database_Model.Dtos;
using Add_Database_Model.Helpers;
using Add_Database_Model.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Add_Database_Model.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMoviesService _moviesService;
        private readonly IGenreService  _genreService;

        public MoviesController(IMoviesService moviesService, IMapper mapper, IGenreService genreService)
        {
            _moviesService = moviesService;
            _mapper = mapper;
            _genreService = genreService;
        }

        private List<string> _allowedEExtenstions = new List<string>()
        {
            ".png",
            ".jpg"
        };
        private long _maxAllowedPosterSize = 1048576;
        

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movie =await _moviesService.GetMoviesAsync();

            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(movie);    

            return Ok(data);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetMovieById( int id)
        {
            var movie = await _moviesService.GetMoviesByIdAsync(id);

            var data = _mapper.Map<MoviesDetailsDto>(movie);   
               

            if (data == null)
            {
                return NotFound("The Id Is Not Found");
            }

            return Ok(data);
        }

        [HttpGet("GetbyGenreId")]

        public async Task<IActionResult> GetbyGenreIdAsync(byte genreid)
        {
            var movie = await _moviesService.GetMoviesAsync(genreid);

            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(movie);

            
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MoviesDto moviesDto)
        {
            if(moviesDto.Poster is null)
            {
                return NotFound("The Poster is Not Found");
            }
            if (!_allowedEExtenstions.Contains(Path.GetExtension(moviesDto.Poster.FileName).ToLower())){
                return BadRequest($"The File Extensionis Not Allowed The .jpj or png is Allowed Extension");
            }

            if (moviesDto.Poster.Length > _maxAllowedPosterSize)
            {
                return BadRequest("The Size Is High and Not Allowed");
            }

            var _isValid =await _genreService.IsValid(moviesDto.GenreId);

            if (!_isValid)
            {
                return BadRequest("The movies is not Valid");

            }

            using var dataStream = new MemoryStream();
            await moviesDto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(moviesDto);
            movie.Poster = dataStream.ToArray();

            var movies =  _moviesService.AddMovies(movie);



            return Ok(movie);

            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsyc(int id, [FromForm] MoviesDto dto)
        {
            var movie = await _moviesService.GetMoviesByIdAsync(id);

            if (movie is null)
            {
                return NotFound("The Movie is not Found");
            }

            var _valid = await _genreService.IsValid(dto.GenreId);

            if (!_valid)
            {
                return BadRequest("The genre is not found");
            }

            

            if (dto.Poster != null)
            {
                if (!_allowedEExtenstions.Contains(Path.GetExtension(dto.Poster.FileName)))
                {
                    return BadRequest("The extension is not allwed");
                }

                if (dto.Poster.Length > _maxAllowedPosterSize)
                {
                    return BadRequest("The size is not allwed");
                }

                using var myImg = new MemoryStream();
                await dto.Poster.CopyToAsync(myImg);

                movie.Poster = myImg.ToArray();
            }

            movie.Title = dto.Title;
            movie.Rate = dto.Rate;
            movie.StoreLine = dto.StoreLine;
            movie.Year = dto.Year;
            
            movie.GenreId = dto.GenreId;

            _moviesService.UpdateMoviesAsync(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetMoviesByIdAsync(id);

            if(movie is null)
            {
                return NotFound("not found the movies");
            }

            _moviesService.DeleteMoviesAsync(movie);
            
            return Ok(movie);
        }
        

    }
}
