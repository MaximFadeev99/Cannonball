using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(SpriteRenderer))]

public class Target : MonoBehaviour
{
    public Action<float, Color> healthChanged;

    private Vector3 [] _wayPoints;
    private float _initialHealth = 100;
    private float _health;
    private float _colorChange = 0.08f;
    private SpriteRenderer _spriteRenderer;
    private Color _healthyColor;
    private Color _damagedColor = Color.red;
    private Color _transitionColor;

    public float Health 
    {
        get { return _health; }

        private set 
        {
            _health = value > _initialHealth ? _initialHealth : value;           
        }   
    }

    private void Awake()
    {
        _wayPoints = new Vector3[10] 
        {   
            new Vector2(-7.13f,2.99f), new Vector2(-5.84f, -2.63f), 
            new Vector2(-4.39f, -0.2f), new Vector2(-3.06f, 3f), 
            new Vector2(-0.96f, 0.38f), new Vector2(0.88f, -4.09f), 
            new Vector2(4.04f, 0.04f), new Vector2(3.68f, 4f), 
            new Vector2(-1.34f, -3.75f), new Vector2(-5.5f, 3f)
        };
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthyColor = _spriteRenderer.color;
        _health = _initialHealth;
    }

    private void Start()
    {
        float completionTime = 60;

        Tween tween = transform.DOPath
            (_wayPoints, completionTime, PathType.CatmullRom).SetOptions(true);
        tween.SetLoops(-1).SetEase(Ease.Linear);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Cannonball>(out Cannonball cannonball)) 
        {
            if (Health > 0)
                TakeDamage(cannonball.Damage);
        }       
    }

    private void TakeDamage(float damage) 
    {
        Health -= damage;
        _transitionColor = new Color
            (Mathf.MoveTowards(_spriteRenderer.color.r, _damagedColor.r, _colorChange),
             Mathf.MoveTowards(_spriteRenderer.color.g, _damagedColor.g, _colorChange),
             Mathf.MoveTowards(_spriteRenderer.color.b, _damagedColor.b, _colorChange));
        _spriteRenderer.color = _transitionColor;
        healthChanged.Invoke(Health, _transitionColor);
    }

    public void Heal() 
    {
        float healthHeal = 10f;
        
        Health += healthHeal;
        _transitionColor = new Color
           (Mathf.MoveTowards(_spriteRenderer.color.r, _healthyColor.r, _colorChange),
            Mathf.MoveTowards(_spriteRenderer.color.g, _healthyColor.g, _colorChange),
            Mathf.MoveTowards(_spriteRenderer.color.b, _healthyColor.b, _colorChange));
        _spriteRenderer.color = _transitionColor;
        healthChanged.Invoke(Health, _transitionColor);
    }
}
