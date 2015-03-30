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
	enum JumpState { Grounded, WillJump, Jumping };

    // Use this for initialization
    void Start () {
        character = GetComponent<CharacterController> ();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {
		if (jumpState == JumpState.Grounded) {
			// 着地状態ならジャンプできる
			if (Input.GetKeyDown (KeyCode.Space)) {
				// スペースキーを押したらジャンプの準備
				jumpState = JumpState.WillJump;
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
				animator.speed = 1f;
			}
		} else if (jumpState == JumpState.Jumping) {
			if (character.isGrounded) {
				velocity = new Vector3(0, 0, 0);
				jumpState = JumpState.Grounded;
			}
		}

		if (jumpState == JumpState.Grounded) {
			animator.speed = 1f;
			animator.SetBool ("Falling", false);
		} else if (jumpState == JumpState.WillJump) {
			animator.SetBool ("WillJump", true);	
		} else if (jumpState == JumpState.Jumping) {
			animator.SetBool ("WillJump", false);
			if (velocity.y < 0) {
				animator.SetBool ("Falling", true);
			}
		}

        velocity.y -= gravity * Time.deltaTime;        // 移動量に重力を加える

        CollisionFlags flag = character.Move (velocity * Time.deltaTime);
    }

	void OnJumpStart()
	{
		animator.speed = 0.25f;
	}

	void OnMaxJump()
	{
		if (jumpState == JumpState.WillJump) {
			animator.speed = 0;
		}
	}

    void OnLeaveFeet()
    {
		velocity = new Vector3(0, jumpSpeed + 5F, 4F);
		jumpSpeed = 0F;
		CollisionFlags flag = character.Move (velocity * Time.deltaTime);
		jumpState = JumpState.Jumping;
	}

	// アニメーション的に完全に落ちはじめた時に呼び出される
	void OnFalling()
	{
		if (jumpState == JumpState.Jumping) {
			animator.speed = 0f;
		}
	}
}
