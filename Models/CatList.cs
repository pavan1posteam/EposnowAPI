using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EposNow.Models
{
   public class CatList
    {
   
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int? RootParentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int? PopupNoteId { get; set; }

        public bool IsWet { get; set; }

        public bool ShowOnTill { get; set; }

        public string ReferenceCode { get; set; }

        public object PopupNote { get; set; }

        public List<CatList> Children { get; set; }  //  THIS IS THE IMPORTANT PART

        public int? SortPosition { get; set; }

        public int? ReportingCategoryId { get; set; }

        public string NominalCode { get; set; }

        public int? PrinterTypeId { get; set; }

        public int? CourseId { get; set; }

        public int? ButtonColourId { get; set; }
    }
}
