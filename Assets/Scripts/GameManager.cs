using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public enum Teams { Player, Enemy}
    public class TeamsEvent : UnityEvent<Teams> { };

    public static GameManager Instance;

    [SerializeField]
    GameObject PlayerLose;
    [SerializeField]
    GameObject EnemyLose;
    [SerializeField]
    GameObject Units;
    AIController ai;

    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ai = GetComponent<AIController>();
        SetComponents(false);
        EnemyLose.SetActive(false);
        PlayerLose.SetActive(false); 
    }

    public void SetComponents(bool value)
    {
        Units.SetActive(value);
        ai.enabled = value;
    }

    public void GameOver(Teams loser)
    {
        if(loser == Teams.Enemy)
        {
            EnemyLose.SetActive(true);
        }
        else
        {
            PlayerLose.SetActive(true);
        }
        SetComponents(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
