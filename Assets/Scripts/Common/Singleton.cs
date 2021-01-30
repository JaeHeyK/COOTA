using UnityEngine;

// Generic Singleton 정의
// Singleton은 하나만 존재해야 한다는 원칙을 준수해야 한다.
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    // Singleton 변수는 이 프로퍼티를 통해 참조한다.
    public static T Instance
    {
        get
        {             
            T[] s = FindObjectsOfType<T>();

            // 찾은 오브젝트가 1개 이상일 때 에러 메세지를 띄움
            if (s.Length > 1)
            {
                for(int i = 1; i < s.Length; i++)
                {
                    Destroy(s[i].gameObject);
                }

                Debug.LogWarning(typeof(T).ToString() + " has one more instance.");
            }
            // 해당 타입의 오브젝트가 없는 경우 null return
            else if (s.Length == 0)
            {
                Debug.LogError(typeof(T).ToString() + " is not exist.");

                return null;
            }

            instance = s[0];

            return instance;
        }
    }
}