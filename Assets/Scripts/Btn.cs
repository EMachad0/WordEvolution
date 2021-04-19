using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn : MonoBehaviour
{
    public int gensPerClick;
    
    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
}
