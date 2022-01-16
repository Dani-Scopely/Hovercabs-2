namespace Hovercabs.Models
{
    public class Track
    {
        public string Id { get; set; }
        public bool IsStartTrack { get; set; }

        public Track(bool isStartTrack = false)
        {
            IsStartTrack = isStartTrack;
        }

        public Track(string id, bool isStartTrack = false)
        {
            Id = id;
            IsStartTrack = isStartTrack;
        }
    }
}