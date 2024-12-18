namespace AirlineWeb.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class FlightDetailCreateDto
    {
        [Required]
        public string FlightCode { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
