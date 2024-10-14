using WebApplication7.Models;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IPlace
	{
       public List<Place> GetAll();
       public Place GetById(int id);
    }
}
