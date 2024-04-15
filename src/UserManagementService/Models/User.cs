namespace UserManagementService.Models
{
    public class User
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public DateTime birthdate { get; set; }
        public bool active { get; set; }
    }
}