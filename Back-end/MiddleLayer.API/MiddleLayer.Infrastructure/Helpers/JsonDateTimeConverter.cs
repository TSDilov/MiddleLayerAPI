using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiddleLayer.Infrastructure.Helpers
{
    public class JsonDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateTimeString = reader.GetString();

            // Check if the string value is empty
            if (string.IsNullOrWhiteSpace(dateTimeString))
            {
                return null; // Return null for empty DateTime strings
            }

            // Parse the string value from JSON and convert it to a DateTime object
            if (DateTime.TryParse(dateTimeString, out DateTime dateTime))
            {
                return dateTime;
            }
            else
            {
                // Handle conversion failure, such as returning a default value or throwing an exception
                throw new JsonException("Invalid DateTime format");
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            // Write DateTime value to JSON
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-ddTHH:mm:ss")); // Customize the date-time format if needed
            }
            else
            {
                writer.WriteNullValue(); // Write null for DateTime values that are null
            }
        }
    }
}
