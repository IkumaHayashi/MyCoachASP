using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyCoach.Models
{
    public class Favorite
    {
        public int ID { get; set; }

        public int TrainingID { get; set; } 

        public virtual Training FavoriteTraining {get;set;}

        [Display(Name = "ユーザーID")]
        public virtual string ApplicationUserId { get; set; }

        [Display(Name = "追加日時")]
        public DateTime AddDateTime { get; set; }
    }
}