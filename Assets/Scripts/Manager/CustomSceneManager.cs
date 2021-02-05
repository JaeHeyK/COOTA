using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

// 씬 전환 시 처리 사항 관리하는 매니저
public class CustomSceneManager : Singleton<CustomSceneManager>
{   
    [SerializeField]
    private CinemachineVirtualCamera[] followCameras;

    [SerializeField]
    private List<string> scenes;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        scenes = new List<string>();

        // 씬 리스트 저장
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);

            int slash = path.LastIndexOf('/') + 1;
            int dot = path.LastIndexOf('.');

            scenes.Add(path.Substring(slash, dot - slash));
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetCamera();
    }

    public bool LoadScene(string name) // 씬 전환
    {
        if (scenes.Contains(name))
        {
            SceneManager.LoadScene(name);
            return true;
        }

        return false;
    }

    public void SetCamera() // 카메라 Follow 타겟 플레이어로 설정
    {
        if (PlayerController.Instance is null) return;

        FindCameras();

        for (int i = 0; i < followCameras.Length; i++)
        {
            followCameras[i].Follow = PlayerController.Instance.gameObject.transform;
        }
    }
    public void SetCamera(GameObject go) // 카메라 Follow 타겟 설정
    {
        FindCameras();

        for (int i = 0; i < followCameras.Length; i++)
        {
            followCameras[i].Follow = go.transform;
        }
    }

    private void FindCameras() // 현재 활성화 되어있는 씬 카메라 리스트 가져오기
    {
        followCameras = FindObjectsOfType<CinemachineVirtualCamera>();
    }
}
