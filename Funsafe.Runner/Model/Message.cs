namespace Funsafe.Runner.Model
{
    public class Message
    {
        public Header Header;
        public int Id;
        public int PartCount;
        public readonly MessagePart[] Parts = new MessagePart[256];
    }
}