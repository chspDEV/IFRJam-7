using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;

    private Resolution[] resolutions;

    void Start()
    {
        // Popula a lista de resoluções disponíveis
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width.ToString() + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate.ToString() + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Inicializa as outras configurações
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;
        vsyncToggle.isOn = QualitySettings.vSyncCount > 0;

        // Liga os métodos aos eventos da UI
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        vsyncToggle.onValueChanged.AddListener(SetVSync);

        // Configurar a cor do texto no início
        resolutionDropdown.captionText.color = Color.black;   // Texto selecionado
        resolutionDropdown.itemText.color = Color.black;      // Texto das opções da lista

        // Configura a cor de todos os itens
        foreach (TMP_Dropdown.OptionData option in resolutionDropdown.options)
        {
            option.text = "<color=black>" + option.text + "</color>";
        }
    }

    // Função para mudar a resolução
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Função para mudar a qualidade gráfica
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Função para alternar entre tela cheia e janela
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Função para ativar/desativar VSync
    public void SetVSync(bool isVSyncOn)
    {
        QualitySettings.vSyncCount = isVSyncOn ? 1 : 0;
    }
}
