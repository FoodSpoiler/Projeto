using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Persistence;
using Microsoft.EntityFrameworkCore;
using AgilFood.Core;
using AgilFood.Core.models;

namespace AgilFood.Persistence
{
  public class PhotoRepository : IPhotoRepository
  {
    private readonly AgilFoodDbContext context;
    public PhotoRepository(AgilFoodDbContext context)
    {
      this.context = context;
    }


    public async Task<IEnumerable<Photo>> GetPhotos(int fornecedorId)
    {
      return await context.Photos
                       .Where(p => p.FornecedorId == fornecedorId)
                    .ToListAsync();
    }
  }
}