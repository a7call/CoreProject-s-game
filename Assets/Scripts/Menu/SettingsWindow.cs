using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem;
//using TMPro;

public class SettingsWindow : MonoBehaviour
{
    public AudioMixer MainAudioMixer;

    
    public Text Dash,Up,Down,Left,Right,Shoot,Reload,UseObject,BlackHole;
    private GameObject CurrentKey = null;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    

    Resolution[] resolutions;


    public Dropdown resoltionDropdown;

    public void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resoltionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
                print(currentResolutionIndex);
            }
        }

        resoltionDropdown.AddOptions(options);
        resoltionDropdown.value = currentResolutionIndex;
        resoltionDropdown.RefreshShownValue();

        Screen.fullScreen = true;

        keys.Add("Dash", KeyCode.W);
        Dash.text = keys["Dash"].ToString();

        keys.Add("Up", KeyCode.Z);
        Up.text = keys["Up"].ToString();

        keys.Add("Down", KeyCode.S);
        Down.text = keys["Down"].ToString();

        keys.Add("Right", KeyCode.D);
        Right.text = keys["Right"].ToString();

        keys.Add("Left", KeyCode.Q);
        Left.text = keys["Left"].ToString();

        keys.Add("Shoot", KeyCode.Mouse0);
        Shoot.text = keys["Shoot"].ToString();

        keys.Add("Reload", KeyCode.R);
        Reload.text = keys["Reload"].ToString();

        keys.Add("BlackHole", KeyCode.Space);
        BlackHole.text = keys["BlackHole"].ToString();

        keys.Add("UseObject", KeyCode.U);
        UseObject.text = keys["UseObject"].ToString();

        

        
    }

    public void SetMainVolume(float volume)
    {
        MainAudioMixer.SetFloat("MainVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        MainAudioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetEffectVolume(float volume)
    {
        MainAudioMixer.SetFloat("EffectsVolume", volume);
    }

    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    protected void OnGUI()
    {
        if (CurrentKey != null)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                keys[CurrentKey.name] = e.keyCode;
                CurrentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                CurrentKey = null;

                
            }
            else if (e.isMouse)
            {
                if (e.button == 0)
                {
                    keys[CurrentKey.name] = KeyCode.Mouse0;
                    CurrentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse0";
                }
                if (e.button == 1)
                {
                    keys[CurrentKey.name] = KeyCode.Mouse1;
                    CurrentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse1";
                }

                CurrentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        CurrentKey = clicked;
        StartRebindProcess();
        
    }

    public InputActionAsset playerControls;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private InputAction actionToRebind;
    private InputActionMap gameplayActionMap;

    private void Awake()
    {
        gameplayActionMap = playerControls.FindActionMap("Player");

    }

    public void StartRebindProcess()
    {
        //print("0");
        actionToRebind = gameplayActionMap.FindAction(CurrentKey.name);
        //print("1");
        actionToRebind.actionMap.Disable();
        rebindOperation = actionToRebind.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .WithControlsExcluding("<Gamepad>/Start")
            .WithControlsExcluding("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindCompleted());
        //print("2");

        rebindOperation.Start();
        //print("22");
        
    }

    void RebindCompleted()
    {

        
        rebindOperation.Dispose();
        print(rebindOperation);
        if (rebindOperation.completed)
        {
            print("3");
        }

        rebindOperation = null;
        actionToRebind.actionMap.Enable();
    }
}
