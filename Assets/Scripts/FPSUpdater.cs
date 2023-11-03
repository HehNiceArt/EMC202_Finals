using TMPro;
using UnityEngine;

public class FPSUpdater : MonoBehaviour
{
    float fps;
    float updateTimer = 0.2f;
    [SerializeField] TextMeshProUGUI fpsTitle;
    
    private void UpdateFPSDisplay()
    {
        updateTimer -= Time.deltaTime;
        if( updateTimer <= 0f)
        {
            fps = 1f / Time.unscaledDeltaTime;
            fpsTitle.text = ""+ Mathf.Round(fps);
            updateTimer = 0.2f;
        }
    }
    void Update()
    {
        Application.targetFrameRate = -1;
        UpdateFPSDisplay();
    }
}
