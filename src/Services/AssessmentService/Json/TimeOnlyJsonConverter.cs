using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aspirra.AssessmentService.Json
{
    public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (TimeOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
                {
                    return time;
                }
            }

            throw new JsonException("Invalid time format.");
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
        }
    }
}
