
using UnityEngine;

public class TextNode : Node
{
    public string ownerId;
    public string nextNodeId;
    public string emphasis;

    public TextNode(string id, string evtId, string ownerId, string nextNodeId)
        : base(id, evtId)
    {
        this.ownerId = ownerId;
        this.nextNodeId = nextNodeId;
    }
}
