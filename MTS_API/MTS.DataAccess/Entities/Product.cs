using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.DataAccess.Entities
{
    public class Product
    {
        public int id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Desription { get; set; }
        public string Brand { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? Expiry { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
    }
}
