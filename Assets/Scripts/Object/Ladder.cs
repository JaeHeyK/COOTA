using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum LadderPart { complete, bottom, top };
    [SerializeField] LadderPart part = LadderPart.complete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();

            switch (part)
            {
                case LadderPart.complete:
                    player.CanClimb = true;
                    break;
                case LadderPart.bottom:
                    player.OnBotLadder = true;
                    break;
                case LadderPart.top:
                    player.OnTopLadder = true;                    
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();

            switch (part)
            {
                case LadderPart.complete:
                    player.CanClimb = false;
                    break;
                case LadderPart.bottom:
                    player.OnBotLadder = false;
                    break;
                case LadderPart.top:
                    player.OnTopLadder = false;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + (Vector3)collider.offset, collider.size);
    }
}
