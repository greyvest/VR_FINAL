using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MotherShip : MonoBehaviour
{
    public GameManager.Teams Team;

    [SerializeField]
    float MaxHealth;
    [SerializeField]
    float currentHelth;

    [SerializeField]
    GameObject Explosion;
    
    
    public float HP
    {
        get { return currentHelth; }
        private set { currentHelth = value; }
    }

    GameManager.TeamsEvent DestroyedEvent;

    // Start is called before the first frame update
    void Start()
    {
        DestroyedEvent = new GameManager.TeamsEvent();
        DestroyedEvent.AddListener(GameManager.Instance.GameOver);
        HP = MaxHealth;
    }

    /*
     * Used by units to destroy the mothership and win
     */ 
    public void TakeDamage(float dmg)
    {
        HP = HP - dmg;
        if (HP <= 0)
        {
            DestroyedEvent.Invoke(Team);
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
