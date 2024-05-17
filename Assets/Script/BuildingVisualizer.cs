using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingVisualizer : MonoBehaviour
{
    public List<GameObject> bassCubes;
    public List<GameObject> midCubes;
    public List<GameObject> highCubes;
    public float maxHeight = 10f;
    public float minHeight = 1f;
    public float visualizerScale = 50f;
    public float smoothSpeed = 2f;
    public GameObject audioAnalyzer;

    public AudioSource audioSource;
    private float[] spectrumData;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        spectrumData = new float[1024];  // A commonly used size for spectrum data
        audioAnalyzer = GameObject.FindWithTag("Music");
        audioSource = audioAnalyzer.GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Blackman);

        // Update cubes for bass, mid, and high frequencies
        UpdateCubes(bassCubes, 0, 3);  // low frequencies, e.g., 0-150 Hz
        UpdateCubes(midCubes, 4, 20);  // mid frequencies, e.g., 150-4000 Hz
        UpdateCubes(highCubes, 21, 60);  // high frequencies, e.g., 4000-20000 Hz
    }

    private void UpdateCubes(List<GameObject> cubes, int start, int end)
    {
        float avg = 0f;
        int count = 0;
        // Calculate average spectrum value over specified range
        for (int i = start; i <= end && i < spectrumData.Length; i++)
        {
            avg += spectrumData[i];
            count++;
        }
        if (count > 0)
            avg /= count;

        float scale = Mathf.Clamp(avg * visualizerScale, minHeight, maxHeight);

        foreach (GameObject cube in cubes)
        {
            if (cube != null)
            {
                Vector3 previousScale = cube.transform.localScale;
                float newYScale = Mathf.Lerp(previousScale.y, scale, Time.deltaTime * smoothSpeed);
                cube.transform.localScale = new Vector3(previousScale.x, newYScale, previousScale.z);
            }
        }
    }
}
