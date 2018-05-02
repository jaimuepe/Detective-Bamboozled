
public class BranchNodeOption
{
    public string nodeId;
    public Condition condition;

    public BranchNodeOption() { }

    public BranchNodeOption(string nodeId) : this(nodeId, new Condition())
    {
    }

    public BranchNodeOption(string nodeId, Condition condition)
    {
        this.nodeId = nodeId;
        this.condition = condition;
    }
}
