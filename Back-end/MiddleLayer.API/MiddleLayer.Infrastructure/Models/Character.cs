﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using MiddleLayer.Infrastructure.Helpers;

namespace MiddleLayer.Infrastructure.Models
{
    public class Character
    {
        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("culture")]
        public string Culture { get; set; }

        [JsonPropertyName("born")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime? Born { get; set; }

        [JsonPropertyName("died")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime? Died { get; set; }

        [JsonPropertyName("titles")]
        public List<string> Titles { get; set; }

        [JsonPropertyName("aliases")]
        public List<string> Aliases { get; set; }

        [JsonPropertyName("father")]
        public string Father { get; set; }

        [JsonPropertyName("mother")]
        public string Mother { get; set; }

        [JsonPropertyName("spouse")]
        public string Spouse { get; set; }

        [JsonPropertyName("allegiances")]
        public List<string> Allegiances { get; set; }

        [JsonPropertyName("books")]
        public List<string> Books { get; set; }

        [JsonPropertyName("povBooks")]
        public List<string> PovBooks { get; set; }

        [JsonPropertyName("tvSeries")]
        public List<string> TvSeries { get; set; }

        [JsonPropertyName("playedBy")]
        public List<string> PlayedBy { get; set; }

        [JsonIgnore]
        public bool IsCreated { get; set; } = false;
    }
}
