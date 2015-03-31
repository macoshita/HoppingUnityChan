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

    private Animator animator;

	JumpState jumpState = JumpState.Grounded;
	enum JumpState {
		Grounded = 0,
		WillJump = 1,
		Jumping = 2,
		Falling = 3
	};

    // Use this for initialization
    void Start () {
        character = GetComponent<CharacterController> ();
        animator = GetComponent<Animator>(); 
    }
    
    // Update is called once per frame
    void Update () {
		if (character.isGrounded) {
			velocity = new Vector3(0, 0, 0);

			if (jumpState == JumpState.Grounded) {
				// 着地状態ならジャンプできる
				if (Input.GetKeyDown (KeyCode.Space)) {
					// スペースキーを押したらジャンプの準備
					jumpState = JumpState.WillJump;
					animator.speed = 0.25f;
				}
			} else if (jumpState == JumpState.WillJump) {
				if (Input.GetKey (KeyCode.Space)) {
					// スペースキーを押し続けてジャンプ力を溜める
					jumpSpeed += 20F * Time.deltaTime;
					if (jumpSpeed > 15f) {
						// 一定量以上は溜まらない
						jumpSpeed = 15f;
					}
					
				} else {
					// スペースキーを離したらジャンプ
					velocity = new Vector3(0, jumpSpeed + 5F, 4F);
					jumpSpeed = 0F;
					jumpState = JumpState.Jumping;
					animator.speed = 1f;
				}
			} else if (jumpState == JumpState.Falling) {
				jumpState = JumpState.Grounded;
			}

		} else {
			if (velocity.y < 0) {
				jumpState = JumpState.Falling;
			}
		}

		Debug.Log((int)jumpState);

  		animator.SetInteger("JumpState", (int)jumpState);

        velocity.y -= gravity * Time.deltaTime;        // 移動量に重力を加える

        CollisionFlags flag = character.Move (velocity * Time.deltaTime);
    }

	// アニメーション的に完全に落ちはじめた時に呼び出される
	void OnFalling()
	{
		if (jumpState == JumpState.Jumping) {
			animator.speed = 0f;
		}
	}
}
