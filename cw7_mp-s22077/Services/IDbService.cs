using cw7_mp_s22077.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cw7_mp_s22077.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeSortOfTrip>> GetTrips();
        Task<bool> RemoveClient(int id);
        Task<string> AssignClient(SomeSortOfClientTrip someSortOfClientTrip);

    }
}

