using DoodleStudio95;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Common")]
    public float updateTextSpeedInSecs = 0.05f;
    public GameObject pausePanel;
    public GameObject pausePanelContinueBtn;
    public Image cluePanel;
    public Text clueText;

    [Header("NPC")]
    public GameObject npcPanel;
    public Image npcPortrait;
    public Text npcMessageText;

    [Header("Player")]
    public GameObject playerPanel;
    public Image playerPortrait;
    public Text playerMessageText;
    public RectTransform playerOptionsScrollPane;
    public RectTransform playerOptionsPanel;
    public PlayerChoiceButton playerChoiceItemPrefab;

    [Header("Detail nodes")]
    public GameObject detailPanel;
    public DoodleAnimator detailAnimator;

    [Header("Map")]
    public GameObject fastTravelMap;

    public RectTransform artist1HouseSpot;
    public RectTransform artist2HouseSpot;
    public RectTransform officeSpot;
    public RectTransform museumSpot;
    public RectTransform guardHouseSpot;
    public RectTransform suburbsSpot;
    public RectTransform crossroadsSpot;
    public RectTransform blackmarketSpot;
    public RectTransform doggoIcon;
    public Text currentlyAtText;

    [Header("Custom")]
    public GameObject cigarettesPanel;
    public GameObject cigarettesButton1;
    public GameObject sorryAboutThatPanel;
    public GameObject smellPanel;

    [Header("End game")]
    public float fadeTime;
    public float timeBetweenFades;
    public Image fadeOutPanel;
    public Image endGamePicture;

    Coroutine updateTextNodeCoroutine;

    [NonSerialized]
    public bool updatingText;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (cigarettesPanel)
        {
            cigarettesPanel.SetActive(false);
        }
        if (playerPanel)
        {
            playerPanel.SetActive(false);
        }
        if (npcPanel)
        {
            npcPortrait.sprite = null;
            npcPanel.SetActive(false);
        }
        if (detailPanel)
        {
            detailPanel.SetActive(false);
        }
        if (fastTravelMap)
        {
            fastTravelMap.SetActive(false);
        }
        if (sorryAboutThatPanel)
        {
            sorryAboutThatPanel.SetActive(false);
        }
        if (smellPanel)
        {
            smellPanel.SetActive(false);
        }
        if (pausePanel)
        {
            pausePanel.SetActive(false);
        }
        if (fadeOutPanel)
        {
            fadeOutPanel.gameObject.SetActive(false);
        }
        if (endGamePicture)
        {
            endGamePicture.gameObject.SetActive(false);
        }

        string text = "Currently at: {0}";
        string area;

        switch (scene.name)
        {
            case "main_menu":
            case "office":
            case "office_end":
                area = "Office";
                break;
            case "museum":
                area = "Museum";
                Database.instance.MarkEvent("evt_museum_visited");
                break;
            case "artist1_house":
                area = "Mutt's";
                break;
            case "artist2_house":
                area = "Rocco's";
                break;
            case "suburbs":
                area = "Suburbs";
                break;
            case "guard_house":
                area = "Baxter's";
                break;
            case "blackmarket":
                area = "Blackmarket";
                break;
            case "crossroad_start":
                area = "Crossroads";
                break;
            default:
                if (scene.name.StartsWith("crossroad_"))
                {
                    area = "Crossroads";
                }
                else
                {
                    throw new Exception("Unknown scene " + scene.name);
                }
                break;
        }

        currentlyAtText.text = string.Format(text, area);
        InteractionManager.Instance.PlayingInteraction = false;
    }

    private void Start()
    {
        playerPanel.SetActive(false);
        npcPanel.SetActive(false);
    }

    private void Update()
    {
        if (fastTravelMap.gameObject.activeSelf)
        {
            GameObject selectedItem = EventSystem.current.currentSelectedGameObject;
            if (selectedItem)
            {
                doggoIcon.anchoredPosition = selectedItem.
                    GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }

    public void ShowText(TextNode tNode)
    {
        Sprite sprite = Database.instance.GetSpriteForEntity(tNode.ownerId);
        DoodleAnimationFile emphasisAnimation = GetMessageEmphasis(tNode.emphasis);

        if (tNode.ownerId == Database.PLAYER_ID)
        {
            playerOptionsScrollPane.gameObject.SetActive(false);
            playerMessageText.gameObject.SetActive(true);
            playerPanel.GetComponent<DoodleAnimator>().ChangeAnimation(emphasisAnimation);

            if (!playerPanel.activeSelf)
            {
                AudioManager.instance.PlaySfxForCharacter("player");
                AnimatePanelIn(playerPanel, "player");
            }

            playerPortrait.sprite = sprite;
            UpdateText(tNode.id, playerMessageText);
        }
        else
        {
            npcMessageText.gameObject.SetActive(true);

            npcPanel.GetComponent<DoodleAnimator>().ChangeAnimation(emphasisAnimation);

            if (!npcPanel.activeSelf)
            {
                AnimatePanelIn(npcPanel, tNode.ownerId);
            }

            if (sprite != npcPortrait.sprite)
            {
                AudioManager.instance.PlaySfxForCharacter(tNode.ownerId);
            }

            npcPortrait.sprite = sprite;
            UpdateText(tNode.id, npcMessageText);
        }
    }

    DoodleAnimationFile GetMessageEmphasis(string emphasis)
    {
        if (string.IsNullOrEmpty(emphasis))
        {
            return Database.instance.emphasisNeutral;
        }
        if (emphasis == "scared")
        {
            return Database.instance.emphasisScared;
        }
        if (emphasis == "angry")
        {
            return Database.instance.emphasisAngry;
        }

        throw new Exception("Uknown emphasis type " + emphasis);
    }

    public void SelectPlayerOption()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        if (selectedButton == null)
        {
            return;
        }

        PlayerChoiceButton pcb = selectedButton.GetComponent<PlayerChoiceButton>();

        TextNode n = Database.instance.GetNode(pcb.nodeId) as TextNode;

        if (!string.IsNullOrEmpty(n.evtId))
        {
            Database.instance.MarkEvent(n.evtId);
        }

        InteractionManager.Instance.UpdateAndPlayCurrentNode(n.nextNodeId);

        HidePlayerOptions();
    }

    public void HidePlayerOptions()
    {
        playerOptionsScrollPane.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ShowPlayerOptions(List<Node> options)
    {
        for (int i = 0; i < playerOptionsPanel.transform.childCount; i++)
        {
            Destroy(playerOptionsPanel.transform.GetChild(i).gameObject);
        }

        float totalItemHeight = 0f;

        for (int i = 0; i < options.Count; i++)
        {
            Node n = options[i];

            if (n.GetType() == typeof(BranchNode))
            {
                BranchNode bNode = n as BranchNode;
                n = bNode.GetNextNode();
            }

            PlayerChoiceButton buttonOption = Instantiate(playerChoiceItemPrefab);
            buttonOption.nodeId = n.id;
            buttonOption.textComponent.text = LocalizationManager.instance.GetLocalizedValue(n.id);
            buttonOption.gameObject.name = "Button option " + i;
            buttonOption.transform.SetParent(playerOptionsPanel.transform, false);

            totalItemHeight += buttonOption.GetComponent<RectTransform>().sizeDelta.y;

            if (i == 0)
            {
                EventSystem.current.SetSelectedGameObject(null);
                StartCoroutine(SetSelectedGameObjectNextFrame(buttonOption.gameObject));
            }
        }

        Vector2 sizeDelta = playerOptionsPanel.sizeDelta;
        sizeDelta.y = totalItemHeight + (options.Count - 1) * playerOptionsPanel.GetComponent<VerticalLayoutGroup>().spacing;
        playerOptionsPanel.sizeDelta = sizeDelta;

        if (!playerPanel.activeSelf)
        {
            AudioManager.instance.PlaySfxForCharacter("player");
            AnimatePanelIn(playerPanel, "player");
        }

        playerMessageText.gameObject.SetActive(false);
        playerOptionsScrollPane.gameObject.SetActive(true);
    }

    public void HideAllInteractionPanels()
    {
        playerPanel.gameObject.SetActive(false);
        playerMessageText.gameObject.SetActive(false);
        playerOptionsScrollPane.gameObject.SetActive(false);

        npcPortrait.sprite = null;
        npcPanel.gameObject.SetActive(false);
        npcMessageText.gameObject.SetActive(false);
        detailPanel.gameObject.SetActive(false);
        detailAnimator.ChangeAnimation(null);
    }

    public void FastForwardText()
    {
        TextNode tNode = InteractionManager.Instance.currentNode as TextNode;

        if (updateTextNodeCoroutine != null)
        {
            StopCoroutine(updateTextNodeCoroutine);
        }

        if (tNode.ownerId == Database.PLAYER_ID)
        {
            playerMessageText.text = LocalizationManager.instance.GetLocalizedValue(tNode.id);
        }
        else
        {
            npcMessageText.text = LocalizationManager.instance.GetLocalizedValue(tNode.id);
        }

        updatingText = false;
    }

    void UpdateText(string textId, Text textComponent)
    {
        if (updateTextNodeCoroutine != null)
        {
            StopCoroutine(updateTextNodeCoroutine);
        }

        updateTextNodeCoroutine = StartCoroutine(UpdateTextCoroutine(textId, textComponent));
    }

    void AnimatePanelIn(GameObject panel, string ownerId)
    {
        Animator anim = panel.GetComponent<Animator>();
        panel.SetActive(true);
        anim.SetBool("in", true);

        StartCoroutine(TurnVariableOffAtEndOfFrame(anim, "in"));
    }

    public void ShowDetailedImage(DoodleAnimationFile doodleAnimation)
    {
        detailAnimator.ChangeAnimation(doodleAnimation);
        detailPanel.SetActive(true);
    }

    public bool ShowingTravelMap()
    {
        return fastTravelMap.activeSelf;
    }

    public bool ShowingSmellPanel()
    {
        return smellPanel.activeSelf;
    }

    public void ShowHideSmellsPanel(bool show)
    {
        smellPanel.SetActive(show);
    }

    public bool ShowingPausePanel()
    {
        return pausePanel.activeSelf;
    }

    public void ShowHidePausePanel()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(pausePanelContinueBtn);
        }
    }

    public void ShowHideTravelMap()
    {
        if (ShowingTravelMap())
        {
            fastTravelMap.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            fastTravelMap.SetActive(true);
            string scene = SceneManager.GetActiveScene().name;

            artist1HouseSpot.gameObject.SetActive(
                Database.instance.CheckEvent("evt_artist_1_house_unlocked"));

            guardHouseSpot.gameObject.SetActive(
                Database.instance.CheckEvent("evt_security_guard_house_unlocked"));

            blackmarketSpot.gameObject.SetActive(
                Database.instance.CheckEvent("evt_black_market_unlocked"));

            artist2HouseSpot.gameObject.SetActive(
               Database.instance.CheckEvent("evt_artist_2_house_unlocked"));

            RectTransform selectedRectTransf;

            if (scene == "office" || scene == "office_end")
            {
                selectedRectTransf = officeSpot;
            }
            else if (scene == "museum")
            {
                selectedRectTransf = museumSpot;
            }
            else if (scene == "artist1_house")
            {
                selectedRectTransf = artist1HouseSpot;
            }
            else if (scene == "artist2_house")
            {
                selectedRectTransf = artist2HouseSpot;
            }
            else if (scene == "suburbs")
            {
                selectedRectTransf = suburbsSpot;
            }
            else if (scene == "blackmarket")
            {
                selectedRectTransf = blackmarketSpot;
            }
            else if (scene == "guard_house")
            {
                selectedRectTransf = guardHouseSpot;
            }
            else if (scene == "crossroad_start")
            {
                selectedRectTransf = crossroadsSpot;
            }
            else
            {
                throw new Exception("Unknown scene " + scene);
            }

            EventSystem.current.SetSelectedGameObject(selectedRectTransf.gameObject);
        }
    }

    public void ShowCigarettesPanel()
    {
        cigarettesPanel.gameObject.SetActive(true);
    }

    public void HideCigarettesPanel()
    {
        cigarettesPanel.gameObject.SetActive(false);
    }

    public void ShowSorryAboutThatMessage(bool show)
    {
        sorryAboutThatPanel.SetActive(show);
    }

    IEnumerator TurnVariableOffAtEndOfFrame(Animator anim, string varName)
    {
        yield return new WaitForEndOfFrame();
        anim.SetBool(varName, false);
    }

    IEnumerator SetSelectedGameObjectNextFrame(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(go);
    }

    IEnumerator UpdateTextCoroutine(string textId, Text textComponent)
    {
        updatingText = true;

        string text = LocalizationManager.instance.GetLocalizedValue(textId);

        textComponent.text = "";
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            sb.Append(text[i]);
            textComponent.text = sb.ToString();
            yield return new WaitForSeconds(updateTextSpeedInSecs);
        }

        updatingText = false;
    }

    public AudioClip screenChangeClip;

    public void LoadScene(string sceneName)
    {
        if (sceneName == SceneManager.GetActiveScene().name)
        {
            fastTravelMap.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            return;
        }

        LevelTransition transition = GetComponent<LevelTransition>();
        transition.nextLevel = "Scenes/" + sceneName;
        transition.StartTransition();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    bool endGamePictureTriggered = false;

    public void ShowEndGamePicture()
    {
        if (endGamePictureTriggered) { return; }

        endGamePictureTriggered = true;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        fadeOutPanel.color = new Color(
                fadeOutPanel.color.r,
                fadeOutPanel.color.g,
                fadeOutPanel.color.b,
                0f);

        fadeOutPanel.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(fadeOutPanel.color.a, 1f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;

            fadeOutPanel.color = new Color(
                fadeOutPanel.color.r,
                fadeOutPanel.color.g,
                fadeOutPanel.color.b,
                alpha);

            yield return null;
        }

        fadeOutPanel.color = new Color(
                fadeOutPanel.color.r,
                fadeOutPanel.color.g,
                fadeOutPanel.color.b,
                1f);

        yield return new WaitForSeconds(timeBetweenFades);
        StartCoroutine(FadeInEndPicture());
    }

    Coroutine fadeInClueCoroutine;
    Coroutine fadeOutClueCoroutine;

    public void ShowClue(string clue)
    {
        if (fadeOutClueCoroutine != null)
        {
            StopCoroutine(fadeOutClueCoroutine);
        }
        if (fadeInClueCoroutine != null)
        {
            StopCoroutine(fadeInClueCoroutine);
        }
        fadeInClueCoroutine = StartCoroutine(FadeInClue(clue));
    }

    IEnumerator FadeInClue(string clue)
    {
        clueText.text = clue;
        cluePanel.color = new Color(
            cluePanel.color.r,
            cluePanel.color.g,
            cluePanel.color.b,
            0f);

        clueText.color = new Color(
            clueText.color.r,
            clueText.color.g,
            clueText.color.b,
            0f);

        cluePanel.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(endGamePicture.color.a, 1f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;

            cluePanel.color = new Color(
                cluePanel.color.r,
                cluePanel.color.g,
                cluePanel.color.b,
                alpha);

            clueText.color = new Color(
                clueText.color.r,
                clueText.color.g,
                clueText.color.b,
                alpha);

            yield return null;
        }

        cluePanel.color = new Color(
                cluePanel.color.r,
                cluePanel.color.g,
                cluePanel.color.b,
                1f);

        clueText.color = new Color(
            clueText.color.r,
            clueText.color.g,
            clueText.color.b,
            1f);

        yield return new WaitForSeconds(4f);
        fadeOutClueCoroutine = StartCoroutine(FadeOutClue());
    }

    IEnumerator FadeOutClue()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(cluePanel.color.a, 0f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;

            cluePanel.color = new Color(
                cluePanel.color.r,
                cluePanel.color.g,
                cluePanel.color.b,
                alpha);

            clueText.color = new Color(
                clueText.color.r,
                clueText.color.g,
                clueText.color.b,
                alpha);

            yield return null;
        }

        cluePanel.color = new Color(
                cluePanel.color.r,
                cluePanel.color.g,
                cluePanel.color.b,
                0f);

        clueText.color = new Color(
            clueText.color.r,
            clueText.color.g,
            clueText.color.b,
            0f);

        cluePanel.gameObject.SetActive(false);
    }

    IEnumerator FadeInEndPicture()
    {
        endGamePicture.color = new Color(
                endGamePicture.color.r,
                endGamePicture.color.g,
                endGamePicture.color.b,
                0f);

        endGamePicture.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(endGamePicture.color.a, 1f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;

            endGamePicture.color = new Color(
                endGamePicture.color.r,
                endGamePicture.color.g,
                endGamePicture.color.b,
                alpha);

            yield return null;
        }

        endGamePicture.color = new Color(
                endGamePicture.color.r,
                endGamePicture.color.g,
                endGamePicture.color.b,
                1f);
    }
}
