using UnityEngine;
using System.Collections;

public class PillarSpawner : MonoBehaviour {

    public GameObject pillar;
    public float interval = 1f;

    float z = 0f;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 50; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        float y = Random.Range(0f, 5f);
        y = 0f;
        Instantiate(pillar, new Vector3(0, y, z), Quaternion.identity);
        z += 1f;
//        z += Random.Range(3f, 10f);
    }
}
