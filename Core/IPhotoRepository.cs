using System.Collections.Generic;
using System.Threading.Tasks;
using AgilFood.Core.models;

namespace AgilFood.Core
{
    public interface IPhotoRepository
    {
         Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}