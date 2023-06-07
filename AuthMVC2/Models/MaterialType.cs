using MessagePack;

namespace AuthMVC2.Models
{
    public class MaterialType
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
    }
}
