using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using OrmLite.Repositories;
using OrmLiteConnection;

namespace OrmLite.Controllers
{
	public class UserController : Controller
	{
		//private readonly UserRepositoryWithOrmLiteDapper _userRepository = new UserRepositoryWithOrmLiteDapper();
		private readonly UserRepository _userRepository = new UserRepository();
		// GET: User
		public async Task<ActionResult> Index()
		{
			return View(await _userRepository.GetUsers());
		}

		// GET: User/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var user = await _userRepository.Get(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// GET: User/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: User/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName")] User user)
		{
			if (ModelState.IsValid)
			{
				await _userRepository.Create(user);
				return RedirectToAction("Index");
			}

			return View(user);
		}

		// GET: User/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var user = await _userRepository.Get(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: User/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName")] User user)
		{
			if (ModelState.IsValid)
			{
				await _userRepository.Update(user);
				return RedirectToAction("Index");
			}
			return View(user);
		}

		// GET: User/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var user = await _userRepository.Get(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: User/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			await _userRepository.Delete(id);
			return RedirectToAction("Index");
		}
	}
}