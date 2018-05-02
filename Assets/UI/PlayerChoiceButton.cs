using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerChoiceButton : MonoBehaviour, ISelectHandler
{

    public string nodeId;
    public Button button;
    public Text textComponent;

    public AudioClip clickSound;
    public AudioClip selectSound;

    private void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
    }

    public void OnSelect(BaseEventData eventData)
    {
        AudioSource.PlayClipAtPoint(selectSound, Camera.main.transform.position);
    }
}
