using UnityEngine;
using System.Collections;

public class PlayerControllerSetup : MonoBehaviour {

    // 重力
    public float gravity = 20F;

    // キャラクターコントローラー
    CharacterController character;
    // 移動量
    Vector3 velocity;
    // ジャンプ力
    float jumpSpeed = 0F;

    // Use this for initialization
    void Start () {
        character = GetComponent<CharacterController> ();
    }
    
    // Update is called once per frame
    void Update () {
        if (character.isGrounded) {
            velocity = new Vector3(0, 0, 0);    // 上下のキー入力からZ軸方向の移動量を取得
            // キャラクターのローカル空間での方向に変換
            velocity = transform.TransformDirection(velocity);
            
            if (Input.GetKey(KeyCode.Space)) {  // スペースキーを押し続けて
                jumpSpeed += 20F * Time.deltaTime;  // ジャンプ力をためる
            }
            if (Input.GetKeyUp(KeyCode.Space)) {    // スペースキーを離すと
                velocity.y = jumpSpeed + 1F;    // 移動量のY軸方向にジャンプ力をセット
                velocity.z = 5F;
                jumpSpeed = 0F;
            }
        }

        velocity.y -= gravity * Time.deltaTime;        // 移動量に重力を加える

        CollisionFlags flag = character.Move (velocity * Time.deltaTime);
    }
}
