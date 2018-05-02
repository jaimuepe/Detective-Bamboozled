using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNode : Node
{
    public string nextNodeId;

    public CustomNode(string nodeId, string nextNodeId) : base(nodeId)
    {
        this.nextNodeId = nextNodeId;
    }
}
