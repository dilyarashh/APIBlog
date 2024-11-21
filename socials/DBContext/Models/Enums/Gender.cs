namespace socials.DBContext.Models.Enums;

using System.Text.Json.Serialization;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male,
    Female
}