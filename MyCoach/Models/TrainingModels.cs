
using System;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCoach.Models
{

    public class TrainingModels : DbContext
    {
        // コンテキストは、アプリケーションの構成ファイル (App.config または Web.config) から 'Trainings' 
        // 接続文字列を使用するように構成されています。既定では、この接続文字列は LocalDb インスタンス上
        // の 'MyCoach.Models.Trainings' データベースを対象としています。 
        // 
        // 別のデータベースとデータベース プロバイダーまたはそのいずれかを対象とする場合は、
        // アプリケーション構成ファイルで 'Trainings' 接続文字列を変更してください。
        public TrainingModels()
            : base("name=TrainingModels")
        {
        }

        // モデルに含めるエンティティ型ごとに DbSet を追加します。Code First モデルの構成および使用の
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=390109 を参照してください。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    }

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


    }

    
    public class Tag
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}