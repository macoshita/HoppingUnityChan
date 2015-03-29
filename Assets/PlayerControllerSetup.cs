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

    JumpState jumpState = JumpState.None;

	enum JumpState { None, WillJump, Jumping };

    private Animator animator;

    // Use this for initialization
    void Start () {
        character = GetComponent<CharacterController> ();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {
        if (character.isGrounded) {
			velocity = new Vector3(0, 0, 0);
			animator.SetBool("Grounded", true);

            if (jumpState == JumpState.None) {
                if (Input.GetKey(KeyCode.Space)) {  // スペースキーを押し続けて
                    jumpSpeed += 20F * Time.deltaTime;  // ジャンプ力をためる
                }
                if (Input.GetKeyUp(KeyCode.Space)) {    // スペースキーを離すと
                    jumpState = JumpState.WillJump;
                    animator.SetBool("Jumping", true); // ジャンプモーションに入る
                }
            } else if (jumpState == JumpState.Jumping) {
				jumpState = JumpState.None;
			}
        } else {
            animator.SetBool("Grounded", false);

			// 着地判定
			if (jumpState == JumpState.Jumping && animator.GetBool("Jumping") && velocity.y < 0) {
				var hit = new RaycastHit ();
				if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
					if (hit.distance < -velocity.y * 0.5f) {
						animator.SetBool ("Jumping", false); // ジャンプモーション終了
					}
				}

			}
		}

        velocity.y -= gravity * Time.deltaTime;        // 移動量に重力を加える

        CollisionFlags flag = character.Move (velocity * Time.deltaTime);
    }

    // 足が離れる直前に呼び出される
    void OnJumpStart()
    {
        velocity = new Vector3(0, jumpSpeed + 7F, 4F);
		CollisionFlags flag = character.Move(velocity * Time.deltaTime);
        jumpSpeed = 0F;
		jumpState = JumpState.Jumping;
    }


}
