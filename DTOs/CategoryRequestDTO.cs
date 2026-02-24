namespace FinTrackAPI.DTOs
{
    public class CategoryRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // "ingreso" o "gasto"
        public string Color { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}