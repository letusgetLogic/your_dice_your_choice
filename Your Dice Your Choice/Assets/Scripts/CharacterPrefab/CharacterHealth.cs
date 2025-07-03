using System;
using Assets.Scripts.CharacterPrefab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Image _fillImage;
    [SerializeField] private TextMeshProUGUI _damageText;

    [SerializeField] private float _animSpeedAct = 0.0001f;
    [SerializeField] private AnimationCurve _animCurve;

    private float _maxHealth => GetComponent<Character>().OriginHP;

    private bool _isHealthChanging = false;
    private float _current;
    private float _oldValue;
    private float _newValue;

    private void FixedUpdate()
    {
        ChangeHealth();
    }

    public void SetHealthSlider()
    {
        SetHealthSlider( CurrentHealth() /  _maxHealth);
        _damageText.enabled = false;
    }

    private void SetHealthSlider(float value)
    {
        _healthSlider.value = value;
    }

    public void TakeDamage(float damage)
    {
        CalculateHealth(-damage);
        _damageText.text = (-damage).ToString();
        _damageText.enabled = true;
    }

    public void Heal(float amount)
    {
        CalculateHealth(+amount);
    }

    private void ChangeHealth()
    {
        if (!_isHealthChanging)
            return;

        _current = Mathf.MoveTowards(_current, 1, _animSpeedAct / Time.deltaTime);
        float value = _animCurve.Evaluate(_current);

        SetHealthSlider(Mathf.Lerp(_oldValue, _newValue, value));
        _fillImage.color = Color.Lerp(Color.red, Color.green, value);

        if (value < 1)
            return;

        _current = 0f;
        _isHealthChanging = false;
        _damageText.enabled = false;
    }

    private float CurrentHealth()
    {
        return GetComponent<Character>().Data.HP;
    }

    private void CalculateHealth(float value)
    {
        _oldValue = CurrentHealth() / _maxHealth;

        GetComponent<Character>().Data.HP += value;

        _newValue = CurrentHealth() / _maxHealth;

        _isHealthChanging = true;
    }

}


