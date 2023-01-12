namespace OnShop.Domain.Arrangements.Commands
{
    public class UpdateArrangementCommand : AddArrangementCommand
    {
        public long Id { get; set; }
    }
}