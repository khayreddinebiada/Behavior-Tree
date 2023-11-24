namespace HCEngine.BT
{
    public enum NodeState
    {
        Running,
        Success,
        Failure,
        Abort
    }

    public enum Policy
    {
        Non,
        RequireOne,
        RequireAll,
    };
}