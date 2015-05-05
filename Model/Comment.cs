using System;

namespace Feelknit.Model
{
    public class Comment
    {
		public Guid Id{ get; set; }

		public string Text { get; set; }

		public string User { get; set; }

		public string UserAvatar { get; set; }

		public DateTime PostedAt { get; set; }

		public bool IsReported { get; set; }

		public string ReportedBy{ get; set; }

		public DateTime ReportedAt{ get; set; }

		public bool IsDeleted { get; set; }

    }
}