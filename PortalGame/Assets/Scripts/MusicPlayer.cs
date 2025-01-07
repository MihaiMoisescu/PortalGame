using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Face obiectul persistent între scene
        }
        else
        {
            Destroy(gameObject); // Evită duplicarea obiectului la revenirea în scenă
        }
    }
}
