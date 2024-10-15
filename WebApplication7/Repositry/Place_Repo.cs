using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;
namespace WebApplication7.Repositry
{
    public class Place_Repo : IPlace
    {
        DepiContext _context;
        public Place_Repo(DepiContext _context)
        {
            this._context = _context;
        }
        public List<Place> GetAll()
        {
            return _context.Places.ToList();

        }
        public List<Place> GetAllMuseum()
        {
            var ans=new List<Place>();
            ans=_context.Places.Where(x=>x.Place_Type=="Museum").ToList();
            return ans;
        } public List<Place> GetAllHotels()
        {
            var ans=new List<Place>();
            ans=_context.Places.Where(x=>x.Place_Type=="Hotel").ToList();
            return ans;
        }
        public Place GetById(int id)
        {
            return _context.Places.FirstOrDefault(x => x.Place_Id == id);
        }
      

        public Place Get(int id)
        {
            return _context.Places.Find(id);
        }

        public void Add(Place place)
        {
            _context.Places.Add(place);
             Save();
        }

        public void Edit(Place place)
        {
            var existingPlace = _context.Places.Find(place.Place_Id);
            if (existingPlace != null)
            {
                existingPlace.Place_Name = place.Place_Name;
                existingPlace.Place_Type = place.Place_Type;
                existingPlace.Place_City = place.Place_City;
                existingPlace.Place_Price = place.Place_Price;
                existingPlace.Place_Rating = place.Place_Rating;
                existingPlace.Description = place.Description;
                if (place.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    place.clientFile.CopyTo(stream);
                    existingPlace.dbimage = stream.ToArray();
                }
                else
                {
                    place.dbimage = existingPlace.dbimage;
                }
                Save();
            }
        }

        public void Delete(int id)
        {
            var place = _context.Places.Find(id);
            if (place != null)
            {
                _context.Places.Remove(place);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
