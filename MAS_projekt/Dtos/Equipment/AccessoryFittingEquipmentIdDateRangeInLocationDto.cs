namespace Api.Dtos.Equipment
{
    public class AccessoryFittingEquipmentIdDateRangeInLocationDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid PieceOfEquipmentId { get; set; }
        public Guid LocationId { get; set; }
    }
}
