using Azure.Identity;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.Repositry
{
    public class Contact_Repo
    {
        private readonly DepiContext dbContext;

        public Contact_Repo(DepiContext context)
        {
            dbContext = context;
        }
        public UserContactViewModel GetInfo(string id)
        {
            UserContactViewModel userContactViewModel = new UserContactViewModel();
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            userContactViewModel.Name = user.UserName;
            userContactViewModel.Email = user.Email;
            return userContactViewModel;
        }
        public void Add(Contact_Us contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact), "Contact object cannot be null");
            }
            dbContext.contact_us.Add(contact);
            dbContext.SaveChanges();
            
        }

        internal async Task<bool> AddAsync(Contact_Us cu)
        {
            throw new NotImplementedException();
        }
    }
}
