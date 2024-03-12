namespace backend.Models
{
    public class CalculationHistory
    {
        public Guid Id { get; set; }
        public string Expression { get; set; }
        public decimal Result { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
