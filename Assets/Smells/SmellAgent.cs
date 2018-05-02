using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmellAgent : MonoBehaviour
{
    public float smellRectangleDuration;
    float timerSmellRectangle;
    NavMeshAgent _navMeshAgent;
    Transform _playerTransform;
    Player player;
    PlayerInteractionController interactionController;

    // Use this for initialization
    void Start () {
        timerSmellRectangle = 0;
        player = GameObject.FindObjectOfType<Player>();
        interactionController = player.GetComponentInChildren<PlayerInteractionController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerTransform = player.transform;
        Destroy(this.gameObject, 12);
    }

    private void SetDestination()
    {
        if (_playerTransform != null)
        {
            Vector3 targetVector = _playerTransform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update () {
        timerSmellRectangle += Time.deltaTime;
        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }
}
