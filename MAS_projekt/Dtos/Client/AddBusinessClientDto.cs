namespace Api.Dtos.Client
{
    public class AddBusinessClientDto
    {
        public Guid LocationId { get; set; }
        public string Nip { get; set; }
        public double Discount { get; set; }
    }
}
