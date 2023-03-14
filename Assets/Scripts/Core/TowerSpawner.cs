using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : Singleton<TowerSpawner>
{
    public List<BehaviourController> lsTowers { get; private set; } = new List<BehaviourController>();
    
    [SerializeField] List<Transform> lsTowerSlots = new List<Transform>();

    [SerializeField] private Transform gameGround;
    public void SpawnTower()
    {
        if (lsTowerSlots.Count == 0) return;
        int randomTowerSlotIndex = Random.Range(0, lsTowerSlots.Count);
        Transform randomSlot = lsTowerSlots[randomTowerSlotIndex];
        Tower tower = PoolHandler.I.GetObject<Tower>();
        lsTowers.Add(tower);
        tower.transform.position = randomSlot.position;
        tower.transform.SetParent(gameGround);
        lsTowerSlots.RemoveAt(randomTowerSlotIndex);
    }

    
}
