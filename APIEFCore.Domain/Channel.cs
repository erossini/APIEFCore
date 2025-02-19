using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIEFCore.Domain
{
    [Table("tbl_Channels")]
    public class Channel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [Required] [JsonPropertyName("name")] public string? Name { get; set; }

        [JsonIgnore] public ICollection<Client>? Client { get; set; }
    }
}