using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI damageText;

    public void SetDamageText(float damage)
    {
        damageText.text = "-" + damage.ToString();
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
