using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    static InteractionManager instance;
    public static InteractionManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("InteractionManager");
                instance = go.AddComponent<InteractionManager>();
            }
            return instance;
        }
    }

    bool playingInteraction = false;
    public bool PlayingInteraction { get { return playingInteraction; } set { playingInteraction = value; } }

    Interactive currentInteractive;
    public Node currentNode;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
            // PlayerPrefs.DeleteAll();
#endif
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayInteraction(Interactive interaction)
    {
        currentInteractive = interaction;

        string interactionId = interaction.interactionId;
        DoodleStudio95.DoodleAnimationFile doodleAnimation = interaction.doodleAnimation;

        InteractionData data = Database.instance.GetInteractionData(interactionId);

        currentNode = data.nodes[0];
        playingInteraction = true;
        PlayNode();
    }

    void HideUIAndRestorePlayerInput()
    {
        UIManager.instance.HideAllInteractionPanels();
        currentNode = null;
        currentInteractive = null;
        playingInteraction = false;
    }

    public void HandleInput()
    {
        if (currentNode.GetType() == typeof(ChoiceNode))
        {
            UIManager.instance.SelectPlayerOption();
        }
        else if (currentNode.GetType() == typeof(TextNode))
        {
            TextNode tNode = currentNode as TextNode;

            if (UIManager.instance.updatingText)
            {
                UIManager.instance.FastForwardText();
            }
            else
            {
                string nextNodeId = tNode.nextNodeId;

                if (nextNodeId == Database.EXIT_NODE_ID)
                {
                    HideUIAndRestorePlayerInput();
                }
                else
                {
                    Node nextNode = Database.instance.GetNode(nextNodeId);
                    currentNode = nextNode;

                    if (nextNode.GetType() == typeof(CustomNode))
                    {
                        PlayCustomNode();
                    }
                    else
                    {
                        PlayNode();
                    }
                }
            }
        }
        else if (currentNode.GetType() == typeof(CustomNode))
        {
            CleanUpCustomNode();

            CustomNode cmNode = currentNode as CustomNode;
            Node nextNode = Database.instance.GetNode(cmNode.nextNodeId);

            currentNode = nextNode;

            if (nextNode.id == Database.EXIT_NODE_ID)
            {
                HideUIAndRestorePlayerInput();
            }
            else if (nextNode.GetType() == typeof(CustomNode))
            {
                PlayCustomNode();
            }
            else
            {
                PlayNode();
            }
        }
    }

    public void UpdateEvents()
    {
        if (!string.IsNullOrEmpty(currentNode.evtId))
        {
            Database.instance.MarkEvent(currentNode.evtId);
        }
    }

    public void UpdateAndPlayCurrentNode(string nextNodeId)
    {

        if (nextNodeId == Database.EXIT_NODE_ID)
        {
            HideUIAndRestorePlayerInput();
        }
        else
        {
            currentNode = Database.instance.GetNode(nextNodeId);
            PlayNode();
        }
    }

    void PlayNode()
    {
        UpdateEvents();

        Type nodeType = currentNode.GetType();

        if (nodeType == typeof(TextNode))
        {
            TextNode tNode = currentNode as TextNode;
            UIManager.instance.ShowText(tNode);
        }

        if (nodeType == typeof(BranchNode))
        {
            BranchNode bNode = currentNode as BranchNode;
            currentNode = bNode.GetNextNode();
            PlayNode();
        }

        if (nodeType == typeof(ChoiceNode))
        {
            ChoiceNode cNode = currentNode as ChoiceNode;
            List<Node> availableNodes = cNode.GetValidNodes();

            UIManager.instance.ShowPlayerOptions(availableNodes);
        }

        if (nodeType == typeof(CustomNode))
        {
            PlayCustomNode();
        }
    }

    void PlayCustomNode()
    {
        switch (currentNode.id)
        {
            case "cus_office_exit":
                UIManager.instance.ShowHideTravelMap();
                break;
            case "cus_detailed_generic_image":
                UIManager.instance.ShowDetailedImage(currentInteractive.doodleAnimation);
                break;
            case "cus_maze_minigame":
                HandleMazeMinigame();
                break;
            case "cus_goodest_doggo_inventory":
                HandleCigarettes();
                break;
            case "cus_map_exit_1":
                UIManager.instance.ShowHideTravelMap();
                break;
            case "cus_start_end_game":
                playingInteraction = false;
                UIManager.instance.LoadScene("office_end");
                break;
            case "cus_sorry_about_that_text":
                UIManager.instance.ShowSorryAboutThatMessage(true);
                break;
            case "cus_end_clear_values":

                PlayerPrefs.DeleteKey("evt_end_artist_1_did_it");
                PlayerPrefs.DeleteKey("evt_end_artist_2_did_it");
                PlayerPrefs.DeleteKey("evt_end_thief_cat_did_it");

                PlayerPrefs.DeleteKey("evt_end_artist_1_blackmailed_him");
                PlayerPrefs.DeleteKey("evt_end_artist_2_blackmailed_him");

                PlayerPrefs.DeleteKey("evt_end_fan_bought_it");
                break;
            case "cus_load_office":

                PlayerPrefs.DeleteKey("evt_end_artist_1_did_it");
                PlayerPrefs.DeleteKey("evt_end_artist_2_did_it");
                PlayerPrefs.DeleteKey("evt_end_thief_cat_did_it");

                PlayerPrefs.DeleteKey("evt_end_artist_1_blackmailed_him");
                PlayerPrefs.DeleteKey("evt_end_artist_2_blackmailed_him");

                PlayerPrefs.DeleteKey("evt_end_fan_bought_it");

                UIManager.instance.LoadScene("office");
                break;
            case "cus_artist_2_door_outside":
                UIManager.instance.LoadScene("artist2_house");
                break;
            case "cus_show_end_game_picture":
                UIManager.instance.ShowEndGamePicture();
                break;
        }
    }

    void HandleMazeMinigame()
    {
        MazeExit exit = currentInteractive.GetComponent<MazeExit>();
        Vector2 direction = exit.direction;

        MazeController.instance.ChangeNode(direction);
        playingInteraction = false;
    }

    void HandleCigarettes()
    {
        UIManager.instance.ShowCigarettesPanel();
    }

    void CleanUpCustomNode()
    {
        switch (currentNode.id)
        {
            case "cus_detailed_generic_image":
                UIManager.instance.detailPanel.SetActive(false);
                break;
            case "cus_goodest_doggo_inventory":
                UIManager.instance.HideCigarettesPanel();
                break;
            case "cus_start_end_game":
                playingInteraction = false;
                break;
            case "cus_sorry_about_that_text":
                UIManager.instance.ShowSorryAboutThatMessage(false);
                break;
        }
    }
}
