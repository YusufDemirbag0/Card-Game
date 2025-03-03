using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject howToPlayScreen;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        howToPlayScreen.gameObject.SetActive(true); 
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
            howToPlayScreen.gameObject.SetActive(false); 
    }
}