using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<GameObject> enemies;
    void Start()
    {
        Debug.Log(enemies.Count);
    }

}
