
using System;

[Serializable]
public class Condition
{
    public enum ConditionType { NONE, TRUE, FALSE };

    public static readonly Condition empty = new Condition("", ConditionType.NONE);

    public string eventId;
    public ConditionType type;

    public Condition()
    {
        type = ConditionType.NONE;
    }

    public Condition(string eventId) : this(eventId, ConditionType.TRUE)
    {
    }

    public Condition(string eventId, ConditionType type)
    {
        this.type = type;
        this.eventId = eventId;
    }

    public bool IsValid()
    {
        switch (type)
        {
            case ConditionType.TRUE:
                return Database.instance.CheckEvent(eventId);
            case ConditionType.FALSE:
                return !Database.instance.CheckEvent(eventId);
            case ConditionType.NONE:
                return true;
        }
        return true;
    }
}