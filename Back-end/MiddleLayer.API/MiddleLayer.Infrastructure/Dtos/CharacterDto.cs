using MiddleLayer.Infrastructure.Inputs;

namespace MiddleLayer.Infrastructure.Dtos
{
    public class CharacterDto 
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime? Born { get; set; }

        public DateTime? Died { get; set; }

        public List<string> Titles { get; set; }
    }
}
