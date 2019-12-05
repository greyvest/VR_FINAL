using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public enum Teams { Player, Enemy}
    public class TeamsEvent : UnityEvent<Teams> { };

    [SerializeField]
    GameObject PlayerLose;
    [SerializeField]
    GameObject EnemyLose;
    [SerializeField]
    GameObject PlayerUnits;
    [SerializeField]
    GameObject EnemyUnits;
    [SerializeField]
    AIController aiController;

    public static GameManager Instance;
    
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

    public void Start()
    {
        SetGameComponentsActive(false);
        PlayerLose.SetActive(false);
        EnemyLose.SetActive(false);
    }

    public void SetGameComponentsActive(bool value)
    {
        PlayerUnits.SetActive(value);
        EnemyUnits.SetActive(value);
        aiController.enabled = value;
    }


    public void GameOver(Teams loser)
    {
        if(loser == Teams.Player)
        {
            PlayerLose.active = true;
        }
        else
        {
            EnemyLose.active = true;
        }
        SetGameComponentsActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
