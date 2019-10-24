using FoodStore.Repositories;
using System.Threading.Tasks;

namespace FoodStore.Services
{
    public interface ISeedDataService
    {
        Task Initialize(FoodDbContext context);
    }
}
