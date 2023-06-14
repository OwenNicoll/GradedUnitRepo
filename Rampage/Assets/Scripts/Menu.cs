using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private string targetScene;
    [SerializeField] private string targetScene2;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene(targetScene);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(targetScene2);
        }
       
    }

   
}
