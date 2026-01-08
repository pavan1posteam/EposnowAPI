using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EposNow
{
    class EposnowCatList
    {
		public class Root
		{
			public int Id { get; set; }

			public object ParentId { get; set; }

			public object RootParentId { get; set; }

			public string Name { get; set; }

			public string Description { get; set; }

			public object ImageUrl { get; set; }

			public object PopupNoteId { get; set; }

			public bool IsWet { get; set; }

			public bool ShowOnTill { get; set; }

			public object ReferenceCode { get; set; }

			public object PopupNote { get; set; }

			public List<object> Children { get; set; }

			public object SortPosition { get; set; }

			public object ReportingCategoryId { get; set; }

			public string NominalCode { get; set; }
		}
	}
}
