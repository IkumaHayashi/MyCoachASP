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
    public class TrainingsController : Controller
    {
        private TrainingModels db = new TrainingModels();

        // GET: Trainings
        public ActionResult Index()
        {
            var trainings = db.Trainings.ToList();
            var trainingIndexViewModels = new List<TrainingIndexViewModel>();
            for (int i = 0; i < trainings.Count; i++)
            {
                //var viewTrainings =
                //    from t in trainings
                //    select new { t.Purpose, t.Title, t.UpdateDateTime, t.AddDateTime, t.Description, t.YoutubeURL };
                //test
                string userName = "";
                using (var appUserContext = new ApplicationDbContext())
                {
                    var userId = trainings[i].ApplicationUserId;
                    var user = appUserContext.Users.FirstOrDefault(u => u.Id == userId);
                    if (user != null) userName = user.UserName;
                }

                var viewTraining = new TrainingIndexViewModel
                {
                    AddDateTime = trainings[i].AddDateTime,
                    Description = trainings[i].Description,
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

        // GET: Trainings/Search
        public ActionResult Search(string searchValue, IList<bool> SearchTag)
        {


            //パラメータからタグの絞り込み状況を取得
            var checkedTagKeys = Request.Params.AllKeys.Where(x => x.Contains("SearchTag_")).ToList();
            List<ViewTagModel> ViewTagModels = new List<ViewTagModel>();

            //パラメータにタグの指定がない場合、DBからタグ情報を取得
            if(checkedTagKeys.Count == 0)
            {
                db.Tags.ToList().ForEach(t => ViewTagModels.Add(new ViewTagModel { ID = t.ID, Name = t.Name, IsChecked = false }));
            }
            //パラメータにタグの指定がある場合、選択状況からタグ情報を取得
            else
            {

                foreach (var tagKey in checkedTagKeys)
                {
                    bool IsChecked = false;
                    var IsCheckedValues = Request.Params[tagKey].Split(',').ToList<string>();
                    foreach (var value in IsCheckedValues)
                    {
                        IsChecked = IsChecked || bool.Parse(value);
                    }

                    var viewTagModel = new ViewTagModel
                    {
                        ID = int.Parse(tagKey.Replace("SearchTag_", "")),
                        IsChecked = IsChecked
                    };
                    viewTagModel.Name = db.Tags.First(tag => tag.ID == viewTagModel.ID).Name;

                    ViewTagModels.Add(viewTagModel);
                }
            }

            //検索文字列にヒットするトレーニングを取得
            List<Training> trainings = new List<Training>();
            var trainingQuery = db.Trainings.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                trainingQuery = trainingQuery.Where(t => t.Title.Contains(searchValue));
                trainingQuery = trainingQuery.Where(t => t.Purpose.Contains(searchValue));
                trainingQuery = trainingQuery.Where(t => t.Description.Contains(searchValue));
                
            }
            trainingQuery.ToList().ForEach(x => trainings.Add(x));

            //タグにヒットするトレーニングを取得
            trainingQuery = db.Trainings.AsQueryable();
            foreach(var tag in ViewTagModels)
            {
                if (!tag.IsChecked) continue;

                foreach(var training in db.Trainings)
                {
                    if (training.Tags.Select(x => x.Name).Contains(tag.Name))
                    {

                        trainingQuery = trainingQuery.Where( t => t.ID == training.ID);

                        trainingQuery.ToList().ForEach(x => trainings.Add(x));
                    }
                    
                }

                //trainings = trainings.Where(
                //    t => t.Tags.Select(x => x.Name).Contains(tag.Name)
                //    );
            }

            if (string.IsNullOrEmpty(searchValue) && checkedTagKeys.Count == 0)
                trainings = db.Trainings.ToList();

            trainings = trainings.Distinct().ToList();

            //検索結果に表示するViewデータを生成
            var trainingIndexViewModels = new List<TrainingIndexViewModel>();
            for (int i = 0; i < trainings.Count; i++)
            {
                //ユーザー名を取得
                string userName = "";
                using (var appUserContext = new ApplicationDbContext())
                {
                    var userId = trainings[i].ApplicationUserId;
                    var user = appUserContext.Users.FirstOrDefault(u => u.Id == userId);
                    if (user != null) userName = user.UserName;
                }

                //オブジェクトに設定
                var viewTraining = new TrainingIndexViewModel
                {
                    ID = trainings[i].ID,
                    AddDateTime = trainings[i].AddDateTime,
                    Description = trainings[i].Description,
                    Purpose = trainings[i].Purpose,
                    Title = trainings[i].Title,
                    UpdateDateTime = trainings[i].UpdateDateTime,
                    YoutubeURL = trainings[i].YoutubeURL,
                    Tags = trainings[i].Tags.Select(x => x.Name).ToList(),
                    UserName = userName
                };
                trainingIndexViewModels.Add(viewTraining);

            }



            //View用オブジェクトに格納
            var trainingSearchViewModel = new TrainingSearchViewModel();
            trainingSearchViewModel.ViewModels = trainingIndexViewModels;
            trainingSearchViewModel.ViewTagModels = ViewTagModels;



            return View(trainingSearchViewModel);
        }

        // GET: Trainings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);

            //View用モデル生成
            var trainingDetailsViewModel = new TrainingDetailsViewModel()
            {
                ID = training.ID,
                AddDateTime = training.AddDateTime,
                Description = training.Description,
                Purpose = training.Purpose,
                Title = training.Title,
                UpdateDateTime = training.UpdateDateTime,
                YoutubeURL = training.YoutubeURL
            };

            //ユーザー名を取得
            string userName = "";
            using (var appUserContext = new ApplicationDbContext())
            {
                var userId = training.ApplicationUserId;
                var user = appUserContext.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null) userName = user.UserName;
            }
            trainingDetailsViewModel.UserName = userName;

            //タグから表示用タグモデルを生成
            List<ViewTagModel> ViewTagModels = new List<ViewTagModel>();
            db.Tags.ToList().ForEach(t => ViewTagModels.Add(new ViewTagModel { ID = t.ID, Name = t.Name, IsChecked = false }));
            foreach(var tag in training.Tags)
            {
                ViewTagModels.FirstOrDefault(t => t.ID == tag.ID).IsChecked = true;
            }
            trainingDetailsViewModel.Tags = ViewTagModels;


            if (trainingDetailsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(trainingDetailsViewModel);
        }

        // GET: Trainings/Create
        [LoginAuthentication]
        public ActionResult Create()
        {
            var trainingCreateViewModel = new TrainingCreateViewModel();
            trainingCreateViewModel.Tags = db.Tags.ToList();

            //var tags = db.Tags.ToList();
            //foreach(var tag in tags)
            //{
            //    var viewTag = new Tag { ID = tag.ID, Name = tag.Name, isChecked = false };
            //    trainingCreateViewModel.ViewTags.Add(viewTag);
            //}
            return View(trainingCreateViewModel);
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [LoginAuthentication]
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


            using (var appUserContext = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();
                training.ApplicationUserId = userId;
                //training.User.
            }

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
        [LoginAuthentication]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Trainings.Find(id);

            //View用モデル生成
            var trainingEditViewModel = new TrainingEditViewModel()
            {
                ID = training.ID,
                AddDateTime = training.AddDateTime,
                Description = training.Description,
                Purpose = training.Purpose,
                Title = training.Title,
                UpdateDateTime = training.UpdateDateTime,
                YoutubeURL = training.YoutubeURL
            };

            //ユーザー名を取得
            string userName = "";
            using (var appUserContext = new ApplicationDbContext())
            {
                var userId = training.ApplicationUserId;
                var user = appUserContext.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null) userName = user.UserName;
            }
            trainingEditViewModel.UserName = userName;

            //タグから表示用タグモデルを生成
            List<ViewTagModel> ViewTagModels = new List<ViewTagModel>();
            db.Tags.ToList().ForEach(t => ViewTagModels.Add(new ViewTagModel { ID = t.ID, Name = t.Name, IsChecked = false }));
            foreach (var tag in training.Tags)
            {
                ViewTagModels.FirstOrDefault(t => t.ID == tag.ID).IsChecked = true;
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
        [LoginAuthentication]
        public ActionResult Edit([Bind(Include = "ID,Title,Purpose,Description,YoutubeURL")] Training training)
        {
            if (ModelState.IsValid)
            {
                training.UpdateDateTime = DateTime.Now;
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(training);
        }

        // GET: Trainings/Delete/5
        [LoginAuthentication]
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
            return View(training);
        }

        // POST: Trainings/Delete/5
        [LoginAuthentication]
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
