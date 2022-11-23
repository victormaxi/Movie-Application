namespace Movie_Client.Models
{
    public class ApiRequestUri
    {
        public string? BaseUri { get; set; }
        public string? Register { get; set; }
        public string? Login { get; set; }
        public string? AddGenre { get; set; }
        public string? UpdateGenre { get; set; }
        public string? DeleteGenre { get; set; }
        public string? GetById { get; set; }
        public string? GetAllGenre { get; set; }

        public string? AddMovie { get; set; }
        public string? UpdateMovie { get; set; }
        public string? DeleteMovie { get; set; }
        public string? GetMovieById { get; set; }
        public string? GetAllMovie { get; set; }
    }
}
