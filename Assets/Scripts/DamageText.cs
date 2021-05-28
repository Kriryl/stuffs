using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public static GameObject Create(float damageAmount, Vector3 pos)
    {
        GameObject newDamageText = Instantiate(FindObjectOfType<TextGetter>().damageDisplayerObject, pos, Quaternion.identity);
        newDamageText.AddComponent<DamageText>();
        newDamageText.GetComponentInChildren<TextMeshPro>().text = damageAmount.ToString();
        newDamageText.AddComponent<BillBoard>();
        GameObject damageTextParent = GameObject.Find("Damage Texts");
        if (damageTextParent)
        {
            newDamageText.transform.parent = damageTextParent.transform;
        }
        return newDamageText;
    }

    public void DestroyText(float secs)
    {
        Destroy(gameObject, secs);
    }
}
