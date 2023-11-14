using System;
using System.Collections.Generic;

namespace TaskManagementApi.DAL.EF;

public partial class TaskGroup
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? Status { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
