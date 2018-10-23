
using System;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCoach.Models
{


    public class Training
    {
        public int ID { get; set; }

        [Display(Name = "タイトル")]
        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "タイトルを入力してください。")]
        public string Title { get; set; }

        [Display(Name = "目的")]
        [Required]
        [StringLength(60, MinimumLength =1, ErrorMessage ="目的を入力してください。")]
        public string Purpose { get; set; }

        [Display(Name = "説明")]
        [Required]
        public string Description { get; set; }

        [Display(Name ="所要時間")]
        [Required]
        public int TimeDuration { get; set; }

        [Display(Name = "最低人数")]
        [Required]
        public int RequiredPersonNumber { get; set; }

        [Display(Name = "推奨人数")]
        [Required]
        public int RecommendPersonNumber { get; set; }



        [Display(Name = "Youtubeのリンク")]
        [Url(ErrorMessage = "正しいURLではありません。")]
        public string YoutubeURL { get; set; }

        [Display(Name = "作成日時")]
        public DateTime AddDateTime { get; set; }

        [Display(Name = "更新日時")]
        public DateTime UpdateDateTime { get; set; }

        [Display(Name = "登録者ID")]
        public virtual string ApplicationUserId { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Procedure> Procedures { get; set; }

    }

    
    public class Tag
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }

    public class Procedure
    {
        public int ID { get; set; }

        [Display(Name = "手順説明")]
        public string Description { get; set; }

        [Display(Name = "画像")]
        public string ImagePath { get; set; }


    }

}