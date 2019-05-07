using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private string SceneToLoad;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneToLoad);
    }


}
