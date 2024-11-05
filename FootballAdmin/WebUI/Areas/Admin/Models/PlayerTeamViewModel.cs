namespace WebUI.Areas.Admin.Models
{
    public class PlayerTeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string County { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
