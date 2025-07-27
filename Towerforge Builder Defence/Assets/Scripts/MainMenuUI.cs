using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("Play Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find("Quit Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
