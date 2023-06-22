namespace Api.Dtos.Transport
{
    public class TransportInfoDto
    {
        public Guid PieceOfEquipmentId { get; set; }
        public Guid ClientId { get; set; }
        public Guid FromLocationId { get; set; }
        public Guid ToLocationId { get; set; }
    }
}
