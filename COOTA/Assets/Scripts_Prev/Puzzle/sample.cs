using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sample : MonoBehaviour
{
    // Start is called before the first frame update
   public void Popup()
    {
        SceneManager.LoadScene("NumberPuzzle", LoadSceneMode.Additive);
    }
}
