using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCoach.Models;

namespace MyCoach.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private MyCoachDatabaseContext db = new MyCoachDatabaseContext();

        // GET: Favorites
        public ActionResult Index()
        {
            return View(db.Favorites.ToList());
        }

        // GET: Favorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: Favorites/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "ID")] int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //対象トレーニング取得
            var training = db.Trainings.Find(id);

            //ユーザーID取得
            var loginUserName = User.Identity.Name;
            var loginUser = db.Users.FirstOrDefault(u => u.UserName == loginUserName);
            if(loginUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //追加処理
            var favorite = new Favorite { FavoriteTraining = training, AddDateTime = DateTime.Now, ApplicationUserId = loginUser.Id };
            db.Favorites.Add(favorite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // POST: Favorites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ApplicationUserId,AddDateTime")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Favorites.Add(favorite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(favorite);
        }

        // GET: Favorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ApplicationUserId,AddDateTime")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
