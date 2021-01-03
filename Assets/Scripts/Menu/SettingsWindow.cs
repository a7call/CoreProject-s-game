using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem;


public class SettingsWindow : MonoBehaviour
{
    public AudioMixer MainAudioMixer;

    Resolution[] resolutions;


    public Dropdown resoltionDropdown;

    [SerializeField] private GameObject ReloadButton, LeftButton, RightButton, UpButton, DownButton, BlackHoleButton, UseObjectButton, ShootButton, DashButton = null;
    [SerializeField] private GameObject waitingForInput = null;
   

    private InputAction actionToRebind;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private InputActionMap gameplayActionMap;
    public InputActionAsset playerControls;
    private Text TextButton = null;
    private Button buton;
    public InputActionReference Reload,Horizontal,Vertical,BlackHole,Shoot,UseObject,Dash;

    private void Awake()
    {
        gameplayActionMap = playerControls.FindActionMap("Player");

    }

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

        Text ReloadButtonText = ReloadButton.GetComponentInChildren<Text>();
        ReloadButtonText.text = InputControlPath.ToHumanReadableString(Reload.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        Text LeftButtonText = LeftButton.GetComponentInChildren<Text>();
        LeftButtonText.text = InputControlPath.ToHumanReadableString(Horizontal.action.bindings[1].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        Text RightButtonText = RightButton.GetComponentInChildren<Text>();
        RightButtonText.text = InputControlPath.ToHumanReadableString(Horizontal.action.bindings[2].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        Text UpButtonText = UpButton.GetComponentInChildren<Text>();
        UpButtonText.text = InputControlPath.ToHumanReadableString(Vertical.action.bindings[2].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        Text DownButtonText = DownButton.GetComponentInChildren<Text>();
        DownButtonText.text = InputControlPath.ToHumanReadableString(Vertical.action.bindings[1].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        Text ShootButtonText = ShootButton.GetComponentInChildren<Text>();
        ShootButtonText.text = InputControlPath.ToHumanReadableString(Shoot.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        Text DashButtonText = DashButton.GetComponentInChildren<Text>();
        DashButtonText.text = InputControlPath.ToHumanReadableString(Dash.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        Text BlackHoleButtonText = BlackHoleButton.GetComponentInChildren<Text>();
        BlackHoleButtonText.text = InputControlPath.ToHumanReadableString(BlackHole.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        Text UseObjectButtonText = UseObjectButton.GetComponentInChildren<Text>();
        UseObjectButtonText.text = InputControlPath.ToHumanReadableString(UseObject.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

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

    

   

    

    public void ButtonReload()
    {
        StartRebinding("Reload", ReloadButton, 0);
    }

    public void ButtonLeft()
    {
        StartRebinding("Horizontal",LeftButton, 1);
    }

    public void ButtonRight()
    {
        StartRebinding("Horizontal",RightButton, 2);
    }

    public void ButtonUp()
    {
        StartRebinding("Vertical",UpButton, 2);
        
    }

    public void ButtonDown()
    {
        StartRebinding("Vertical",DownButton, 1);

    }

    public void ButtonBlackHole()
    {
        StartRebinding("BlackHole", BlackHoleButton, 0);
    }

    public void ButtonUseObject()
    {
        StartRebinding("UseObject", UseObjectButton, 0);
    }

    public void ButtonShoot()
    {
        StartRebinding("Shoot", ShootButton, 0);
    }

    public void ButtonDash()
    {
        StartRebinding("Dash", DashButton, 0);
    }

    public void StartRebinding(string ActionName, GameObject ActiveButton = null, int bindingIndex = -1)
    {
        
        print(ActionName);

        actionToRebind = gameplayActionMap.FindAction(ActionName);
        actionToRebind.actionMap.Disable();

        buton = ActiveButton.GetComponentInChildren<Button>();
        buton.GetComponent<Image>().color = Color.red;
       
        waitingForInput.SetActive(true);
        
        

        rebindOperation = actionToRebind.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/Delta")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete((x) =>
            {
                
                ActiveButton.SetActive(true);
                
                waitingForInput.SetActive(false);
                buton.GetComponent<Image>().color = Color.green;
                TextButton = ActiveButton.GetComponentInChildren<Text>();
                TextButton.text = InputControlPath.ToHumanReadableString(x.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

                actionToRebind.actionMap.Enable();
                x.Dispose();
            })
            .Start();


    }

   
}
