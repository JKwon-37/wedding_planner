using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

public class WeddingsController : Controller
{
    private WeddingPlannerContext _context;

    public WeddingsController(WeddingPlannerContext context)
    {
        _context = context;
    }

    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("UUID");
        }
    }

    private bool loggedIn
    {
        get
        {
            return uid != null;
        }
    }

    [HttpGet("weddings/new")]
    public IActionResult NewWedding()
    {
        if (!loggedIn)
        {
            return RedirectToAction("LoginReg", "Users");
        }
        return View("PlanWedding");
    }

    [HttpPost("weddings/create")]
    public IActionResult CreateWedding(Wedding newWedding)
    {
        if (uid == null)
        {
            return RedirectToAction("LoginReg", "Users");
        }

        if(ModelState.IsValid)
        {
            newWedding.UserId = (int)uid;
            _context.Weddings.Add(newWedding);
            _context.SaveChanges();
            return Redirect($"/weddings/details/{newWedding.WeddingId}");
        }
        return NewWedding();
    }

    [HttpGet("weddings/details/{weddingId}")]
    public IActionResult WeddingDetails(int weddingId)
    {
        if (!loggedIn)
        {
            return RedirectToAction("LoginReg", "Users");
        }
        Wedding? weddingDetails = _context.Weddings
            .Include(w => w.AttendsWedding)
            .ThenInclude(w => w.Attender)
            .FirstOrDefault(w => w.WeddingId == weddingId);

        return View("WeddingDetails", weddingDetails);
    }

    [HttpPost("weddings/rsvp/{weddingId}")]
    public IActionResult RSVP(int weddingId)
    {
        if(uid == null)
        {
            return RedirectToAction("LoginReg", "Users");
        }

        UserWeddingAttend? existingRSVP = _context.Attends.FirstOrDefault(e => e.UserId == uid && e.WeddingId  == weddingId);
        if(existingRSVP == null)
        {
            UserWeddingAttend? newRSVP = new UserWeddingAttend()
            {
                WeddingId = weddingId,
                UserId = (int)uid
            };
            _context.Attends.Add(newRSVP);
        } else
        {
            _context.Attends.Remove(existingRSVP);
        }
        _context.SaveChanges();
        return RedirectToAction("Dashboard", "Users");
    }
}