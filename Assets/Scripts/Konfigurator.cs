using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;
using Newtonsoft.Json;

public class Konfigurator : MonoBehaviour
{

    [Tooltip("Das genutzte Logging-Script muss in dem nutzenden Script instanziert werden.")]
    public Logging log; // immer erst initialisieren via log.Initialisiere();

    [Tooltip("true = Erhoehung der Loggingtiefe")]
    public bool debug = false;

    public GUI_Innentuer gui;
    public GENERATOR_Innentuer generator;



    // ---------------------------------------------------------------------------------------------------
    // Instanziierung der Klassen
    // ---------------------------------------------------------------------------------------------------



    // ---------------------------------------------------------------------------------------------------
    // Variablen
    // ---------------------------------------------------------------------------------------------------





    // Start is called before the first frame update
    void Awake()
    {
        //Initialisiere das Logging
        log.Initialisiere();
    }



    // ---------------------------------------------------------------------------------------------------
    // Initialaktionen
    // ---------------------------------------------------------------------------------------------------

    void Update()
    {
        if (!gui.colorLerpAktive)
        {

            if (Input.GetKeyDown("0"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("0");
                aktualisiere();
            }


            if (Input.GetKeyDown("1"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("1");
                aktualisiere();
            }

            if (Input.GetKeyDown("2"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("2");
                aktualisiere();
            }

            if (Input.GetKeyDown("3"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("3");
                aktualisiere();
            }

            if (Input.GetKeyDown("4"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("4");
                aktualisiere();
            }

            if (Input.GetKeyDown("5"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("5");
                aktualisiere();
            }

            if (Input.GetKeyDown("6"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("6");
                aktualisiere();
            }

            if (Input.GetKeyDown("7"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("7");
                aktualisiere();
            }
            if (Input.GetKeyDown("8"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("8");
                aktualisiere();
            }

            if (Input.GetKeyDown("9"))
            {
                gui.ermitteleAktionAnhandEingabeUndAktivemMenue("9");
                aktualisiere();
            }


            // Tastenaktionen F-Tasten

            // toggle Menü Hauptmenü
            if (Input.GetKeyDown("f1"))
            {
                gui.toggleMenue(gui.guiHauptmenue.Name, gui.guiCanvas.AktuellAktivesMenu);
                aktualisiere();
            }

            // Toggle Menü Innentür
            if (Input.GetKeyDown("f2"))
            {
                gui.toggleMenue(gui.guiInnentuer.Name, gui.guiCanvas.AktuellAktivesMenu);
                aktualisiere();
            }

            // Toggle Menü Konfigurator
            if (Input.GetKeyDown("f3"))
            {
                gui.toggleMenue(gui.guiKonfigurator.Name, gui.guiCanvas.AktuellAktivesMenu);
                aktualisiere();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            generator.Rotation("zu");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            generator.Rotation("auf");
        }
    }   



    // ---------------------------------------------------------------------------------------------------
    // MAIN
    // ---------------------------------------------------------------------------------------------------

    void aktualisiere ()
    {
        generator.oBT_Tuerblatt = gui.aktuelleGetoggelteInnentuer.Tuerblatt;
        generator.oBT_Zarge = gui.aktuelleGetoggelteInnentuer.Zarge; 
        generator.oBT_DrueckerFalz = gui.aktuelleGetoggelteInnentuer.DrueckerFalz;
        generator.oBT_DrueckerZier = gui.aktuelleGetoggelteInnentuer.DrueckerZier;
        generator.oBT_Band1 = gui.aktuelleGetoggelteInnentuer.Band1;
        generator.oBT_Band2 = gui.aktuelleGetoggelteInnentuer.Band2;
        generator.oBT_Bandaufnahme1 = gui.aktuelleGetoggelteInnentuer.Bandaufnahme1;
        generator.oBT_Bandaufnahme2 = gui.aktuelleGetoggelteInnentuer.Bandaufnahme2;
        generator.oBT_Schlosskasten = gui.aktuelleGetoggelteInnentuer.Schlosskasten;
        generator.oBT_Schliessblech = gui.aktuelleGetoggelteInnentuer.Schliessblech;
        generator.oBT_Schwelle = gui.aktuelleGetoggelteInnentuer.Schwelle;
        generator.Schritt_2();

        generator.materialKombinationZarge.Material1 = gui.aktuelleMaterialKombinationZarge.Material1;
        generator.materialKombinationZarge.Material2 = gui.aktuelleMaterialKombinationZarge.Material2;
        generator.materialKombinationZarge.Material3 = gui.aktuelleMaterialKombinationZarge.Material3;

        generator.materialKombinationTuerblatt.Material1 = gui.aktuelleMaterialKombinationTuerblatt.Material1;
        generator.materialKombinationTuerblatt.Material2 = gui.aktuelleMaterialKombinationTuerblatt.Material2;
        generator.materialKombinationTuerblatt.Material3 = gui.aktuelleMaterialKombinationTuerblatt.Material3;

        generator.materialKombinationDrueckerFalz.Material1 = gui.aktuelleMaterialKombinationDrueckerFalz.Material1;
        generator.materialKombinationDrueckerFalz.Material2 = gui.aktuelleMaterialKombinationDrueckerFalz.Material2;
        generator.materialKombinationDrueckerFalz.Material3 = gui.aktuelleMaterialKombinationDrueckerFalz.Material3;

        generator.materialKombinationDrueckerZier.Material1 = gui.aktuelleMaterialKombinationDrueckerZier.Material1;
        generator.materialKombinationDrueckerZier.Material2 = gui.aktuelleMaterialKombinationDrueckerZier.Material2;
        generator.materialKombinationDrueckerZier.Material3 = gui.aktuelleMaterialKombinationDrueckerZier.Material3;

        generator.materialKombinationBand1.Material1 = gui.aktuelleMaterialKombinationBand1.Material1;
        generator.materialKombinationBand1.Material2 = gui.aktuelleMaterialKombinationBand1.Material2;
        generator.materialKombinationBand1.Material3 = gui.aktuelleMaterialKombinationBand1.Material3;

        generator.materialKombinationBand2.Material1 = gui.aktuelleMaterialKombinationBand2.Material1;
        generator.materialKombinationBand2.Material2 = gui.aktuelleMaterialKombinationBand2.Material2;
        generator.materialKombinationBand2.Material3 = gui.aktuelleMaterialKombinationBand2.Material3;

        generator.materialKombinationBandaufnahme1.Material1 = gui.aktuelleMaterialKombinationBandaufnahme1.Material1;
        generator.materialKombinationBandaufnahme1.Material2 = gui.aktuelleMaterialKombinationBandaufnahme1.Material2;
        generator.materialKombinationBandaufnahme1.Material3 = gui.aktuelleMaterialKombinationBandaufnahme1.Material3;

        generator.materialKombinationBandaufnahme2.Material1 = gui.aktuelleMaterialKombinationBandaufnahme2.Material1;
        generator.materialKombinationBandaufnahme2.Material2 = gui.aktuelleMaterialKombinationBandaufnahme2.Material2;
        generator.materialKombinationBandaufnahme2.Material3 = gui.aktuelleMaterialKombinationBandaufnahme2.Material3;
        
        generator.materialKombinationSchlosskasten.Material1 = gui.aktuelleMaterialKombinationSchlosskasten.Material1;
        generator.materialKombinationSchlosskasten.Material2 = gui.aktuelleMaterialKombinationSchlosskasten.Material2;
        generator.materialKombinationSchlosskasten.Material3 = gui.aktuelleMaterialKombinationSchlosskasten.Material3;

        generator.materialKombinationSchliessblech.Material1 = gui.aktuelleMaterialKombinationSchliessblech.Material1;
        generator.materialKombinationSchliessblech.Material2 = gui.aktuelleMaterialKombinationSchliessblech.Material2;
        generator.materialKombinationSchliessblech.Material3 = gui.aktuelleMaterialKombinationSchliessblech.Material3;

        generator.materialKombinationSchwelle.Material1 = gui.aktuelleMaterialKombinationSchwelle.Material1;
        generator.materialKombinationSchwelle.Material2 = gui.aktuelleMaterialKombinationSchwelle.Material2;
        generator.materialKombinationSchwelle.Material3 = gui.aktuelleMaterialKombinationSchwelle.Material3;

        generator.toggleMaterial3();
    }

}
