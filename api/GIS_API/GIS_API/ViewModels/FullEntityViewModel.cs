namespace GIS_API.ViewModels
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class FullEntityViewModel
    {
        [JsonPropertyName("layer")]
        public string Layer { get; set; }

        [JsonPropertyName("geom_id")]
        public int GeomId { get; set; }

        public Dictionary<string, string> Attributes { get; set; }
    }
}
