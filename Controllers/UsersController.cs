using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

public class UsersController : Controller
{
private WeddingPlannerContext _context;

public UsersController(WeddingPlannerContext context)
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

[HttpGet("signup")]
public IActionResult LoginReg()
{
    if(uid != null)
    {
        return RedirectToAction("Dashboard", "Users");
    }
    return View("LoginReg");
}

[HttpPost("weddingplanner/register")]
public IActionResult Register(User newUser)
{
    if(ModelState.IsValid)
    {
        if(_context.Users.Any(u => u.Email == newUser.Email))
        {
            ModelState.AddModelError("Email", " is taken");
        }
    }

    if(ModelState.IsValid == false)
    {
        return LoginReg();
    }

    PasswordHasher<User> Hasher = new PasswordHasher<User>();
    newUser.Pw = Hasher.HashPassword(newUser, newUser.Pw);
    _context.Users.Add(newUser);
    _context.SaveChanges();

    HttpContext.Session.SetInt32("UUID", newUser.UserId);

    return RedirectToAction("Dashboard");
}

[HttpPost("weddingplanner/login")]
public IActionResult Login(LogUser loginUser)
{
    if (ModelState.IsValid == false)
    {
        return LoginReg();
    }

    User? dbUser = _context.Users.FirstOrDefault(u => u.Email == loginUser.LogEmail);

    if (dbUser == null)
    {
        ModelState.AddModelError("LogEmail", " and Password do not match!");
        return LoginReg();
    }

    PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
    PasswordVerificationResult pwCompare = Hasher.VerifyHashedPassword(loginUser, dbUser.Pw, loginUser.LogPw);

    if(pwCompare == 0)
    {
        ModelState.AddModelError("LogEmail", " and Password do not match!");
        return LoginReg();
    }

    HttpContext.Session.SetInt32("UUID", dbUser.UserId);
    return RedirectToAction("Dashboard");
}

[HttpGet("dashboard")]
public IActionResult Dashboard(int weddingId)
{
    if(HttpContext.Session.GetInt32("UUID") == null)
    {
        return LoginReg();
    }

    List<Wedding> allWeddings = _context.Weddings.Include(a => a.LoggedUser).Include(a => a.AttendsWedding).ThenInclude(a => a.Attender).ToList();
    ViewBag.allWeddings = allWeddings;
    return View("Dashboard");
}

[HttpPost("logout")]
public IActionResult Logout()
{
    HttpContext.Session.Clear();
    return RedirectToAction("LoginReg");
}
}