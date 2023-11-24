namespace HCEngine.BT
{
    public class BTreeBuilder
    {
        public IDecorator Root { get; internal set; }
        public IDecorator Bottom { get; internal set; }

        public static BTreeBuilder Create()
        {
            return new BTreeBuilder();
        }
    }
}