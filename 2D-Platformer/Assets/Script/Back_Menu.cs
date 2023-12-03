using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackGame()
    {
        SceneManager.LoadScene(0);
    }
}

