namespace Funsafe.Runner.Model
{
    public class Message
    {
        public Header Header;
        public int Id;
        public int PartCount;
        public MessagePart[] Parts = new MessagePart[256];
    }
}