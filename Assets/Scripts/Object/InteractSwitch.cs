using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Observer 패턴을 이용하여 PlayerController에게 상호작용 함수를 전달함
public class InteractSwitch : MonoBehaviour
{
    protected PlayerController playerController;

    [Header("Collider")]
    [SerializeField] private BoxCollider2D interactCollider2D = null;

    [Header("InteractObject")]
    [SerializeField] private GameObject goSwitch = null;        // 상호작용할 스위치
    [SerializeField] private GameObject goInteract = null;      // 상호작용하여 보여줄 오브젝트
    [SerializeField] private GameObject goNavigation = null;    // 키 조작 안내할 오브젝트

    private bool HasSwitchObject { get { return goSwitch != null; } }
    private bool HasInteractObject { get { return goInteract != null; } }
    private bool HasNavObject { get { return goNavigation != null; } }
    
    void Start()
    {
        interactCollider2D = GetComponent<BoxCollider2D>();
        playerController = PlayerController.Instance;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 스위치 작동 영역에 진입했을 시
        if (collision.CompareTag("Player"))
        {
            playerController.AddInteraction(this.name, InteractEvent);
            ShowNavigation(true);
            ShowSwitchHighlight(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 스위치 작동 영역을 벗어났을 시
        if (collision.CompareTag("Player"))
        {
            playerController.RemoveInteraction(this.name);
            ShowNavigation(false);
            ShowSwitchHighlight(false);
        }
    }

    public virtual void InteractEvent() // 스위치 작동
    {
        if (!HasInteractObject) return;

        playerController.EnableInteractionObject(goInteract);
    }
    private void ShowNavigation(bool active) // 안내 키 표시
    {
        if (!HasNavObject) return;

        goNavigation.SetActive(active);
    }
    private void ShowSwitchHighlight(bool active) // 상호작용하는 스위치 하이라이트
    {
        if (!HasSwitchObject) return;

        // 하이라이트 넣기
    }

    private void OnDrawGizmos() // Scene 뷰에 표시되는 Collider 범위
    {
        if (interactCollider2D == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + interactCollider2D.offset, interactCollider2D.size);
    }
}
