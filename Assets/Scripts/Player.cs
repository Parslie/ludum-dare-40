using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Player : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight, jumpTime;
    private float jumpVelocity, gravity;
    private Vector2 velocity;
    private bool canAirJump;

    [Header("Graphics")]
    [SerializeField]
    private Animator anim;

    private Controller cont;

    private void Start()
    {
        cont = GetComponent<Controller>();

        // d = 0.5 * a * t * t   <=>   a = d / (0.5 * t * t)
        gravity = jumpHeight / (0.5f * Mathf.Pow(jumpTime, 2));
        // v = a * t
        jumpVelocity = gravity * jumpTime;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        velocity.x = (GameManager.gameState == GameManager.GameState.Playing) ? moveSpeed : 0;

        if (GameManager.gameState == GameManager.GameState.Playing)
        {
            if (cont.collInfo.bottom || cont.collInfo.top)
            {
                velocity.y = 0;
                canAirJump = true;
            }
            velocity.y -= gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.DownArrow) && gravity < 0 || Input.GetKeyDown(KeyCode.UpArrow) && gravity > 0)
            {
                StopAllCoroutines();
                StartCoroutine(TryJump());
            }

            cont.Move(velocity * Time.deltaTime);
        }
    }

    private void Reverse()
    {
        moveSpeed *= -1;
    }

    private void InverseGravity()
    {
        gravity *= -1;
        jumpVelocity *= -1;

        anim.transform.localScale = (gravity < 0) ? Vector3.right * 180 : Vector3.zero;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator TryJump()
    {
        for (float i = 0; i < 0.05f; i += Time.deltaTime)
        {
            if (cont.collInfo.bottom && gravity > 0 || cont.collInfo.top && gravity < 0 || canAirJump)
            {
                velocity.y = jumpVelocity;
                cont.collInfo.bottom = false;
                cont.collInfo.top = false;
                canAirJump = false;
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
