using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public static AIController Instance;

    List<Unit> units;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        units = new List<Unit>();
    }

    public void RegisterUnit(Unit u)
    {
        units.Add(u);
    }

    public void UnRegisterUnit(Unit u)
    {
        units.Remove(u);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
