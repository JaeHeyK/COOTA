using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBound : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (virtualCamera is null) return;

            // 카메라 우선순위 변경
            var Cameras = FindObjectsOfType<CinemachineVirtualCamera>();

            for (int i = 0; i < Cameras.Length; i++)
            {
                Cameras[i].Priority = 0;
            }

            virtualCamera.Priority = 10;
        }
    }
}
