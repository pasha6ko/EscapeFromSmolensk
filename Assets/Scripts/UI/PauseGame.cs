using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerInput input;
    [Header("UI Components")]
    [SerializeField] private GameObject pause;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        continueButton.onClick.AddListener(Continue);
        exitButton.onClick.AddListener(Exit);
        pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPause()
    {
        input.enabled = false;
        pause.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Continue()
    {
        input.enabled = true;
        pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
