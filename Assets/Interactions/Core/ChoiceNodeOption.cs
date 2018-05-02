
using System;

[Serializable]
public class ChoiceNodeOption
{
    public string nodeId;
    public Condition condition;

    public ChoiceNodeOption()
    {
    }

    public ChoiceNodeOption(string nodeId) : this(nodeId, new Condition())
    {
    }

    public ChoiceNodeOption(string nodeId, Condition condition)
    {
        this.nodeId = nodeId;
        this.condition = condition;
    }
}
