using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;
using UnityEditor;
using System.Net;
using System.Security.Cryptography;
using System.Collections.Specialized;
using UnityEngine.UI;
using System.Globalization;

public class GENERATOR_Innentuer : MonoBehaviour
{

    public Material IN;
    public Material OUT;
    public Material ROT;

    // Objekt-Bestandteile
    public string oBT_Tuerblatt = "";
    public string oBT_Zarge = "";
    public string oBT_DrueckerFalz = "";
    public string oBT_DrueckerZier = "";
    public string oBT_Band1 = "";
    public string oBT_Band2 = "";
    public string oBT_Bandaufnahme1 = "";
    public string oBT_Bandaufnahme2 = "";
    public string oBT_Schlosskasten = "";
    public string oBT_Schliessblech = "";
    public string oBT_Schwelle = "";

    public Vector3 position;
    public Vector3 rotation;


    private GameObject[] gosInPrefabOrdner;
    private GameObject goNeueInstanz;
    private GameObject goNeuInHierarchie;

    // ---------------------------------------------------------------------------------------------------
    // Objekt-Konstruktion
    // ---------------------------------------------------------------------------------------------------

    // hierarchiePfadObjekt 
    private string hierarchiePfadPrefabs = "TEMP_PREFABS";
    private string[] objektBestandteile = { "Tuerblatt", "Zarge", "DrueckerFalz", "DrueckerZier", "Band1", "Band2", "Bandaufnahme1", "Bandaufnahme2", "Schlosskasten", "Schliessblech", "Schwelle" };
    private int objektBestandteileAnzahl;
    private string[] objektBestandteileAktuell;
    private string[] objektRotationen = { "Rotation1" };

    // ---------------------------------------------------------------------------------------------------
    //  Unity Hierarchie(-Elemente)
    // ---------------------------------------------------------------------------------------------------

    // hierarchiePfadObjekt 
    string hierarchiePfadObjekt = "INNENTUER";

    // 2D-Array mit Zuordnung 
    // 1 Position = Bestandteil
    // 2 Position = das dazugehörige parent GameObjekt)
    string[,] objektElementeUndParents = {
        // Level 1  
        { "OUT_Zarge", "INNENTUER" },
        // Level 2
        {"3D_Zarge" , "OUT_Zarge"},
        {"IN_Bandaufnahme1" , "OUT_Zarge"},
        {"OUT_Bandaufnahme1" , "OUT_Zarge"},
        {"IN_Bandaufnahme2" , "OUT_Zarge"},
        {"OUT_Bandaufnahme2" , "OUT_Zarge"},
        {"IN_Schliessblech" , "OUT_Zarge"},
        {"OUT_Schliessblech" , "OUT_Zarge"},
        {"IN_Tuerblatt" , "OUT_Zarge"},
        {"OUT_Tuerblatt" , "OUT_Zarge"},
        {"IN_Schwelle" , "OUT_Zarge"},
        {"OUT_Schwelle" , "OUT_Zarge"},
        // Level 3        
        {"3D_Bandaufnahme1" , "OUT_Bandaufnahme1"},
        {"3D_Bandaufnahme2" , "OUT_Bandaufnahme2"},
        {"3D_Schliessblech" , "OUT_Schliessblech"},
        {"3D_Tuerblatt" , "OUT_Tuerblatt"},
        {"IN_DrueckerFalz" , "OUT_Tuerblatt"},
        {"OUT_DrueckerFalz" , "OUT_Tuerblatt"},
        {"IN_DrueckerZier" , "OUT_Tuerblatt"},
        {"OUT_DrueckerZier" , "OUT_Tuerblatt"},
        {"IN_Band1" , "OUT_Tuerblatt"},
        {"OUT_Band1" , "OUT_Tuerblatt"},
        {"IN_Band2" , "OUT_Tuerblatt"},
        {"OUT_Band2" , "OUT_Tuerblatt"},
        {"IN_Schlosskasten" , "OUT_Tuerblatt"},
        {"OUT_Schlosskasten" , "OUT_Tuerblatt"},
        {"ROTIN_Rotation1" , "OUT_Tuerblatt"},
        {"3D_Schwelle" , "OUT_Schwelle"},
        // Level 4
        {"3D_DrueckerFalz" , "OUT_DrueckerFalz"},
        {"3D_DrueckerZier" , "OUT_DrueckerZier"},
        {"3D_Band1" , "OUT_Band1"},
        {"3D_Band2" , "OUT_Band2"},
        {"3D_Schlosskasten" , "OUT_Schlosskasten"}

    };

