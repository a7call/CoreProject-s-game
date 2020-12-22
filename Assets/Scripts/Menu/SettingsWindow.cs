using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem;
using TMPro;

public class SettingsWindow : MonoBehaviour
{
    public AudioMixer MainAudioMixer;

    //public InputActionMap actionMap;
    public Text Dash,Up,Down,Left,Right,Shoot,Reload,UseObject,BlackHole;
    private GameObject CurrentKey;

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

        keys.Add("Dash", KeyCode.Mouse1);
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

    private void OnGUI()
    {
        if (CurrentKey != null)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                //print(e);
                keys[CurrentKey.name] = e.keyCode;
                //print(actionMap.FindAction(CurrentKey.name));
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
        //actionName = keys[CurrentKey.name].ToString();
        //print(actionName);
    }

    //private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    //private PlayerInput focusedPlayerInput;
    //private InputAction focusedInputAction;
    //public string actionName;

    //public PlayerControl playerInput;

    //public void UpdateBehaviour()
    //{
    //    //GetFocusedPlayerInput();
    //    //SetupFocusedInputAction();
    //    //UpdateActionDisplayUI();
    //    //UpdateBindingDisplayUI();
    //}

    //void GetFocusedPlayerInput()
    //{
    //    PlayerMouvement focusedPlayerController = GameManager.Instance.GetFocusedPlayerController();
    //    focusedPlayerInput = focusedPlayerController.GetPlayerInput();
    //}

    //void SetupFocusedInputAction()
    //{
    //    focusedInputAction = PlayerAttack.actions.FindAction(actionName);
    //}

    //public void ButtonPressedStartRebind()
    //{
    //    print("1");
    //    StartRebindProcess();
    //    focusedInputAction = playerInput.FindAction(actionName);

    //}

    //public void StartRebindProcess()
    //{
    //    print("2");

    //    rebindOperation = focusedInputAction.PerformInteractiveRebinding()
    //        .WithControlsExcluding("<Mouse>/position")
    //        .WithControlsExcluding("<Mouse>/delta")
    //        .WithControlsExcluding("<Gamepad>/Start")
    //        .WithControlsExcluding("<Keyboard>/escape")
    //        .OnMatchWaitForAnother(0.1f)
    //        .OnComplete(operation => RebindCompleted());

    //    rebindOperation.Start();
    //}


    //void RebindCompleted()
    //{
    //    rebindOperation.Dispose();
    //    print(rebindOperation);
    //    rebindOperation = null;

    //}


    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    public InputAction inputAction;

    public void StartInteractiveRebind()
    {
        
        rebindOperation = inputAction.PerformInteractiveRebinding().OnComplete(operation => RebindCompleted());
        rebindOperation.Start();
    }

    void RebindCompleted()
    {
        rebindOperation.Dispose();

        //Apply UI Changes (IE: New Binding Icon)
    }

}
