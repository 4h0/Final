using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHNGEndPickup : MonoBehaviour
{
    private KHNGGameController gameControllerReference;
    private Rigidbody2D heartRigidBody2D;
    private AudioSource soundSource;

    private bool destroy;
    private bool ground;
    private bool moving;

    private void Awake()
    {
        heartRigidBody2D = GetComponent<Rigidbody2D>();
        soundSource = GetComponent<AudioSource>();

        destroy = false;
        ground = false;
        moving = false;
    }

    private void Start()
    {
        gameControllerReference = FindObjectOfType<KHNGGameController>();
    }

    private void Update()
    {
        if(destroy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (gameControllerReference.canMove)
            {
                if (!moving)
                {
                    if (ground)
                    {
                        StartCoroutine(Jumping());
                    }
                    else
                    {
                        StartCoroutine(Descending());
                    }
                }
            }
        }
    }

    IEnumerator Jumping()
    {
        heartRigidBody2D.gravityScale = -4f;

        moving = true;

        yield return new WaitForSeconds(.3f);

        moving = false;
    }
    IEnumerator Descending()
    {
        heartRigidBody2D.gravityScale += .6f;

        moving = true;

        yield return new WaitForSeconds(.1f);

        moving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameControllerReference.endGame = true;
            destroy = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            ground = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PickUp")
        {
            GameLoader.score++;

            soundSource.Play();

            ground = false;
        }
    }
}
