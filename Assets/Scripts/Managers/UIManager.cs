using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats;

    [Header("Bars")]
    [SerializeField] private Image healthbar;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI healthTMP;

    private void Update()
    {
        UpdatePlayerUI();   
    }

    private void UpdatePlayerUI()
    {
        // updates 1st parameter with the 2nd parameter value by the 3rd parameter's time 
        healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, stats.Health / stats.MaxHealth, 10f * Time.deltaTime);
        healthTMP.text = $"{stats.Health} / {stats.MaxHealth}";
    }
}
