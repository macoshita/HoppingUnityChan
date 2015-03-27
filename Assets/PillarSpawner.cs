using UnityEngine;
using System.Collections;

public class PillarSpawner : MonoBehaviour {

    public GameObject pillar;
    public float interval = 1f;

    float z = 0f;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 10; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(pillar, new Vector3(0, Random.Range(0f, 5f), z), Quaternion.identity);
        z += Random.Range(3f, 10f);
    }
}
