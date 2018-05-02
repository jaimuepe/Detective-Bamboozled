using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    public GameObject artist1;
    public GameObject artist2;
    public GameObject fan;
    public GameObject securityGuard;
    public GameObject thiefCat;

    private void Start()
    {

#if UNITY_EDITOR
        PlayerPrefs.DeleteKey("evt_end_artist_1_did_it");
        PlayerPrefs.DeleteKey("evt_end_artist_2_did_it");
        PlayerPrefs.DeleteKey("evt_end_thief_cat_did_it");

        PlayerPrefs.DeleteKey("evt_end_artist_1_blackmailed_him");
        PlayerPrefs.DeleteKey("evt_end_artist_2_blackmailed_him");

        PlayerPrefs.DeleteKey("evt_end_fan_bought_it");
#endif

        if (!Database.instance.CheckEvent("evt_met_artist_1"))
        {
            artist1.SetActive(false);
        }
        if (!Database.instance.CheckEvent("evt_met_artist_2"))
        {
            artist2.SetActive(false);
        }
        if (!Database.instance.CheckEvent("evt_met_security_guard"))
        {
            securityGuard.SetActive(false);
        }
        if (!Database.instance.CheckEvent("evt_met_fan"))
        {
            fan.SetActive(false);
        }
        if (!Database.instance.CheckEvent("evt_met_thief_cat"))
        {
            thiefCat.SetActive(false);
        }
    }
}
