using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

// 씬 전환 시 처리 사항 관리하는 매니저
public class SceneSettingManager : Singleton<SceneSettingManager>
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private CinemachineVirtualCamera[] cameras;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log(scene.name + " Hello~");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = PlayerController.Instance;

        SetCamera();
    }

    // 현재 활성화 되어있는 씬 카메라 리스트 가져오기
    private void FindCameras()
    {
        cameras = FindObjectsOfType<CinemachineVirtualCamera>();
    }

    // 카메라 Folow 타겟 설정
    public void SetCamera()
    {
        if (player is null) return;

        FindCameras();

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Follow = player.gameObject.transform;
        }
    }
    // 카메라 Folow 타겟 설정
    public void SetCamera(GameObject go)
    {
        FindCameras();

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Follow = go.transform;
        }
    }
}
