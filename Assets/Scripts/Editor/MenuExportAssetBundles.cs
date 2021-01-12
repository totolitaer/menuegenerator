using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class MenuExportAssetBundles : Editor
{

    [MenuItem("Assets/ZTools/Asset Bundles Build")]

    static void BuildAllAssetBundles()
    {

        string outputPath = (@"F:\AssetBundles");
        string workingPath = "Assets\\Resources\\PrefabsToAssetBundles";

        Object[] go;
        var assetNamen = new List<string>();
        var assetPfade = new List<string>();

        var directories = Directory.GetDirectories(workingPath, "*", SearchOption.AllDirectories);

        foreach (var d in directories)
        {

            //Debug.Log("Directory: " + d);
            string dReverse = ReverseString(d);
            //Debug.Log("Directory reverse: " + dReverse);
            int index = dReverse.IndexOf("\\");
            //Debug.Log(index.ToString());
            dReverse = dReverse.Substring(0, index);
            string subfolder = ReverseString(dReverse);
            //Debug.Log(subfolder);

            go = Resources.LoadAll("PrefabsToAssetBundles\\" + subfolder, typeof(GameObject));
            foreach (var g in go)
            {
                //Debug.Log(g.name);
                string zielPfad = (outputPath + "\\" + subfolder + "\\" + g.name);

                assetNamen.Add(g.name);
                assetPfade.Add(zielPfad);

                if (!Directory.Exists(zielPfad))
                {
                    var folder = Directory.CreateDirectory(zielPfad);
                }
            }

        }

        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        for (var index = 0; index < assetNamen.Count; index++)
        {
            Debug.Log(assetNamen[index] + " " + assetPfade[index]);
            File.Move(@"F:\AssetBundles\" + assetNamen[index], assetPfade[index] + "\\" + assetNamen[index]);
            File.Move(@"F:\AssetBundles\" + assetNamen[index] + ".manifest", assetPfade[index] + "\\" + assetNamen[index] + ".manifest");
        }




    }

    public static string ReverseString(string s)
    {
        char[] arr = s.ToCharArray();
        System.Array.Reverse(arr);
        return new string(arr);
    }
}
