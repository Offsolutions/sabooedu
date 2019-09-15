using sabooedu.Areas.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sabooedu.Models
{
    public class Data
    {
        dbcontext db = new dbcontext();
        public List<Slider> Sliders
        {
            get
            {
                List<Slider> set = db.Sliders.ToList();
                
                return set;
            }
        }
        public List<Feature> TopFeature
        {
            get
            {
                List<Feature> set = db.Features.Take(2).ToList();

                return set;
            }
        }
        public List<Feature> NextFeature
        {
            get
            {
                List<Feature> set = db.Features.OrderBy(x=>x.Fid).Skip(2).ToList();

                return set;
            }
        }
        public List<Coaching> Coadhing
        {
            get
            {
                List<Coaching> set = db.Coachings.Take(6).ToList();

                return set;
            }
        }
        public List<Testimonials> Testimonials
        {
            get
            {
                List<Testimonials> set = db.Testimonials.ToList();

                return set;
            }
        }
        public List<Pages> Pages
        {
            get
            {
                List<Pages> set = db.Pages.ToList();

                return set;
            }
        }
        public List<Setting> Setting
        {
            get
            {
                List<Setting> set = db.Settings.ToList();

                return set;
            }
        }
        public List<SocialMedia> SocialMedia
        {
            get
            {
                List<SocialMedia> set = db.SocialMedias.ToList();

                return set;
            }
        }
        public List<Coaching> coachinglist
        {
            get
            {
                List<Coaching> set = db.Coachings.ToList();

                return set;
            }
        }
    }
}