using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateInventoryItemList
{
    [MenuItem("Assets/Create/Weapon List")]
    public static WeaponList Create()
    {
        WeaponList asset = ScriptableObject.CreateInstance<WeaponList>();

        AssetDatabase.CreateAsset(asset, "Assets/WeaponList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}