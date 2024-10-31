namespace WebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int PlayerCount { get; set; }
        public int TeamCount { get; set; }
        public int ManagerCount { get; set; }
        public DateTime MatchDate { get; set; }
        public string Match {  get; set; }
    }
}
