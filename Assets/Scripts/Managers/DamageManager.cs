using UnityEngine;
public class DamageManager : MonoBehaviour
{
    // singleton method (call a instance of this class to call its methods): 
    public static DamageManager Instance;

    [Header("Config")]
    [SerializeField] private DamageText damageTextPrefab;

    private void Awake()
    {
        // this is referring to this DamageManager
        Instance = this;
    }
    public void ShowDamageText(float damageAmount, Transform parent)
    {
        DamageText text = Instantiate(damageTextPrefab, parent);
        text.transform.position = parent.position;
        text.SetDamageText(damageAmount);
    }
}
