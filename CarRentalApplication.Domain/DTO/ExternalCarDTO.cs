namespace CarRentalApplication.Domain.DTO
{
    
    public class ExternalCarDTO
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public double PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
}
