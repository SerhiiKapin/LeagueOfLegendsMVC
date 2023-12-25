using System.ComponentModel.DataAnnotations;

namespace LeagueOfLegendsMVC.Models
{
    public class ChampionModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Blue Essence must be a non-negative number")]
        public int BlueEssence { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Riot Points must be a non-negative number")]
        public int RiotPoints { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string Image { get; set; }
    }
}
