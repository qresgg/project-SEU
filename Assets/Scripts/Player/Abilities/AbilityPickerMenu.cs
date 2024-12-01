using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickerMenu : MonoBehaviour
{
    [SerializeField] private Transform abilityContainer;
    private List<Ability> abilities = new List<Ability>();
    [SerializeField] private AbilityUI[] abilitySlots;
    private string _innateAbility;
    private AbilityInventory _abilityInventory;
    private Player _player;

    private void Start()
    {
        _abilityInventory = GameObject.Find("AbilityInventory").GetComponent<AbilityInventory>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void GetOBJ(List<Ability> m_abilities)
    {
        abilities = m_abilities;
        ShuffleAbilities();
        DisplayAbilities();
    }

    public void GetInnateAbility(string get_innateAbility)
    {
        _innateAbility = get_innateAbility;
    }

    private void ShuffleAbilities()
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

    private void DisplayAbilities()
    {
        int slotIndex = 0;
        for (int i = 0; i < abilities.Count && slotIndex < abilitySlots.Length; i++)
        {
            if (abilities[i].abilityCode != _innateAbility)
            {
                abilitySlots[slotIndex].SetAbility(abilities[i]);
                Debug.Log(abilities[i]);
                slotIndex++;
            }
        }
    }
    public void SetActive(bool arg)
    {
        this.gameObject.SetActive(arg);
    }
    public void OpenAbilityPickerMenu()
    {
        this.gameObject.SetActive(true);
        _player.PauseGame();
    }
    public void CloseAbilityPickerMenu()
    {
        this.gameObject.SetActive(false);
        _player.ResumeGame();
    }
}
