using System.Text.Json.Serialization;

namespace MiddleLayer.Infrastructure.Inputs
{
    public class CharacterInput
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime Born { get; set; }

        public DateTime Died { get; set; }

        public List<string> Titles { get; set; }

        public bool IsCreated { get; set; } = true;
    }
}
