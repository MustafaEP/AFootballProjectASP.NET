namespace WebUI.Areas.Admin.Models
{
    public class PlayerUpdateViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public int Team { get; set; }
        public string Position { get; set; }
        public string SecondPosition { get; set; }
        public string StrongFoot { get; set; }
        public string? TemporaryPassword {  get; set; }
    }

}
