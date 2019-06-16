using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodinovaTask.Repository.Model
{
    public class Product
    {
        [Key]  
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }  

        public string ProductImage { get; set; }

        //public DateTime CreatedDate { get; set; }  

        //public int Createdby  { get; set; }

        //public DateTime ModifiedDate { get; set; }

        //public int ModifiedBy { get; set; } 
    }
}
