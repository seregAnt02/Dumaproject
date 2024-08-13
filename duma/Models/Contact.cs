using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace duma.Models
{
    public class Contact
    {        
        //[Required]
        //[DataType(DataType.Text)]
        public string Name { get; set; }
        //[Required]
        //[DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }     
        //[Required]
        //[DataType(DataType.EmailAddress)]   
        public string EmailAdress { get; set; }
        public string Wishes { get; set; }
        //public Price Prices { get; set; }
    }
}