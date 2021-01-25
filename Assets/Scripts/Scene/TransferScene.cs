using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScene : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField]
    private string transferSceneName = "";   // 로드 할 씬 이름

    [SerializeField]
    private Transform trMovePosition = null;  // 캐릭터가 이동할 위치

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            SceneManager.LoadScene(transferSceneName);
        }
    }
}
