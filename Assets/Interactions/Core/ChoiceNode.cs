
using System.Collections.Generic;
using UnityEngine.Assertions;

public class ChoiceNode : Node
{
    public List<ChoiceNodeOption> options;

    public ChoiceNode(string id, List<ChoiceNodeOption> options)
        : base(id, null)
    {
        this.options = options;
    }

    public List<Node> GetValidNodes()
    {
        List<Node> validNodes = new List<Node>();

        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].condition.IsValid())
            {
                validNodes.Add(Database.instance.GetNode(options[i].nodeId));
            }
        }

        Assert.AreNotEqual(validNodes.Count, 0);

        return validNodes;
    }
}
