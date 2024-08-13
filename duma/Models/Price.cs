using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace duma.Models
{
    public class Price
    {
        
        //[Range(1, 1000, ErrorMessage = "Пожалуйста введите от 1 до 1000")]
        //[Display(Name = "Площадь")]
        public int Area { get; set; }
        public string Lighting { get; set; }

        
        //[Range(1, 10, ErrorMessage = "Пожалуйста введите от 1 до 10")]
        public int Rooms { get; set; }
        public string Heatings { get; set; }
        public string Fan { get; set; }
        public string Conditioner { get; set; }
        public string Fireman { get; set; }
        public string Movements { get; set; }
        public string Management { get; set; }
        public string Protection { get; set; }
        public string Electrician { get; set; }
    }
}