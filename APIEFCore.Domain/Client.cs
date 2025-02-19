using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIEFCore.Domain
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("firstname")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastname")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastName { get; set; }

        [JsonPropertyName("channels")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Channel>? Channels { get; set; }
    }
}