    //string[,] objektRotationenUndParents = {
    //    // Rotationen
    //    { "ROT_Rotation1", "OUT_Zarge" },
    //};

    // Rotation
    private Transform rotierer;
    private Transform rotierachseBezug;
    public float rotationZeit = 1.0f;
    public float rotationWinkel = -90.0f;
    public bool rotating = false;
    public bool tuerAuf = false;

    // ---------------------------------------------------------------------------------------------------
    // GUI
    // ---------------------------------------------------------------------------------------------------


    public class MaterialKombination
    {
        public string Material1 { get; set; } // Hauptmaterial
        public string Material2 { get; set; } // Metall
        public string Material3 { get; set; } // Glas
    }
    public MaterialKombination materialKombinationTuerblatt = new MaterialKombination();
    public MaterialKombination materialKombinationZarge = new MaterialKombination();
    public MaterialKombination materialKombinationBand1 = new MaterialKombination();
    public MaterialKombination materialKombinationBand2 = new MaterialKombination();
    public MaterialKombination materialKombinationBandaufnahme1 = new MaterialKombination();
    public MaterialKombination materialKombinationBandaufnahme2 = new MaterialKombination();
    public MaterialKombination materialKombinationDrueckerFalz = new MaterialKombination();
    public MaterialKombination materialKombinationDrueckerZier = new MaterialKombination();
    public MaterialKombination materialKombinationSchliessblech = new MaterialKombination();
    public MaterialKombination materialKombinationSchlosskasten = new MaterialKombination();
    public MaterialKombination materialKombinationSchwelle = new MaterialKombination();


    public Material[] materialTuerblatt;

     // Start is called before the first frame update
     void Awake()
    {
        // leite Variablen ab
        //objektBestandteileAnzahl = objektBestandteile.Length;
        //objektBestandteileAktuell = new string[objektBestandteileAnzahl];
    }

    // Start is called before the first frame update
    void Start()
    {
        Schritt_1();
        Schritt_2();
        toggleMaterial3();
    }

    // Update is called once per frame
    public void Rotation(string richtung)
    {
        // Rotation
        if (richtung == "zu" && !rotating && tuerAuf)
        {
            rotierer = GameObject.Find("OUT_Tuerblatt").GetComponent<Transform>();
            rotierachseBezug = GameObject.Find("ROTIN_Rotation1").GetComponent<Transform>();
            //Debug.Log(" S32 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
            //Debug.Log(" S32 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));
            StartCoroutine(Rotate(rotierer, rotierachseBezug, Vector3.up, -rotationWinkel, rotationZeit));
            tuerAuf = false;
        }

        if (richtung == "auf" && !rotating && !tuerAuf)
        {
            rotierer = GameObject.Find("OUT_Tuerblatt").GetComponent<Transform>();
            rotierachseBezug = GameObject.Find("ROTIN_Rotation1").GetComponent<Transform>();
            //Debug.Log(" S32 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
            //Debug.Log(" S32 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));
            StartCoroutine(Rotate(rotierer, rotierachseBezug, Vector3.up, rotationWinkel, rotationZeit));
            tuerAuf = true;
        }

    }

    //Schritt 1: Generiere Objekt-Hierarchie
    void Schritt_1()
    {

        int anzahlobjektElementeUndParents = objektElementeUndParents.Length / 2;
        Debug.Log("Schritt 1: Generiere Objekt-Hierarchie aus insgesamt " + anzahlobjektElementeUndParents.ToString() + " Elementen gemaess Excel Datei Punkt 6. Unity Hierarchie(-Elemente)");

        // generiere Hirarchie

        // generiere als hierarchiePfadObjekt ein leeres GameObjekt
        goNeuInHierarchie = new GameObject(hierarchiePfadObjekt);

        // generiere für jedes Bestandteil des Objeketes ein GameObjekt mit leerem MeshFilter und leerem MeshRenderer
        for (int i = 0; i < anzahlobjektElementeUndParents; i++)
        {
            //log.wl(objektElementeUndParents[i, 0] + " --- " + objektElementeUndParents[i, 1]);
            goNeuInHierarchie = new GameObject(objektElementeUndParents[i, 0]);
            MeshFilter meshFilter = goNeuInHierarchie.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = goNeuInHierarchie.AddComponent<MeshRenderer>();

            // NEWNEWNEW
            //if (objektElementeUndParents[i, 0] == "3D_Tuerblatt")
            //{
            // meshRenderer.materials = new Material[3];
            //}


            // ordne jedem Bestandteil (child) dem jeweiligen parent GameObjekt zu
            GameObject.Find(objektElementeUndParents[i, 0]).transform.parent = GameObject.Find(objektElementeUndParents[i, 1]).transform;

        };

    }

