namespace Api.Dtos.Rental
{
    public class AddRentalDto
    {
        public Guid SalesmanId { get; set; }
        public Guid ClientId { get; set; }
        public Guid PieceOfEquipmentId { get; set; }
        public List<Guid> AccessoryIds { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
