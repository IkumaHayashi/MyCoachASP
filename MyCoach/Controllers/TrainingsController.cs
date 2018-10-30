using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCoach.Models;
using MyCoach.ActionFilters;
using Microsoft.AspNet.Identity;

namespace MyCoach.Controllers
{
    [Authorize]
    public class TrainingsController : Controller
    {
        private MyCoachDatabaseContext db = new MyCoachDatabaseContext();

        // GET: Trainings
        public ActionResult Index()
        {

            //ログインユーザーのID取得
            string loginUserId = User.Identity.GetUserId();
            var trainings = db.Trainings.Where(t => t.ApplicationUserId == loginUserId).ToList();

            var trainingIndexViewModels = new List<TrainingIndexViewModel>();
            for (int i = 0; i < trainings.Count; i++)
            {
                //var viewTrainings =
                //    from t in trainings
                //    select new { t.Purpose, t.Title, t.UpdateDateTime, t.AddDateTime, t.Description, t.YoutubeURL };
                
                string userName = "";
                var userId = trainings[i].ApplicationUserId;
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null) userName = user.UserName;

                var viewTraining = new TrainingIndexViewModel
                {
                    ID = trainings[i].ID,
                    AddDateTime = trainings[i].AddDateTime.ToShortDateString(),
                    Purpose = trainings[i].Purpose,
                    Title = trainings[i].Title,
                    UpdateDateTime = trainings[i].UpdateDateTime,
                    YoutubeURL = trainings[i].YoutubeURL,
                    Tags = trainings[i].Tags.Select(x => x.Name).ToList(),
                    UserName = userName
                };
                trainingIndexViewModels.Add(viewTraining);

            }

            return View(trainingIndexViewModels);
        }

        //GET: Trainings/Search
        //[AllowAnonymous]
        //public ActionResult Search()
        //{

        //    return View(trainingSearchViewModel);
        //}


        // GET: Trainings/Search?searchValue=&SearchTag=
        [AllowAnonymous]
        public ActionResult Search(string searchValue, IList<string> SearchTags)
        {


            //表示用タグオブジェクトを取得
            List<ViewTagModel> ViewTagModels = new List<ViewTagModel>();
            db.Tags.ToList().ForEach(t => ViewTagModels.Add(new ViewTagModel { ID = t.ID, Name = t.Name, Checked = false }));

            //パラメータにタグの指定がある場合、チェック済みとする。
            if (SearchTags != null)
            {
                foreach( var tag in SearchTags)
                {
                    int ID = int.Parse(tag);
                    ViewTagModels.Find(vt => vt.ID == ID).Checked = true;

                }

            }

            List<Training> trainings = new List<Training>();

            //検索文字列、タグどちらも指定がない場合、全件取得
            if (string.IsNullOrEmpty(searchValue) && SearchTags == null)
            {
                trainings = db.Trainings.ToList();

            }
            else
            {


                //検索文字列にヒットするトレーニングを取得
                if (!string.IsNullOrEmpty(searchValue))
                {
                    db.Trainings.Where(t => t.Title.Contains(searchValue)).ToList().ForEach(training => trainings.Add(training));
                    db.Trainings.Where(t => t.Purpose.Contains(searchValue)).ToList().ForEach(training => trainings.Add(training));

                }

                //タグにヒットするトレーニングを取得
                foreach (var tag in ViewTagModels)
                {
                    if (tag.Checked == false) continue;

                    foreach (var training in db.Trainings.ToList())
                    {
                        if (training.Tags.Select(x => x.Name).Contains(tag.Name))
                        {
                            trainings.Add(training);
                        }

                    }
                }

                //重複削除
                trainings = trainings.Distinct().ToList();
            }

            //検索結果に表示するViewデータを生成
            var trainingIndexViewModels = new List<TrainingIndexViewModel>();
            for (int i = 0; i < trainings.Count; i++)
            {
                var viewTraining = new TrainingIndexViewModel(trainings[i]);

                ////ユーザー名を取得
                //string userName = "";
                //using (var appUserContext = new MyCoachDatabaseContext())
                //{
                //    var userId = trainings[i].ApplicationUserId;
                //    var user = appUserContext.Users.FirstOrDefault(u => u.Id == userId);
                //    if (user != null) userName = user.Name;
                //}

                ////オブジェクトに設定
                //var viewTraining = new TrainingIndexViewModel
                //{
                //    ID = trainings[i].ID,
                //    AddDateTime = trainings[i].AddDateTime.ToShortDateString(),
                //    Purpose = trainings[i].Purpose,
                //    Title = trainings[i].Title,
                //    UpdateDateTime = trainings[i].UpdateDateTime,
                //    YoutubeURL = trainings[i].YoutubeURL,
                //    Tags = trainings[i].Tags.Select(x => x.Name).ToList(),
                //    UserName = userName
                //};
                trainingIndexViewModels.Add(viewTraining);

            }



            //View用オブジェクトに格納
            var trainingSearchViewModel = new TrainingSearchViewModel();
            trainingSearchViewModel.ViewModels = trainingIndexViewModels;
            trainingSearchViewModel.ViewTagModels = ViewTagModels;



            return View(trainingSearchViewModel);
        }

