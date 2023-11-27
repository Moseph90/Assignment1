using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum PickUpType
    {
        powerUp, life, score, magic
    }

    public PickUpType currentPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();

        if (collision.CompareTag("Player"))
        {
            switch(currentPickUp)
            {
                case PickUpType.powerUp:
                    pc.StartJumpForceChange();
                    break;
                case PickUpType.life:
                    pc.lives++;
                    break;
                case PickUpType.score:
                    pc.score++;
                    break;
                case PickUpType.magic:
                    pc.magic++;
                    break;
            }
            //Destroy(gameObject);
        }
    }
}
