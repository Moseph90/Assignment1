using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawn : MonoBehaviour
{
    SpriteRenderer sr;

    public Transform[] PUSpawn;
    public Pickups[] pickUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        for (int i = 0; i < PUSpawn.Length; i++)
        {
            if (!PUSpawn[i] || !pickUpPrefab[i])
            {
                Debug.Log("Please Set Default Values On " + gameObject.name);
            }
            int pick = Random.Range(0, pickUpPrefab.Length);
            Pickups pickups = Instantiate(pickUpPrefab[pick], PUSpawn[i].position, PUSpawn[i].rotation);
        }
    }
}