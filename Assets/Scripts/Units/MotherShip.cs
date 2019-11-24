using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MotherShip : MonoBehaviour
{
    public GameManager.Teams Team;

    [SerializeField]
    float MaxHealth;
    float CurrentHealth;

    GameManager.TeamsEvent DestroyedEvent;

    // Start is called before the first frame update
    void Start()
    {
        DestroyedEvent = new GameManager.TeamsEvent();
        DestroyedEvent.AddListener(GameManager.Instance.GameOver);
    }

    /*
     * Used by units to destroy the mothership and win
     */ 
    public void TakeDamage(float dmg)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0)
        {
            DestroyedEvent.Invoke(Team);
            Destroy(gameObject);
        }
    }
}
