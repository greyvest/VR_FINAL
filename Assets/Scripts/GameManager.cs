using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public enum Teams { Player, Enemy}
    public class TeamsEvent : UnityEvent<Teams> { };

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
