using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace sabooedu.Models
{
    public class dbcontext:DbContext
    {
        public dbcontext() : base("dbcontext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<dbcontext, sabooedu.Migrations.Configuration>("dbcontext"));
        }
        public System.Data.Entity.DbSet<sabooedu.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Account> Accounts { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Slider> Sliders { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Feature> Features { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Coaching> Coachings { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Testimonials> Testimonials { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Pages> Pages { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Setting> Settings { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.SocialMedia> SocialMedias { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.BlogCategory> BlogCategories { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.TheBlog> TheBlogs { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.VideoLecture> VideoLectures { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.DownloadNotes> DownloadNotes { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.FullCourse> FullCourses { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.AllLesson> AllLessons { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.AllModule> AllModules { get; set; }

        public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Assign> Assigns { get; set; }


        //public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Chapter> Chapters { get; set; }

        //public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Module> Modules { get; set; }

        //public System.Data.Entity.DbSet<sabooedu.Areas.Auth.Models.Blog> Blogs { get; set; }
    }
}