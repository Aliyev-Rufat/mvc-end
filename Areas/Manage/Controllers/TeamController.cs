using Microsoft.AspNetCore.Mvc;
using WebApplication10.Models;
using WebApplication13.DAL;


namespace WebApplication13.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {

        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _environment;


        public TeamController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }




        public IActionResult Index()
        {
            return View( _context.Teams.ToList());
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]


        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string path = _environment.WebRootPath + @"\Upload\Team\";


            string filename = Guid.NewGuid() + team.ImgFile.FileName;
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                team.ImgFile.CopyTo(stream);
            }
            team.ImgUrl = filename;
            _context.Teams.Add(team);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Team team = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (team == null) 
            {
                return RedirectToAction("Index");
            }
            return View(team);
        }
        [HttpPost]
        public IActionResult Update(Team newteam)
        {
            Team oldteam = _context.Teams.FirstOrDefault(x => x.Id == newteam.Id);
            if (oldteam == null) return NotFound();
            if (!ModelState.IsValid) return View(oldteam);
            if (newteam.ImgFile != null)
            {
                
                string path = _environment.WebRootPath + @"\Upload\Team\";
                FileInfo info = new FileInfo(path + oldteam.ImgUrl);
                if(info.Exists)
                {
                    info.Delete();
                }
                string filename = Guid.NewGuid() + newteam.ImgFile.FileName;
                using (FileStream stream = new FileStream(path + filename, FileMode.Create))
                {
                    newteam.ImgFile.CopyTo(stream);
                }
                oldteam.ImgUrl = filename;

            }
            oldteam.Name=newteam.Name;
            oldteam.Position=newteam.Position;
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }
            
    }
}
