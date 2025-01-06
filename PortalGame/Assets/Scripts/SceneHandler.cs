using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadScene(string name)
    {
        Debug.Log("Buton apasat");
        SceneManager.LoadScene(name);
    }
}
