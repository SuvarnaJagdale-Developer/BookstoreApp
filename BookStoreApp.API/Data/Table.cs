using System;
using System.Collections.Generic;

namespace BookStoreApp.API.Data;

public partial class Table
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Bio { get; set; }
}
