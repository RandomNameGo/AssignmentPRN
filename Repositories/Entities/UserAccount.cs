using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class UserAccount
{
    public int MemberId { get; set; }

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public int? Role { get; set; }
}
