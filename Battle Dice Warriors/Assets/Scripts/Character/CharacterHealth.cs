using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private Slider          _healthSlider;
    [SerializeField] private Image           _fillImage;
    [SerializeField] private TextMeshProUGUI _damageText;

    [SerializeField] private GameObject _critDamage;
    [SerializeField] private TextMeshProUGUI _critDamageText;

    [SerializeField] private float           _animSpeedTakeDamage = 5f;
    [SerializeField] private AnimationCurve  _animCurve;

    public float  CurrentHP { get; private set; }
    private float _maxHealth => GetComponent<Character>().Data.HP;

    private bool  _isHealthChanging = false;
    private float _current;
    private float _oldValue;
    private float _newValue;
    private float _newHealth = -1f;

    /// <summary>
    /// FixedUpdate method.
    /// </summary>
    private void FixedUpdate()
    {
        ChangeHealth();
    }

    /// <summary>
    /// Sets the health slider at the beginning.
    /// </summary>
    public void SetData()
    {
        CurrentHP = _maxHealth;
        SetHealthSlider(CurrentHP / _maxHealth);
        _damageText.enabled = false;
        _critDamage.SetActive(false);
    }

    /// <summary>
    /// Sets the health slider.
    /// </summary>
    /// <param name="normalizedValue"></param>
    private void SetHealthSlider(float normalizedValue)
    {
        _healthSlider.value = normalizedValue;
        float currentHealth = normalizedValue * _maxHealth;
        CurrentHP = currentHealth;
    }

    /// <summary>
    /// Takes the damage.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage, bool crit)
    {
        CalculateHealth(-damage);

        if (crit)
        {
            _critDamage.SetActive(true);
            _critDamageText.text = (damage).ToString("0");
            return;
        }

        _damageText.text = (damage).ToString("0");
        _damageText.enabled = true;
    }

    /// <summary>
    /// Heals the amount.
    /// </summary>
    /// <param name="amount"></param>
    public void Heal(float amount)
    {
        CalculateHealth(+amount);
    }

    /// <summary>
    /// Changes the health value.
    /// </summary>
    private void ChangeHealth()
    {
        if (!_isHealthChanging)
            return;

        _current = Mathf.MoveTowards(_current, 1, _animSpeedTakeDamage * 0.0001f / Time.deltaTime);
        float interpolation = _animCurve.Evaluate(_current);

        SetHealthSlider(Mathf.Lerp(_oldValue, _newValue, interpolation));
        _fillImage.color = Color.Lerp(Color.red, Color.green, interpolation);

        if (interpolation >= 1)
        {
            _current = 0f;
            _damageText.enabled = false;
            _critDamage.SetActive(false);

            CurrentHP = _newHealth;

            if (CurrentHP <= 0)
                GetComponent<Character>().SetInteractibleFalse();

            _isHealthChanging = false;
        }
    }

    /// <summary>
    /// Calculates the health values.
    /// </summary>
    /// <param name="value"></param>
    private void CalculateHealth(float value)
    {
        float currentHealth = CurrentHP;

        _oldValue = currentHealth / _maxHealth;

        _newHealth = currentHealth + value;

        if (_newHealth <= 0)
        { 
            _newHealth = 0;
        }

        _newValue = _newHealth / _maxHealth;

        _isHealthChanging = true;
    }
}


