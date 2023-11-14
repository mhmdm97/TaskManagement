using System;
using System.Collections.Generic;

namespace TaskManagementApi.DAL.EF;

public partial class Task
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public short? Status { get; set; }

    public DateTime? DueDate { get; set; }

    public int? AssignedUser { get; set; }

    public int? TaskGroup { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdated { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? AssignedUserNavigation { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual TaskGroup? TaskGroupNavigation { get; set; }
}
