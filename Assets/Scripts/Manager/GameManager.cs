using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임을 전체적으로 관리하는 매니저
public class GameManager : Singleton<GameManager>
{   
    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private PlayerController playerController = null;

    private bool bPlayerInstantiated;

    // Start is called before the first frame update
    void Start()
    {   
        Initialization();
    }

    private void Initialization() // 초기화
    {
        bPlayerInstantiated = false;

        CheckPlayerInScene();

        if (bPlayerInstantiated)
        {
            CustomSceneManager.Instance.SetCamera(playerController.gameObject);
        }
    }

    // 현재 활성화 된 씬 내에 플레이어 오브젝트가 있는지 확인
    private void CheckPlayerInScene()
    {
        playerController = PlayerController.Instance;

        // 없다면 0, 0 위치에 오브젝트 생성 (임시)
        if (playerController is null)
        {
            bPlayerInstantiated = true;
            playerController = Instantiate(playerPrefab).GetComponent<PlayerController>();
        }
    }

    public void PauseGame()  // 게임 일시정지
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame() // 게임 재개
    {
        Time.timeScale = 1f;
    }
}
