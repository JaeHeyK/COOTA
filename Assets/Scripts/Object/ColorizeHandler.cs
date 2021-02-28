using UnityEngine;
using UnityEngine.Playables;

//플레이어가 Collider에 감지되었을 때 ColorizePlay 타임라인을 실행시키는 핸들러
[RequireComponent(typeof(PlayableDirector))]
public class ColorizeHandler : MonoBehaviour
{
    public bool repeatable = false;
    void Start()
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && other.CompareTag("Player"))
        {
            GetComponent<PlayableDirector>().Play();
            if(!repeatable)
            {
              enabled = false;
            }
        }
    }
}
