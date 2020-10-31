using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace prevScript
{
    public class Stop_player : MonoBehaviour
    {
        public Image Panel;
        public Text text;
        float time = 0f;
        float F_time = 1f;
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
            Color beta = text.color;
            Panel.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            time = 0f;
            while (alpha.a < 1f && beta.a < 1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time);
                beta.a = Mathf.Lerp(0, 1, time);
                Panel.color = alpha;
                text.color = beta;
                yield return null;
            }
            time = 0f;
            yield return new WaitForSeconds(0f);
            while (alpha.a > 0f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(1, 0, time);
                beta.a = Mathf.Lerp(1, 0, time);
                Panel.color = alpha;
                text.color = beta;
                yield return null;
            }
            Panel.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            yield return null;

        }
    }
}
