using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToAlley : MonoBehaviour
{
    public GameObject player;
    private bool isTouching = false;
    public Image Panel;
    float time = 0f;
    float F_time = 0.5f;
    public Rigidbody2D ri;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Touching");
            isTouching = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Detached");
            isTouching = false;
        }

    }

   
    // Update is called once per frame
    void Update()
    {
        if (isTouching == true && Input.GetButtonDown("Fire1"))
            Fade();

    }
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        float time = 0;
        ri.isKinematic = false;
        while (player.transform.localScale.x > 0.7f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            player.transform.localScale = Vector3.one * (1 - time); // Player being small
            time += Time.deltaTime;
        }


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
        SceneManager.LoadScene("Alley");

    }
}