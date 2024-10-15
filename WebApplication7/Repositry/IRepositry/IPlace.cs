using WebApplication7.Models;

namespace WebApplication7.Repositry.IRepositry
{
    public interface IPlace
	{
       public List<Place> GetAll(); 
       public List<Place> GetAllMuseum();
        public List<Place> GetAllHotels();
        public Place Get(int id);
       public void Add(Place place);
        public void Edit(Place place);
        public void Delete(int id);
        public void Save();
    }
}
