using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임을 전체적으로 관리하는 매니저
public class GameManager : Singleton<GameManager>
{
    #region Manager
    private SceneSettingManager sceneSettingManager;
    #endregion

    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private PlayerController player = null;

    // Start is called before the first frame update
    void Start()
    {
        CheckPlayerInScene();
        Initialization();
    }

    // 현재 활성화 된 씬 내에 플레이어 오브젝트가 있는지 확인
    private void CheckPlayerInScene()
    {
        player = PlayerController.Instance;

        // 없다면 0, 0 위치에 오브젝트 생성 (임시)
        if (player is null)
        {
            player = Instantiate(playerPrefab).GetComponent<PlayerController>();
        }
    }

    private void Initialization() // 초기화
    {
        sceneSettingManager = SceneSettingManager.Instance;
        sceneSettingManager.SetCamera(player.gameObject);
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
