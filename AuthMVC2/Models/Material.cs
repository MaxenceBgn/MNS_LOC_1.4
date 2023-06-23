namespace AuthMVC2.Models
{
    public class Material
    {
        public int MaterialId { get; set; }
        public int TypeId { get; set; }
        public string? ColorMaterial { get; set; }
        public string? DefaultMaterial { get; set; }
        public bool IsFunctional { get; set; }
        public string? PurchaseDate { get; set; }
        public string? SerialNumber { get; set; }
        public bool HaveAnAlimentation { get; set; }
        public string? MaterialDocumentation { get; set; }
    }

}
