namespace Portal.Domain.Movies;

public class Film :BaseEntity
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string Tags { get; set; }
    public string ImageUrl { get; set; }
    public decimal Budget { get; set; }
    public decimal Revenue { get; set; }
    public DateTime ReleaseDate { get; set; }
    public DateTime Runtime { get; set; }
    public int Score { get; set; }
    public string IMDBLink { get; set; }
    public string HomepageLink { get; set; }

}
