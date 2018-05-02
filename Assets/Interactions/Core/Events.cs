
using UnityEngine;

class Events
{
    public static bool EventDiscovered(string evtId)
    {
        return PlayerPrefs.GetInt(evtId) == 1;
    }
}
