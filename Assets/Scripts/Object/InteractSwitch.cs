using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSwitch : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] private BoxCollider2D interactCollider2D = null;

    [Header("InteractObject")]
    [SerializeField] private GameObject goInteract = null;    

    private bool hasInteractObject { get { return goInteract != null; } }

    void Start()
    {
        interactCollider2D = GetComponent<BoxCollider2D>();
    }

    // 스위치 작동 영역에 진입했을 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.Instance.AddInteraction(this.name, InteractEvent());
        }
    }

    // 스위치 작동 영역을 벗어났을 시
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.Instance.RemoveInteraction(this.name);
        }
    }

    // 스위치 작동 루틴
    public virtual IEnumerator InteractEvent()
    {
        if (!hasInteractObject) yield break;

        PlayerController.Instance.SetInteractionObject(goInteract);
    }

    private void OnDrawGizmos()
    {
        if (interactCollider2D == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + interactCollider2D.offset, interactCollider2D.size);
    }
}
