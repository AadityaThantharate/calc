namespace backend.DTO
{
    public class CalculationHistoryDto
    {
        public Guid Id { get; set; }
        public string Expression { get; set; }
        public decimal Result { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
