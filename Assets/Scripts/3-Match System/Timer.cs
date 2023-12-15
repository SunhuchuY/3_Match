using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    [Range(0,3)]
    private int _readyDuration = 3;

    [SerializeField]
    [Range(0, 60)]
    private int _gameDuration = 60;

    [SerializeField]
    private TMP_Text _remainingDurationText;

    [SerializeField]
    private Image _remainingDurationFillImage;

    private AudioSource readyAudio;


    private void Awake()
    {
        _remainingDurationText.text = _gameDuration.ToString();
        readyAudio = GetComponent<AudioSource>();
    }

    private async Task Ready()
    {
        readyAudio.Play();
        
        while (readyAudio.isPlaying)
        {
            await Task.Delay(10); 
        }
    }

    public async Task TimerStart()
    {
        await Ready();

        for (int i = _gameDuration; i >= 0; i--)
        {
            float fillAmount = (float)i / (float)_gameDuration;

            _remainingDurationText.text = i.ToString();
            _remainingDurationFillImage.transform.localScale =
                new Vector3(fillAmount, _remainingDurationFillImage.transform.localScale.y, _remainingDurationFillImage.transform.localScale.z);

            await Task.Delay(1000);
        }

        GameManager.Instance.GameOver();
    }
}
