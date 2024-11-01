namespace WebUI.Areas.Admin.Models
{
    public class PlayerDataTableViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public int? TeamId { get; set; }
        public string Position { get; set; }
        public string? SecondPosition { get; set; }
        public string StrongFoot { get; set; }
    }
}
