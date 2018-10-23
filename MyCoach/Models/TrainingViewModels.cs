using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Configuration;

namespace MyCoach.Models
{
    public class TrainingIndexViewModel
    {


        public int ID { get; set; }

        [Display(Name = "タイトル")]
        public string Title { get; set; }

        [Display(Name = "目的")]
        public string Purpose { get; set; }

        [Display(Name = "説明")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "動画")]
        public string YoutubeURL { get; set; }
        
        public string GetYoutubeEmbedURL()
        {
            var url = this.YoutubeURL;
            var id = url.Replace(ConfigurationManager.AppSettings["YoutubeNormalURL"], "");
            return ConfigurationManager.AppSettings["YoutubeEmbedURL"].Replace("%ID", id);
        }

        public string GetSumbnailURL(string size = "")
        {
            var url = this.YoutubeURL;
            var id = url.Replace(ConfigurationManager.AppSettings["YoutubeNormalURL"], "");
            if (size == "")
            {
                return ConfigurationManager.AppSettings["YoutubeSumbnailUrl"].Replace("%ID", id);
            }
            else if (size == "mq")
            {
                return ConfigurationManager.AppSettings["YoutubeMqSumbnailUrl"].Replace("%ID", id);
            }
            else
            {
                return ConfigurationManager.AppSettings["YoutubeHqSumbnailUrl"].Replace("%ID", id);
            }
            
        }

        [Display(Name = "作成日時")]
        public string AddDateTime { get; set; }

        [Display(Name = "更新日時")]
        public DateTime UpdateDateTime { get; set; }

        [Display(Name = "登録者名")]
        public string UserName { get; set; }

        [Display(Name = "タグ")]
        public List<string> Tags { get; set; }

        [Display(Name = "所要時間")]
        [Required]
        public int TimeDuration { get; set; }

        [Display(Name = "最低人数")]
        [Required]
        public int RequiredPersonNumber { get; set; }

        [Display(Name = "推奨人数")]
        [Required]
        public int RecommendPersonNumber { get; set; }
    }

    public class TrainingCreateViewModel
    {

        [Display(Name = "タイトル")]
        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "タイトルを入力してください。")]
        public string Title { get; set; }

        [Display(Name = "目的")]
        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "目的を入力してください。")]
        public string Purpose { get; set; }

        [Display(Name = "説明")]
        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "説明を入力してください。。")]
        public string Description { get; set; }

        [Display(Name = "動画URL")]
        [Url(ErrorMessage = "正しいURLではありません。")]
        public string YoutubeURL { get; set; }

        [Display(Name = "タグ")]
        public List<Tag> Tags { get; set; }

        [Display(Name = "所要時間")]
        [Required]
        public int TimeDuration { get; set; }

        [Display(Name = "最低人数")]
        [Required]
        public int RequiredPersonNumber { get; set; }

        [Display(Name = "推奨人数")]
        [Required]
        public int RecommendPersonNumber { get; set; }
    }


    public class TrainingSearchViewModel
    {
        public List<TrainingIndexViewModel> ViewModels { get; set; }

        [Display(Name = "タグ")]
        public List<ViewTagModel> ViewTagModels { get; set; }
    }

    public class TrainingDetailsViewModel
        : TrainingIndexViewModel
    {
        public bool IsFavorite { get; set; }
    }

    public class TrainingEditViewModel
        : TrainingIndexViewModel
    {
        private new DateTime AddDateTime { get; set; }

        [Display(Name = "タグ")]
        public new List<ViewTagModel> Tags { get; set; }
    }
    public class ViewTagModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }



}