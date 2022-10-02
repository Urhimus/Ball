using UnityEngine;
using TMPro;

public abstract class Star : MonoBehaviour
{
    protected TextMeshProUGUI tmPro;

    public virtual void Awake()
    {
        tmPro = GameObject.Find(gameObject.name + "TMP").GetComponent<TextMeshProUGUI>();
    }

    public abstract bool ConditionMet();
    public abstract void Description();
}
