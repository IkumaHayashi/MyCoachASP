﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace MyCoach.Models
{
    // ApplicationUser クラスにさらにプロパティを追加すると、ユーザーのプロファイル データを追加できます。詳細については、https://go.microsoft.com/fwlink/?LinkID=317594 を参照してください。
    public class ApplicationUser : IdentityUser
    {
        public static ApplicationUser GetUserFromMail(string mail)
        {
            using (var db = new MyCoachDatabaseContext())
            {
                var getUserTask = db.Users.FirstOrDefaultAsync(u => u.Email == mail);
                getUserTask.Wait();
                var applicationUser = getUserTask.Result;
                if(applicationUser != null)
                {
                    return applicationUser;
                }
                else
                {
                    return null;
                }
            }
        }

        [Required]
        [StringLength(100)]
        [Display(Name = "ユーザー名")]
        public string Name { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // authenticationType が CookieAuthenticationOptions.AuthenticationType で定義されているものと一致している必要があります
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // ここにカスタム ユーザー クレームを追加します
            return userIdentity;
        }

    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        //: base("DefaultConnection", throwIfV1Schema: false)
    //        : base("MyCoachDatabaseContext", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);
    //    }
    //}
}