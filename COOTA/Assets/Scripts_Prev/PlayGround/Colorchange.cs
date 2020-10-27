using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace prevScript
{
    public class Colorchange : MonoBehaviour
    {
        public Image Panel, Panel1, Panel2, Panel3, Panel4;

        public float time, time1, time2, time3, time4;
        float F_time = 2f;
        float F_time1 = 1f;
        CircleCollider2D cd;
        public BoxCollider2D cd1;
        private void Start()
        {
            cd = GetComponent<CircleCollider2D>();

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                cd.enabled = false;
                cd1.enabled = false;
                Fadein();
            }
        }
        public void Fadein()
        {

            StartCoroutine(FadeFlow());
        }
        IEnumerator FadeFlow()
        {

            Panel1.gameObject.SetActive(true);
            Color alpha = Panel.color;
            Color beta = Panel1.color;
            Color ceta = Panel2.color;
            Color d = Panel3.color;
            Color e = Panel4.color;
            time = 0f;
            time1 = 0f;
            time2 = 0f;
            time3 = 0f;
            time4 = 0f;
            yield return new WaitForSeconds(0f);
            //black
            while (beta.a < 1f)
            {
                time1 += Time.deltaTime / F_time1;
                beta.a = Mathf.Lerp(0, 1, time1);
                Panel1.color = beta;
                yield return null;
            }
            Panel.gameObject.SetActive(true);
            //playground
            while (alpha.a < 1f)
            {
                time += Time.deltaTime / F_time;
                alpha.a = Mathf.Lerp(0, 1, time);
                Panel.color = alpha;
                yield return null;
            }
            //colorchange
            Panel2.gameObject.SetActive(true);
            while (ceta.a < 1f)
            {
                time2 += Time.deltaTime / F_time;
                ceta.a = Mathf.Lerp(0, 1, time2);
                Panel2.color = ceta;
                yield return null;
            }
            while (ceta.a > 0f)
            {
                time2 -= Time.deltaTime / F_time;
                ceta.a = Mathf.Lerp(0, 1, time2);
                Panel2.color = ceta;
                yield return null;
            }
            //pointing
            Panel3.gameObject.SetActive(true);
            while (d.a < 1f)
            {
                time3 += Time.deltaTime / F_time;
                d.a = Mathf.Lerp(0, 1, time3);
                Panel3.color = d;
                yield return null;
            }
            //Patroll
            Panel4.gameObject.SetActive(true);
            while (e.a < 1f)
            {
                time4 += Time.deltaTime / F_time;
                e.a = Mathf.Lerp(0, 1, time4);
                Panel4.color = e;
                yield return null;
            }
            while (e.a > 0f)
            {
                time4 -= Time.deltaTime / F_time;
                e.a = Mathf.Lerp(0, 1, time4);
                d.a = Mathf.Lerp(0, 1, time4);
                Panel4.color = e;
                Panel3.color = d;
                yield return null;
            }


            while (alpha.a > 0f)
            {
                time -= Time.deltaTime / F_time1;
                alpha.a = Mathf.Lerp(0, 1, time);
                beta.a = Mathf.Lerp(0, 1, time);
                Panel.color = alpha;
                Panel1.color = beta;
                yield return null;
            }
            Panel.gameObject.SetActive(false);
            Panel1.gameObject.SetActive(false);
            Panel2.gameObject.SetActive(false);
            Panel3.gameObject.SetActive(false);
            Panel4.gameObject.SetActive(false);
        }

    }
}
