using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace prevScript
{
    public class PlaygroundToTrace : MonoBehaviour
    {
        public Image Panel;
        float time = 0f;
        float F_time = 1f;
        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Fade();
            }
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
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time);
                Panel.color = alpha;
                yield return null;
            }

            goScene();
        }
        public void goScene()
        {
            SceneManager.LoadScene("Trace");

        }

    }
}