using UnityEngine;
using System.Collections;

public class PillarController : MonoBehaviour {

    // 柱を生成するスクリプト
    GameObject pillarSpawner;

    // Use this for initialization
	void Start () {
        pillarSpawner = GameObject.Find("PillarSpawner");
    }
	
	// Update is called once per frame
	void Update () {
        if (isOutOfScreen())
        {
            Destroy(gameObject);

            // 新しい柱を生成する
            pillarSpawner.SendMessage("Spawn");
        }
	}

    bool isOutOfScreen()
    {
        Vector3 positionInScreen = Camera.main.WorldToViewportPoint(transform.position);

        return positionInScreen.x <= -0.1F;
    }
}
