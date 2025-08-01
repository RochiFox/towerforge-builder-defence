using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;

    private TextMeshProUGUI soundVolumeText;
    private TextMeshProUGUI musicVolumeText;

    private void Awake()
    {
        soundVolumeText = transform.Find("Sound Volume Text").GetComponent<TextMeshProUGUI>();
        musicVolumeText = transform.Find("Music Volume Text").GetComponent<TextMeshProUGUI>();

        transform.Find("Sound Increase Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            soundManager.IncreaseVolume();
            UpdateText();
        });

        transform.Find("Sound Decrease Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            soundManager.DecreaseVolume();
            UpdateText();
        });

        transform.Find("Music Increase Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            musicManager.IncreaseVolume();
            UpdateText();
        });

        transform.Find("Music Decrease Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            musicManager.DecreaseVolume();
            UpdateText();
        });

        transform.Find("Main Menu Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        transform.Find("Edge Scrolling Toggle").GetComponent<Toggle>().onValueChanged.AddListener((bool set) =>
        {
            CameraHandler.Instance.SetEdgeScrolling(set);
        });
    }

    private void Start()
    {
        UpdateText();
        gameObject.SetActive(false);

        transform.Find("Edge Scrolling Toggle").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrolling());
    }

    private void UpdateText()
    {
        soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetVolume() * 10).ToString());
        musicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10).ToString());
    }

    public void ToggleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
