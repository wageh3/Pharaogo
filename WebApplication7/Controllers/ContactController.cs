using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using WebApplication7.Models;
using WebApplication7.Repositry;

public class ContactController : Controller
{
    private readonly Contact_Repo contact_Repo;

    public ContactController(Contact_Repo contactRepo) 
    {
       contact_Repo = contactRepo; 
    }
    public IActionResult ContactPartial()
    {
        return PartialView("_ContactPartial");
    }

    public IActionResult Index(string id)
    {
        var x = contact_Repo.GetInfo(id);
        return View(x);
    }

    [HttpPost]
    public async Task<IActionResult> AddMessage(Contact_Us cu)
    {
        if (ModelState.IsValid)
        {
            bool result = await contact_Repo.AddAsync(cu);

            if (result)
            {
                TempData["SuccessMessage"] = "Your message has been successfully sent!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "There was a problem submitting your message. Please try again.";
            }
        }

        return View("Index", cu);
    }
}
