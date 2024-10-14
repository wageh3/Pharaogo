using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
namespace WebApplication7.Repositry
{
    public class Place_Repo : IPlace
    {
        DepiContext depi;
        public Place_Repo(DepiContext depi)
        {
            this.depi = depi;
        }
        public List<Place> GetAll()
        {
            return depi.Places.ToList();

        }
        public Place GetById(int id)
        {
            return depi.Places.FirstOrDefault(x => x.Place_Id == id);
        }
        public void Add(Place place) { 
            depi.Places.Add(place);
            depi.SaveChanges();
        }
	}
}
