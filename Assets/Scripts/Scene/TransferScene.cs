using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferScene : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField]
    private string transferSceneName = "";   // 로드 할 씬 이름
    [SerializeField]
    private Vector2 startPosition = Vector2.zero;  // 로드된 씬 시작 위치

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (CustomSceneManager.Instance.LoadScene(transferSceneName))
            {
                // 해당 씬 시작위치로 이동
                collision.transform.position = startPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)collider.offset, collider.size);
    }
}
