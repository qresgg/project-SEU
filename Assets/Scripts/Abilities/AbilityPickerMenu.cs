using System.Collections.Generic;
using UnityEngine;

public class AbilityPickerMenu : MonoBehaviour
{
    private List<Ability> abilities = new List<Ability>();

    [SerializeField] private Transform abilityContainer;
    [SerializeField] private AbilityUI[] abilitySlots;

    private string _innateAbility;

    private AbilityInventory _abilityInventory;
    private Player _player;
    private AbilityManager _abilityManager;

    [SerializeField] private bool isFirstCall = true;

    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _abilityManager = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        _innateAbility = GameManager.Instance.GetInnateAbilityCode();
        abilities = _abilityManager.GetAbilityListFiltered();

        ShuffleAbilities();
        DisplayAbilities();
    }

    public void ShuffleAbilities()
    {
        System.Random random = new System.Random();
        int n = abilities.Count;

        for (int i = 0; i < n; i++)
        {
            int randomIndex = random.Next(i, n);
            Ability temp = abilities[i];
            abilities[i] = abilities[randomIndex];
            abilities[randomIndex] = temp;
        }
    }

    public void DisplayAbilities()
    {
        int slotIndex = 0;
        for (int i = 0; i < abilities.Count && slotIndex < abilitySlots.Length; i++)
        {
            if (abilities[i].AbilityLevel.Level != 8) //|| !pickedAbilityCodes.Contains(abilities[i].Code))
            {
                abilitySlots[slotIndex].SetAbility(abilities[i]);
                slotIndex++;
            }
        }
        for (int i = slotIndex; i < abilitySlots.Length; i++) 
        { 
            abilitySlots[i].SetAbility(null); 
        }

        if (isFirstCall)
        {
            isFirstCall = false;
            Debug.Log("First call of DisplayAbilities completed.");
        }
    }

    public void SetActive(bool arg)
    {
        this.gameObject.SetActive(arg);
        if (arg)
        {
            ShuffleAbilities();
            DisplayAbilities();
        }
    }
}
