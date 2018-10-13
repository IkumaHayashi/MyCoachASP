using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyCoach.Models
{
    public class TrainingIndexViewModel
    {
        public int ID { get; set; }

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
        public string Description { get; set; }

        [Display(Name = "Youtubeのリンク")]
        public string YoutubeURL { get; set; }

        [Display(Name = "作成日時")]
        public DateTime AddDateTime { get; set; }

        [Display(Name = "更新日時")]
        public DateTime UpdateDateTime { get; set; }

        [Display(Name = "登録者名")]
        public string UserName { get; set; }

        [Display(Name = "タグ")]
        public List<string> Tags { get; set; }
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
        public string Description { get; set; }

        [Display(Name = "Youtubeのリンク")]
        [Url(ErrorMessage = "正しいURLではありません。")]
        public string YoutubeURL { get; set; }

        [Display(Name = "タグ")]
        public List<Tag> Tags { get; set; }
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

        [Display(Name = "タグ")]
        public new List<ViewTagModel> Tags { get; set; }
    }

    public class TrainingEditViewModel
        : TrainingIndexViewModel
    {

        [Display(Name = "タグ")]
        public new List<ViewTagModel> Tags { get; set; }
    }
    public class ViewTagModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }



}