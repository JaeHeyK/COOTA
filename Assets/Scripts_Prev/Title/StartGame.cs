using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace prevScript
{
    public class StartGame : MonoBehaviour
    {
        public Image Panel;
        float time = 0f;
        float F_time = 1f;
        public Transform target;
        public float speed;

        public Vector2 center;
        public Vector2 size;
        float height;
        float width;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, size);
        }
        public void Fade()
        {
            StartCoroutine(FadeFlow());
        }
        IEnumerator FadeFlow()
        {

            Color alpha = Panel.color;
            Panel.gameObject.SetActive(true);
            time = 0f;
            while (alpha.a < 1f)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
                float lx = size.x * 0.5f - width;
                float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
                float ly = size.y * 0.5f - height;
                float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);
                transform.position = new Vector3(clampX, clampY, 0f);

                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time);
                Panel.color = alpha;
                yield return null;
            }

            goScene();
        }
        public void goScene()
        {
            SceneManager.LoadScene("BigRoad");

        }
    }
}
