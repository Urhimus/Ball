using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialiizableDictionary<TKey, KValue> : Dictionary<TKey, KValue>, ISerializationCallbackReceiver
{
    private List<TKey> key = new List<TKey>();
    private List<KValue> value = new List<KValue> ();

    public void OnBeforeSerialize()
    {
        key.Clear ();
        value.Clear ();
        foreach (KeyValuePair<TKey, KValue> kvp in this)
        {
            key.Add(kvp.Key);
            value.Add(kvp.Value);
        }

    }

    public void OnAfterDeserialize()
    {
        this.Clear ();

        for(int i = 0; i < key.Count; i++)
        {
            this.Add(key[i], value[i]);
        }
    }
}
