using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas _endgameWindow;
    public Menu Menu;

    private bool _isEndgame;
    private bool _isMenuOpen = true;

    [SerializeField] private Canvas _menuCanvas;

    public bool IsMenuOpen => _isMenuOpen;

    [SerializeField] private Slider _msSlider;
    [SerializeField] private Text _msValueText;

    [SerializeField] private Slider _tacSlider;
    [SerializeField] private Text _tacValueText;

    [SerializeField] private Slider _tetSlider;
    [SerializeField] private Text _tetValueText;

    [SerializeField] private Toggle _automodeToggle;

    [SerializeField] private CrosshairManager _crosshairManager;

    [SerializeField] private Text _endgameText;
    [SerializeField] private Text _scoreText;

    private void Awake()
    {
        _endgameWindow.gameObject.SetActive(false);
        Menu = new Menu(new Settings(0, 0, 0, false), _menuCanvas);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _crosshairManager.Disable();
    }

    public void ShowEndgameWindow()
    {
        _endgameWindow.gameObject.SetActive(true);
        _isEndgame = true;
        _isMenuOpen = true;
        _crosshairManager.Disable();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _endgameText.text = _scoreText.text;
    }

    public void ToggleMenu()
    {
        if (!_isEndgame)
            Menu.UI.gameObject.SetActive(_isMenuOpen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isEndgame)
        {
            if (!_isMenuOpen)
            {
                _isMenuOpen = true;
                _crosshairManager.Disable();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                _isMenuOpen = false;
                _crosshairManager.Enable();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            ToggleMenu();
        }
    }

    public void OnSliderChange()
    {
        _msValueText.text = _msSlider.value.ToString();
        Menu.Settings.MovementSpeed = _msSlider.value;
    }

    public void OnTargetAppearanceSliderChange()
    {
        _tacValueText.text = _tacSlider.value.ToString() + '%';
        Menu.Settings.TargetProcChance = (int)_tacSlider.value;
    }

    public void OnTargetExpireSliderChange()
    {
        _tetValueText.text = _tetSlider.value.ToString() + 's';
        Menu.Settings.TargetExpireTime = _tetSlider.value;
    }

    public void OnAutomodeChange()
    {
        Menu.Settings.IsAutoMode = _automodeToggle.isOn;
    }

    public void OnRestart()
    {
        GameManager.ReloadGame();
    }

    public void OnExit()
    {
        Application.Quit();
    }
}

public class Menu
{
    public Canvas UI;
    public Settings Settings;

    public Menu(Settings settings, Canvas ui)
    {
        UI = ui;
        Settings = settings;
    }
}

public class Settings
{
    public float MovementSpeed { get; set; }
    public float TargetExpireTime { get; set; }
    public int TargetProcChance { get; set; }
    public bool IsAutoMode { get; set; }

    public Settings(float movementSpeed, float targetExpireTime, int targetProcChance, bool isAutoMode)
    {
        MovementSpeed = movementSpeed;
        TargetExpireTime = targetExpireTime;
        TargetProcChance = targetProcChance;
        IsAutoMode = isAutoMode;
    }
}
