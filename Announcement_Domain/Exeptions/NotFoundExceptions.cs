namespace Announcement_Domain.Exeptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException() { }
    } 
}
