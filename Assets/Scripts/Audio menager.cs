using UnityEngine;
using UnityEngine.UI;

public class AudioManagement : MonoBehaviour
{
    [Header("Referências da UI")]
    public Slider volumeSlider;   // Slider de volume
    public Button muteButton;     // Botão de cortar áudio 

    private bool isMuted = false;
    private float lastVolume = 1f;  // Volume antes de mutar

    private void Start()
    {
        // Garantir que o volume começa em 1
        AudioListener.volume = 1f;

        // Configurar slider 
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = AudioListener.volume;

            // Sempre que o slider mudar, chamamos OnVolumeChanged
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // Configurar botão de mute
        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleMute);
        }
    }

    // Chamado pelo Slider
    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;

        // Se o volume for maior que 0, significa que não tá mutado
        if (value > 0f)
        {
            lastVolume = value;
            isMuted = false;
        }
        else
        {
            isMuted = true;
        }
    }

    // Chamado pelo botão de "cortar áudio"
    public void ToggleMute()
    {
        if (!isMuted)
        {
            // Pega o volume atual e corta tudo. Pei buf
            lastVolume = AudioListener.volume;
            AudioListener.volume = 0f;
            if (volumeSlider != null)
                volumeSlider.value = 0f;

            isMuted = true;
        }
        else
        {
            // Voltar para o último volume salvo
            AudioListener.volume = lastVolume;
            if (volumeSlider != null)
                volumeSlider.value = lastVolume;

            isMuted = false;
        }
    }
}