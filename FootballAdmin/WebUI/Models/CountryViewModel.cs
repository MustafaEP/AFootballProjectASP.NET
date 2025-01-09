namespace WebUI.Models
{
    public class CountryViewModel
    {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Name_tr { get; set; }
            public Continent Continent { get; set; }
        
    }
    public class Continent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name_tr { get; set; }
    }
}
