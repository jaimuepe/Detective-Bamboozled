using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Database : MonoBehaviour
{
    Dictionary<string, InteractionData> interactions = new Dictionary<string, InteractionData>();
    Dictionary<string, Node> nodes = new Dictionary<string, Node>();

    public static Database instance;

    public static readonly Node EXIT_NODE = new ExitNode();
    public static readonly string EXIT_NODE_ID = "__exit__";

    public static readonly string DETAILED_IMAGE_INTERACTION = "int_detailed_generic_image";
    public static readonly string DETAILED_NODE_ID = "cus_detailed_generic_image";

    public static readonly string PLAYER_ID = "player";

    [Header("UI Animations")]
    public DoodleStudio95.DoodleAnimationFile exclamationMarkUIAnimation;
    public DoodleStudio95.DoodleAnimationFile doggoUIAnimation;
    public DoodleStudio95.DoodleAnimationFile exitUIAnimation;

    public DoodleStudio95.DoodleAnimationFile emphasisNeutral;
    public DoodleStudio95.DoodleAnimationFile emphasisScared;
    public DoodleStudio95.DoodleAnimationFile emphasisAngry;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

#if UNITY_EDITOR
        // PlayerPrefs.DeleteAll();
#endif

        foreach (EntitySprite eSprite in sprites)
        {
            spritesMap[eSprite.entityId] = eSprite.sprite;
        }

        string interactionsPath = Path.Combine(Application.streamingAssetsPath, "interactions/");
        string[] interactionFiles = Directory.GetFiles(interactionsPath);

        foreach (string interactionFile in interactionFiles)
        {
            if (!interactionFile.EndsWith(".json"))
            {
                continue;
            }
            string dataAsJson = File.ReadAllText(interactionFile);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            InteractionData data = JsonConvert.DeserializeObject<InteractionData>(dataAsJson, settings);

            for (int i = 0; i < data.nodes.Count; i++)
            {
                nodes[data.nodes[i].id] = data.nodes[i];
            }

            interactions[data.id] = data;
        }
    }

    public Node GetNode(string nodeId)
    {
        if (nodeId == EXIT_NODE_ID)
        {
            return EXIT_NODE;
        }
        if (!nodes.ContainsKey(nodeId))
        {
            Debug.LogError("nodeId " + nodeId + " not found");
        }
        return nodes[nodeId];
    }

    public InteractionData GetInteractionData(string interactionId)
    {
        if (interactions.ContainsKey(interactionId))
        {
            return interactions[interactionId];
        }

        throw new FileNotFoundException("Unexistent interaction file " + interactionId + "!");
    }

    public List<EntitySprite> sprites;
    Dictionary<string, Sprite> spritesMap = new Dictionary<string, Sprite>();

    public Sprite GetSpriteForEntity(string entityId)
    {
        if (!spritesMap.ContainsKey(entityId))
        {
            Debug.LogWarning("Non-existent sprite for entity " + entityId);
            return null;
        }
        return spritesMap[entityId];
    }

    public void MarkEvent(string eventId)
    {
        int value = 1;
        if (eventId.StartsWith("!"))
        {
            value = 0;
            eventId = eventId.Substring(1);
        }

        if (PlayerPrefs.GetInt(eventId) != value)
        {
            PlayerPrefs.SetInt(eventId, value);
            PlayerPrefs.Save();

            Debug.Log(eventId + "=" + value);
            ShowEventHint(eventId);
        }
    }

    void ShowEventHint(string eventId)
    {
        string msg;
        switch (eventId)
        {
            case "evt_black_market_unlocked":
            case "evt_artist_1_house_unlocked":
            case "evt_artist_2_house_unlocked":
            case "evt_security_guard_house_unlocked":
            case "evt_artist_1_clue_tobacco_smell":
            case "evt_find_info_black_market":
                msg = LocalizationManager.instance.GetLocalizedValue(eventId);
                break;
            default:
                return;
        }
        UIManager.instance.ShowClue(msg);
    }

    public bool CheckEvent(string eventId)
    {
        // tokenizamos el string (ej. condicionA&condicionB)
        string[] tokens;
        if (eventId.Contains("&"))
        {
            tokens = eventId.Split('&');
        }
        else
        {
            tokens = new string[] { eventId };
        }

        int returnValue = 0;

        foreach (string tok in tokens)
        {
            string tokCpy = tok;

            int value = 1;
            // si el token empieza por ! buscamos condición inversa
            if (tok.StartsWith("!"))
            {
                tokCpy = tok.Substring(1);
                value = 0;
            }

            returnValue = PlayerPrefs.GetInt(tokCpy, -1);

            // si el valor del token no coincide con el esperado ya no seguimos mirando el resto
            if (returnValue != value)
            {
                if (!(value == 0 && returnValue == -1))
                {
                    return false;
                }
            }
        }
        return true;
    }
}

[Serializable]
public class EntitySprite
{
    public string entityId;
    public Sprite sprite;
}

[Serializable]
public class IdDoodleAnimation
{
    public string id;
    public DoodleStudio95.DoodleAnimationFile animation;
}