using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputHandler : MonoBehaviour
{
    [SerializeField] private string _MenuSceneName;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (_MenuSceneName != SceneManager.GetActiveScene().name)
            {
                SceneManager.LoadSceneAsync(_MenuSceneName);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
