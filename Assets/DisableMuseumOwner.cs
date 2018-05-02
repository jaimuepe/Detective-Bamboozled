using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMuseumOwner : MonoBehaviour {

    public GameObject museumOwner;

	void Start () {
	    if (Database.instance.CheckEvent("evt_museum_owner_office_end"))
        {
            museumOwner.SetActive(false);
        }	
    }
}
