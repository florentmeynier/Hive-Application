using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HivePSTL.Models
{
    //if[UserProfile] 
    public class UserProfileViewModel
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        //if[mobilePhone]
        public string MobilePhone { get; set; }
        //endif[mobilePhone]

        //if[PostalAddress]
        public string PostalAddress { get; set; }
        //endif[PostalAddress]

        //if[title]
        public string Title { get; set; }
        //endif[title]

        //if[city]
        public string City { get; set; }
        //endif[city]

        //if[countrycode]
        public string CountryCode { get; set; }
        //endif[countrycode]
    }
    //endif[UserProfile]
}