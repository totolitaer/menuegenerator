using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System.IO;


public class test : MonoBehaviour
{

    private string aktuellerPfadOhnePraefix = "MaterialTextures/01_Architektur/01-1_Beton/M_2K_CC0T_Concrete020_2K-JPG";
    private Object[] textures;

    // Start is called before the first frame update
    void Start()
    {
        textures = Resources.LoadAll(aktuellerPfadOhnePraefix, typeof(Texture2D));
        foreach (Texture2D t in textures)
        {
            Debug.Log("Texture2D :      " + t.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
