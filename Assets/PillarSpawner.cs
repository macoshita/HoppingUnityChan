using UnityEngine;
using System.Collections;

public class PillarSpawner : MonoBehaviour {

    public GameObject pillar;
    public float interval = 1f;

    float z = 0f;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 20; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        //float y = Random.Range(0f, 5f);
		float y = 0f;
		Instantiate(pillar, new Vector3(0, y, z), Quaternion.identity);
    //    z += Random.Range(3f, 8f);
		z += 1f;
	}
}
