using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{

    // Use this for initializations
    public void BackToStart()
    {
        SceneManager.LoadScene("StartGame");
    }
}
