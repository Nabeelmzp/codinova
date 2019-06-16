using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodinovaTask.Repository.Model
{
    public class SaveOrder
    {
        public int OrderBy { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string ProductImage { get; set; }


    }


}
