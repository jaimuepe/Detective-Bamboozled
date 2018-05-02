
using System.Collections.Generic;

public class BranchNode : Node
{
    public List<BranchNodeOption> options;

    public BranchNode(string id, List<BranchNodeOption> options)
        : base(id)
    {
        this.options = options;
    }

    public Node GetNextNode()
    {
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].condition.IsValid())
            {
                return Database.instance.GetNode(options[i].nodeId);
            }
        }

        throw new System.Exception("No next node in branchNode" + id);
    }
}
