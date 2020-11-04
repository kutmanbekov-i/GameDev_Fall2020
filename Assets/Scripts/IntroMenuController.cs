using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenuController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
