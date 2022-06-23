using System.ComponentModel.DataAnnotations;

namespace Portal.Application.Movies.Commands.AddFilm
{
    public class AddFilmDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Tags { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime Runtime { get; set; }
        
        public int Score { get; set; }
        public string IMDBLink { get; set; }
        public string HomepageLink { get; set; }
    }
}