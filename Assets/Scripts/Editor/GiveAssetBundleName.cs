using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GiveAssetBundleName : Editor
{

    [MenuItem("Assets/ZTools/Asset Bundles Give Name")]
    static void GiveName()
    {
        Object[] go;

        go = Resources.LoadAll("PrefabsToAssetBundles", typeof(GameObject));

        foreach (var g in go)
        {
            string assetPath = AssetDatabase.GetAssetPath(g);

            Debug.Log(assetPath + " : " + g.name);
            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(g.name, "");
        }

    }
}
