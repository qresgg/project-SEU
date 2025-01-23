using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] P_HPController _hpController;
    [SerializeField] AbilityPickerMenu _abilityPickerMenu;

    GameManager _gameManager;
    AbilityUICooldowns _abilityUICooldowns;

    [Header("Movement")]
    private Vector3 _input;
    [SerializeField] private float _speed = 3.5f;
    private Rigidbody _rb;

    [Header("Shooting")]
    [SerializeField] private int _projectileCount;
    private float _lastFireball;
    private float _lastWhirligig;
    float _orbitalSpheresDurationCounter;
    float _orbitalSpheresCooldownCounter;

    [Header("Stats")]
    [SerializeField] private float _maxHealth = 0;
    [SerializeField] private float _healthRegeneration = 0.1f;
    [SerializeField] private float _currentHealth = 0;
    [SerializeField] private int xp_points = 0;

    private Fireball Fireball;
    private OrbitalSpheres OrbitalSpheres;
    private Whirligig Whirligig;

    public bool _isEnemyNearby = false;

    [Header("Container")]
    [SerializeField] public GameObject SpheresContainer;

    P_AbilityUser player_abilityUser;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _hpController = GameObject.Find("HP").GetComponent<P_HPController>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _abilityUICooldowns = GameObject.Find("Cooldowns").GetComponent<AbilityUICooldowns>();
        player_abilityUser = GameObject.Find("P_AbilityUser").GetComponent<P_AbilityUser>();
        _abilityPickerMenu.SetActive(false);

        Fireball = new Fireball();
        OrbitalSpheres = new OrbitalSpheres();
        Whirligig = new Whirligig();
    }

    void FixedUpdate()
    {
        HealthManager();
        Movement();
        Shoot();
    }

    void Movement()
    {
        _input.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 newPosition = _rb.position + _input * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }
    void HealthManager()
    {
        _currentHealth = _hpController.GetCurrentHP();
        _maxHealth = _hpController.GetMaxHP();
    }
    void Shoot()
    {
        player_abilityUser.ManageFireballActivator();
        player_abilityUser.ManageOrbitalSphereActivator();
        player_abilityUser.ManageWhirligigActivator();
    }
    public int GetXP()
    {
        return xp_points;
    }
    public void TakeXP(int takenXP)
    {
        xp_points += takenXP;
        if (xp_points >= 100)
        {
            OpenAbilityPickerMenu();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
    public int GetActiveProjectileCount()
    {
        return SpheresContainer.transform.childCount;
    }
    public void OpenAbilityPickerMenu()
    {
        _abilityPickerMenu.SetActive(true);
        _gameManager.PauseGame();
    }
}
