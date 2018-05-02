
using System;
using UnityEngine;

[Serializable]
public abstract class Node
{
    public string id;
    public string evtId;

    public Node(string id)
    {
        this.id = id;
    }

    public Node(string id, string evtId) : this(id)
    {
        this.evtId = evtId;
    }

    public static bool operator true(Node n) { return n != null; }
    public static bool operator false(Node n) { return n == null; }
}
