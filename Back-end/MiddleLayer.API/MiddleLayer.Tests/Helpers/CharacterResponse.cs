using MiddleLayer.Infrastructure.Models;
using System.Text.Json.Serialization;

namespace MiddleLayer.Tests.Helpers
{
    public class CharacterResponse
    {
        [JsonPropertyName("Result")]
        public Character Result { get; set; }
    }
}
