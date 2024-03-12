namespace backend.Models
{
    public class CustomCalculation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
