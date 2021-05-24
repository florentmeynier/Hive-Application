using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HivePSTL.Models
{
    //if Profile 
    public class UserProfileViewModel
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        //if mobilePhone
        public string MobilePhone { get; set; }

        //Endif mobilePhone
        public string PostalAddress { get; set; }

        //if title 
        public string Title { get; set; }
        //Endif tile

        //if city
        public string City { get; set; }
        //endif city

        //if countrycode
        public string CountryCode { get; set; }

        //endif countrycode
    }
    //Endif Profile
}