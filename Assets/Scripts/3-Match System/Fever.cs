using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum FeverType
{
    None = 0,
    Fever = 1,
    SuperFever = 2,
    HyperFever = 3
}

public class Fever : MonoBehaviour
{
    [SerializeField]
    private Color _noneFeverColor, _feverColor, _superFeverColor, _hyperFeverColor;

    [SerializeField]
    private Image _feverImage;

    [SerializeField]
    private TMP_Text _feverText;

    private AudioSource _feverAudioSource;

    [SerializeField]
    private AudioClip[] feverSoundClip = new AudioClip[4]; // 인스펙터에서 할당하기 위한 AudioClip 변수

    [SerializeField]
    private float _maxFever = 100;

    [SerializeField]
    [Range(0, 15)]
    private int _hyperFeverDuration = 10;

    private FeverType _feverType = FeverType.None;

    private float _currentFever;

    private bool isHyperFever = false;

    public float currentFever 
    {
        get
        {
            return _currentFever;   
        }

        set
        {
            // HyperFever 지속시간인 경우, 종료
            if (isHyperFever)
                return;

            _currentFever = value;

            // 피버가 안채워진 경우
            if (_currentFever < _maxFever)
            {

            }
            // 피버가 채워진 경우
            else
            {
                if ((int)_feverType == 3)
                {
                    HyperFever();
                    return;
                }

                _currentFever = 0;
                _feverType++;
                FeverFunction(_feverType);
            }

            UpdateFeverBar();
        }
    }


    private void Awake()
    {
        // GetComponent
        _feverAudioSource = GetComponent<AudioSource>();

        // 초기화
        currentFever = 0;
        FeverFunction(_feverType);
    }

    public void FeverFunction(FeverType feverType)
    {
        int state = (int)feverType;

        switch (feverType)
        {   
            case FeverType.None:
                _feverImage.color = _noneFeverColor;
                _feverText.text = "";
                break;
            case FeverType.Fever:
                _feverImage.color = _feverColor;
                _feverText.text = "Fever";
                break;  
            case FeverType.SuperFever:
                _feverImage.color = _superFeverColor;
                _feverText.text = "SuperFever";
                break;
            case FeverType.HyperFever:
                _feverImage.color = _hyperFeverColor;
                _feverText.text = "HyperFever";
                break;
        }

        _feverAudioSource.clip = feverSoundClip[state];
        _feverAudioSource?.Play();
    }

    private async Task HyperFever()
    {
        HyperFeverOn();

        Debug.Log(_hyperFeverDuration * 1000);

        await Task.Delay(_hyperFeverDuration * 1000);

        HyperFeverOff();
    }

    private void HyperFeverOn()
    {
        isHyperFever = true;

        _currentFever = _maxFever;

        UpdateFeverBar();
    }

    private void HyperFeverOff()
    {
        isHyperFever = false;

        _currentFever = 0;
        _feverType = 0;

        FeverFunction(_feverType);
        UpdateFeverBar();
    }

    private void UpdateFeverBar()
    {
        float fillAmount = currentFever / _maxFever;
        _feverImage.transform.localScale =  
            new Vector3(fillAmount, _feverImage.transform.localScale.y , _feverImage.transform.localScale.z);
    }

    public void AttributeFever(float attributeAmount)
    {
        currentFever += attributeAmount;
    }
}
