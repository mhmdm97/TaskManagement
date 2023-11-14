using System;
using System.Collections.Generic;

namespace TaskManagementApi.DAL.EF;

public partial class User
{
    public int Id { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<Task> TaskAssignedUserNavigations { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskCreatedByNavigations { get; set; } = new List<Task>();

    public virtual ICollection<TaskGroup> TaskGroups { get; set; } = new List<TaskGroup>();
}
