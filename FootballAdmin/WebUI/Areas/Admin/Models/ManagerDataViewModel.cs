namespace WebUI.Areas.Admin.Models
{
    public class ManagerDataViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string PreferredLineUp { get; set; }
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public string TemporaryPassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
