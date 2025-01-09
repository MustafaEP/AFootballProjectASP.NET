using Business.Concrete.NewVersion;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WebUI.Models;

namespace WebUI.Areas.Manager.ViewComponents
{
	public class CountrySelectViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{

			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "countries.json");

			string countriesJsonString = System.IO.File.ReadAllText(filePath);

			var countries = JsonConvert.DeserializeObject<Dictionary<string, CountryViewModel>>(countriesJsonString);

			return View(countries);
		}
	}
}
