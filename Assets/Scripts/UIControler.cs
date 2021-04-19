using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{
    
    CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");

    public TMP_InputField textIf;
    public TMP_InputField mutationRateIf;
    public TextMeshProUGUI speedText;
    public Slider sliderSpeed;
    public TextMeshProUGUI popSizeText;
    public Slider sliderPopSize;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Target")) textIf.text = PlayerPrefs.GetString("Target");
        if (PlayerPrefs.HasKey("MutationRate")) mutationRateIf.text = PlayerPrefs.GetFloat("MutationRate").ToString(culture);
        if (PlayerPrefs.HasKey("Speed")) sliderSpeed.value = PlayerPrefs.GetInt("Speed");
        if (PlayerPrefs.HasKey("PopSize")) sliderPopSize.value = PlayerPrefs.GetInt("PopSize");
    }

    public void UpdatePopSizeText(int value)
    {
        popSizeText.text = sliderPopSize.value.ToString();
    }

    public void UpdateSpeedText(int value)
    {
        speedText.text = sliderSpeed.value.ToString();
    }

    public void OnClick()
    {
        PlayerPrefs.SetString("Target", textIf.text);
        PlayerPrefs.SetFloat("MutationRate", float.Parse(mutationRateIf.text, culture));
        PlayerPrefs.SetInt("Speed", (int) sliderSpeed.value);
        PlayerPrefs.SetInt("PopSize", (int) sliderPopSize.value);

        SceneManager.LoadScene("Scenes/Scene");
    }
}
