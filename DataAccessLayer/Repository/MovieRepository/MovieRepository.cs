using DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.MovieRepository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDBContext _dbContext;
        public MovieRepository(MovieDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountMoviesByGenre(string Genre)
        {
            return await _dbContext.title_basics.Where(m => m.genres.ToLower() == Genre.ToLower()) 
            .CountAsync();
        }
        public async Task<int> CountMoviesByReleaseYear(string ReleaseYear)
        {
            return await _dbContext.title_basics.Where(m => m.startyear == ReleaseYear) 
            .CountAsync();
        }
         public async Task<int> CountMoviesBySubString(string Substring)
        {
            return await _dbContext.title_basics.CountAsync(m => m.primarytitle.ToLower().Contains(Substring) || m.originaltitle.ToLower().Contains(Substring));
            
        }

        public async Task<IEnumerable<title_basics>> GetAllMoviesByGenre(string Genre, int page, int pagesize)
        {
            return await _dbContext.title_basics
                .Where(m => m.genres.ToLower() == Genre.ToLower())
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToListAsync();
        }
        public async Task<IEnumerable<title_basics>> GetAllMoviesByReleaseYear(string ReleaseYear, int page, int pagesize)
        {
            return await _dbContext.title_basics
                .Where(m => m.startyear == ReleaseYear.ToString())
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToListAsync();
        }
        public async Task<IEnumerable<title_basics>> SearchMoviesBySubString(string SubString, int page, int pagesize)
        {
            return await _dbContext.title_basics
                .Where(m => m.originaltitle.ToLower().Contains(SubString) || m.primarytitle.ToLower().Contains(SubString))
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToListAsync();
        }

        public async Task<title_basics> GetMovieByIdAsync(string id)
        {
           
            return await _dbContext.title_basics
                .FirstOrDefaultAsync(m => m.tconst == id);
        }
    }
}
