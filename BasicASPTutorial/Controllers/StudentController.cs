using BasicASPTutorial.Data;
using BasicASPTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace BasicASPTutorial.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext _db;

        public StudentController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        { /*
            IEnumerable <Student> allStudent = _db.Students;
            return View(allStudent); */            
            return View();
        }

        [HttpPost]
        public IActionResult Index(string idtxt, string nametxt, string datetxt, int pageIndex)
        { /*
            IEnumerable <Student> allStudent = _db.Students;
            return View(allStudent); 
            ViewBag.idtxt=idtxt;*/
            int pageSize = 5;

            var std = from p in _db.Students
                      select p;


            /*int TotalPages = (int)Math.Ceiling(std.Count() / (double)pageSize); */
                        
            if (!String.IsNullOrEmpty(idtxt))
            {
                var abc = Int32.Parse(idtxt);
                std = std.Where(s => s.Id == abc);

            }
            if (!String.IsNullOrEmpty(nametxt))
            {
                std = std.Where(s => s.Name.Contains(nametxt));              

            }
            if (!String.IsNullOrEmpty(datetxt))
            {
                DateTime dtFrom = Convert.ToDateTime(datetxt);
                std = std.Where(s => s.DateUpdate >= dtFrom);

            }

            /*int TotalPages = (int)(std.Count() / pageSize); */
            int CountRows = std.Count();
            int TotalPages = ((CountRows + pageSize - 1) / pageSize);
            ViewBag.CountRows = CountRows; // Hidden field

            if (!std.IsNullOrEmpty())
            {
                pageIndex = (pageIndex <= 0) ? 1 : pageIndex; // pg_aml_PageNo Bind from Page
                int skipRow = (pageIndex - 1) * pageSize;
                if (skipRow > 0)
                {
                    std = std.Skip(skipRow).Take(pageSize);
                }
                else
                {
                    std = std.Take(pageSize);
                }
                ViewBag.PageNo = pageIndex; // Hidden field
                ViewBag.TotalPages = TotalPages; // Hidden field
                return View(std);
            }
            return View();
        }

        // GET METHOD
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create2(Student obj, string DateUpdate)
        {
            /*
            if (ModelState.IsValid)
            {
               
            } */
            obj.DateUpdate = DateTime.ParseExact(DateUpdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            _db.Students.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            ViewBag.abc = "xyz";
            return View(obj);
        }

        [HttpPost]        
        public IActionResult Create(Student obj, string DateUpdate)
        {
            /*
            if (ModelState.IsValid)
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);  */
            obj.DateUpdate = DateTime.ParseExact(DateUpdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            _db.Students.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            var obj = _db.Students.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]        
        public IActionResult Edit(Student obj, string DateUpdate)
        {    
            /*
            if (ModelState.IsValid)
            {              
                obj.DateUpdate = DateTime.ParseExact(DateUpdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _db.Students.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj); */
                                      
            obj.DateUpdate = DateTime.ParseExact(DateUpdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            _db.Students.Update(obj);                 
            _db.SaveChanges();
            return RedirectToAction("Index");            
            
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //ค้นหาข้อมูล
            var obj = _db.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Students.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
