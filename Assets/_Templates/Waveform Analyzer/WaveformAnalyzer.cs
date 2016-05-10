using UnityEngine;
using UnityEngine.UI;

public class WaveformAnalyzer : MonoBehaviour
{
    float c1;
    float c2;
    float c3;
    float c4;
    //float c5;
    float[] spectrum = new float[1024];
    Color c = Color.red;
    public void Update()
    {
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        c1 = spectrum[3] + spectrum[4] + spectrum[5];
        c2 = spectrum[6] + spectrum[7] + spectrum[8];
        c3 = spectrum[11] + spectrum[12] + spectrum[13];
        c4 = spectrum[22] + spectrum[23] + spectrum[24];
        //c5 = spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49];
        //print((c3+c4)*10);
        c.a = (c1 + c3 + c4 + c2) * 10;
        GetComponent<Image>().color = c;
    }
     
}
