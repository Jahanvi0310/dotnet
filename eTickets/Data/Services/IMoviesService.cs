using System.Threading.Tasks;
using eTickets.Models;
using eTickets.Data.Base;
using eTickets.Data.ViewModels;
namespace eTickets.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
       
    }
}
