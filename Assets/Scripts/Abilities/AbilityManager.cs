using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }
    private List<Ability> allAbilities = new List<Ability>();
    private List<Ability> abilitiesFiltered = new List<Ability>();
    [SerializeField] private AbilityPickerMenu _abilityPickerMenu;

    private string _innateAbility;

    private AbilityInventory _abilityInventory;
    private Player _player;

    void Awake()
    {
        if (Instance == null)   
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAbilities();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        _innateAbility = GameManager.Instance.GetInnateAbilityCode();
    }
    private void LoadAbilities()
    {
        allAbilities.Add(new Fireball()); 
        allAbilities.Add(new PlasmaSpheres());
        allAbilities.Add(new Whirligig());
        allAbilities.Add(new RicochetStone());
        allAbilities.Add(new LaserBeam());
        allAbilities.Add(new UFORay());
    }
    public List<Ability> GetAbilityList()
    {
        return allAbilities;
    }
    public List<Ability> GetAbilityListFiltered()
    {
        abilitiesFiltered = new List<Ability>(allAbilities);
        abilitiesFiltered.RemoveAll(ability => ability.Code == _innateAbility);
        return abilitiesFiltered;
    }
}
