using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHNGPickUp : MonoBehaviour
{
    public GameObject barrier;
    public SpriteRenderer starSprite;

    private KHNGGameController gameControllerReference;
    private AudioSource soundSource;

    private bool destroy;
    private bool blinking;

    private void Awake()
    {
        barrier.SetActive(true);

        destroy = false;
    }

    private void Start()
    {
        soundSource = GetComponent<AudioSource>();

        gameControllerReference = FindObjectOfType<KHNGGameController>();
    }

    private void Update()
    {
        if (destroy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if(gameControllerReference.canMove && !blinking)
            {
                StartCoroutine(NoticeMeSenpai());
            }
        }
    }

    IEnumerator NoticeMeSenpai()
    {
        blinking = true;
        starSprite.enabled = true;

        yield return new WaitForSeconds(.4f);

        starSprite.enabled = false;

        yield return new WaitForSeconds(.4f);

        blinking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            soundSource.Play();

            destroy = true;

            barrier.SetActive(false);
        }
    }
}
