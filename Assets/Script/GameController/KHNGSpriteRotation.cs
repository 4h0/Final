using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHNGSpriteRotation : MonoBehaviour
{
    private KHNGGameController gameControllerReference;

    private void Start()
    {
        gameControllerReference = FindObjectOfType<KHNGGameController>();
    }

    private void Update()
    {
        if (gameControllerReference.canMove)
        {
            transform.Rotate(new Vector3(Random.Range(45f, 180f), 0, Random.Range(45f, 180f)) * Time.deltaTime);
        }
    }
}
