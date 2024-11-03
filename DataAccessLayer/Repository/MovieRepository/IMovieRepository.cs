using DataAccessLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.MovieRepository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<title_basics>> GetAllMoviesByGenre(string Genre,int page, int pagesize);
        Task<IEnumerable<title_basics>> GetAllMoviesByReleaseYear(string ReleaseYear, int page, int pagesize);
        Task<IEnumerable<title_basics>> SearchMoviesBySubString(string Substring, int page, int pagesize);
        Task<int> CountMoviesByGenre(string Genre);
        Task<int> CountMoviesByReleaseYear(string ReleaseYear);
        Task<int> CountMoviesBySubString(string Substring);
        Task<title_basics> GetMovieByIdAsync(string id);
    }
}
