using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IPlace
	{
       public List<Place> GetAll(); 
       public PlaceViewModel GetAllMuseum();
        public PlaceViewModel GetAllHotels();
        public PlaceViewModel Get(int id);
        public void Add(Place place);
        public void Edit(Place place);
        public void Delete(int id);
        public void Save();
    }
}