    public void Schritt_2()
    {
        // generiere Hirarchie
        // generiere als hierarchiePfadPrefabs ein leeres GameObjekt
        goNeuInHierarchie = new GameObject(hierarchiePfadPrefabs);

        string[] gesuchtePrefabNamen = { oBT_Tuerblatt, oBT_Zarge };

        // ermittele alle Prefabs im Prefab Order "Assets/Resourcen/Prefabs"
        gosInPrefabOrdner = Resources.LoadAll<GameObject>("Prefabs");


       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Tuerblatt, "Tuerblatt");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Zarge, "Zarge");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_DrueckerFalz, "DrueckerFalz");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_DrueckerZier, "DrueckerZier");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Band1, "Band1");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Band2, "Band2");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Bandaufnahme1, "Bandaufnahme1");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Bandaufnahme2, "Bandaufnahme2");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Schlosskasten, "Schlosskasten");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Schliessblech, "Schliessblech");
       InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Schwelle, "Schwelle");

       Schritt_3();
    }

    void InstanzierePrefabWennNameDesPrefabsExistiert(string nameDesGesuchtenPrefabs, string bestandteil)
    {
        //Debug.Log("InstanzierePrefabWennNameDesPrefabsExistiert");
        //Debug.Log("nameDesGesuchtenPrefab: " + nameDesGesuchtenPrefabs + " - bestandteil: " + bestandteil);

        bool esGibtEinPrefabDesGesuchtenNamens = false;

        // checke für jedes Prefab im Prefab-Ordner, ... 
        foreach (GameObject goInPrefabOrdner in gosInPrefabOrdner)
        {

            // ob das Prefab (ohne den seitens Unity automatisch hinzugefühten Suffix " (UnityEngine.GameObject)" so heißt, wie der Name des gesuchten Prefabs
            // Wenn ja:
            if (KappeStringSuffix(goInPrefabOrdner.ToString(), " (UnityEngine.GameObject)") == nameDesGesuchtenPrefabs)
            {
                esGibtEinPrefabDesGesuchtenNamens = true;

                // generiere ein neues GameObject in der Scene aus einer Intanz des Prefabs
                goNeueInstanz = goInPrefabOrdner;
                Instantiate(goNeueInstanz);

                // ordne dem neuen GameObject das parent GameObjekt zu
                GameObject.Find(nameDesGesuchtenPrefabs + "(Clone)").transform.parent = GameObject.Find(hierarchiePfadPrefabs).transform;

                // benenne das neue GameObjekt in den jeweiligen Namen des Bestandteils 
                GameObject.Find(nameDesGesuchtenPrefabs + "(Clone)").name = bestandteil;

            }
        }

        if (!esGibtEinPrefabDesGesuchtenNamens)
        {
            Debug.Log("ACHTUNG: Dem Objektbestandteil OBT_" + bestandteil + " wurde ein Name zugewiesen, zu dem es kein dazugehöriges Prefab im Prefab-Ordner gibt.");
        }
    }

    string KappeStringSuffix(string original, string suffix)
    {
        int positionAnDerSuffixImOriginalStringBeginnt = original.IndexOf(suffix);
        string originalOhneSuffix = original.Substring(0, positionAnDerSuffixImOriginalStringBeginnt);
        return originalOhneSuffix;
    }

    void Schritt_3()
    {
        //Debug.Log(" S3 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
        //Debug.Log(" S3 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));

        //Debug.Log("Schritt 3: Ordne die MeshFilter der Prefab-GameObjekte den dazugehoerigen GameObjekten des Unity-Hierarchie zu ");

        // TEMP_PREFABS --> INNENTUER
        string[,] tempZuObjekt = {
            // Zarge
            { "/Zarge/OUT_Zarge", "/OUT_Zarge" },
            { "/Zarge/3D_Zarge", "/OUT_Zarge/3D_Zarge" },
            { "/Zarge/IN_Bandaufnahme1", "/OUT_Zarge/IN_Bandaufnahme1" },
            { "/Zarge/IN_Bandaufnahme2", "/OUT_Zarge/IN_Bandaufnahme2" },
            { "/Zarge/IN_Schliessblech", "/OUT_Zarge/IN_Schliessblech" },
            { "/Zarge/IN_Tuerblatt", "/OUT_Zarge/IN_Tuerblatt" },
            { "/Zarge/IN_Tuerblatt", "/OUT_Zarge/IN_Tuerblatt" },
            //{ "/Zarge/IN_Schwelle", "/OUT_Zarge/IN_Schwelle" },
            //{ "/Zarge/ROTIN_Rotation1", "/OUT_Zarge/ROT_Rotation1" },
            // Bandaufnahme1
            { "/Bandaufnahme1/OUT_Bandaufnahme", "/OUT_Zarge/OUT_Bandaufnahme1" },
            { "/Bandaufnahme1/3D_Bandaufnahme", "/OUT_Zarge/OUT_Bandaufnahme1/3D_Bandaufnahme1" },
            // Bandaufnahme2
            { "/Bandaufnahme2/OUT_Bandaufnahme", "/OUT_Zarge/OUT_Bandaufnahme2" },
            { "/Bandaufnahme2/3D_Bandaufnahme", "/OUT_Zarge/OUT_Bandaufnahme2/3D_Bandaufnahme2" },
            // Schliessblech
            { "/Schliessblech/OUT_Schliessblech", "/OUT_Zarge/OUT_Schliessblech" },
            { "/Schliessblech/3D_Schliessblech", "/OUT_Zarge/OUT_Schliessblech/3D_Schliessblech" },
            // Tuerblatt
            { "/Tuerblatt/OUT_Tuerblatt", "/OUT_Zarge/OUT_Tuerblatt" },
            { "/Tuerblatt/3D_Tuerblatt", "/OUT_Zarge/OUT_Tuerblatt/3D_Tuerblatt" },
            { "/Tuerblatt/IN_DrueckerFalz", "/OUT_Zarge/OUT_Tuerblatt/IN_DrueckerFalz" },
            { "/Tuerblatt/IN_DrueckerZier", "/OUT_Zarge/OUT_Tuerblatt/IN_DrueckerZier" },
            { "/Tuerblatt/IN_Band1", "/OUT_Zarge/OUT_Tuerblatt/IN_Band1" },
            { "/Tuerblatt/IN_Band2", "/OUT_Zarge/OUT_Tuerblatt/IN_Band2" },
            { "/Tuerblatt/IN_Schlosskasten", "/OUT_Zarge/OUT_Tuerblatt/IN_Schlosskasten" },
            // Schwelle
            //{ "/Schwelle/OUT_Schwelle", "/OUT_Zarge/OUT_Schwelle" },
            //{ "/Schwelle/3D_Schwelle", "/OUT_Zarge/OUT_Schwelle/3D_Schwelle" },
            // DrueckerFalz
            { "/DrueckerFalz/OUT_Druecker", "/OUT_Zarge/OUT_Tuerblatt/OUT_DrueckerFalz" },
            { "/DrueckerFalz/3D_Druecker", "/OUT_Zarge/OUT_Tuerblatt/OUT_DrueckerFalz/3D_DrueckerFalz" },
            // DrueckerZier
            { "/DrueckerZier/OUT_Druecker", "/OUT_Zarge/OUT_Tuerblatt/OUT_DrueckerZier" },
            { "/DrueckerZier/3D_Druecker", "/OUT_Zarge/OUT_Tuerblatt/OUT_DrueckerZier/3D_DrueckerZier" },
            // Band1
            { "/Band1/OUT_Band", "/OUT_Zarge/OUT_Tuerblatt/OUT_Band1" },
            { "/Band1/3D_Band", "/OUT_Zarge/OUT_Tuerblatt/OUT_Band1/3D_Band1" },
            // Band 2
            { "/Band2/OUT_Band", "/OUT_Zarge/OUT_Tuerblatt/OUT_Band2" },
            { "/Band2/3D_Band", "/OUT_Zarge/OUT_Tuerblatt/OUT_Band2/3D_Band2" },
            // Level 4
            { "/Schlosskasten/OUT_Schlosskasten", "/OUT_Zarge/OUT_Tuerblatt/OUT_Schlosskasten" },
            { "/Schlosskasten/3D_Schlosskasten", "/OUT_Zarge/OUT_Tuerblatt/OUT_Schlosskasten/3D_Schlosskasten" },
            // Rotationen
            { "/Tuerblatt/ROTIN_Rotation1", "/OUT_Zarge/OUT_Tuerblatt/ROTIN_Rotation1" },
        };

        int anzahlTempZuObjekt = tempZuObjekt.Length / 2;

        for (int i = 0; i < anzahlTempZuObjekt; i++)
        {
            // weise dem Objekt in der Hierarchie das mesh der dazugehörigen Prefab-GameObjekt-Instanz zu 
            if (GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]))
            {
                GameObject.Find(hierarchiePfadObjekt + tempZuObjekt[i, 1]).GetComponent<MeshFilter>().mesh = GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]).GetComponent<MeshFilter>().mesh;
                GameObject.Find(hierarchiePfadObjekt + tempZuObjekt[i, 1]).transform.position = GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]).transform.position;
                GameObject.Find(hierarchiePfadObjekt + tempZuObjekt[i, 1]).transform.rotation = GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]).transform.rotation;
            }
        }
        Destroy(GameObject.Find(hierarchiePfadPrefabs));


        //Renderer r;
        GameObject.Find("OUT_Band1").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_Band2").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_Bandaufnahme1").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_Bandaufnahme2").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_DrueckerFalz").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_DrueckerZier").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_Schliessblech").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_Schlosskasten").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_Schwelle").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_Tuerblatt").GetComponent<Renderer>().material = OUT;
        GameObject.Find("OUT_Zarge").GetComponent<Renderer>().material = OUT;

        GameObject.Find("IN_Band1").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_Band2").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_Bandaufnahme1").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_Bandaufnahme2").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_DrueckerFalz").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_DrueckerZier").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_Schliessblech").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_Schlosskasten").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_Schwelle").GetComponent<Renderer>().material = IN;
        GameObject.Find("IN_Tuerblatt").GetComponent<Renderer>().material = IN;
        //GameObject.Find("IN_Zarge").GetComponent<Renderer>().material = IN;

        //GameObject.Find("ROTIN_Rotation1").GetComponent<Renderer>().material = ROT;
        ////r.material = OUT;

        foreach (string objektBestandteil in objektBestandteile)
        {
            if (objektBestandteil != "Zarge")
            {
                GameObject.Find("OUT_" + objektBestandteil).transform.position = GameObject.Find("IN_" + objektBestandteil).transform.position;
                GameObject.Find("OUT_" + objektBestandteil).transform.rotation = GameObject.Find("IN_" + objektBestandteil).transform.rotation;
            }
        }

        Vector3 ls = GameObject.Find("3D_DrueckerZier").transform.localScale;
        //ls += new Vector3(ls.x, ls.y, -ls.z);
        ls = new Vector3(1, 1, -1);
        GameObject.Find("3D_DrueckerZier").transform.localScale = ls;

        GameObject.Find("OUT_Zarge").transform.position = position;
        GameObject.Find("OUT_Zarge").transform.rotation = Quaternion.Euler(rotation);

    }
 
    public void toggleMaterial3()
    {


        //Debug.Log("Array-Laenge: " + mTuerblatt.GetLength(0).ToString());
        //Debug.Log("Array-Index:  " + aktuellesMaterial.ToString());
        //Debug.Log("Material 1:   " + mTuerblatt[aktuellesMaterial, 0]);
        //Debug.Log("Material 2:    " + mTuerblatt[aktuellesMaterial, 1]);
        //Debug.Log("Material 3:    " + mTuerblatt[aktuellesMaterial, 2]);
        //Debug.Log("--------------------------------------------------------------------------------------");
        Material[] yourMaterial;
        Renderer r;

        // Zarge ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Zarge").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationZarge.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationZarge.Material1);
        }
        else if (materialKombinationZarge.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationZarge.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationZarge.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationZarge.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationZarge.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationZarge.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;


        // Tuerblatt ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Tuerblatt").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json

        if (materialKombinationTuerblatt.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationTuerblatt.Material1);
        }
        
        else if (materialKombinationTuerblatt.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationTuerblatt.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationTuerblatt.Material2);
        }

        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationTuerblatt.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationTuerblatt.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationTuerblatt.Material3);
        }
 
        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;


        // DrueckerFalz ------------------------------------------------------------------------------

        r = GameObject.Find("3D_DrueckerFalz").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationDrueckerFalz.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationDrueckerFalz.Material1);
        }
        else if (materialKombinationDrueckerFalz.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationDrueckerFalz.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationDrueckerFalz.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationDrueckerFalz.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationDrueckerFalz.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationDrueckerFalz.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;



        // DrueckerZier ------------------------------------------------------------------------------

        r = GameObject.Find("3D_DrueckerZier").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationDrueckerZier.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationDrueckerZier.Material1);
        }
        else if (materialKombinationDrueckerZier.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationDrueckerZier.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationDrueckerZier.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationDrueckerZier.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationDrueckerZier.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationDrueckerZier.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;



        // Band1 ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Band1").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationBand1.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBand1.Material1);
        }
        else if (materialKombinationBand1.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBand1.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationBand1.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBand1.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationBand1.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationBand1.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;


        // Band2 ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Band2").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationBand2.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBand2.Material1);
        }
        else if (materialKombinationBand2.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBand2.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationBand2.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBand2.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationBand2.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationBand2.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;



        // Bandaufnahme1 ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Bandaufnahme1").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationBandaufnahme1.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBandaufnahme1.Material1);
        }
        else if (materialKombinationBandaufnahme1.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBandaufnahme1.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationBandaufnahme1.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBandaufnahme1.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationBandaufnahme1.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationBandaufnahme1.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;



        // Bandaufnahme2 ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Bandaufnahme2").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationBandaufnahme2.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBandaufnahme2.Material1);
        }
        else if (materialKombinationBandaufnahme2.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBandaufnahme2.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationBandaufnahme2.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationBandaufnahme2.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationBandaufnahme2.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationBandaufnahme2.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;


        // Schliessblech ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Schliessblech").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationSchliessblech.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchliessblech.Material1);
        }
        else if (materialKombinationSchliessblech.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchliessblech.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationSchliessblech.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchliessblech.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationSchliessblech.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationSchliessblech.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;



        // Schlosskasten ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Schlosskasten").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationSchlosskasten.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchlosskasten.Material1);
        }
        else if (materialKombinationSchlosskasten.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchlosskasten.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationSchlosskasten.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchlosskasten.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationSchlosskasten.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationSchlosskasten.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;



        // Schwelle ------------------------------------------------------------------------------

        r = GameObject.Find("3D_Schwelle").GetComponent<Renderer>();

        // setze Size von Array genau auf die Anzahl der Materialien in Json
        if (materialKombinationSchwelle.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            yourMaterial = new Material[1];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchwelle.Material1);
        }
        else if (materialKombinationSchwelle.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            yourMaterial = new Material[2];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchwelle.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationSchwelle.Material2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            yourMaterial = new Material[3];
            yourMaterial[0] = Resources.Load<Material>(materialKombinationSchwelle.Material1);
            yourMaterial[1] = Resources.Load<Material>(materialKombinationSchwelle.Material2);
            yourMaterial[2] = Resources.Load<Material>(materialKombinationSchwelle.Material3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        r.materials = yourMaterial;




    }

    private IEnumerator Rotate(Transform rotierer, Transform rotierachseBezug, Vector3 rotateAxis, float degrees, float totalTime)
    {
        Debug.Log(rotierachseBezug.ToString());

        if (rotating)
        {
            yield return null;
        }
        else
        {
            rotating = true;
        }


        float rate = degrees / totalTime;

        //Start Rotate
        for (float i = 0.0f; Mathf.Abs(i) < Mathf.Abs(degrees); i += Time.deltaTime * rate)
        {
            rotierer.RotateAround(rotierachseBezug.position, rotateAxis, Time.deltaTime * rate);
            yield return null;
        }

        rotating = false;
    }

}
