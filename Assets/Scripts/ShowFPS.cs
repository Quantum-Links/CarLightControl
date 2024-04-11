using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    private const int bufferSize = 60; // 样本数量
    private float[] fpsBuffer = new float[bufferSize];
    private int fpsBufferIndex = 0;

    Text FPSText;
    void Awake()
    {
        FPSText = GetComponentInChildren<Text>();
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        float fps = 1 / Time.deltaTime;
        fpsBuffer[fpsBufferIndex] = fps;
        fpsBufferIndex = (fpsBufferIndex + 1) % bufferSize;
        float averageFPS = CalculateAverageFPS();
        FPSText.text = $"{averageFPS:F2}fps";
    }
    private float CalculateAverageFPS()
    {
        float sum = 0f;
        for (int i = 0; i < bufferSize; i++)
        {
            sum += fpsBuffer[i];
        }
        return sum / bufferSize;
    }
}
