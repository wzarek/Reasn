using System;
using System.Collections.Generic;

namespace ReasnAPI.Models.Database;

public partial class Address
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string State { get; set; } = null!;

    public string? ZipCode { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
