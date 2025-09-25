namespace OnlineStore.Models;

public class Address : BaseEntity
{
    
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public bool IsDefault { get; set; }  // default value of boolean is false 
    public int UserId { get; set; } // default value of int is 0
    public User User { get; set; } = null!; // belongs to one user 

    public ICollection<Order> Orders = new List<Order>();

}
