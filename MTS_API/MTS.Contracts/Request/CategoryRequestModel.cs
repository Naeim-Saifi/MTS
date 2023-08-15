using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.Contracts.Request
{
    public class CategoryRequestModel
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string SeoName { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
    }
}
