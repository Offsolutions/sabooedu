using sabooedu.Areas.Auth.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sabooedu.Models
{
    public class Registration
    {
        [Key]
        public int Rid { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public string EmailCode { get; set; }
        public Boolean EmailVerify { get; set; }
        public virtual ICollection<Assign> Assigns { get; set; }


    }
}