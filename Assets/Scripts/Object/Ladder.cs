using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum LadderPart { complete, bottom, top };
    [SerializeField] LadderPart part = LadderPart.complete;

    private PlayerController playerController;

    void Start()
    {
        playerController = PlayerController.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (part)
            {
                case LadderPart.complete:
                    break;
                case LadderPart.bottom:
                    break;
                case LadderPart.top:
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
            switch (part)
            {
                case LadderPart.complete:
                    break;
                case LadderPart.bottom:
                    break;
                case LadderPart.top:
                    break;
                default:
                    break;
            }
        }
    }
}
