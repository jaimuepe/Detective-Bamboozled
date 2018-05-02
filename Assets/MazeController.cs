using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MazeController : MonoBehaviour
{
    int posX = 3;
    int posY = 2;

    string lastMap;

    public GameObject smellSpawnerPrefab;

    public string smell0Direction;
    public string smell1Direction;
    public string smell2Direction;
    public string smell3Direction;
    public string smell4Direction;
    public string smell5Direction;
    public string smell6Direction;
    public string smell7Direction;

    public GameObject topSideSmellSource;
    public GameObject leftSideSmellSource;
    public GameObject rightSideSmellSource;
    public GameObject downSideSmellSource;

    public static MazeController instance;

    string directionSceneChange = "down";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            lastMap = null;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void GenerateSmellTraces()
    {
        if (posX == 1 && posY == 0)
        {
            smell0Direction = "left";
            smell1Direction = "down";
            smell2Direction = "down";
            smell3Direction = "down";
            smell4Direction = "down";
            smell5Direction = "down";
            smell6Direction = "down";
            smell7Direction = "down";
        }
        else if (posX == 2 && posY == 0)
        {
            smell0Direction = "down";
            smell1Direction = "down";
            smell2Direction = "right";
            smell3Direction = "down";
            smell4Direction = "down";
            smell5Direction = "down";
            smell6Direction = "down";
            smell7Direction = "down";
        }
        else if (posX == 3 && posY == 0)
        {
            smell0Direction = "left";
            smell1Direction = "down";
            smell2Direction = "right";
            smell3Direction = "down";
            smell4Direction = "down";
            smell5Direction = "down";
            smell6Direction = "down";
            smell7Direction = "down";
        }
        else if (posX == 1 && posY == 1)
        {
            smell0Direction = "up";
            smell1Direction = "right";
            smell2Direction = "right";
            smell3Direction = "right";
            smell4Direction = "right";
            smell5Direction = "right";
            smell6Direction = "right";
            smell7Direction = "right";
        }
        else if (posX == 2 && posY == 1)
        {
            smell0Direction = "left";
            smell1Direction = "right";
            smell2Direction = "up";
            smell3Direction = "right";
            smell4Direction = "right";
            smell5Direction = "right";
            smell6Direction = "right";
            smell7Direction = "right";
        }
        else if (posX == 3 && posY == 1)
        {
            smell0Direction = "left";
            smell1Direction = "down";
            smell2Direction = "up";
            smell3Direction = "down";
            smell4Direction = "down";
            smell5Direction = "down";
            smell6Direction = "down";
            smell7Direction = "down";
        }
        else if (posX == 1 && posY == 2)
        {
            smell0Direction = "right";
            smell1Direction = "down";
            smell2Direction = "right";
            smell3Direction = "down";
            smell4Direction = "right";
            smell5Direction = "right";
            smell6Direction = "right";
            smell7Direction = "down";
        }
        else if (posX == 2 && posY == 2)
        {
            smell0Direction = "right";
            smell1Direction = "left";
            smell2Direction = "right";
            smell3Direction = "left";
            smell4Direction = "right";
            smell5Direction = "down";
            smell6Direction = "right";
            smell7Direction = "left";
        }
        else if (posX == 3 && posY == 2)
        {
            smell0Direction = "up";
            smell1Direction = "left";
            smell2Direction = "up";
            smell3Direction = "left";
            smell4Direction = "right";
            smell5Direction = "left";
            smell6Direction = "left";
            smell7Direction = "left";
        }
        else if (posX == 4 && posY == 2)
        {
            smell0Direction = "left";
            smell1Direction = "left";
            smell2Direction = "left";
            smell3Direction = "left";
            smell4Direction = "right";
            smell5Direction = "left";
            smell6Direction = "left";
            smell7Direction = "left";
        }
        else if (posX == 5 && posY == 2)
        {
            smell0Direction = "left";
            smell1Direction = "left";
            smell2Direction = "left";
            smell3Direction = "left";
            smell4Direction = "down";
            smell5Direction = "left";
            smell6Direction = "left";
            smell7Direction = "left";
        }
        else if (posX == 0 && posY == 3)
        {
            smell0Direction = "right";
            smell1Direction = "up";
            smell2Direction = "right";
            smell3Direction = "down";
            smell4Direction = "right";
            smell5Direction = "right";
            smell6Direction = "right";
            smell7Direction = "down";
        }
        else if (posX == 1 && posY == 3)
        {
            smell0Direction = "up";
            smell1Direction = "left";
            smell2Direction = "up";
            smell3Direction = "down";
            smell4Direction = "up";
            smell5Direction = "up";
            smell6Direction = "right";
            smell7Direction = "down";
        }
        else if (posX == 2 && posY == 3)
        {
            smell0Direction = "up";
            smell1Direction = "up";
            smell2Direction = "up";
            smell3Direction = "up";
            smell4Direction = "right";
            smell5Direction = "right";
            smell6Direction = "right";
            smell7Direction = "up";
        }
        else if (posX == 3 && posY == 3)
        {
            smell0Direction = "left";
            smell1Direction = "left";
            smell2Direction = "left";
            smell3Direction = "left";
            smell4Direction = "left";
            smell5Direction = "down";
            smell6Direction = "down";
            smell7Direction = "left";
        }
        else if (posX == 0 && posY == 4)
        {
            smell0Direction = "up";
            smell1Direction = "up";
            smell2Direction = "up";
            smell3Direction = "right";
            smell4Direction = "up";
            smell5Direction = "up";
            smell6Direction = "up";
            smell7Direction = "right";
        }
        else if (posX == 1 && posY == 4)
        {
            smell0Direction = "up";
            smell1Direction = "left";
            smell2Direction = "up";
            smell3Direction = "right";
            smell4Direction = "up";
            smell5Direction = "up";
            smell6Direction = "up";
            smell7Direction = "right";
        }
        else if (posX == 2 && posY == 4)
        {
            smell0Direction = "left";
            smell1Direction = "left";
            smell2Direction = "left";
            smell3Direction = "down";
            smell4Direction = "left";
            smell5Direction = "left";
            smell6Direction = "left";
            smell7Direction = "up";
        }
        else if (posX == 3 && posY == 4)
        {
            smell0Direction = "up";
            smell1Direction = "up";
            smell2Direction = "up";
            smell3Direction = "up";
            smell4Direction = "up";
            smell5Direction = "right";
            smell6Direction = "right";
            smell7Direction = "up";
        }
        else if (posX == 4 && posY == 4)
        {
            smell0Direction = "left";
            smell1Direction = "left";
            smell2Direction = "left";
            smell3Direction = "left";
            smell4Direction = "left";
            smell5Direction = "down";
            smell6Direction = "right";
            smell7Direction = "left";
        }
        else if (posX == 5 && posY == 4)
        {
            smell0Direction = "left";
            smell1Direction = "left";
            smell2Direction = "left";
            smell3Direction = "left";
            smell4Direction = "left";
            smell5Direction = "left";
            smell6Direction = "down";
            smell7Direction = "left";
        }
        else if (posX == 2 && posY == 5)
        {
            smell0Direction = "up";
            smell1Direction = "up";
            smell2Direction = "up";
            smell3Direction = "left";
            smell4Direction = "up";
            smell5Direction = "up";
            smell6Direction = "up";
            smell7Direction = "up";
        }
        else if (posX == 4 && posY == 5)
        {
            smell0Direction = "up";
            smell1Direction = "up";
            smell2Direction = "up";
            smell3Direction = "up";
            smell4Direction = "up";
            smell5Direction = "right";
            smell6Direction = "up";
            smell7Direction = "up";
        }
        else if (posX == 1 && posY == 5)
        {
            smell0Direction = "right";
            smell1Direction = "right";
            smell2Direction = "right";
            smell3Direction = "up";
            smell4Direction = "right";
            smell5Direction = "right";
            smell6Direction = "right";
            smell7Direction = "right";
        }
    }

    public void ChangeNode(Vector2 dir)
    {

        if (GetMazePieceForPosition(posX, posY) == "crossroad_start")
        {
            posX = 3;
            posY = 2;
        }

        int nextX = posX + (int)dir.x;
        int nextY = posY + (int)dir.y;

        string direccion;

        if (nextX > posX)
        {
            direccion = "right";
        }
        else if (nextX < posX)
        {
            direccion = "left";
        }
        else if (nextY > posY)
        {
            direccion = "up";
        }
        else if (nextY < posY)
        {
            direccion = "down";
        }
        else
        {
            string err = string.Format("Invalid node pos -> current = ({0}, {1}), next = ({2}, {3})", posX, posY, nextX, nextY);
            throw new System.Exception(err);
        }

        string nextPiece = GetMazePieceForPosition(nextX, nextY);
        string debug = "Going from ({0}, {1}) to ({2}, {3}) -> {4}";
        Debug.Log(string.Format(debug, posX, posY, nextX, nextY, nextPiece));

        directionSceneChange = direccion;
        SceneManager.LoadScene(nextPiece);

        posX = nextX;
        posY = nextY;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name.ToUpper().Contains("CROSSROAD") && !scene.name.ToUpper().Equals("CROSSROAD_FINAL"))
        {

            if (scene.name.ToString().ToUpper().Equals("CROSSROAD_START"))
            {
                posX = 3;
                posY = 2;
                Debug.Log(lastMap);
                if((!Database.instance.CheckEvent("evt_artist_2_house_unlocked") && Database.instance.CheckEvent("evt_artist_1_clue_tobacco_smell"))
                    && (lastMap == null || !lastMap.ToUpper().Contains("CROSSROAD")))
                {
                    InteractionManager.Instance.PlayInteraction(new Interactive()
                    {
                        interactionId = "int_detective_crossroad"
                    });
                }
            }

            Player player = GameObject.FindObjectOfType<Player>();

            if (directionSceneChange == "left")
            {
                player.transform.position = GameObject.Find("exit_right").transform.position;
            }
            else if (directionSceneChange == "right")
            {
                player.transform.position = GameObject.Find("exit_left").transform.position;
            }
            else if (directionSceneChange == "up")
            {
                player.transform.position = GameObject.Find("exit_top").transform.position;
            }
            else if (directionSceneChange == "down")
            {
                player.transform.position = GameObject.Find("exit_bot").transform.position;
            }
            else
            {
                string err = string.Format("Invalid direction ( {0} )", directionSceneChange);
                throw new System.Exception(err);
            }

            GameObject smell0 = Instantiate(smellSpawnerPrefab);
            smell0.GetComponent<PathfindingParticles>().color = new Color(1, 0, 1, 0.3f);
            smell0.GetComponent<PathfindingParticles>().timeToReach = 2;
            smell0.name = "Olorcillo 0";
            GameObject smell1 = Instantiate(smellSpawnerPrefab);
            smell1.GetComponent<PathfindingParticles>().color = new Color(0.9f, 0.95f, 0, 0.3f);
            smell1.GetComponent<PathfindingParticles>().timeToReach = 2;
            smell1.name = "Olorcillo 1";
            GameObject smell2 = Instantiate(smellSpawnerPrefab);
            smell2.GetComponent<PathfindingParticles>().color = new Color(0.95f, 0, 0.05f, 0.3f);
            smell2.GetComponent<PathfindingParticles>().timeToReach = 2;
            smell2.name = "Olorcillo 2";
            GameObject smell3 = Instantiate(smellSpawnerPrefab);
            smell3.GetComponent<PathfindingParticles>().color = new Color(1, 0.7f, 0.45f, 0.3f);
            smell3.GetComponent<PathfindingParticles>().timeToReach = 2;
            smell3.name = "Olorcillo 3";
            GameObject smell4 = Instantiate(smellSpawnerPrefab);
            smell4.GetComponent<PathfindingParticles>().color = new Color(0.49f, 0.47f, 1, 0.3f);
            smell4.GetComponent<PathfindingParticles>().timeToReach = 2;
            smell4.name = "Olorcillo 4";
            GameObject smell5 = Instantiate(smellSpawnerPrefab);
            smell5.GetComponent<PathfindingParticles>().color = new Color(0.85f, 0.85f, 0.85f, 0.3f);
            smell5.GetComponent<PathfindingParticles>().timeToReach = 2;
            smell5.name = "Olorcillo 5";
            GameObject smell6 = Instantiate(smellSpawnerPrefab);
            smell6.GetComponent<PathfindingParticles>().color = new Color(0.95f, 0.5f, 0, 0.3f);
            smell6.GetComponent<PathfindingParticles>().timeToReach = 2;
            smell6.name = "Olorcillo 6";
            GameObject smell7 = Instantiate(smellSpawnerPrefab);
            smell7.GetComponent<PathfindingParticles>().color = new Color(0.38f, 0.65f, 0.25f, 0.3f);
            smell7.GetComponent<PathfindingParticles>().timeToReach = 2;
            smell7.name = "Olorcillo 7";

            if (scene.name == "crossroad_start")
            {
                Vector3 positionPlus = new Vector3(smell0.transform.position.x, smell0.transform.position.y + 1f, smell0.transform.position.z);
                smell0.transform.position = positionPlus;
                positionPlus = new Vector3(smell1.transform.position.x, smell1.transform.position.y + 1f, smell1.transform.position.z);
                smell1.transform.position = positionPlus;
                positionPlus = new Vector3(smell2.transform.position.x, smell2.transform.position.y + 1f, smell2.transform.position.z);
                smell2.transform.position = positionPlus;
                positionPlus = new Vector3(smell3.transform.position.x, smell3.transform.position.y + 1f, smell3.transform.position.z);
                smell3.transform.position = positionPlus;
                positionPlus = new Vector3(smell4.transform.position.x, smell4.transform.position.y + 1f, smell4.transform.position.z);
                smell4.transform.position = positionPlus;
                positionPlus = new Vector3(smell5.transform.position.x, smell5.transform.position.y + 1f, smell5.transform.position.z);
                smell5.transform.position = positionPlus;
                positionPlus = new Vector3(smell6.transform.position.x, smell6.transform.position.y + 1f, smell6.transform.position.z);
                smell6.transform.position = positionPlus;
                positionPlus = new Vector3(smell7.transform.position.x, smell7.transform.position.y + 1f, smell7.transform.position.z);
                smell7.transform.position = positionPlus;
            }

            List<GameObject> smellSpawners = new List<GameObject>()
            {
                smell0,smell1,smell2,smell3,
                smell4,smell5,smell6,smell7
            };

            GenerateSmellTraces();

            Player p = FindObjectOfType<Player>();

            for (int i = 0; i < 8; i++)
            {
                GameObject smellSpawner;
                string position;

                if (i == 0)
                {
                    smellSpawner = smell0;
                    position = smell0Direction;
                }
                else if (i == 1)
                {
                    smellSpawner = smell1;
                    position = smell1Direction;
                }
                else if (i == 2)
                {
                    smellSpawner = smell2;
                    position = smell2Direction;
                }
                else if (i == 3)
                {
                    smellSpawner = smell3;
                    position = smell3Direction;
                }
                else if (i == 4)
                {
                    smellSpawner = smell4;
                    position = smell4Direction;
                }
                else if (i == 5)
                {
                    smellSpawner = smell5;
                    position = smell5Direction;
                }
                else if (i == 6)
                {
                    smellSpawner = smell6;
                    position = smell6Direction;
                }
                else
                {
                    smellSpawner = smell7;
                    position = smell7Direction;
                }

                Vector3 pos;

                if (position == "up")
                {
                    if (scene.name == "crossroad_final")
                    {
                        pos = GameObject.Find("SmellFromHouse").transform.position;
                    }
                    else
                    {
                        pos = topSideSmellSource.transform.position;
                    }
                }
                else if (position == "down")
                {
                    pos = downSideSmellSource.transform.position;
                }
                else if (position == "left")
                {
                    pos = leftSideSmellSource.transform.position;
                }
                else if (position == "right")
                {
                    pos = rightSideSmellSource.transform.position;
                }
                else
                {
                    Debug.LogError("Unknown position " + position);
                    return;
                }

                smellSpawner.transform.position = pos;
            }
        }
        lastMap = SceneManager.GetActiveScene().name;

    }

    string GetMazePieceForPosition(int x, int y)
    {
        if ((x == 3 && y == 0) ||
            (x == 2 && y == 2) ||
            (x == 4 && y == 2) ||
            (x == 4 && y == 4))
        {
            return "crossroad_1";
        }

        if ((x == 3 && y == 1) ||
            (x == 1 && y == 3) ||
            (x == 3 && y == 3) ||
            (x == 2 && y == 4))
        {
            return "crossroad_2";
        }

        if ((x == 2 && y == 0) ||
            (x == 1 && y == 2))
        {
            return "crossroad_3";
        }

        if ((x == 2 && y == 1) ||
            (x == 1 && y == 4) ||
            (x == 2 && y == 5))
        {
            return "crossroad_4";
        }

        if ((x == 2 && y == 1) ||
            (x == 1 && y == 4) ||
            (x == 5 && y == 4) ||
            (x == 1 && y == 0) ||
            (x == 5 && y == 2))
        {
            return "crossroad_5";
        }

        if ((x == 1 && y == 1) ||
            (x == 2 && y == 3) ||
            (x == 0 && y == 4) ||
            (x == 3 && y == 4) ||
            (x == 4 && y == 5))
        {
            return "crossroad_6";
        }

        if (x == 0 && y == 3)
        {
            return "crossroad_7";
        }

        if (x == 1 && y == 5)
        {
            return "crossroad_final";
        }

        if (x == 3 && y == 2)
        {
            return "crossroad_start";
        }

        Debug.Log("Back to start");

        return "crossroad_start";
    }
}
