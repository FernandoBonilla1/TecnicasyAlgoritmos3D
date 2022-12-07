using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceSounds;
    [SerializeField] private AudioSource _audioSourceMusic;   
    [SerializeField] private AudioClip _bellSound;
    [SerializeField] private AudioClip _minigameMusic;

    [SerializeField] private List<GameObject> _targets;
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _maxPointsText;
    [SerializeField] private int _points = 0;
    [SerializeField] private int _maxPoints = 0;

    [SerializeField] private float _maxTimer = 15f;
    private float _timer = 15f;
    private int _randomNumber = -1;
    private bool _isMinigameActive = false;

    public void StartMinigame()
    {
        _timer = _maxTimer;
        _pointsText.text = "Puntos: " + _points;
        _maxPoints = PlayerPrefs.GetInt("MaxPoints");
        _maxPointsText.text = "Max Puntos: " + _maxPoints;
        _audioSourceSounds.clip = _bellSound;
        _audioSourceSounds.Play();
        _audioSourceMusic.clip = _minigameMusic;
        _audioSourceMusic.Play();
        RiseRandomTargets();
        _maxPointsText.gameObject.SetActive(true);
        _pointsText.gameObject.SetActive(true);
        _timerText.gameObject.SetActive(true);
        _isMinigameActive = true;
    }

    private void Update()
    {
        _pointsText.text = "Puntos: " + _points;
        if (_isMinigameActive)
        {
            TimerCount();
            if (_randomNumber > -1 && _isMinigameActive)
            {
                if (!_targets[_randomNumber].activeSelf)
                {
                    RiseRandomTargets();
                }
            }
        }
    }

    private void RiseRandomTargets()
    {
        _randomNumber = Random.Range(0, _targets.Count);
        _targets[_randomNumber].SetActive(true);
    }

    private void TimerCount()
    {
        _timer -= Time.deltaTime;
        _timerText.text = "Tiempo: " + (int)_timer + " seg.";
        if (_timer < 0)
        {
            _timerText.text = "Tiempo: 0 seg.";
            DisableAllTargets();
            FinishMinigame();
        }
    }

    public void FinishMinigame()
    {
        _isMinigameActive = false;
        int auxMaxPoints = _points;
        if(auxMaxPoints > _maxPoints)
        {
            _maxPoints = auxMaxPoints;
            _maxPointsText.text = "Max Puntos: " + _maxPoints;
            PlayerPrefs.SetInt("MaxPoints", _points);
        }
    }

    private void DisableAllTargets()
    {
        for(int i=0; i<_targets.Count; i++)
        {
            _targets[i].SetActive(false);
        }
    }

    public void SetPoints(int value)
    {
        _points = value;
    }

    public bool getStatusMinigame()
    {
        return _isMinigameActive;
    }
}
