using Contentful.Core;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reload.Data;
using Reload.Models;
using Reload.Models.Contentful;
using Reload.Models.DataTables;
using System.Diagnostics;
using System.Linq;

namespace Reload.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContentfulClient _client;

        private readonly AuthDbContext _db;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(IContentfulClient client, AuthDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _client = client;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Start()
        {
            return View();
        }

        public async Task<IActionResult> DisplayCourses(string searchString, string categorySelection)
        {
            var builder = QueryBuilder<Course>.New.ContentTypeIs("course");
            var entries = await _client.GetEntries(builder);

            List<Course> courses = (List<Course>)entries.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                courses = (List<Course>)courses.Where(s => s.Title.ToLower().Contains(searchString)).ToList();
            }

            if (!String.IsNullOrEmpty(categorySelection))
            {
                categorySelection = categorySelection.ToLower();
                courses = (List<Course>)courses.Where(s => s.Category.ToLower().Contains(categorySelection)).ToList();
            }


            if (_signInManager.IsSignedIn(User))
            {
                IdentityUser user = await GetCurrentUserAsync();
                var userDb = _db.CourseProgress.Where(c => c.UserId.Equals(user.Id)).ToList();

                foreach (var course in courses)
                {
                    var userDbCourseName = userDb.Where(u => u.CourseName.Equals(course.Title)).ToArray();
                    if (userDbCourseName.Count() > 0)
                    {
                        course.ChapterStartIdx = userDbCourseName[0].ChapterIdx;
                    }
                    else
                    {
                        course.ChapterStartIdx = 0;
                    }
                }
            }

            return View(courses.ToArray());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}