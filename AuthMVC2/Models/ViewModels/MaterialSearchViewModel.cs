namespace AuthMVC2.Models.ViewModels
{
    public class MaterialSearchViewModel
    {
        public List<Material>? Materials { get; set; }
        public List<MaterialType>? MaterialTypes { get; set; }
        public bool SearchResult { get; set; }
        public List<string>? MaterialList { get; set; }
    }

}
