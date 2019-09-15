using sabooedu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sabooedu.Areas.Auth.Models
{
    public class Bal
    {
    }

    public class Slider
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
    }
    public class Feature
    {[Key]
        public int Fid { get; set; }
        [Display(Name = "Feature Name")]
        public string Name { get; set; }
        public string Icon { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    }

    public class Coaching
    {
        [Key]
        public int Cid { get; set; }
        [Display(Name = "Coaching Name")]
        public string Name { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }

        public virtual ICollection<VideoLecture> VideoLectures { get; set; }
        public virtual ICollection<DownloadNotes> DownloadNotes { get; set; }
    }
    public class Testimonials
    {[Key]
        public int Tid { get; set; }
        [AllowHtml]
        public string Message { get; set; }
        [Display(Name = "Customer Name")]
        [Required]
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Image { get; set; }
    }
    public class Pages
    {[Key]
        public int Pid { get; set; }
        public string PageName { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
    }
    public class Account
    {[Key]
   
        public int Aid { get; set; }
        [Required]
        public string Name { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
    public class Setting
    {
        [Key]

        public int Sid { get; set; }
        [Display(Name ="Contact Person")]
        public string ContactPerson { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string BusinessContact { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Whatsapp { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string BusinessEmai { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Logo { get; set; }
    }
    public class SocialMedia
    {
        public int id { get; set; }
        [Display(Name ="Platform Name")]
        public string Platform { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
    }
    public class BlogCategory
    {
        [Key]
        public int Bid { get; set; }
        [Display(Name ="Category Name")]
        public string Name { get; set; }
        public virtual ICollection<TheBlog> Blogs { get; set; }
    }
    public class TheBlog
    {
        [Key]
        public int lid { get; set; }
        public int Bid { get; set; }
        public virtual BlogCategory BlogCategories { get; set; }

        public string Title { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string Description { get; set; }

        public string Thumbnail { get; set; }
        public string Image { get; set; }

    }
    public class VideoLecture
    {
        [Key]
        public int Vid { get; set; }
        [Display(Name ="Classes")]
        public int Cid { get; set; }
        public virtual Coaching Coachings { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        public string Image { get; set; }
    }
    public class DownloadNotes
    {
        [Key]
        public int Did { get; set; }
        [Display(Name = "Classes")]
        public int Cid { get; set; }
        public virtual Coaching Coachings { get; set; }
       
        public string Notes { get; set; }
    }

    public class FullCourse
    {
        [Key]
        public int Cid { get; set; }
        [Display(Name ="Course Name")]
        [Required]
        public string Name { get; set; }
        public string Cover { get; set; }
        public virtual ICollection<AllLesson> AllLessons  { get; set; }
        public virtual ICollection<AllModule> AllModules { get; set; }
        public virtual ICollection<Assign> Assigns { get; set; }
    }
    public class AllLesson
    {
        [Key]
        public int Chid { get; set; }
        [Display(Name ="Course")]
        public int Cid { get; set; }
        public virtual FullCourse FullCourses { get; set; }
        [Display(Name ="Chapter Name")]
        public string Name { get; set; }
        
    }
    public class AllModule
    {
        [Key]
        public int Mid { get; set; }
        [Display(Name ="Course")]
        public int Cid { get; set; }
     
        public int Chid { get; set; }

        [Display(Name ="Module Name")]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name ="Video Url")]
        public string Video { get; set; }

        public string File { get; set; }
        public string Type { get; set; }

    }
    public class Assign
    {
        [Key]
        public int Aid { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Rid { get; set; }
        public virtual Registration Registrations { get; set; }
        public int Cid { get; set; }
        public virtual FullCourse FullCourses { get; set; }
    }
    public class HelpDesk
    {[Key]
        public int Hid { get; set; }
        public int Rid { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Title { get; set; }

        public string Question { get; set; }
        public int QuestionId { get; set; }

    }
    public class HelpReply
    {[Key]
        public int Hrid { get; set; }
        public int QuestionId { get; set; }
        [DataType(DataType.Date)]
        public DateTime AdminDate { get; set; }
        public string UserReply { get; set; }
        [DataType(DataType.Date)]
        public string UserDate { get; set; }


    }
    public enum Type
    {
        Free,Paid
    }
}