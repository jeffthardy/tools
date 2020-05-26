using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuController : MonoBehaviour
{
    public InputController inputController;
    public Button startButton;
    public Button optionsButton;
    public Button mainMenuButton;
    public Button quitButton;
    public string gameScene;


    public Canvas mainMenuCanvas;
    public Canvas optionsCanvas;

    // Options menu controls
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider vsyncSlider;


    private Resolution[] supportedResolutions;
    private GameObject vsyncSliderText;

    private enum MenuButtonList { None=0, Start= 3, Options=2, Quit=1 };
    private MenuButtonList currentMenuButton;

    private enum OptionsButtonList { None = 0, Resolution = 4, Fullscreen = 3, Vsync = 2, Back=1};
    private OptionsButtonList currentOptionsButton;

    // Start is called before the first frame update
    void Start()
    {

        // Find any objects that aren't set in scene
        vsyncSliderText =GameObject.Find("VsyncPanel/Slider/VsyncValue");

        // Get initial values
        supportedResolutions = Screen.resolutions;
        fullscreenToggle.isOn = Screen.fullScreen;
        setVsyncSliderText((int)vsyncSlider.GetComponent<Slider>().value);


        // Switch between main and options menu via 2 canvases
        mainMenuCanvas.enabled = true;
        optionsCanvas.enabled = false;

        // Initially have nothing selected
        currentMenuButton = MenuButtonList.None;
        currentOptionsButton = OptionsButtonList.None;


        // Attach handlers
        inputController.OnInput += ProcessInput;

        if (resolutionDropdown)
        {
            resolutionDropdown.onValueChanged.AddListener(delegate
            {
                ResolutionDropdownValueChanged(resolutionDropdown);
            });
        }

        if (fullscreenToggle)
        {
            fullscreenToggle.onValueChanged.AddListener(delegate
            {
                FullscreenToggleValueChanged(fullscreenToggle);
            });
        }

        if (vsyncSlider)
        {
            vsyncSlider.onValueChanged.AddListener(delegate
            {
                VsyncSliderValueChanged(vsyncSlider);
            });
        }


        if (startButton)
            startButton.onClick.AddListener(StartButtonOnClick);
        if (optionsButton)
            optionsButton.onClick.AddListener(OptionsButtonOnClick);
        if (mainMenuButton)
            mainMenuButton.onClick.AddListener(MainMenuButtonOnClick);
        if (quitButton)
            quitButton.onClick.AddListener(QuitButtonOnClick);


        // Add all supported resolutions to options menu
        List<string> resolutionDropOptions = new List<string> { };
        float ratio32x9 = Mathf.Round(((float)32 / 9) * 100.0F) / 100.0F;
        float ratio43x18 = Mathf.Round(((float)43 / 18) * 100.0F) / 100.0F;
        float ratio64x27 = Mathf.Round(((float)64 / 27) * 100.0F) / 100.0F;
        float ratio12x5 = Mathf.Round(((float)12 / 5) * 100.0F) / 100.0F;
        float ratio16x9 = Mathf.Round(((float)16 / 9) * 100.0F) / 100.0F;
        float ratio16x10 = Mathf.Round(((float)16 / 10) * 100.0F) / 100.0F;
        float ratio5x4 = Mathf.Round(((float)5 / 4) * 100.0F) / 100.0F;
        float ratio4x3 = Mathf.Round(((float)4 / 3) * 100.0F) / 100.0F;
        float ratio3x2 = Mathf.Round(((float)3 / 2) * 100.0F) / 100.0F;

        //Debug.Log(ratio32x9);
        //Debug.Log(ratio43x18);
        //Debug.Log(ratio64x27);
        //Debug.Log(ratio12x5);
        //Debug.Log(ratio16x9);
        //Debug.Log(ratio16x10);
        //Debug.Log(ratio5x4);
        //Debug.Log(ratio4x3);
        //Debug.Log(ratio3x2);


        foreach (var res in supportedResolutions)
        {
            string ratio = "(unknown aspect)";
            float aspectRatio = Mathf.Round(((float)res.width/res.height) * 100.0F) / 100.0F;
            //Debug.Log(aspectRatio);
            if (aspectRatio == ratio32x9)
                ratio = "(32:9)";
            // 21:9 can be 43:18, 64:27, 12:5 in monitors... https://en.wikipedia.org/wiki/21:9_aspect_ratio
            if (aspectRatio == ratio43x18)
                ratio = "(21:9)";
            if(aspectRatio == ratio64x27)
                ratio = "(21:9)";
            if (aspectRatio == ratio12x5)
                ratio = "(21:9)";
            if (aspectRatio == ratio16x9)
                ratio = "(16:9)";
            if (aspectRatio == ratio16x10)
                ratio = "(16:10)";
            if (aspectRatio == ratio5x4)
                ratio = "(5:4)";
            if (aspectRatio == ratio4x3)
                ratio = "(4:3)";
            if (aspectRatio == ratio3x2)
                ratio = "(3:2)";



            string supportedResolution = string.Format("{0}x{1}@{2}{3}", res.width, res.height, res.refreshRate,ratio);
            resolutionDropOptions.Add(supportedResolution);
        }
        resolutionDropdown.AddOptions(resolutionDropOptions);
    }

    private void OnDestroy()
    {
        inputController.OnInput -= ProcessInput;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void changeMenuButton(int dir)
    {
        if((int)currentMenuButton == 0)
            currentMenuButton = (MenuButtonList)3;
        else  if (((int)currentMenuButton == 1) && (dir == -1))
            currentMenuButton = (MenuButtonList)3;
        else if (((int)currentMenuButton == 3) && (dir == 1))
            currentMenuButton = (MenuButtonList)1;
        else
            currentMenuButton = (MenuButtonList)((int)currentMenuButton + dir);

        switch (currentMenuButton)
        {
            case MenuButtonList.None:
                break;
            case MenuButtonList.Start:
                startButton.Select();
                break;
            case MenuButtonList.Options:
                optionsButton.Select();
                break;
            case MenuButtonList.Quit:
                quitButton.Select();
                break;
        }

    }

    void changeOptionsButton(int dir)
    {
        if ((int)currentOptionsButton == 0)
            currentMenuButton = (MenuButtonList)4;
        else if (((int)currentOptionsButton == 1) && (dir == -1))
            currentOptionsButton = (OptionsButtonList)4;
        else if (((int)currentOptionsButton == 4) && (dir == 1))
            currentOptionsButton = (OptionsButtonList)1;
        else
            currentOptionsButton = (OptionsButtonList)((int)currentOptionsButton + dir);

        switch (currentOptionsButton)
        {
            case OptionsButtonList.None:
                break;
            case OptionsButtonList.Resolution:
                resolutionDropdown.Select();
                break;
            case OptionsButtonList.Fullscreen:
                fullscreenToggle.Select();
                break;
            case OptionsButtonList.Vsync:
                vsyncSlider.Select();
                break;
            case OptionsButtonList.Back:
                mainMenuButton.Select();
                break;
        }

    }

    void ProcessInput(string input)
    {
        switch (input)
        {
            case "up":
                if(mainMenuCanvas.enabled)
                    changeMenuButton(1);
                if (optionsCanvas.enabled)
                    changeOptionsButton(1);
                break;
            case "down":
                if(mainMenuCanvas.enabled)
                    changeMenuButton(-1);
                if (optionsCanvas.enabled)
                    changeOptionsButton(-1);
                break;
            default:
                break;
        }
        Debug.Log("Handling input " + input);
    }

    


    void ResolutionDropdownValueChanged(Dropdown dropdown)
    {
        Debug.Log("Detected request to change resolution to " + dropdown.value);
        string parseWidth = dropdown.captionText.text.Split('x')[0];
        string parseHeight = dropdown.captionText.text.Split('x')[1].Split('@')[0];
        string parseRefresh = dropdown.captionText.text.Split('x')[1].Split('@')[1].Split('(')[0];

        int width;
        int.TryParse(parseWidth, out width);
        int height;
        int.TryParse(parseHeight, out height);
        int refreshRate;
        int.TryParse(parseRefresh, out refreshRate);

        Debug.Log(parseWidth + " x " + parseHeight + " = " + width + ":" + height + "@" + refreshRate);
        Screen.SetResolution(width, height, Screen.fullScreen, refreshRate);

    }


    void FullscreenToggleValueChanged(Toggle toggle)
    {
        Debug.Log("Detected request to change fullscreen to " + toggle.isOn);
        if (toggle.isOn)
        {
            // Just toggling fullscreen will fullscreen as windowed resolution which we don't want
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true, Screen.currentResolution.refreshRate);
            //Screen.fullScreen = true;
        } else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false, Screen.currentResolution.refreshRate);
            //Screen.fullScreen = false;
        }
    }

    void setVsyncSliderText(int value)
    {
        TextMeshProUGUI descriptor = vsyncSliderText.GetComponent<TextMeshProUGUI>();
        if (value == 0)
            descriptor.text = "OFF";
        else
            descriptor.text = string.Format("{0}-{1}", "ON", value);

    }

    void VsyncSliderValueChanged(Slider slider)
    {
        setVsyncSliderText((int)slider.value);
        if((int)slider.value >=0 && (int)slider.value <=4)
            QualitySettings.vSyncCount = (int)slider.value;
    }


    void StartButtonOnClick()
    {
        Debug.Log("You have clicked the start button.  Loading game...");
        SceneManager.LoadScene(gameScene);
    }


    void OptionsButtonOnClick()
    {
        Debug.Log("You have clicked the options button! Switching to Options Canvas");
        mainMenuCanvas.enabled = false;
        optionsCanvas.enabled = true;

    }

    void MainMenuButtonOnClick()
    {
        Debug.Log("You have clicked the Main Menu button! Switching to Main Menu Canvas");
        mainMenuCanvas.enabled = true;
        optionsCanvas.enabled = false;

    }



    void QuitButtonOnClick()
    {
        Debug.Log("You have clicked the quit button!  Bye!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        //Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}
