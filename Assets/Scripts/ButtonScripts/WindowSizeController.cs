using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowSizeController : MonoBehaviour
{
    #region First attempt
    //public TMP_Dropdown resoltionDropdown;

    //public TextMeshProUGUI selectedResolutionText;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    SetResolutionOptions();
    //    SetWindowSize(1920, 1080);
    //}
    //void SetResolutionOptions()
    //{
    //    resoltionDropdown.ClearOptions();

    //    List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>
    //    {
    //        new TMP_Dropdown.OptionData("800x600"),
    //        new TMP_Dropdown.OptionData("1280x720"),
    //        new TMP_Dropdown.OptionData("1920x1080"),
    //        new TMP_Dropdown.OptionData("2560x1440"),
    //        new TMP_Dropdown.OptionData("3840x2160"),
    //    };
    //    resoltionDropdown.AddOptions(options);
    //    resoltionDropdown.value = 2;
    //    resoltionDropdown.RefreshShownValue();

    //}
    //public void OnResolutionDropdonwValueChanged()
    //{
    //    string selectedResolution = resoltionDropdown.options[resoltionDropdown.value].text;
    //    if(selectedResolution != null)
    //    {
    //        selectedResolutionText.text = "Selected Resolution: " + selectedResolution;
    //    }
    //    string[] dimensions = selectedResolution.Split('x');
    //    int width = int.Parse(dimensions[0]);
    //    int height = int.Parse(dimensions[1]);

    //    SetWindowSize(width, height);
    //}
    //void SetWindowSize(int width, int height)
    //{
    //    Screen.SetResolution(width, height, Screen.fullScreen);
    //}
    #endregion
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    private void Start()
    {
       resolutions = Screen.resolutions;
       resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
