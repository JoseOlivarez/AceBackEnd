using System;
using System.Collections.Generic;

namespace AceBackEnd.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Addressone { get; set; }

    public string? Addresstwo { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Zipcode { get; set; }
}
