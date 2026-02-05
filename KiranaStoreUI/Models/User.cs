namespace KiranaStoreUI.Models
{
    public class User
    {
        public int? UserId { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }      // Admin, Employee, Cashier
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
}
