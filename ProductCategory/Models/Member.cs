using System;
using System.Collections.Generic;

namespace ProductCategory.Models;

public partial class Member
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? NickName { get; set; }

    public string? AvatarPath { get; set; }
}
