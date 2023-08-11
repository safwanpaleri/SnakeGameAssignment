using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    private float MaxZ = 10;
    private float MaxX = 20;
    private float MinZ = -10;
    private float MinX = -20;

    [SerializeField] public  int NoOfGoodEggs = 10;
    [SerializeField] private int NoOfPosionEggs = 5;
    [SerializeField] private GameObject GoodEgg;
    [SerializeField] private GameObject PoisonEgg;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGoodEgg();
        SpawnPoisonEgg();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGoodEgg()
    {
        for (int i = 0; i < NoOfGoodEggs; i++)
        {

            float currentX = Random.Range(MinX, MaxX);
            float currentZ = Random.Range(MinZ, MaxZ);

            Vector3 newPos = new Vector3(currentX, 0.37f, currentZ);
            Instantiate(GoodEgg, newPos, Quaternion.identity, transform);
        }

    }

    public void SpawnPoisonEgg()
    {
        for(int i = 0; i < NoOfPosionEggs; i++)
        {
            float currentX = Random.Range(MinX, MaxX);
            float currentZ = Random.Range(MinZ, MaxZ);

            Vector3 newPos = new Vector3(currentX, 0.37f, currentZ);
            Instantiate(PoisonEgg, newPos, Quaternion.identity, transform);
        }
        
    }
}
