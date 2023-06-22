namespace Api.NewFolder.NewFolder
{
    public class AddIndividualClientDto
    {
        public Guid LocationId { get; set; }
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
