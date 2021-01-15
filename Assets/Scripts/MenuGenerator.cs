using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class MenuGenerator : MonoBehaviour
{

    private Canvas myCanvas;
    private GameObject myGO;
    private GameObject myGO2;
    private GameObject[] menuId = new GameObject[99999];
    private Image myImage;
    private Font myFont;
    private Text myText;


    // String, der in der GUI den aktuellen Menüpfad anzeigt
    private string menuPfad = "";

    // String, der im Konfigurator die Anzahl in der Ergebnisliste anzeigt
    private string anzahlErgebnis = "";

    private int breiteMenuCanvas = 1920;
    private int hoeheMenuCanvas = 1080;

    // Konfigurationsmenü
    private int breiteMenuContainer = 960;
    private int hoeheMenuContainer = 1080;
    private int breiteMenuZeile = 960;
    private int hoeheMenuZeile = 120;
    private int breiteMenuLogo = 80;
    private int hoeheMenuLogo = 80;
    private int breiteMenuText = 800;
    private int hoeheMenuText = 120;

    // Konfigurator
    private int BreiteContainer = 960;
    private int HoeheContainer = 1080;

    //private int hoeheMenueInfoContainer = 30;

    // Konfiguration für Menu
    private Color32 colorMenuContainer = new Color32(0, 0, 0, 180);
    private Color32 colorMenuHeaderContainer = new Color32(0, 0, 0, 150);
    private Color32 colorMenuHeaderLogo = new Color32(255, 255, 255, 100);
    private string headerFontType = "Arial.ttf";
    private int headerFontSize = 60;
    private FontStyle headerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

    // Konfiguration für menuPunkt
    private Color32 colorMenumenuPunktContainer = new Color32(0, 0, 0, 140);
    private Color32 colorMenumenuPunktAktiv = new Color32(255, 116, 0, 255);
    private Color32 colorMenumenuPunktDefault = new Color32(255, 255, 255, 255);
    private string menuPunktFontType = "Arial.ttf";
    private int menuPunktFontSize = 40;
    private FontStyle menuPunktFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
    private int menuPunktVerschiebungX = 140;
    private int menuPunktVerschiebungY = -38;


    // Konfiguration für Info
    private Color32 colorMenuMenueInfoContainer = new Color32(0, 0, 0, 220);
    private string infoFontType = "Arial.ttf";
    private int infoFontSize = 20;
    private FontStyle infoFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
    private int infoVerschiebungX = 20;
    private int infoVerschiebungY = -128;


    private int verschiebungYErste = -230;
    private int verschiebungYWeitere = -130;

    private string aktuellesMenu = "";
    private string hoechsteMenuId = "";

    private string aktuellAktivesMenu = "";


    // ---------------------------------------------------------------------------------------------------
    // Klassen
    // ---------------------------------------------------------------------------------------------------


    public class HauptmenueParameter
    {
        public List<string> Menuepunkt { get; set; }
        public string[][] Toggle { get; set; }
        public string[][] ToggleAktion { get; set; }
        public int[] TogglePunktIndex { get; set; }
        public int MaximaleAnzahlMenuepunkte = 8;
        public int MaximaleAnzahlToogglePunkte = 10;
    }

    public class InnentuerParameter
    {
        public List<string> Menuepunkt { get; set; }
        public string[][] Toggle { get; set; }
        public string[][] ToggleAktion { get; set; }
        public int[] TogglePunktIndex { get; set; }
        public int MaximaleAnzahlMenuepunkte = 8;
        public int MaximaleAnzahlToogglePunkte = 10;
    }

    public class GuiHauptmenue
    {
        public string Name = "Hauptmenü";

        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static int VerschiebungX = 480;
        private static int VerschiebungY = 0;

        // Hauptmenue
        public string NameInCanvas = "Hauptmenue";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;
        public int Width = 960;
        public int Height = 1080;
        public Color32 Color = new Color32(0, 0, 0, 100);

        // HauptmenueHeaderContainer
        public string HeaderContainerNameInCanvas = "HauptmenueHeaderContainer";
        public string HeaderContainerParent = "Hauptmenue";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public int HeaderContainerHeight = 120;
        public Color32 HeaderContainerColor = new Color32(0, 0, 0, 250);

        // HauptmenueHeaderLogo
        public string HeaderLogoNameInCanvas = "HauptmenueHeaderLogo";
        public string HeaderLogoParent = "HauptmenueHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public int HeaderLogoWidth = 80;
        public int HeaderLogoHeight = 80;
        public Color32 HeaderLogoColor = new Color32(255, 255, 255, 255);
        public string HeaderLogoSourceImage = "Logos/cog-wheel-silhouette_white";

        // HauptmenueHeaderText
        public string HeaderTextNameInCanvas = "HauptmenueHeaderText";
        public string HeaderTextParent = "HauptmenueHeaderContainer";
        public string HeaderTextText = "Hauptmenü";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public int HeaderTextWidth = 780;
        public int HeaderTextHeight = 120;
        public Color32 HeaderTextColor = new Color32(255, 255, 255, 255);
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //HauptmenueInfoContainer
        public string InfoContainerNameInCanvas = "HauptmenueInfoContainer";
        public string InfoContainerParent = "Hauptmenue";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public int InfoContainerHeight = 100;
        public Color32 InfoContainerColor = new Color32(0, 0, 0, 230);

        // HauptmenueInfoText
        public string InfoTextNameInCanvas = "HauptmenueInfoText";
        public string InfoTextParent = "HauptmenueInfoContainer";
        public string InfoTextText = "generelle Einstellungen";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 123;
        public int InfoTextWidth = 780;
        public int InfoTextHeight = 120;
        public Color32 InfoTextColor = new Color32(255, 255, 255, 50);
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //HauptmenueInfoLogo
        public string InfoLogoNameInCanvas = "HauptmenueInfoLogo";
        public string InfoLogoParent = "HauptmenueInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public int InfoLogoWidth = 60;
        public int InfoLogoHeight = 100;
        public Color32 InfoLogoColor = new Color32(0, 0, 0, 0); // ausgeblendet
        public string InfoLogoSourceImage = "";//"Logos/list-with-dots_white";

        //HauptmenueHauptContainer
        public string HauptContainerNameInCanvas = "HauptmenueHauptContainer";
        public string HauptContainerParent = "Hauptmenue";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public int HauptContainerHeight = 920;
        public Color32 HauptContainerColor = new Color32(0, 000, 0, 100);

        // ab hier die einzelnen Zeilen

        //HauptmenueZeileContainer
        public int ZeileContainerAnzahlZeilen = 8; // momentan max. 8 wegen Höehe 1080 px
        public string ZeileContainerNameInCanvas = "HauptmenueZeileContainer";
        public string ZeileContainerParent = "HauptmenueHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public int ZeileContainerHeight = 100;
        public Color32 ZeileContainerColor = new Color32(0, 000, 0, 100);
        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        //HauptmenueZeileHeader
        public string ZeileHeaderNameInCanvas = "HauptmenueZeileHeader";
        public string ZeileHeaderParent = "HauptmenueZeileContainer";
        public string ZeileHeaderText = "";
        public int ZeileHeaderPosX = VerschiebungX + 80;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public int ZeileHeaderWidth = 600;
        public int ZeileHeaderHeight = 60;
        public Color32 ZeileHeaderColor = new Color32(255, 255, 255, 255);
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderFontSize = 45;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        ////HauptmenueZeileMaterial
        //public int ZeileMaterialAnzahlMaterialOptionenX = 4;
        //public int ZeileMaterialAnzahlMaterialOptionenY = 3;
        //public int ZeileMaterialAnzahlMaterialAbstandX = 10;
        //public int ZeileMaterialAnzahlMaterialAbstandY = 10;
        //public string ZeileMaterialNameInCanvas = "HauptmenueZeileMaterial";
        //public string ZeileMaterialParent = "HauptmenueZeileContainer";
        //public int ZeileMaterialPosX = VerschiebungX + 860;
        //public int ZeileMaterialPosY = VerschiebungY - 241;
        //public int ZeileMaterialWidth = 20;
        //public int ZeileMaterialHeight = 20;
        //public Color32 ZeileMaterialColor = new Color32(255, 255, 255, 255);

        //HauptmenueZeileDetail
        public string ZeileDetailNameInCanvas = "HauptmenueZeileDetail";
        public string ZeileDetailParent = "HauptmenueZeileContainer";
        public string ZeileDetailText = "";
        public int ZeileDetailPosX = VerschiebungX + 80;
        public int ZeileDetailPosY = VerschiebungY - 294;
        public int ZeileDetailWidth = 920;
        public int ZeileDetailHeight = 50;
        public Color32 ZeileDetailColor = new Color32(255, 250, 200, 255);
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailFontSize = 25;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //HauptmenueZeileLogo
        public string ZeileLogoNameInCanvas = "HauptmenueZeileDetailLogo";
        public string ZeileLogoParent = "HauptmenueZeileContainer";
        public int ZeileLogoPosX = VerschiebungX;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public int ZeileLogoWidth = 60;
        public int ZeileLogoHeight = 100;
        public Color32 ZeileLogoColor = new Color32(0, 0, 0, 150);
        public string ZeileLogoSourceImage = "";//"Logos/list-with-dots_white";

        // HauptmenueZeilenNummer
        public string ZeileNummerNameInCanvas = "HauptmenueZeileNummer";
        public string ZeileNummerParent = "HauptmenueZeileContainer";
        public string ZeileNummerText = "";
        public int ZeileNummerPosX = VerschiebungX + 12;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public int ZeileNummerWidth = 60;
        public int ZeileNummerHeight = 100;
        public Color32 ZeileNummerColor = new Color32(255, 255, 255, 255);
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
    }

    public class GuiInnentuer
    {
        public string Name = "Innentür";

        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static int VerschiebungX = 0;
        private static int VerschiebungY = 0;

        // Innentuer
        public string NameInCanvas = "Innentuer";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;
        public int Width = 960;
        public int Height = 1080;
        public Color32 Color = new Color32(0, 0, 0, 100);

        // InnentuerHeaderContainer
        public string HeaderContainerNameInCanvas = "InnentuerHeaderContainer";
        public string HeaderContainerParent = "Innentuer";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public int HeaderContainerHeight = 120;
        public Color32 HeaderContainerColor = new Color32(0, 0, 0, 250);

        // InnentuerHeaderLogo
        public string HeaderLogoNameInCanvas = "InnentuerHeaderLogo";
        public string HeaderLogoParent = "InnentuerHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public int HeaderLogoWidth = 80;
        public int HeaderLogoHeight = 80;
        public Color32 HeaderLogoColor = new Color32(255, 255, 255, 255);
        public string HeaderLogoSourceImage = "Logos/numbered-list_white";

        // InnentuerHeaderText
        public string HeaderTextNameInCanvas = "InnentuerHeaderText";
        public string HeaderTextParent = "InnentuerHeaderContainer";
        public string HeaderTextText = "Innentür";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public int HeaderTextWidth = 780;
        public int HeaderTextHeight = 120;
        public Color32 HeaderTextColor = new Color32(255, 255, 255, 255);
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //InnentuerInfoContainer
        public string InfoContainerNameInCanvas = "InnentuerInfoContainer";
        public string InfoContainerParent = "Innentuer";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public int InfoContainerHeight = 100;
        public Color32 InfoContainerColor = new Color32(0, 0, 0, 230);

        // InnentuerInfoText
        public string InfoTextNameInCanvas = "InnentuerInfoText";
        public string InfoTextParent = "InnentuerInfoContainer";
        public string InfoTextText = "wählen Sie bitte Voreinstellungen";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 123;
        public int InfoTextWidth = 780;
        public int InfoTextHeight = 120;
        public Color32 InfoTextColor = new Color32(255, 255, 255, 50);
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //InnentuerInfoLogo
        public string InfoLogoNameInCanvas = "InnentuerInfoLogo";
        public string InfoLogoParent = "InnentuerInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public int InfoLogoWidth = 60;
        public int InfoLogoHeight = 100;
        public Color32 InfoLogoColor = new Color32(0, 0, 0, 0); //ausgeblendet
        public string InfoLogoSourceImage = "";//"Logos/list-with-dots_white";

        //InnentuerHauptContainer
        public string HauptContainerNameInCanvas = "InnentuerHauptContainer";
        public string HauptContainerParent = "Innentuer";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public int HauptContainerHeight = 920;
        public Color32 HauptContainerColor = new Color32(0, 000, 0, 100);

        // ab hier die einzelnen Zeilen

        //InnentuerZeileContainer
        public int ZeileContainerAnzahlZeilen = 8; // momentan max. 8 wegen Höehe 1080 px
        public string ZeileContainerNameInCanvas = "InnentuerZeileContainer";
        public string ZeileContainerParent = "InnentuerHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public int ZeileContainerHeight = 100;
        public Color32 ZeileContainerColor = new Color32(0, 000, 0, 100);
        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        //InnentuerZeileHeader
        public string ZeileHeaderNameInCanvas = "InnentuerZeileHeader";
        public string ZeileHeaderParent = "InnentuerZeileContainer";
        public string ZeileHeaderText = "";
        public int ZeileHeaderPosX = VerschiebungX + 80;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public int ZeileHeaderWidth = 600;
        public int ZeileHeaderHeight = 60;
        public Color32 ZeileHeaderColor = new Color32(255, 255, 255, 255);
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderFontSize = 45;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        ////InnentuerZeileMaterial
        //public int ZeileMaterialAnzahlMaterialOptionenX = 4;
        //public int ZeileMaterialAnzahlMaterialOptionenY = 3;
        //public int ZeileMaterialAnzahlMaterialAbstandX = 10;
        //public int ZeileMaterialAnzahlMaterialAbstandY = 10;
        //public string ZeileMaterialNameInCanvas = "InnentuerZeileMaterial";
        //public string ZeileMaterialParent = "InnentuerZeileContainer";
        //public int ZeileMaterialPosX = VerschiebungX + 860;
        //public int ZeileMaterialPosY = VerschiebungY - 241;
        //public int ZeileMaterialWidth = 20;
        //public int ZeileMaterialHeight = 20;
        //public Color32 ZeileMaterialColor = new Color32(255, 255, 255, 255);

        //InnentuerZeileDetail
        public string ZeileDetailNameInCanvas = "InnentuerZeileDetail";
        public string ZeileDetailParent = "InnentuerZeileContainer";
        public string ZeileDetailText = "";
        public int ZeileDetailPosX = VerschiebungX + 80;
        public int ZeileDetailPosY = VerschiebungY - 294;
        public int ZeileDetailWidth = 920;
        public int ZeileDetailHeight = 50;
        public Color32 ZeileDetailColor = new Color32(255, 250, 200, 255);
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailFontSize = 25;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //InnentuerZeileLogo
        public string ZeileLogoNameInCanvas = "InnentuerZeileDetailLogo";
        public string ZeileLogoParent = "InnentuerZeileContainer";
        public int ZeileLogoPosX = VerschiebungX;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public int ZeileLogoWidth = 60;
        public int ZeileLogoHeight = 100;
        public Color32 ZeileLogoColor = new Color32(0, 0, 0, 150);
        public string ZeileLogoSourceImage = "";//"Logos/list-with-dots_white";

        // InnentuerZeilenNummer
        public string ZeileNummerNameInCanvas = "InnentuerZeileNummer";
        public string ZeileNummerParent = "InnentuerZeileContainer";
        public string ZeileNummerText = "";
        public int ZeileNummerPosX = VerschiebungX + 12;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public int ZeileNummerWidth = 60;
        public int ZeileNummerHeight = 100;
        public Color32 ZeileNummerColor = new Color32(255, 255, 255, 255);
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
    }

    public class GuiKonfigurator
    {
        public string Name = "Konfigurator";
        public string[] AnzuzeigendeSpalteDerTabelleInnentuer = { "Zarge", "Tuerblatt", "DrueckerZier", "DrueckerFalz" };

        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static int VerschiebungX = 960;
        private static int VerschiebungY = 0;

        // Konfigurator
        public string NameInCanvas = "Konfigurator";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;
        public int Width = 960;
        public int Height = 1080;
        public Color32 Color = new Color32(0, 0, 0, 100);
        public Color32 ColorPressed = new Color32(135, 8, 11, 200);

        // KonfiguratorHeaderContainer
        public string HeaderContainerNameInCanvas = "KonfiguratorHeaderContainer";
        public string HeaderContainerParent = "Konfigurator";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public int HeaderContainerHeight = 120;
        public Color32 HeaderContainerColor = new Color32(0, 0, 0, 250);

        // KonfiguratorHeaderLogo
        public string HeaderLogoNameInCanvas = "KonfiguratorHeaderLogo";
        public string HeaderLogoParent = "KonfiguratorHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public int HeaderLogoWidth = 80;
        public int HeaderLogoHeight = 80;
        public Color32 HeaderLogoColor = new Color32(255, 255, 255, 255);
        public string HeaderLogoSourceImage = "Logos/list-with-dots_white";

        // KonfiguratorHeaderText
        public string HeaderTextNameInCanvas = "KonfiguratorHeaderText";
        public string HeaderTextParent = "KonfiguratorHeaderContainer";
        public string HeaderTextText = "Konfigurator";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public int HeaderTextWidth = 780;
        public int HeaderTextHeight = 120;
        public Color32 HeaderTextColor = new Color32(255, 255, 255, 255);
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorInfoContainer
        public string InfoContainerNameInCanvas = "KonfiguratorInfoContainer";
        public string InfoContainerParent = "Konfigurator";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public int InfoContainerHeight = 100;
        public Color32 InfoContainerColor = new Color32(0, 0, 0, 230);

        // KonfiguratorInfoText
        public string InfoTextNameInCanvas = "KonfiguratorInfoText";
        public string InfoTextParent = "KonfiguratorInfoContainer";
        public string InfoTextText = "Innentür";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 123;
        public int InfoTextWidth = 780;
        public int InfoTextHeight = 120;
        public Color32 InfoTextColor = new Color32(255, 255, 255, 255);
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorInfoLogo
        public string InfoLogoNameInCanvas = "KonfiguratorInfoLogo";
        public string InfoLogoParent = "KonfiguratorInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public int InfoLogoWidth = 60;
        public int InfoLogoHeight = 100;
        public Color32 InfoLogoColor = new Color32(0, 0, 0, 150);
        public string InfoLogoSourceImage = "";//"Logos/list-with-dots_white";

        // KonfiguratorInfoNummer
        public string InfoNummerNameInCanvas = "KonfiguratorInfoNummer";
        public string InfoNummerParent = "KonfiguratorInfoContainer";
        public string InfoNummerText = "0";
        public int InfoNummerPosX = VerschiebungX + 912;
        public int InfoNummerPosY = VerschiebungY - 132;
        public int InfoNummerWidth = 60;
        public int InfoNummerHeight = 100;
        public Color32 InfoNummerColor = new Color32(255, 255, 255, 255);
        public string InfoNummerFontType = "Arial.ttf";
        public int InfoNummerFontSize = 70;
        public FontStyle InfoNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorHauptContainer
        public string HauptContainerNameInCanvas = "KonfiguratorHauptContainer";
        public string HauptContainerParent = "Konfigurator";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public int HauptContainerHeight = 920;
        public Color32 HauptContainerColor = new Color32(0, 000, 0, 100);

        // ab hier die einzelnen Zeilen

        //KonfiguratorZeileContainer
        public int ZeileContainerAnzahlZeilen = 8; // momentan max. 8 wegen Höehe 1080 px
        public string ZeileContainerNameInCanvas = "KonfiguratorZeileContainer";
        public string ZeileContainerParent = "KonfiguratorHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public int ZeileContainerHeight = 100;
        public Color32 ZeileContainerColor = new Color32(0, 000, 0, 100);
        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        //KonfiguratorZeileHeader
        public string ZeileHeaderNameInCanvas = "KonfiguratorZeileHeader";
        public string ZeileHeaderParent = "KonfiguratorZeileContainer";
        public string ZeileHeaderText = "";
        public int ZeileHeaderPosX = VerschiebungX + 20;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public int ZeileHeaderWidth = 600;
        public int ZeileHeaderHeight = 40;
        public Color32 ZeileHeaderColor = new Color32(255, 255, 255, 255);
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderFontSize = 30;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorZeileMaterial
        public int ZeileMaterialAnzahlMaterialOptionenX = 4;
        public int ZeileMaterialAnzahlMaterialOptionenY = 2;
        public int ZeileMaterialAnzahlMaterialAbstandX = 10;
        public int ZeileMaterialAnzahlMaterialAbstandY = 10;
        public string ZeileMaterialNameInCanvas = "KonfiguratorZeileMaterial";
        public string ZeileMaterialParent = "KonfiguratorZeileContainer";
        public int ZeileMaterialPosX = VerschiebungX + 860;
        public int ZeileMaterialPosY = VerschiebungY - 241 ;
        public int ZeileMaterialWidth = 20;
        public int ZeileMaterialHeight = 20;
        public Color32 ZeileMaterialColor = new Color32(255, 255, 255, 255);

        //KonfiguratorZeileDetail
        public string ZeileDetailNameInCanvas = "KonfiguratorZeileDetail";
        public string ZeileDetailParent = "KonfiguratorZeileContainer";
        public string ZeileDetailText = "";
        public int ZeileDetailPosX = VerschiebungX + 20;
        public int ZeileDetailPosY = VerschiebungY -274;
        public int ZeileDetailWidth = 920;
        public int ZeileDetailHeight = 50;
        public Color32 ZeileDetailColor = new Color32(255, 255, 255, 155);
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailFontSize = 20;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorZeileLogo
        public string ZeileLogoNameInCanvas = "KonfiguratorZeileDetailLogo";
        public string ZeileLogoParent = "KonfiguratorZeileContainer";
        public int ZeileLogoPosX = VerschiebungX + 900;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public int ZeileLogoWidth = 60;
        public int ZeileLogoHeight = 100;
        public Color32 ZeileLogoColor = new Color32(0, 0, 0, 150);
        public string ZeileLogoSourceImage = "";//"Logos/list-with-dots_white";

        // KonfiguratorZeilenNummer
        public string ZeileNummerNameInCanvas = "KonfiguratorZeileNummer";
        public string ZeileNummerParent = "KonfiguratorZeileContainer";
        public string ZeileNummerText = "";
        public int ZeileNummerPosX = VerschiebungX + 912;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public int ZeileNummerWidth = 60;
        public int ZeileNummerHeight = 100;
        public Color32 ZeileNummerColor = new Color32(255, 255, 255, 255);
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
    }

    public class MenuePunkt
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Aktion { get; set; }
    }


    // diese Methode soll den json-Material auseinandernehmen und zurückgeben
    // Liste mit String [MATId] und String Array [Materialbezeichnung]
    // Beispiel
    // MATId = "Mat1"; Materialbezeichnung["M_1K_3DTE_Wood_020","M_2K_CC0T_Wood025_2K-JPG"]
    // MATId = "Mat2"; Materialbezeichnung["M_1K_3NAS_Wood_018","M_2K_CC0T_Floor025_2K-JPG"]

    public class InnentuerOutputParameter
    {
        public string HoeheDIN { get; set; }
        public string BreiteDIN { get; set; }
        public string Wandstaerke { get; set; }
        public string Bekleidungsbreite { get; set; }
        public string Oeffnungsrichtung { get; set; }
        public string Frontseite { get; set; }
    }

    public class HauptmenueOutputParameter
    {
        public string Konfiguratorschema { get; set; }
        public string Farbeschema { get; set; }
        public string Datenbank { get; set; } // noch keine Verwendung
    }

    public class AktuelleKonfiguration
    {
        public int Index { get; set; }

        public List<string> Liste = new List<string>();
        public string Id { get; set; }
        public string Detail { get; set; }
        public string Zarge { get; set; }
        public string Tuerblatt { get; set; }
        public string Band1 { get; set; }
        public string Band2 { get; set; }
        public string Bandaufnahme1 { get; set; }
        public string Bandaufnahme2 { get; set; }
        public string DrueckerFalz { get; set; }
        public string DrueckerZier { get; set; }
        public string Schlosskasten { get; set; }
        public string Schliessblech { get; set; }
        public string Schwelle { get; set; }

    }
    
    GuiHauptmenue guiHauptmenue = new GuiHauptmenue();
    HauptmenueParameter hauptmenueParameter = new HauptmenueParameter();

    GuiInnentuer guiInnentuer = new GuiInnentuer();
    InnentuerParameter innentuerParameter = new InnentuerParameter();


    AktuelleKonfiguration aktuelleKonfiguration = new AktuelleKonfiguration();
    List<T_Innentuer> ergebnisInnentuer = new List<T_Innentuer>();

    List<T_Innentuer> ergebnisInnentuerNachCheckHoehe = new List<T_Innentuer>();
    List<T_Innentuer> ergebnisInnentuerNachCheckBreite = new List<T_Innentuer>();
    List<T_Innentuer> ergebnisInnentuerNachCheckWandstaerke = new List<T_Innentuer>();
    List<T_Innentuer> ergebnisInnentuerNachCheckBekleidungsbreite = new List<T_Innentuer>();

    T_Innentuer aktuellGetoggelteInnentuer = new T_Innentuer();
    


    GuiKonfigurator guiKonfigurator = new GuiKonfigurator();
    List<MenuePunkt> innentuerPunkt = new List<MenuePunkt>();

    HauptmenueOutputParameter hauptmenueOutputParameter = new HauptmenueOutputParameter();
    InnentuerOutputParameter innentuerOutputParameter = new InnentuerOutputParameter();


    // Datenbank
    // lokale Tabellen zu den Datenbank Tabellen
    List<T_Band> tabelleBand = new List<T_Band>();
    List<T_Bandaufnahme> tabelleBandaufnahme = new List<T_Bandaufnahme>();
    List<T_Druecker> tabelleDruecker = new List<T_Druecker>();
    List<T_Schliessblech> tabelleSchliessblech = new List<T_Schliessblech>();
    List<T_Schlosskasten> tabelleSchlosskasten = new List<T_Schlosskasten>();
    List<T_Schwelle> tabelleSchwelle = new List<T_Schwelle>();
    List<T_Tuerblatt> tabelleTuerblatt = new List<T_Tuerblatt>();
    List<T_Zarge> tabelleZarge = new List<T_Zarge>();
    List<T_Innentuer> tabelleInnentuer = new List<T_Innentuer>();
    List<T_Hersteller> tabelleHersteller = new List<T_Hersteller>();
    List<T_MAT> tabelleMAT = new List<T_MAT>();
    List<T_Objektteil> tabelleObjektteil = new List<T_Objektteil>();

    int aktuellGetoggelteInnentuerIndex = 0;

    //public imr
    //public Color lerpedColor = Color.white;
    public Image imaget;
    public bool tempvar = false;

    //public float duration = 0.1f; // This will be your time in seconds.
    //public float smoothness = 0.01f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
    //public Color32 floatColor = new Color32 (0, 0, 0, 255);

    //public Color floatColor = Color.green; // This is the state of the color in the current interpolation.
    // ---------------------------------------------------------------------------------------------------
    // Initialaktionen
    // ---------------------------------------------------------------------------------------------------

    void Start()
    {
        

        // Datenbank: Hole alle Daten
        initiiereDatenbankAbfragen();

        // GUI: Rahmen
        generiereMenueCanvas("GUI");

        //// GUI: Hauptmenue
        generiereHauptmenueContainer();
        generiereHauptmenueHeaderContainer();
        generiereHauptmenueHeaderLogo();
        generiereHauptmenueHeaderText();
        generiereHauptmenueInfoContainer();
        generiereHauptmenueInfoText();
        generiereHauptmenueInfoLogo();
        generiereHauptmenueHauptContainer();
        generiereHauptmenueHauptContainerZeilen(guiHauptmenue.ZeileContainerAnzahlZeilen);

        //// GUI: Innentuer
        generiereInnentuerContainer();
        generiereInnentuerHeaderContainer();
        generiereInnentuerHeaderLogo();
        generiereInnentuerHeaderText();
        generiereInnentuerInfoContainer();
        generiereInnentuerInfoText();
        generiereInnentuerInfoLogo();
        generiereInnentuerHauptContainer();
        generiereInnentuerHauptContainerZeilen(guiInnentuer.ZeileContainerAnzahlZeilen);

        // GUI: Konfigurator
        generiereKonfiguratorContainer();
        generiereKonfiguratorHeaderContainer();
        generiereKonfiguratorHeaderLogo();
        generiereKonfiguratorHeaderText();
        generiereKonfiguratorInfoContainer();
        generiereKonfiguratorInfoText();
        generiereKonfiguratorInfoLogo();
        generiereKonfiguratorInfoNummer();
        generiereKonfiguratorHauptContainer();
        generiereKonfiguratorHauptContainerZeilen(guiKonfigurator.ZeileContainerAnzahlZeilen);

        konfigurationen();

        updateHauptmenue(hauptmenueParameter);
        updateInnentuer(innentuerParameter);

        // lege Startsituation fest
        ausgangssituation();
        zeigeMenuAn(aktuellesMenu);

        // ermittele die höchste MenüPunkt-ID
        if (innentuerPunkt.Count > 0)
        {
            var item = innentuerPunkt[innentuerPunkt.Count - 1];
            hoechsteMenuId = item.Id;
        }
    }


    void konfigurationen()
    {

        // Hauptmenü
        hauptmenueParameter.Menuepunkt = new List<string>();
        // Bezeichnung der Menüpunkte, wie sie angezeigt werden sollen
        hauptmenueParameter.Menuepunkt.Add("Konfiguratorschema");
        hauptmenueParameter.Menuepunkt.Add("Farbeschema");
        hauptmenueParameter.Menuepunkt.Add("Synchronisation Datenbank");

        // Bezeichnung der zu toggelnden Werte je Untermenü, wie sie angezeigt werden sollen
        hauptmenueParameter.Toggle = new string[][] {
            new string[] { "Innentür" },
            new string[] { "dunkel", "hell" },
            new string[] { "" }
        };

        // Bezeichnung der zu toggelnden Werte je Untermenü, wie sie in den Ausgabewerten für
        // [hauptmenueOutputParameter] gesetzt werden sollen
        hauptmenueParameter.ToggleAktion = new string[][] {
            new string[] { "Innentür" },
            new string[] { "dunkel", "hell" },
            new string[] { "Thomas" }
        };

        hauptmenueParameter.TogglePunktIndex = new int[hauptmenueParameter.Menuepunkt.Count()];
        hauptmenueParameter.TogglePunktIndex[0] = 0;
        hauptmenueParameter.TogglePunktIndex[1] = 0;
        hauptmenueParameter.TogglePunktIndex[2] = 0;


        // Innentuer
        innentuerParameter.Menuepunkt = new List<string>();
        innentuerParameter.Menuepunkt.Add("Höhe");
        innentuerParameter.Menuepunkt.Add("Breite");
        innentuerParameter.Menuepunkt.Add("Wandstärke");
        innentuerParameter.Menuepunkt.Add("Bekleidungsbreite");
        innentuerParameter.Menuepunkt.Add("Öffnungsrichtung");
        innentuerParameter.Menuepunkt.Add("Frontseite");

        innentuerParameter.Toggle = new string[][] {
            new string[] { "alle", "1875 mm (DIN) | 1885 mm (WÖM) | 1860 mm (TAM)", "2010 mm (DIN) | 2010 mm (WÖM) | 1985 mm (TAM)", "2125 mm (DIN) | 2135 mm (WÖM) | 2110 mm (TAM)", "2250 mm (DIN) | 2260 mm (WÖM) | 2235 mm (TAM)" },
            new string[] { "alle", "625 mm (DIN) | 635 mm (WÖM) | 610 mm (TAM)", "750 mm (DIN) | 760 mm (WÖM) | 735 mm (TAM)", "875 mm (DIN) | 885 mm (WÖM) | 860 mm (TAM)", "1000 mm (DIN) | 1010 mm (WÖM) | 985 mm (TAM)", "1125 mm (DIN) | 1135 mm (WÖM) | 1110 mm (TAM)" },
            new string[] { "alle", "115 mm", "180 mm", "200 mm", "265 mm", "300 mm"  },
            new string[] { "alle", "50 mm" },
            new string[] { "DIN links", "DIN rechts" },
            new string[] { "Falzfront", "Zierfront" }
        };

        innentuerParameter.ToggleAktion = new string[][] {
            new string[] { "%", "1875", "2000", "2125", "2250" },
            new string[] { "%", "625", "750", "875", "1000", "1125" },
            new string[] { "%", "115", "180", "200", "265", "300"  },
            new string[] { "%", "50" },
            new string[] { "DIN links", "DIN rechts" },
            new string[] { "Falzfront", "Zierfront" }
        };

        innentuerParameter.TogglePunktIndex = new int[innentuerParameter.Menuepunkt.Count()];
        innentuerParameter.TogglePunktIndex[0] = 0;
        innentuerParameter.TogglePunktIndex[1] = 0;
        innentuerParameter.TogglePunktIndex[2] = 0;
        innentuerParameter.TogglePunktIndex[3] = 0;
        innentuerParameter.TogglePunktIndex[4] = 0;
        innentuerParameter.TogglePunktIndex[5] = 0;
    }


    // Konfiguration der Ausgangssituation
    void ausgangssituation()
    {
        aktuelleKonfiguration.Index = -1;

        innentuerOutputParameter.HoeheDIN = "%";
        innentuerOutputParameter.BreiteDIN = "%";
        innentuerOutputParameter.Oeffnungsrichtung = "%";
        innentuerOutputParameter.Wandstaerke = "%";
        innentuerOutputParameter.Bekleidungsbreite = "%";


        ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
        GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();


        aktuellGetoggelteInnentuer.Zarge = "";

        // blende alle Menüs aus
        GameObject.Find("Hauptmenue").GetComponent<CanvasGroup>().alpha = 0.0f;
        GameObject.Find("Innentuer").GetComponent<CanvasGroup>().alpha = 0.0f;
        GameObject.Find("Konfigurator").GetComponent<CanvasGroup>().alpha = 0.0f;

        GameObject.Find(guiHauptmenue.ZeileDetailNameInCanvas + "3").GetComponent<Text>().text = "letzte Aktualisierung : " + DateTime.Now.ToString("dd.MM.yyyy | HH:mm:ss");

    }


    // ---------------------------------------------------------------------------------------------------
    // Checks je Frame
    // ---------------------------------------------------------------------------------------------------

    void Update()
    {
        //lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 11));
        // Tastenaktionen 0-9
        if (!tempvar)
        {
            if (Input.GetKeyDown("0"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("0");
            }

            if (Input.GetKeyDown("1"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("1");
            }

            if (Input.GetKeyDown("2"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("2");
            }
            if (Input.GetKeyDown("3"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("3");
            }

            if (Input.GetKeyDown("4"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("4");
            }

            if (Input.GetKeyDown("5"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("5");
            }

            if (Input.GetKeyDown("6"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("6");
            }

            if (Input.GetKeyDown("7"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("7");
            }
            if (Input.GetKeyDown("8"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("8");
            }

            if (Input.GetKeyDown("9"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("9");
            }


            // Tastenaktionen F-Tasten

            // toggle Menü Hauptmenü
            if (Input.GetKeyDown("f1"))
            {
                toggleMenue(guiHauptmenue.Name, aktuellAktivesMenu);
            }

            // Toggle Menü Innentür
            if (Input.GetKeyDown("f2"))
            {
                toggleMenue(guiInnentuer.Name, aktuellAktivesMenu);
            }

            // Toggle Menü Konfigurator
            if (Input.GetKeyDown("f3"))
            {
                toggleMenue(guiKonfigurator.Name, aktuellAktivesMenu);
            }


        }

    }




    // ---------------------------------------------------------------------------------------------------
    // MAIN
    // ---------------------------------------------------------------------------------------------------


    // schreibe aus allen DB-Tabellen die Daten in lokale Tabellen
    void initiiereDatenbankAbfragen()
    {
        tabelleBand = dBAbfrageBand();
        tabelleBandaufnahme = dBAbfrageBandaufnahme();
        tabelleDruecker = dBAbfrageDruecker();
        tabelleSchliessblech = dBAbfrageSchliessblech();
        tabelleSchlosskasten = dBAbfrageSchlosskasten();
        tabelleSchwelle = dBAbfrageSchwelle();
        tabelleTuerblatt = dBAbfrageTuerblatt();
        tabelleZarge = dBAbfrageZarge();
        tabelleInnentuer = dBAbfrageInnentuer();
        tabelleHersteller = dBAbfrageHersteller();
        tabelleMAT = dBAbfrageMAT();
        tabelleObjektteil = dBAbfrageObjektteil();
    }

    void toggleMenue(string menueName, string aktivesMenue)
    {

        //Debug.Log("rufe toggleMenue auf mit den Parametern ( menueName : " + menueName + " - aktivesMenue : " + aktivesMenue + " )");
        switch (menueName)
        {

            // Block Hauptmenü
            case "Hauptmenü":
                {
                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiHauptmenue.Name;
                    }
                    else if (aktivesMenue == guiHauptmenue.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        aktuellAktivesMenu = "";
                    }
                    else if (aktivesMenue == guiInnentuer.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiHauptmenue.Name;
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                    }
                    else if (aktivesMenue == guiKonfigurator.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiHauptmenue.Name;
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                    }
                    break;
                }


            case "Innentür":
                {
                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiInnentuer.Name;
                    }
                    else if (aktivesMenue == guiHauptmenue.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiInnentuer.Name;
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                    }
                    else if (aktivesMenue == guiInnentuer.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        if (GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha == 0.5f)
                        {
                            GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                            aktuellAktivesMenu = guiKonfigurator.Name;
                        }
                        else
                        {
                            aktuellAktivesMenu = "";
                        }
                    }
                    else if (aktivesMenue == guiKonfigurator.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiInnentuer.Name;
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.5f;
                    }
                    break;
                }


            case "Konfigurator":
                {
                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiKonfigurator.Name;
                        // Spezial
                        uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);
                        //
                    }
                    else if (aktivesMenue == guiHauptmenue.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiKonfigurator.Name;
                        // Spezial
                        uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);
                        //
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                    }
                    else if (aktivesMenue == guiInnentuer.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        aktuellAktivesMenu = guiKonfigurator.Name;
                        // Spezial
                        Debug.Log("ergebnisInnentuer :" + ergebnisInnentuer.Count().ToString());
                        uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);
                        //
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.5f;
                    }
                    else if (aktivesMenue == guiKonfigurator.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        if (GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha == 0.5f)
                        {
                            GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                            aktuellAktivesMenu = guiInnentuer.Name;
                        }
                        else
                        {
                            aktuellAktivesMenu = "";
                        }
                    }
                    break;
                }


            default:
                {
                    Debug.Log("ACHTUNG: Keinen case gefunden in: [toggleMenue]");
                    break;
                }
        }

    }


    void ermitteleAktionAnhandEingabeUndAktivemMenue(string eingabe)
    {
        switch (eingabe)
        {
            case "0":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("10");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("20");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("30");
                    }
                    break;
                }
            case "1":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("11");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("21");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("31");
                    }
                    break;
                }
            case "2":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("12");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("22");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("32");
                    }
                    break;
                }
            case "3":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("13");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("23");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("33");
                    }
                    break;
                }
            case "4":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("14");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("24");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("34");
                    }
                    break;
                }
            case "5":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("15");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("25");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("35");
                    }
                    break;
                }
            case "6":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("16");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("26");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("36");
                    }
                    break;
                }
            case "7":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("17");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("27");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("37");
                    }
                    break;
                }
            case "8":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("18");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("28");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("38");
                    }
                    break;
                }
            case "9":
                {
                    if (aktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("19");
                    }
                    else if (aktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("29");
                    }
                    else if (aktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("39");
                    }
                    break;
                }
            default:
                {
                    Debug.Log("ACHTUNG: KEINE AKTION HINTERLEGT");
                    break;
                }
        }



    }


    //Konfiguration der zu den jeweiligen Menüpunkten zugehörigen auszuführenden Aktionen
    void fuehreAktionAus(string aktion)
    {
        //Debug.Log("Führe Aktion: " + aktion + " aus");

        switch (aktion)
        {

            case "11":
                {
                    toggleMenuePunkt(1, 1);
                    hauptmenueOutputParameter.Konfiguratorschema = hauptmenueParameter.ToggleAktion[0][hauptmenueParameter.TogglePunktIndex[0]];
                    StartCoroutine(LerpColor(guiHauptmenue.ZeileContainerNameInCanvas + "1"));
                    Debug.Log("Setze Konfiguratorschema = " + hauptmenueOutputParameter.Konfiguratorschema);
                    break;
                }
            case "12":
                {

                    toggleMenuePunkt(1, 2);
                    hauptmenueOutputParameter.Farbeschema = hauptmenueParameter.ToggleAktion[1][hauptmenueParameter.TogglePunktIndex[1]];
                    StartCoroutine(LerpColor(guiHauptmenue.ZeileContainerNameInCanvas + "2"));
                    Debug.Log("Setze Farbschema = " + hauptmenueOutputParameter.Farbeschema);
                    break;
                }
            case "13":
                {
                    toggleMenuePunkt(1, 3);
                    hauptmenueOutputParameter.Datenbank = hauptmenueParameter.ToggleAktion[2][hauptmenueParameter.TogglePunktIndex[2]];
                    StartCoroutine(LerpColor(guiHauptmenue.ZeileContainerNameInCanvas + "3"));
                    Debug.Log("Synchronisation Datenbank = " + hauptmenueOutputParameter.Datenbank);
                    // Spezial
                    initiiereDatenbankAbfragen();
                    GameObject.Find(guiHauptmenue.ZeileDetailNameInCanvas + "3").GetComponent<Text>().text = "letzte Aktualisierung : " + DateTime.Now.ToString("dd.MM.yyyy | HH:mm:ss");
                    //
                    break;
                }
            case "21":
                {
                    toggleMenuePunkt(2, 1);
                    innentuerOutputParameter.HoeheDIN = innentuerParameter.ToggleAktion[0][innentuerParameter.TogglePunktIndex[0]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "1"));
                    Debug.Log("Setze HoeheDIN = " + innentuerOutputParameter.HoeheDIN);
                    ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
                    GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();
                    break;
                }
            case "22":
                {
                    toggleMenuePunkt(2, 2);
                    innentuerOutputParameter.BreiteDIN = innentuerParameter.ToggleAktion[1][innentuerParameter.TogglePunktIndex[1]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "2"));
                    Debug.Log("Setze BreiteDIN = " + innentuerOutputParameter.BreiteDIN);
                    ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
                    GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();
                    break;
                }
            case "23":
                {
                    toggleMenuePunkt(2, 3);
                    innentuerOutputParameter.Wandstaerke = innentuerParameter.ToggleAktion[2][innentuerParameter.TogglePunktIndex[2]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "3"));
                    Debug.Log("Setze Wandstärke = " + innentuerOutputParameter.Wandstaerke);
                    ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
                    GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();
                    break;
                }
            case "24":
                {
                    toggleMenuePunkt(2, 4);
                    innentuerOutputParameter.Bekleidungsbreite = innentuerParameter.ToggleAktion[3][innentuerParameter.TogglePunktIndex[3]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "4"));
                    Debug.Log("Setze Bekleidungsbreite = " + innentuerOutputParameter.Bekleidungsbreite);
                    ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
                    GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();
                    break;
                }
            case "25":
                {
                    toggleMenuePunkt(2, 5);
                    innentuerOutputParameter.Oeffnungsrichtung = innentuerParameter.ToggleAktion[4][innentuerParameter.TogglePunktIndex[4]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "5"));
                    Debug.Log("Setze Öffnungsrichtung = " + innentuerOutputParameter.Oeffnungsrichtung);
                    break;
                }
            case "26":
                {
                    toggleMenuePunkt(2, 6);
                    innentuerOutputParameter.Frontseite = innentuerParameter.ToggleAktion[5][innentuerParameter.TogglePunktIndex[5]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "6"));
                    Debug.Log("Setze Frontseite = " + innentuerOutputParameter.Frontseite);
                    break;
                }
            case "30":
                {
                    toggleInnentuerImKonfigurator();
                    //GameObject.Find(guiKonfigurator.InfoContainerNameInCanvas).GetComponent<Image>().color = guiKonfigurator.ColorPressed;
                    break;
                }

            default:
                {
                    Debug.Log("ACHTUNG: KEINE AKTION DEFINIERT");
                    break;
                }
        }
    }


    void uebergiebParameterAusMenueInnentuerInMenueKonfigurator(List<T_Innentuer> ergebnisInnentuer)
    {
        Debug.Log("ergebnisInnentuer :" + ergebnisInnentuer.Count().ToString());

        if (ergebnisInnentuer.Count() == 0)
        {
            GameObject.Find(guiKonfigurator.InfoTextNameInCanvas).GetComponent<Text>().text = "Es gibt keinen Treffer zu den aktuellen Suchkriterien";
        }
        else
        {
            updateKonfigurator(ergebnisInnentuer, 0);
        }
    }


    void toggleInnentuerImKonfigurator()
    {

        // toggle nur, wenn es mindestens eine Zeile in der Ergebnistabelle gibt 
        // und wenn Konfigurator-Menü Sichtbar ist
        if ((ergebnisInnentuer.Count() >= 0) && (GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha == 1.0f))
        {
            // Info Anzeige
            if (aktuellGetoggelteInnentuerIndex < ergebnisInnentuer.Count())
            {
                updateKonfigurator(ergebnisInnentuer, aktuellGetoggelteInnentuerIndex);
                aktuellGetoggelteInnentuerIndex++;
            }
            else
            {
                aktuellGetoggelteInnentuerIndex = 0;
                updateKonfigurator(ergebnisInnentuer, aktuellGetoggelteInnentuerIndex);
                aktuellGetoggelteInnentuerIndex++;
            }
        }

    }




    void updateKonfigurator(List<T_Innentuer> ergebnisInnentuer, int aktuellGetoggelteInnentuerIndex)
    {

        GameObject.Find(guiKonfigurator.InfoTextNameInCanvas).GetComponent<Text>().text = "Innentür";

        if (ergebnisInnentuer.Count() > 0)
        {
            GameObject.Find(guiKonfigurator.InfoTextNameInCanvas).GetComponent<Text>().text += " (" + (aktuellGetoggelteInnentuerIndex + 1).ToString() + " von " + ergebnisInnentuer.Count().ToString() + ")";
            GameObject.Find(guiKonfigurator.InfoTextNameInCanvas).GetComponent<Text>().text += "\n" + ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].Id;
        }
        else
        {
            GameObject.Find(guiKonfigurator.InfoTextNameInCanvas).GetComponent<Text>().text += " : Keine Treffer bei den aktuellen Suchkriterien.";
        }


        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (int i = 1; i <= guiKonfigurator.ZeileContainerAnzahlZeilen; i++)
        {
            GameObject.Find(guiKonfigurator.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = "";
            GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = "";
            GameObject.Find(guiKonfigurator.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 0.0f;
            // hier nich alle Matrialien
        }

        for (int i = 1; i <= guiKonfigurator.AnzuzeigendeSpalteDerTabelleInnentuer.Count(); i++)
        {
            switch (i - 1)
            {
                case 0:
                    {
                        //GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = tabelleObjektteil.Find(x => x.Id == "Band_001").Detail;
                        GameObject.Find(guiKonfigurator.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].Zarge;
                        GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].Zarge).Detail;
                        Debug.Log("Material Zarge: " + ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].Zarge).MAT));
                        break;
                    }
                case 1:
                    {
                        GameObject.Find(guiKonfigurator.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].Tuerblatt;
                        GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].Tuerblatt).Detail;
                        Debug.Log("Material Tuerblatt: " + ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].Tuerblatt).MAT));

                        break;
                    }
                case 2:
                    {
                        GameObject.Find(guiKonfigurator.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].DrueckerZier;
                        GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].DrueckerZier).Detail;
                        Debug.Log("Material DrueckerZier: " + ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].DrueckerZier).MAT));
                        break;
                    }
                case 3:
                    {
                        GameObject.Find(guiKonfigurator.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].DrueckerFalz;
                        GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].DrueckerFalz).Detail;
                        Debug.Log("Material DrueckerFalz: " + ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuellGetoggelteInnentuerIndex].DrueckerFalz).MAT));
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
            GameObject.Find(guiKonfigurator.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 1.0f;
        }



    }



    void toggleMenuePunkt(int menue, int untermenue)
    {
        //Debug.Log("aktueller Index: " + innentuerParameter.TogglePunktIndex[untermenue - 1].ToString());
        //Debug.Log("Anzahl Toggles" + innentuerParameter.Toggle[untermenue - 1].Count());
        switch (menue)
        {
            case 1:
                {
                    if (hauptmenueParameter.TogglePunktIndex[untermenue - 1] < (hauptmenueParameter.Toggle[untermenue - 1].Count() - 1))
                    {
                        hauptmenueParameter.TogglePunktIndex[untermenue - 1] += 1;
                    }
                    else
                    {
                        hauptmenueParameter.TogglePunktIndex[untermenue - 1] = 0;
                    }
                    GameObject.Find(guiHauptmenue.ZeileDetailNameInCanvas + untermenue.ToString()).GetComponent<Text>().text = hauptmenueParameter.Toggle[untermenue - 1][hauptmenueParameter.TogglePunktIndex[untermenue - 1]];
                    break;
                }
            case 2:
                {
                    if (innentuerParameter.TogglePunktIndex[untermenue - 1] < (innentuerParameter.Toggle[untermenue - 1].Count() - 1))
                    {
                        innentuerParameter.TogglePunktIndex[untermenue - 1] += 1;
                    }
                    else
                    {
                        innentuerParameter.TogglePunktIndex[untermenue - 1] = 0;
                    }
                    GameObject.Find(guiInnentuer.ZeileDetailNameInCanvas + untermenue.ToString()).GetComponent<Text>().text = innentuerParameter.Toggle[untermenue - 1][innentuerParameter.TogglePunktIndex[untermenue - 1]];
                    break;
                }
            case 3:
                {
                    break;
                }
            default:
                {
                    break;
                }

        }



    }



    // ---------------------------------------------------------------------------------------------------
    // CoRoutine
    // ---------------------------------------------------------------------------------------------------


    IEnumerator LerpColor(string gameObjectName)
    {

        // wenn diese drei Zeilen ausserhalb des IEnumarators als globale variablen definiere, wären Änderungen erst
        // nach Neustart von Unity übernommen (?)
        float duration = 0.2f; // Zeit in Sekunden.
        float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
        Color32 floatColor = new Color32(0, 0, 0, 255);

        tempvar = true;
        imaget = GameObject.Find(gameObjectName).GetComponent<Image>();
        Color32 colorOld = imaget.color;

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            imaget.color = Color32.Lerp(imaget.color, floatColor, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

        progress = 0;
        while (progress < 1)
        {
            imaget.color = Color32.Lerp(imaget.color, colorOld, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

        tempvar = false;
    }



    // ---------------------------------------------------------------------------------------------------
    // generiere GUI
    // ---------------------------------------------------------------------------------------------------


    // generiere GameObject Canvas
    void generiereMenueCanvas(string canvasName)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = canvasName;
        myGO.layer = 5; // 5:UI

        // Canvas
        myGO.AddComponent<Canvas>();
        myCanvas = myGO.GetComponent<Canvas>();

        // Canvas renderMode
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Canvas RectTransform
        myCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        myCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Canvas Scaler
        myGO.AddComponent<CanvasScaler>();
        CanvasScaler myCanvasScaler;
        myCanvasScaler = myGO.GetComponent<CanvasScaler>();
        myCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        myCanvasScaler.referenceResolution = new Vector2(breiteMenuCanvas, hoeheMenuCanvas); ;

        // Canvas GraphicRaycaster
        myGO.AddComponent<GraphicRaycaster>();
    }



    // ---------------------------------------------------------------------------------------------------
    // generiere GUI: Hauptmenue
    // ---------------------------------------------------------------------------------------------------


    void generiereHauptmenueContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.Width, guiHauptmenue.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.Parent).transform);

        // Image color
        myImage.color = guiHauptmenue.Color;
    }

    void generiereHauptmenueHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.HeaderContainerPosX, guiHauptmenue.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.HeaderContainerWidth, guiHauptmenue.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.HeaderContainerParent).transform);

        // Image color
        myImage.color = guiHauptmenue.HeaderContainerColor;
    }

    void generiereHauptmenueHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.HeaderLogoPosX, guiHauptmenue.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.HeaderLogoWidth, guiHauptmenue.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiHauptmenue.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.HeaderLogoParent).transform);

        // Image color
        myImage.color = guiHauptmenue.HeaderLogoColor;
    }

    void generiereHauptmenueHeaderText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.HeaderTextPosX, guiHauptmenue.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.HeaderTextWidth, guiHauptmenue.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.HeaderTextFontType) as Font;
        myText.fontSize = guiHauptmenue.HeaderTextFontSize;
        myText.fontStyle = guiHauptmenue.HeaderTextFontStyle;
        myText.color = guiHauptmenue.HeaderTextColor;

        // Text
        myText.text = guiHauptmenue.HeaderTextText;
    }

    void generiereHauptmenueInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.InfoContainerPosX, guiHauptmenue.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.InfoContainerWidth, guiHauptmenue.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.InfoContainerParent).transform);

        // Image color
        myImage.color = guiHauptmenue.InfoContainerColor;
    }

    void generiereHauptmenueInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.InfoTextPosX, guiHauptmenue.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.InfoTextWidth, guiHauptmenue.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.InfoTextFontType) as Font;
        myText.fontSize = guiHauptmenue.InfoTextFontSize;
        myText.fontStyle = guiHauptmenue.InfoTextFontStyle;
        myText.color = guiHauptmenue.InfoTextColor;

        // Text
        myText.text = guiHauptmenue.InfoTextText;
    }

    void generiereHauptmenueInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.InfoLogoPosX, guiHauptmenue.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.InfoLogoWidth, guiHauptmenue.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiHauptmenue.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.InfoLogoParent).transform);

        // Image color
        myImage.color = guiHauptmenue.InfoLogoColor;
    }

    void generiereHauptmenueHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.HauptContainerPosX, guiHauptmenue.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.HauptContainerWidth, guiHauptmenue.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.HauptContainerParent).transform);

        // Image color
        myImage.color = guiHauptmenue.HauptContainerColor;
    }

    void generiereHauptmenueHauptContainerZeilen(int anzahlZeilen)
    {
        for (int i = 1; i <= anzahlZeilen; i++)
        {
            // übergib: GameObject-Name(!) / Zeilennummer, beginnen mit 1 / Index der aktuell getoggelten Innentür
            generiereHauptmenueZeileContainer(i);
            //    // übergib: GameObject-Parent(!) / Zeilennummer, beginnen mit 1 /
            generiereHauptmenueZeileHeader(i);
            //    generiereHauptmenueZeileMaterial(i);
            generiereHauptmenueZeileDetail(i);
            generiereHauptmenueZeileLogo(i);
            generiereHauptmenueZeileNummer(i);
        }
    }

    void generiereHauptmenueZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileContainerPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileContainerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileContainerWidth, guiHauptmenue.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileContainerParent).transform);

        // Image color
        myImage.color = guiHauptmenue.ZeileContainerColor;
    }

    void generiereHauptmenueZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileHeaderNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileHeaderPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileHeaderWidth, guiHauptmenue.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileHeaderParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.ZeileHeaderFontType) as Font;
        myText.fontSize = guiHauptmenue.ZeileHeaderFontSize;
        myText.fontStyle = guiHauptmenue.ZeileHeaderFontStyle;
        myText.color = guiHauptmenue.ZeileHeaderColor;

        myText.text = "Header";
    }

    void generiereHauptmenueZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileDetailPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileDetailNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileDetailWidth, guiHauptmenue.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileDetailParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.ZeileDetailFontType) as Font;
        myText.fontSize = guiHauptmenue.ZeileDetailFontSize;
        myText.fontStyle = guiHauptmenue.ZeileDetailFontStyle;
        myText.color = guiHauptmenue.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    void generiereHauptmenueZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileLogoPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileLogoNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileLogoWidth, guiHauptmenue.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiHauptmenue.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileLogoParent + nummer.ToString()).transform);

        // Image color
        myImage.color = guiHauptmenue.ZeileLogoColor;
    }

    void generiereHauptmenueZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileNummerPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileNummerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileNummerWidth, guiHauptmenue.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileNummerParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.ZeileNummerFontType) as Font;
        myText.fontSize = guiHauptmenue.ZeileNummerFontSize;
        myText.fontStyle = guiHauptmenue.ZeileNummerFontStyle;
        myText.color = guiHauptmenue.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }



    // ---------------------------------------------------------------------------------------------------
    // generiere GUI: Innentuer
    // ---------------------------------------------------------------------------------------------------

    void generiereInnentuerContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.Width, guiInnentuer.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.Parent).transform);

        // Image color
        myImage.color = guiInnentuer.Color;
    }

    void generiereInnentuerHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.HeaderContainerPosX, guiInnentuer.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.HeaderContainerWidth, guiInnentuer.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.HeaderContainerParent).transform);

        // Image color
        myImage.color = guiInnentuer.HeaderContainerColor;
    }

    void generiereInnentuerHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.HeaderLogoPosX, guiInnentuer.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.HeaderLogoWidth, guiInnentuer.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiInnentuer.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.HeaderLogoParent).transform);

        // Image color
        myImage.color = guiInnentuer.HeaderLogoColor;
    }

    void generiereInnentuerHeaderText()
    {   // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.HeaderTextPosX, guiInnentuer.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.HeaderTextWidth, guiInnentuer.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.HeaderTextFontType) as Font;
        myText.fontSize = guiInnentuer.HeaderTextFontSize;
        myText.fontStyle = guiInnentuer.HeaderTextFontStyle;
        myText.color = guiInnentuer.HeaderTextColor;

        // Text
        myText.text = guiInnentuer.HeaderTextText;
    }

    void generiereInnentuerInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.InfoContainerPosX, guiInnentuer.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.InfoContainerWidth, guiInnentuer.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.InfoContainerParent).transform);

        // Image color
        myImage.color = guiInnentuer.InfoContainerColor;
    }

    void generiereInnentuerInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.InfoTextPosX, guiInnentuer.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.InfoTextWidth, guiInnentuer.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.InfoTextFontType) as Font;
        myText.fontSize = guiInnentuer.InfoTextFontSize;
        myText.fontStyle = guiInnentuer.InfoTextFontStyle;
        myText.color = guiInnentuer.InfoTextColor;

        // Text
        myText.text = guiInnentuer.InfoTextText;
    }

    void generiereInnentuerInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.InfoLogoPosX, guiInnentuer.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.InfoLogoWidth, guiInnentuer.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiInnentuer.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.InfoLogoParent).transform);

        // Image color
        myImage.color = guiInnentuer.InfoLogoColor;
    }

    void generiereInnentuerHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.HauptContainerPosX, guiInnentuer.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.HauptContainerWidth, guiInnentuer.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.HauptContainerParent).transform);

        // Image color
        myImage.color = guiInnentuer.HauptContainerColor;
    }

    void generiereInnentuerHauptContainerZeilen(int anzahlZeilen)
    {
        for (int i = 1; i <= anzahlZeilen; i++)
        {
            // übergib: GameObject-Name(!) / Zeilennummer, beginnen mit 1 / Index der aktuell getoggelten Innentür
            generiereInnentuerZeileContainer(i);
            //    // übergib: GameObject-Parent(!) / Zeilennummer, beginnen mit 1 /
            generiereInnentuerZeileHeader(i);
            //    generiereInnentuerZeileMaterial(i);
            generiereInnentuerZeileDetail(i);
            generiereInnentuerZeileLogo(i);
            generiereInnentuerZeileNummer(i);
        }
    }

    void generiereInnentuerZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileContainerPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileContainerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileContainerWidth, guiInnentuer.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileContainerParent).transform);

        // Image color
        myImage.color = guiInnentuer.ZeileContainerColor;
    }

    void generiereInnentuerZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileHeaderNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileHeaderPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileHeaderWidth, guiInnentuer.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileHeaderParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.ZeileHeaderFontType) as Font;
        myText.fontSize = guiInnentuer.ZeileHeaderFontSize;
        myText.fontStyle = guiInnentuer.ZeileHeaderFontStyle;
        myText.color = guiInnentuer.ZeileHeaderColor;

        myText.text = "Header";
    }

    void generiereInnentuerZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileDetailPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileDetailNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileDetailWidth, guiInnentuer.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileDetailParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.ZeileDetailFontType) as Font;
        myText.fontSize = guiInnentuer.ZeileDetailFontSize;
        myText.fontStyle = guiInnentuer.ZeileDetailFontStyle;
        myText.color = guiInnentuer.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    void generiereInnentuerZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileLogoPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileLogoNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileLogoWidth, guiInnentuer.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiInnentuer.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileLogoParent + nummer.ToString()).transform);

        // Image color
        myImage.color = guiInnentuer.ZeileLogoColor;


    }

    void generiereInnentuerZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileNummerPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileNummerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileNummerWidth, guiInnentuer.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileNummerParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.ZeileNummerFontType) as Font;
        myText.fontSize = guiInnentuer.ZeileNummerFontSize;
        myText.fontStyle = guiInnentuer.ZeileNummerFontStyle;
        myText.color = guiInnentuer.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }




    // ---------------------------------------------------------------------------------------------------
    // generiere GUI: Konfigurator
    // ---------------------------------------------------------------------------------------------------


    void generiereKonfiguratorContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.Width, guiKonfigurator.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.Parent).transform);

        // Image color
        myImage.color = guiKonfigurator.Color;
    }

    void generiereKonfiguratorHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.HeaderContainerPosX, guiKonfigurator.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.HeaderContainerWidth, guiKonfigurator.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.HeaderContainerParent).transform);

        // Image color
        myImage.color = guiKonfigurator.HeaderContainerColor;
    }

    void generiereKonfiguratorHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.HeaderLogoPosX, guiKonfigurator.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.HeaderLogoWidth, guiKonfigurator.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiKonfigurator.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.HeaderLogoParent).transform);

        // Image color
        myImage.color = guiKonfigurator.HeaderLogoColor;
    }

    void generiereKonfiguratorHeaderText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.HeaderTextPosX, guiKonfigurator.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.HeaderTextWidth, guiKonfigurator.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.HeaderTextFontType) as Font;
        myText.fontSize = guiKonfigurator.HeaderTextFontSize;
        myText.fontStyle = guiKonfigurator.HeaderTextFontStyle;
        myText.color = guiKonfigurator.HeaderTextColor;

        // Text
        myText.text = guiKonfigurator.HeaderTextText;
    }

    void generiereKonfiguratorInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.InfoContainerPosX, guiKonfigurator.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.InfoContainerWidth, guiKonfigurator.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.InfoContainerParent).transform);

        // Image color
        myImage.color = guiKonfigurator.InfoContainerColor;
    }

    void generiereKonfiguratorInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.InfoTextPosX, guiKonfigurator.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.InfoTextWidth, guiKonfigurator.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.InfoTextFontType) as Font;
        myText.fontSize = guiKonfigurator.InfoTextFontSize;
        myText.fontStyle = guiKonfigurator.InfoTextFontStyle;
        myText.color = guiKonfigurator.InfoTextColor;

        // Text
        myText.text = guiKonfigurator.InfoTextText;
    }

    void generiereKonfiguratorInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.InfoLogoPosX, guiKonfigurator.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.InfoLogoWidth, guiKonfigurator.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiKonfigurator.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.InfoLogoParent).transform);

        // Image color
        myImage.color = guiKonfigurator.InfoLogoColor;
    }

    void generiereKonfiguratorInfoNummer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.InfoNummerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.InfoNummerPosX, guiKonfigurator.InfoNummerPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.InfoNummerWidth, guiKonfigurator.InfoNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.InfoNummerParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.InfoNummerFontType) as Font;
        myText.fontSize = guiKonfigurator.InfoNummerFontSize;
        myText.fontStyle = guiKonfigurator.InfoNummerFontStyle;
        myText.color = guiKonfigurator.InfoNummerColor;

        // Text
        myText.text = guiKonfigurator.InfoNummerText;
    }

    void generiereKonfiguratorHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.HauptContainerPosX, guiKonfigurator.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.HauptContainerWidth, guiKonfigurator.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.HauptContainerParent).transform);

        // Image color
        myImage.color = guiKonfigurator.HauptContainerColor;
    }

    void generiereKonfiguratorHauptContainerZeilen(int anzahlZeilen)
    {
        for (int i = 1; i <= anzahlZeilen; i++)
        {
            // übergib: GameObject-Name(!) / Zeilennummer, beginnen mit 1 / Index der aktuell getoggelten Innentür
            generiereKonfiguratorZeileContainer(i);
            // übergib: GameObject-Parent(!) / Zeilennummer, beginnen mit 1 /
            generiereKonfiguratorZeileHeader(i);
            generiereKonfiguratorZeileMaterial(i);
            generiereKonfiguratorZeileDetail(i);
            generiereKonfiguratorZeileLogo(i);
            generiereKonfiguratorZeileNummer(i);
        }
    }

    void generiereKonfiguratorZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileContainerPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileContainerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileContainerWidth, guiKonfigurator.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileContainerParent).transform);

        // Image color
        myImage.color = guiKonfigurator.ZeileContainerColor;
    }

    void generiereKonfiguratorZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileHeaderNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileHeaderPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileHeaderWidth, guiKonfigurator.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileHeaderParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.ZeileHeaderFontType) as Font;
        myText.fontSize = guiKonfigurator.ZeileHeaderFontSize;
        myText.fontStyle = guiKonfigurator.ZeileHeaderFontStyle;
        myText.color = guiKonfigurator.ZeileHeaderColor;

        myText.text = "Header";
    }

    void generiereKonfiguratorZeileMaterial(int nummer)
    {
        int neuePositionY;
        int neuePositionX;

        int i = 0;
        for (int y = 1; y <= guiKonfigurator.ZeileMaterialAnzahlMaterialOptionenY; y++)

        {
            for (int x = 1; x <= guiKonfigurator.ZeileMaterialAnzahlMaterialOptionenX; x++)
            {
                i++;
                // Berechnung Position des aktuellen Containers
                neuePositionY = -((y - 1) * (guiKonfigurator.ZeileMaterialAnzahlMaterialAbstandY + guiKonfigurator.ZeileMaterialHeight)) + guiKonfigurator.ZeileMaterialPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));
                neuePositionX = -((guiKonfigurator.ZeileMaterialAnzahlMaterialOptionenX - x) * (guiKonfigurator.ZeileMaterialAnzahlMaterialAbstandX + guiKonfigurator.ZeileMaterialWidth)) + guiKonfigurator.ZeileMaterialPosX;

                // Game Object
                myGO = new GameObject();
                myGO.name = guiKonfigurator.ZeileMaterialNameInCanvas + nummer.ToString() + i.ToString();
                myGO.layer = 5; // 5:UI

                // Image
                myGO.AddComponent<Image>();

                //Image myImage;
                myImage = myGO.GetComponent<Image>();
                myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(neuePositionX, neuePositionY, 0);
                myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileMaterialWidth, guiKonfigurator.ZeileMaterialHeight);
                myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
                myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
                myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

                // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
                myGO.AddComponent<CanvasGroup>();

                // mache das GameObject Info-Container Image zum child des Menu
                myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileMaterialParent + nummer.ToString()).transform);

                // Image color
                myImage.color = guiKonfigurator.ZeileMaterialColor;
            }
        }
    }

    void generiereKonfiguratorZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileDetailPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileDetailNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileDetailWidth, guiKonfigurator.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileDetailParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.ZeileDetailFontType) as Font;
        myText.fontSize = guiKonfigurator.ZeileDetailFontSize;
        myText.fontStyle = guiKonfigurator.ZeileDetailFontStyle;
        myText.color = guiKonfigurator.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    void generiereKonfiguratorZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileLogoPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileLogoNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileLogoWidth, guiKonfigurator.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiKonfigurator.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileLogoParent + nummer.ToString()).transform);

        // Image color
        myImage.color = guiKonfigurator.ZeileLogoColor;


    }

    void generiereKonfiguratorZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileNummerPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileNummerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileNummerWidth, guiKonfigurator.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileNummerParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.ZeileNummerFontType) as Font;
        myText.fontSize = guiKonfigurator.ZeileNummerFontSize;
        myText.fontStyle = guiKonfigurator.ZeileNummerFontStyle;
        myText.color = guiKonfigurator.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }



    // ---------------------------------------------------------------------------------------------------
    // Datenbank-Abfragen
    // ---------------------------------------------------------------------------------------------------

    List<T_Band> dBAbfrageBand()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Band";
        //Debug.Log(query);
        List<T_Band> queryResult = restClient.GetExecuteResultsBand(query);
        return queryResult;
    }

    List<T_Bandaufnahme> dBAbfrageBandaufnahme()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Bandaufnahme";
        //Debug.Log(query);
        List<T_Bandaufnahme> queryResult = restClient.GetExecuteResultsBandaufnahme(query);
        return queryResult;
    }

    List<T_Druecker> dBAbfrageDruecker()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Druecker";
        //Debug.Log(query);
        List<T_Druecker> queryResult = restClient.GetExecuteResultsDruecker(query);
        return queryResult;
    }

    List<T_Schliessblech> dBAbfrageSchliessblech()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Schliessblech";
        //Debug.Log(query);
        List<T_Schliessblech> queryResult = restClient.GetExecuteResultsSchliessblech(query);
        return queryResult;
    }

    List<T_Schlosskasten> dBAbfrageSchlosskasten()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Schlosskasten";
        //Debug.Log(query);
        List<T_Schlosskasten> queryResult = restClient.GetExecuteResultsSchlosskasten(query);
        return queryResult;
    }

    List<T_Schwelle> dBAbfrageSchwelle()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Schwelle";
        //Debug.Log(query);
        List<T_Schwelle> queryResult = restClient.GetExecuteResultsSchwelle(query);
        return queryResult;
    }

    List<T_Tuerblatt> dBAbfrageTuerblatt()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Tuerblatt";
        //Debug.Log(query);
        List<T_Tuerblatt> queryResult = restClient.GetExecuteResultsTuerblatt(query);
        return queryResult;
    }

    List<T_Zarge> dBAbfrageZarge()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Zarge";
        //Debug.Log(query);
        List<T_Zarge> queryResult = restClient.GetExecuteResultsZarge(query);
        return queryResult;
    }

    List<T_Innentuer> dBAbfrageInnentuer()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Innentuer";
        //Debug.Log(query);
        List<T_Innentuer> queryResult = restClient.GetExecuteResultsInnentuer(query);
        return queryResult;
    }

    List<T_Objektteil> dBAbfrageObjektteil()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Objektteil";
        //Debug.Log(query);
        List<T_Objektteil> queryResult = restClient.GetExecuteResultsObjektteil(query);
        return queryResult;
    }

    List<T_Hersteller> dBAbfrageHersteller()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Hersteller";
        //Debug.Log(query);
        List<T_Hersteller> queryResult = restClient.GetExecuteResultsHersteller(query);
        return queryResult;
    }

    List<T_MAT> dBAbfrageMAT()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.MAT";
        //Debug.Log(query);
        List<T_MAT> queryResult = restClient.GetExecuteResultsMAT(query);
        return queryResult;
    }

    List<T_Innentuer> ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien()
    {
        //RestClientManager restClient = new RestClientManager();
        //string query = "SELECT Thomas.Innentuer.* FROM Thomas.Innentuer INNER JOIN Thomas.Zarge ON Thomas.Innentuer.Zarge = Thomas.Zarge.Id INNER JOIN Thomas.Tuerblatt ON Thomas.Innentuer.Tuerblatt = Thomas.Tuerblatt.Id WHERE (Thomas.Tuerblatt.HoeheDIN LIKE '" + innentuerOutputParameter.HoeheDIN + "' AND Thomas.Tuerblatt.BreiteDIN LIKE '" + innentuerOutputParameter.BreiteDIN + "' AND Thomas.Zarge.HoeheDIN LIKE '" + innentuerOutputParameter.HoeheDIN + "' AND Thomas.Zarge.BreiteDIN LIKE '" + innentuerOutputParameter.BreiteDIN + "' AND Thomas.Zarge.Wandstaerke LIKE '" + innentuerOutputParameter.Wandstaerke + "' AND Thomas.Zarge.Bekleidungsbreite LIKE '" + innentuerOutputParameter.Bekleidungsbreite + "' )";
        //Debug.Log(query);

        //List<T_Innentuer> queryResult = restClient.GetExecuteResultsInnentuer(query);

        //foreach (T_Innentuer zeile in queryResult)
        //{
        //    Debug.Log(zeile.Id + " - " + zeile.Detail + " - " + zeile.Zarge + " - " + zeile.Tuerblatt + " - " + zeile.Band1 + " - " + zeile.Band2 + " - " + zeile.Bandaufnahme1 + " - " + zeile.Bandaufnahme2 + " - " + zeile.DrueckerFalz + " - " + zeile.DrueckerZier + " - " + zeile.Schlosskasten + " - " + zeile.Schliessblech + " - " + zeile.Schwelle);
        //    aktuelleKonfiguration.Zarge = zeile.Zarge;
        //    aktuelleKonfiguration.Tuerblatt = zeile.Tuerblatt;
        //    aktuelleKonfiguration.DrueckerFalz = zeile.DrueckerFalz;
        //    aktuelleKonfiguration.DrueckerZier = zeile.DrueckerZier;
        //}
        //return queryResult;

        // gehe alle Zeilen der Tabelle Innentuer durch ..
        ergebnisInnentuer.Clear();
        ergebnisInnentuerNachCheckHoehe.Clear();
        ergebnisInnentuerNachCheckBreite.Clear();
        ergebnisInnentuerNachCheckWandstaerke.Clear();
        ergebnisInnentuerNachCheckBekleidungsbreite.Clear();

        Debug.Log("-----------ergebnisInnentuer.Count() START : " + ergebnisInnentuer.Count().ToString());


        foreach (T_Innentuer zeileInnentuer in tabelleInnentuer)
        {
            // .. und ermittele aus der entsprechenden Spalte die Zargen-ID
            // gehe alle Zeilen der Tabelle Zargen durch ..

            foreach (T_Zarge zeileZarge in tabelleZarge)
            {
                // .. und prüfe nur in den Zeilen mit der relevanten Zargen-ID, ..
                if (zeileZarge.Id == zeileInnentuer.Zarge)
                {
                    Debug.Log("-----------innentuerOutputParameter.HoeheDIN) : " + innentuerOutputParameter.HoeheDIN);
                    // ob die Höhe der Zargen-Id der momentan eingestelletn Höhe ist
                    if ((zeileZarge.HoeheDIN.ToString() == innentuerOutputParameter.HoeheDIN) || (innentuerOutputParameter.HoeheDIN == "%"))
                    {
                        ergebnisInnentuerNachCheckHoehe.Add(zeileInnentuer);
                    }
                }
            }
        }
        Debug.Log("-----------ergebnisInnentuerNachCheckHoehe.Count() nach Hoehe : " + ergebnisInnentuerNachCheckHoehe.Count().ToString());
        Debug.Log("-----------ergebnisInnentuer.Count() nach Hoehe : " + ergebnisInnentuer.Count().ToString());
        ergebnisInnentuer = ergebnisInnentuerNachCheckHoehe;

        // wenn es mindesten einen Treffer nach Check Hoehe gab, mache weiter
        if (ergebnisInnentuerNachCheckHoehe.Count() > 0)
        {
            foreach (T_Innentuer zeileInnentuerNachCheckHoehe in ergebnisInnentuerNachCheckHoehe)
            {
                foreach (T_Zarge zeileZarge in tabelleZarge)
                {
                    if (zeileZarge.Id == zeileInnentuerNachCheckHoehe.Zarge)
                    {
                        Debug.Log("-----------innentuerOutputParameter.BreiteDIN) : " + innentuerOutputParameter.BreiteDIN);
                        if ((zeileZarge.BreiteDIN.ToString() == innentuerOutputParameter.BreiteDIN) || (innentuerOutputParameter.BreiteDIN == "%"))
                        {
                            ergebnisInnentuerNachCheckBreite.Add(zeileInnentuerNachCheckHoehe);
                        }
                    }
                }
            }
            ergebnisInnentuer = ergebnisInnentuerNachCheckBreite;
            Debug.Log("-----------ergebnisInnentuerNachCheckBreite.Count() nach Breite : " + ergebnisInnentuerNachCheckBreite.Count().ToString());
            Debug.Log("-----------ergebnisInnentuer.Count() nach Breite : " + ergebnisInnentuer.Count().ToString());

            // wenn es mindesten einen Treffer nach Check Breite gab, mache weiter
            if (ergebnisInnentuerNachCheckBreite.Count() > 0)
            {
                foreach (T_Innentuer zeileInnentuerNachCheckBreite in ergebnisInnentuerNachCheckBreite)
                {
                    foreach (T_Zarge zeileZarge in tabelleZarge)
                    {
                        if (zeileZarge.Id == zeileInnentuerNachCheckBreite.Zarge)
                        {
                            Debug.Log("-----------innentuerOutputParameter.Wandstärke) : " + innentuerOutputParameter.Wandstaerke);
                            if ((zeileZarge.Wandstaerke.ToString() == innentuerOutputParameter.Wandstaerke) || (innentuerOutputParameter.Wandstaerke == "%"))
                            {
                                ergebnisInnentuerNachCheckWandstaerke.Add(zeileInnentuerNachCheckBreite);
                            }
                        }
                    }
                }
                ergebnisInnentuer = ergebnisInnentuerNachCheckWandstaerke;
                Debug.Log("-----------ergebnisInnentuerNachCheckWandstaerke.Count() nachWandstaerke : " + ergebnisInnentuerNachCheckWandstaerke.Count().ToString());
                Debug.Log("-----------ergebnisInnentuer.Count() nach Wandstaerke : " + ergebnisInnentuer.Count().ToString());

                // wenn es mindesten einen Treffer nach Check Wandstaerke gab, mache weiter
                if (ergebnisInnentuerNachCheckWandstaerke.Count() > 0)
                {
                    foreach (T_Innentuer zeileInnentuerNachCheckWandstaerke in ergebnisInnentuerNachCheckWandstaerke)
                    {
                        foreach (T_Zarge zeileZarge in tabelleZarge)
                        {
                            if (zeileZarge.Id == zeileInnentuerNachCheckWandstaerke.Zarge)
                            {
                                Debug.Log("-----------innentuerOutputParameter.Bekleidungsbreite) : " + innentuerOutputParameter.Bekleidungsbreite);
                                if ((zeileZarge.Bekleidungsbreite.ToString() == innentuerOutputParameter.Bekleidungsbreite) || (innentuerOutputParameter.Bekleidungsbreite == "%"))
                                {
                                    ergebnisInnentuerNachCheckBekleidungsbreite.Add(zeileInnentuerNachCheckWandstaerke);
                                }
                            }
                        }
                    }
                    ergebnisInnentuer = ergebnisInnentuerNachCheckBekleidungsbreite;
                    Debug.Log("-----------ergebnisInnentuerNachCheckBekleidungsbreite.Count() nach Bekleidungsbreite : " + ergebnisInnentuerNachCheckBekleidungsbreite.Count().ToString());
                    Debug.Log("-----------ergebnisInnentuer.Count() nach Bekleidungsbreite : " + ergebnisInnentuer.Count().ToString());

                }
                else
                {
                    ergebnisInnentuer.Clear();
                }
            }
            else
            {
                ergebnisInnentuer.Clear();
            }
        }
        else
        {
            ergebnisInnentuer.Clear();
        }               

        return ergebnisInnentuer;
    }




    // ---------------------------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------------


    string ermitteleMaterialien(string MAT)
    {
        // diese Methode soll den json-Material auseinandernehmen und zurückgeben
        // Liste mit String [MATId] und String Array [Materialbezeichnung]
        // Beispiel
        // MATId = "Mat1"; Materialbezeichnung["M_1K_3DTE_Wood_020","M_2K_CC0T_Wood025_2K-JPG"]
        // MATId = "Mat2"; Materialbezeichnung["M_1K_3NAS_Wood_018","M_2K_CC0T_Floor025_2K-JPG"]
        return MAT;
    }


    void updateHauptmenue(HauptmenueParameter parameter)
    {

        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (int i = 1; i <= guiHauptmenue.ZeileContainerAnzahlZeilen; i++)
        {
            GameObject.Find(guiHauptmenue.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = "";
            GameObject.Find(guiHauptmenue.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 0.0f;
            // hier nich alle Matrialien
        }
        

        for (int i = 1; i <= parameter.Menuepunkt.Count(); i++)
        {
            GameObject.Find(guiHauptmenue.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = parameter.Menuepunkt[i - 1];
            GameObject.Find(guiHauptmenue.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 1.0f;
            GameObject.Find(guiHauptmenue.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = hauptmenueParameter.Toggle[i - 1][hauptmenueParameter.TogglePunktIndex[i - 1]];
        }
    }

    void updateInnentuer(InnentuerParameter parameter)
    {

        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (int i = 1; i <= guiInnentuer.ZeileContainerAnzahlZeilen; i++)
        {
            GameObject.Find(guiInnentuer.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = "";
            GameObject.Find(guiInnentuer.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 0.0f;
            // hier nich alle Matrialien
        }

        for (int i = 1; i <= parameter.Menuepunkt.Count(); i++)
        {
            GameObject.Find(guiInnentuer.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = parameter.Menuepunkt[i - 1];
            GameObject.Find(guiInnentuer.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 1.0f;
            GameObject.Find(guiInnentuer.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = innentuerParameter.Toggle[i - 1][innentuerParameter.TogglePunktIndex[i - 1]];
        }
    }



    // spezielle Konfigurationen für die Menüpunkte (bspw. Logo)
    void spezialkonfigurationMenuepunkt()
    {
        // prüfe alle GameObjects der Scene auf bestimmte Strings in Namen        
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
        {
            // String "ZeilenLogo1"
            if (gos[i].name.Contains("ZeilenLogo1"))
            {
                Sprite FULLHP = Resources.Load<Sprite>("Logos/cog-wheel-silhouette_white");
                gos[i].GetComponent<Image>().sprite = FULLHP;
            }
            // String "ZeilenLogo2"
            else if (gos[i].name.Contains("ZeilenLogo2"))
            {
                Sprite FULLHP = Resources.Load<Sprite>("Logos/list-with-dots_white");
                gos[i].GetComponent<Image>().sprite = FULLHP;
            }
            // String "ZeilenLogo3"
            else if (gos[i].name.Contains("ZeilenLogo3"))
            {
                Sprite FULLHP = Resources.Load<Sprite>("Logos/list-with-dots_white");
                gos[i].GetComponent<Image>().sprite = FULLHP;
            }
            // String "ZeilenText"
            else if (gos[i].name.Contains("ZeilenText"))
            {
                gos[i].GetComponent<Text>().fontSize = 70;
            }
        }
    }




    // ---------------------------------------------------------------------------------------------------
    // Logik
    // ---------------------------------------------------------------------------------------------------


    // generiere den String, der in der GUI den aktuellen Menüpfad anzeigt
    void ermitteleAktuellenMenuPfad(string aktuellesMenu)
    {
        menuPfad = "";
        string temp;
        int i = 1;

        // ermittele die Tiefe des Menüpfades und gehe durch alle Menütiefen
        while (i <= aktuellesMenu.Length)
        {
            temp = aktuellesMenu.Substring(0, i);
            // füge den Namen des Menüpunktes der aktuellen Menütiefe zu dem String menuPfad hinzu
            menuPfad = menuPfad + " | " + innentuerPunkt.Find(Menu => Menu.Id == temp).Name;
            i++;
        }

        GameObject.Find("MenueInfoText").GetComponent<Text>().text = menuPfad;
    }


    void zeigeMenuAn(string menuAktuell)
    {
        //Debug.Log(menuAktuell);

        if (menuAktuell == "")
        {
            GameObject.Find("Innentuer").GetComponent<CanvasGroup>().alpha = 0.0f;
        }

        else
        {
            deaktiviereAlleZeilenContainer();

            GameObject.Find("Innentuer").GetComponent<CanvasGroup>().alpha = 1.0f;

            var menuPunktDescending = innentuerPunkt.OrderByDescending(i => i.Id).ToList();
            foreach (var smd in menuPunktDescending)
            {
                int i = Int32.Parse(smd.Id);
                menuId[i] = GameObject.Find("ZeilenContainer" + smd.Id);

                if (menuId[i])
                {
                    menuId[i].GetComponent<CanvasGroup>().alpha = 0.0f;
                }
            }

            menuId[Int32.Parse(menuAktuell)].GetComponent<CanvasGroup>().alpha = 1.0f;

            // generiere Untermenüs
            generiereMenuePunkt(menuAktuell);
        }
    }



    void toggleKonfiguratorTeilobjekt(int index)
    {

        aktuelleKonfiguration.Id = ergebnisInnentuer[index].Id;
        aktuelleKonfiguration.Detail = ergebnisInnentuer[index].Detail;
        aktuelleKonfiguration.Zarge = ergebnisInnentuer[index].Zarge;
        aktuelleKonfiguration.Tuerblatt = ergebnisInnentuer[index].Tuerblatt;
        aktuelleKonfiguration.Band1 = ergebnisInnentuer[index].Band1;
        aktuelleKonfiguration.Band2 = ergebnisInnentuer[index].Band2;
        aktuelleKonfiguration.Bandaufnahme2 = ergebnisInnentuer[index].Bandaufnahme1;
        aktuelleKonfiguration.Bandaufnahme2 = ergebnisInnentuer[index].Bandaufnahme2;
        aktuelleKonfiguration.DrueckerFalz = ergebnisInnentuer[index].DrueckerFalz;
        aktuelleKonfiguration.DrueckerZier = ergebnisInnentuer[index].DrueckerZier;
        aktuelleKonfiguration.Schlosskasten = ergebnisInnentuer[index].Schlosskasten;
        aktuelleKonfiguration.Schliessblech = ergebnisInnentuer[index].Schliessblech;
        aktuelleKonfiguration.Schwelle = ergebnisInnentuer[index].Schwelle;

        //Debug.Log(aktuelleKonfiguration.Zarge);
        //Debug.Log(aktuelleKonfiguration.Tuerblatt);
        //Debug.Log(aktuelleKonfiguration.DrueckerFalz);
        //Debug.Log(aktuelleKonfiguration.DrueckerZier);

        GameObject.Find("ZeilenText31").GetComponent<Text>().text = aktuelleKonfiguration.Zarge;
        GameObject.Find("ZeilenText32").GetComponent<Text>().text = aktuelleKonfiguration.Tuerblatt;
        GameObject.Find("ZeilenText33").GetComponent<Text>().text = aktuelleKonfiguration.DrueckerFalz;
        GameObject.Find("ZeilenText34").GetComponent<Text>().text = aktuelleKonfiguration.DrueckerZier;

    }

    void deaktiviereAlleZeilenContainer()
    {
        // prüfe alle GameObjects der Scene auf bestimmte Strings in Namen        
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
        {
            // String "ZeilenContainer"
            if (gos[i].name.Contains("ZeilenContainer"))
            {
                gos[i].GetComponent<CanvasGroup>().alpha = 0.0f;
                gos[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }
        }
    }

    void generiereZeilenContainer(string headerContainerName)
    {

        // ---------------------------------------------------------------------------------------------------
        // Header (Image)
        // ---------------------------------------------------------------------------------------------------
        myGO = new GameObject();
        myGO.name = headerContainerName;
        myGO.layer = 5; // 5:UI

        //Make the GameObject child of the Canvas
        //myGO.transform.SetParent(GameObject.Find("Menu1").transform);

        // Image
        myGO.AddComponent<Image>();
        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(breiteMenuZeile, hoeheMenuZeile);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        myImage.color = colorMenuHeaderContainer;
        myGO.AddComponent<CanvasGroup>();

    }

    void generiereZeilenLogo(string headerLogoName, string logoName)
    {

        // ---------------------------------------------------------------------------------------------------
        // Header (Image)
        // ---------------------------------------------------------------------------------------------------
        myGO = new GameObject();
        myGO.name = headerLogoName;
        myGO.layer = 5; // 5:UI

        //Make the GameObject child of the Canvas
        //myGO.transform.SetParent(GameObject.Find("Header").transform);

        // Image
        myGO.AddComponent<Image>();
        //Image myImage;
        myImage = myGO.GetComponent<Image>();

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, -20, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(breiteMenuLogo, hoeheMenuLogo);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        Sprite FULLHP = Resources.Load<Sprite>("Logos/" + logoName);
        myImage.sprite = FULLHP;
        myImage.color = colorMenuHeaderLogo;

    }

    void generiereZeilenText(string headerTextName, string headerTextText)
    {
        // ---------------------------------------------------------------------------------------------------
        // Header (Text)
        // ---------------------------------------------------------------------------------------------------
        myGO = new GameObject();
        myGO.name = headerTextName;
        myGO.layer = 5; // 5:UI

        //Make the GameObject child of the Canvas
        //myGO.transform.SetParent(GameObject.Find("Header").transform);

        // Text
        myGO.AddComponent<Text>();
        //Image myImage;
        myText = myGO.GetComponent<Text>();

        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(140, -20, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(breiteMenuText, hoeheMenuText);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        myText.text = headerTextText;
        myText.font = Resources.GetBuiltinResource(typeof(Font), headerFontType) as Font;
        myText.fontSize = headerFontSize;
        myText.fontStyle = headerFontStyle;
    }

    void generiereMenuePunkt(string mainMenu)
    {
        myGO = GameObject.Find("ZeilenContainer" + mainMenu);
        myGO.GetComponent<Image>().color = colorMenuContainer;

        // untermenüs
        for (int i = 1; i < 10; i++)
        {
            string unterMenu = mainMenu + i.ToString();
            if (GameObject.Find("ZeilenContainer" + unterMenu))
            {
                myGO = GameObject.Find("ZeilenContainer" + unterMenu);
                myGO.GetComponent<Image>().color = colorMenumenuPunktContainer;

                GameObject myGOLogo = GameObject.Find("ZeilenLogo" + unterMenu);
                GameObject myGOText = GameObject.Find("ZeilenText" + unterMenu);

                if (i == 1)
                {
                    menuId[Int32.Parse(unterMenu)].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, verschiebungYErste, 0);
                }
                else
                {
                    menuId[Int32.Parse(unterMenu)].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, verschiebungYErste + ((i - 1) * verschiebungYWeitere), 0);
                }
                menuId[Int32.Parse(unterMenu)].GetComponent<CanvasGroup>().alpha = 1.0f;
                //Debug.Log(" menuId[Int32.Parse(unterMenu)]: " + menuId[Int32.Parse(unterMenu)]);
                Sprite FULLHP = Resources.Load<Sprite>("Logos/" + i.ToString());
                //Debug.Log("Logos/" + i.ToString());
                myGOLogo.GetComponent<Image>().sprite = FULLHP;
                myGOText.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), menuPunktFontType) as Font;
                myGOText.GetComponent<Text>().fontSize = menuPunktFontSize;
                myGOText.GetComponent<Text>().fontStyle = menuPunktFontStyle;
                myGOText.GetComponent<Text>().GetComponent<RectTransform>().anchoredPosition = new Vector3(menuPunktVerschiebungX, menuPunktVerschiebungY, 0);
            }
        }

    }

    void setzeMenuePunktAktiv(string menuPunkt)
    {
        setzeMenuePunktDerselbenStufeInaktiv(menuPunkt);
        GameObject myGOLogo = GameObject.Find("ZeilenLogo" + menuPunkt);
        myGOLogo.GetComponent<Image>().color = colorMenumenuPunktAktiv;
        GameObject myGOText = GameObject.Find("ZeilenText" + menuPunkt);
        myGOText.GetComponent<Text>().color = colorMenumenuPunktAktiv;
    }

    void setzeMenuePunktDerselbenStufeInaktiv(string menuePunkt)
    {
        GameObject myGOLogo;
        GameObject myGOText;

        string topMenu = menuePunkt.Substring(0, menuePunkt.Length - 1);

        for (int i = 1; i < 10; i++)
        {
            if (GameObject.Find("ZeilenContainer" + topMenu + i.ToString()))
            {
                myGOLogo = GameObject.Find("ZeilenLogo" + topMenu + i.ToString());
                myGOLogo.GetComponent<Image>().color = colorMenumenuPunktDefault;
                myGOText = GameObject.Find("ZeilenText" + topMenu + i.ToString());
                myGOText.GetComponent<Text>().color = colorMenumenuPunktDefault;
            }
        }
    }



}
