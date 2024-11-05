using Entities.Concrete;

namespace WebUI.Areas.Admin.Models
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string ManagerName {  get; set; }
        public List<Player>? players { get; set; }
    }
}
