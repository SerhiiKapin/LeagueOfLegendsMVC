using LeagueOfLegendsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeagueOfLegendsMVC.Controllers
{
    public class HomeController : Controller
    {
        private static List<ChampionModel> champions;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            if (champions == null)
            {
                champions = new List<ChampionModel> {
                new () { Name = "Irelia", Role = "Top", BlueEssence = 4800, RiotPoints = 880, Image = "https://raw.communitydragon.org/latest/game/assets/characters/irelia/hud/irelia_square_0.png" },
                new() { Name = "Jinx", Role = "ADC", BlueEssence = 6300, RiotPoints = 975, Image = "https://raw.communitydragon.org/latest/game/assets/characters/jinx/hud/jinx_square.png" },
                new() { Name = "Zed", Role = "Mid", BlueEssence = 4800, RiotPoints = 880, Image = "https://raw.communitydragon.org/latest/game/assets/characters/zed/hud/zed_square_0.png" },
                new() { Name = "Lee Sin", Role = "Jungle", BlueEssence = 4800, RiotPoints = 880, Image = "https://raw.communitydragon.org/latest/game/assets/characters/leesin/hud/leesin_square.png" },
                new() { Name = "Leona", Role = "Support", BlueEssence = 4800, RiotPoints = 880, Image = "https://raw.communitydragon.org/latest/game/assets/characters/leona/hud/leona_square.png" },
                new() { Name = "Senna", Role = "ADC", BlueEssence = 6300, RiotPoints = 975, Image = "https://raw.communitydragon.org/latest/game/assets/characters/senna/hud/senna_square.png" },
                new() { Name = "Yone", Role = "Mid", BlueEssence = 6300, RiotPoints = 975, Image = "https://raw.communitydragon.org/latest/game/assets/characters/yone/hud/yone_square.png" },
                new() { Name = "Yasuo", Role = "Mid", BlueEssence = 4800, RiotPoints = 880, Image = "https://raw.communitydragon.org/latest/game/assets/characters/yasuo/hud/yasuo_square.png" },
                new() { Name = "Riven", Role = "Top", BlueEssence = 4800, RiotPoints = 880, Image = "https://raw.communitydragon.org/latest/game/assets/characters/riven/hud/riven_square.png" },
                new() { Name = "Vayne", Role = "ADC", BlueEssence = 4800, RiotPoints = 880, Image = "https://raw.communitydragon.org/latest/game/assets/characters/vayne/hud/vayne_square.png" },
                new() { Name = "Katarina", Role = "Mid", BlueEssence = 3150, RiotPoints = 790, Image = "https://raw.communitydragon.org/latest/game/assets/characters/katarina/hud/katarina_square.png" },
                new() { Name = "Akali", Role = "Mid", BlueEssence = 3150, RiotPoints = 790, Image = "https://raw.communitydragon.org/latest/game/assets/characters/akali/hud/akali_square.png" },
                };
            }
            _logger = logger;
        }

        public IActionResult Index(int page = 1, int pageSize = 5, string search = "")
        {
            var filteredChampions = string.IsNullOrEmpty(search) ?
                champions :
                champions.Where(c => c.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase) || c.Role.Contains(search, System.StringComparison.OrdinalIgnoreCase)).ToList();

            var paginatedChampions = filteredChampions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.PageNumber = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalChampions = filteredChampions.Count;
            ViewBag.SearchTerm = search;

            return View(paginatedChampions);
        }

        public IActionResult EditChampions()
        {
            return View(champions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Edit(int id)
        {
            var champion = champions.FirstOrDefault(c => c.Id == id);
            if (champion == null)
            {
                return NotFound();
            }

            return View(champion);
        }

        [HttpPost]
        public IActionResult Edit(ChampionModel updatedChampion)
        {
            var existingChampion = champions.FirstOrDefault(c => c.Id == updatedChampion.Id);
            if (existingChampion == null)
            {
                return NotFound();
            }

            existingChampion.Name = updatedChampion.Name;
            existingChampion.Role = updatedChampion.Role;
            existingChampion.BlueEssence = updatedChampion.BlueEssence;
            existingChampion.RiotPoints = updatedChampion.RiotPoints;
            existingChampion.Image = updatedChampion.Image;

            return RedirectToAction("EditChampions");
        }

        public IActionResult Delete(int id)
        {
            var champion = champions.FirstOrDefault(c => c.Id == id);
            if (champion == null)
            {
                return NotFound();
            }

            return View(champion);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var champion = champions.FirstOrDefault(c => c.Id == id);
            if (champion == null)
            {
                return NotFound();
            }

            champions.Remove(champion);

            return RedirectToAction("EditChampions");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ChampionModel newChampion)
        {
            if (!ModelState.IsValid)
            {
                return View(newChampion);
            }

            newChampion.Id = champions.Max(c => c.Id) + 1;

            champions.Add(newChampion);

            return RedirectToAction("EditChampions");
        }

    }
}