        // GET: Trainings/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);

            //ユーザー名を取得
            string userName = "";
            var userId = training.ApplicationUserId;
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null) userName = user.UserName;

            //お気に入り追加済みか判定
            var favorite = db.Favorites.FirstOrDefault(f => f.ApplicationUserId == userId && f.TrainingID == id);
            bool isFavorite = true;
            if (favorite == null) isFavorite = false;



            //View用モデル生成
            var trainingDetailsViewModel = new TrainingDetailsViewModel()
            {
                ID = training.ID,
                AddDateTime = training.AddDateTime.ToShortDateString(),
                Purpose = training.Purpose,
                Title = training.Title,
                UpdateDateTime = training.UpdateDateTime,
                YoutubeURL = training.YoutubeURL,
                Tags = training.Tags.Select(t => t.Name).ToList(),
                IsFavorite = isFavorite,
                RecommendPersonNumber = training.RecommendPersonNumber,
                RequiredPersonNumber = training.RequiredPersonNumber,
                TimeDuration = training.TimeDuration, 
                UserName = userName
            };


            if (trainingDetailsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(trainingDetailsViewModel);
        }

        // GET: Trainings/Create
        public ActionResult Create()
        {
            var trainingCreateViewModel = new TrainingCreateViewModel();
            trainingCreateViewModel.Tags = db.Tags.ToList();
            return View(trainingCreateViewModel);
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Purpose,Description,YoutubeURL,AddPersonId")] Training training,
                                   string[] checkedTagValues)
        {

            //チェック済みタグの取得
            checkedTagValues = checkedTagValues ?? new string[] { };

            var checkedTagList = new List<Tag>();
            foreach(var tagValue in checkedTagValues)
            {
                var intTagValue = int.Parse(tagValue);
                Tag tag = db.Tags.FirstOrDefault(t => t.ID == intTagValue);
                if (tag != null) checkedTagList.Add(tag);
            }


            var userId = User.Identity.GetUserId();
            training.ApplicationUserId = userId;

            training.Tags = checkedTagList;
            training.AddDateTime = DateTime.Now;
            training.UpdateDateTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Trainings.Add(training);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(training);
        }

        // GET: Trainings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);

            //ユーザー権限チェック
            var loginUserId = User.Identity.GetUserId();
            if(training.ApplicationUserId != loginUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //View用モデル生成
            var trainingEditViewModel = new TrainingEditViewModel()
            {
                ID = training.ID,
                AddDateTime = training.AddDateTime.ToShortDateString(),
                Purpose = training.Purpose,
                Title = training.Title,
                UpdateDateTime = training.UpdateDateTime,
                YoutubeURL = training.YoutubeURL
            };

            //ユーザー名を取得
            string userName = "";
            var userId = training.ApplicationUserId;
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null) userName = user.UserName;
            trainingEditViewModel.UserName = userName;

            //タグから表示用タグモデルを生成
            List<ViewTagModel> ViewTagModels = new List<ViewTagModel>();
            db.Tags.ToList().ForEach(t => ViewTagModels.Add(new ViewTagModel { ID = t.ID, Name = t.Name, Checked = false }));
            foreach (var tag in training.Tags)
            {
                ViewTagModels.FirstOrDefault(t => t.ID == tag.ID).Checked = true;
            }
            trainingEditViewModel.Tags = ViewTagModels;


            if (training == null)
            {
                return HttpNotFound();
            }
            return View(trainingEditViewModel);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainingEditViewModel editTraining)
        {
            var training = db.Trainings.Find(editTraining.ID);
            if(training == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //チェックされたタグを取得
            List<Tag> checkedTags = new List<Tag>();
            var checkedTagValues = Request.Params["SearchTags"].Split(',').ToList();
            checkedTagValues.ForEach(ct => checkedTags.Add(db.Tags.Find(int.Parse(ct))));


            if (ModelState.IsValid)
            {
                training.Purpose = editTraining.Purpose;
                training.Title = editTraining.Title;
                training.YoutubeURL = editTraining.YoutubeURL;
                training.Tags = checkedTags;

                training.UpdateDateTime = DateTime.Now;
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(training);
        }

        // GET: Trainings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Training training = db.Trainings.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }


            //ユーザー権限チェック
            var loginUserId = User.Identity.GetUserId();
            if (training.ApplicationUserId != loginUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Training training = db.Trainings.Find(id);
            db.Trainings.Remove(training);
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
