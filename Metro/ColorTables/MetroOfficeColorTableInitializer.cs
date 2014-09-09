using System;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar.Schedule;
using DevComponents.DotNetBar.Rendering;
using DevComponents.AdvTree.Display;
using DevComponents.WinForms.Drawing;

namespace DevComponents.DotNetBar.Metro.ColorTables
{
    internal static class MetroOfficeColorTableInitializer
    {
        public static void InitializeColorTable(Office2007ColorTable table, ColorFactory factory, MetroPartColors metroColors)
        {
            #region RibbonControl Start Images
            table.RibbonControl.StartButtonDefault = BarFunctions.LoadBitmap("SystemImages.BlankOffice2010NormalSilver.png");
            table.RibbonControl.StartButtonMouseOver = BarFunctions.LoadBitmap("SystemImages.BlankOffice2010HotSilver.png");
            table.RibbonControl.StartButtonPressed = BarFunctions.LoadBitmap("SystemImages.BlankOffice2010PressedSilver.png");
            #endregion

            #region RibbonControl
            table.RibbonControl.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade));
            table.RibbonControl.InnerBorder = null; // LinearGradientColorTable.Empty;// new LinearGradientColorTable(factory.GetColor(Color.FromArgb(32, Color.White)));
            table.RibbonControl.TabsBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.RibbonControl.TabsGlassBackground = new LinearGradientColorTable(Color.Transparent, factory.GetColor(metroColors.CanvasColor));
            table.RibbonControl.TabDividerBorder = Color.Empty; // factory.GetColor(0xA7BAD1);
            table.RibbonControl.TabDividerBorderLight = Color.Empty; // factory.GetColor(0xF4F8FD);
            table.RibbonControl.CornerSize = 1;
            table.RibbonControl.PanelTopBackgroundHeight = 0;
            table.RibbonControl.PanelTopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.RibbonControl.PanelBottomBackground = null; // new LinearGradientColorTable(factory.GetColor(0xF6F7F8), factory.GetColor(0xE5E9EE));
            #endregion

            #region Ribbon KeyTips
            table.KeyTips.KeyTipBackground = factory.GetColor(metroColors.TextDisabledColor);
            table.KeyTips.KeyTipText = factory.GetColor(metroColors.CanvasColor);
            table.KeyTips.KeyTipBorder = factory.GetColor(metroColors.TextDisabledColor);
            #endregion

            #region Item Group
            table.ItemGroup.OuterBorder = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(0xC0C3C8));
            table.ItemGroup.InnerBorder = LinearGradientColorTable.Empty;
            table.ItemGroup.TopBackground = LinearGradientColorTable.Empty;
            table.ItemGroup.BottomBackground = LinearGradientColorTable.Empty;
            table.ItemGroup.ItemGroupDividerDark = Color.Empty;
            table.ItemGroup.ItemGroupDividerLight = Color.Empty;
            #endregion

            #region RibbonBar
            table.RibbonBar.Default = GetRibbonBar(factory, metroColors);
            table.RibbonBar.MouseOver = GetRibbonBarMouseOver(factory, metroColors);
            table.RibbonBar.Expanded = GetRibbonBarExpanded(factory, metroColors);
            #endregion

            #region ButtonItem Colors Initialization
            table.RibbonButtonItemColors.Clear();
            table.ButtonItemColors.Clear();
            table.MenuButtonItemColors.Clear();
            // Orange
            Office2007ButtonItemColorTable cb = GetButtonItemDefault(factory, metroColors);
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Orange);
            table.ButtonItemColors.Add(cb);
            // Orange with background
            cb = GetButtonItemDefaultWithBackground(factory, metroColors);
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.OrangeWithBackground);
            table.ButtonItemColors.Add(cb);
            // Blue
            cb = GetButtonItemBlue(factory, metroColors);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Blue);
            table.ButtonItemColors.Add(cb);
            // Blue with background
            cb = GetButtonItemBlueWithBackground(factory, metroColors);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.MouseOverSplitInactive = cb.Default;
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.BlueWithBackground);
            table.ButtonItemColors.Add(cb);
            // Magenta
            cb = GetButtonItemMagenta(factory, metroColors);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Magenta);
            table.ButtonItemColors.Add(cb);
            // Blue with background
            cb = GetButtonItemMagentaWithBackground(factory, metroColors);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.MouseOverSplitInactive = cb.Default;
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.MagentaWithBackground);
            table.ButtonItemColors.Add(cb);

            cb = Office2010BlueFactory.GetButtonItemOffice2007WithBackground(factory);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Office2007WithBackground);
            table.ButtonItemColors.Add(cb);

            table.ButtonItemColors.Add(CreateBlueOrbColorTable(factory, metroColors));

            table.BackstageButtonItemColors.Clear();
            table.BackstageButtonItemColors.Add(GetButtonItemBackstageDefault(factory, metroColors));

            table.ContextualTables.Add(Office2007ColorTable.GetContextualKey(typeof(Office2007ButtonItemColorTable), "StatusBar"), GetButtonItemStatusBar(factory, metroColors));
            #endregion

            #region RibbonTabItem Colors Initialization
            table.RibbonTabItemColors.Clear();
            Office2007RibbonTabItemColorTable rt = GetRibbonTabItemBlueDefault(factory, metroColors);
            rt.Name = Enum.GetName(typeof(eRibbonTabColor), eRibbonTabColor.Default);
            table.RibbonTabItemColors.Add(rt);

            // Magenta
            rt = GetRibbonTabItemBlueDefault(factory, metroColors); //GetRibbonTabItemBlueMagenta(factory);
            //rt.CornerSize = 2;
            rt.Name = Enum.GetName(typeof(eRibbonTabColor), eRibbonTabColor.Magenta);
            table.RibbonTabItemColors.Add(rt);

            // Green
            rt = GetRibbonTabItemBlueDefault(factory, metroColors); //GetRibbonTabItemBlueGreen(factory);
            //rt.CornerSize = 2;
            rt.Name = Enum.GetName(typeof(eRibbonTabColor), eRibbonTabColor.Green);
            table.RibbonTabItemColors.Add(rt);

            // Orange
            rt = GetRibbonTabItemBlueDefault(factory, metroColors); //GetRibbonTabItemBlueOrange(factory);
            //rt.CornerSize = 2;
            rt.Name = Enum.GetName(typeof(eRibbonTabColor), eRibbonTabColor.Orange);
            table.RibbonTabItemColors.Add(rt);
            #endregion

            #region RibbonTabItemGroup Colors Initialization
            table.RibbonTabGroupColors.Clear();
            // Default
            Office2007RibbonTabGroupColorTable tg = GetRibbonTabGroupDefault(factory);
            tg.Name = Enum.GetName(typeof(eRibbonTabGroupColor), eRibbonTabGroupColor.Default);
            table.RibbonTabGroupColors.Add(tg);

            // Magenta
            tg = GetRibbonTabGroupMagenta(factory);
            tg.Name = Enum.GetName(typeof(eRibbonTabGroupColor), eRibbonTabGroupColor.Magenta);
            table.RibbonTabGroupColors.Add(tg);

            // Green
            tg = GetRibbonTabGroupGreen(factory);
            tg.Name = Enum.GetName(typeof(eRibbonTabGroupColor), eRibbonTabGroupColor.Green);
            table.RibbonTabGroupColors.Add(tg);

            // Orange
            tg = GetRibbonTabGroupOrange(factory);
            tg.Name = Enum.GetName(typeof(eRibbonTabGroupColor), eRibbonTabGroupColor.Orange);
            table.RibbonTabGroupColors.Add(tg);
            #endregion

            #region Initialize Bar
            table.Bar.ToolbarTopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor)/*, factory.GetColor(metroColors.BaseColorDark)*/);
            table.Bar.ToolbarBottomBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            table.Bar.ToolbarBottomBorder = Color.Empty;
            table.Bar.PopupToolbarBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), Color.Empty);
            table.Bar.PopupToolbarBorder = factory.GetColor(metroColors.BaseColor);
            table.Bar.StatusBarTopBorder = factory.GetColor(metroColors.BaseColor);
            table.Bar.StatusBarTopBorderLight = factory.GetColor(metroColors.BaseColor);
            table.Bar.StatusBarAltBackground.Clear();
            table.Bar.StatusBarAltBackground.Add(new BackgroundColorBlend(factory.GetColor(metroColors.BaseColor), 0f));
            table.Bar.StatusBarAltBackground.Add(new BackgroundColorBlend(factory.GetColor(metroColors.BaseColor), 1f));
            #endregion

            #region Menu
            table.Menu.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), Color.Empty);
            table.Menu.Border = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorDarkShade), Color.Empty);
            table.Menu.Side = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), Color.Empty);
            table.Menu.SideBorder = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), Color.Empty);
            table.Menu.SideBorderLight = LinearGradientColorTable.Empty;
            table.Menu.SideUnused = LinearGradientColorTable.Empty;
            table.Menu.FileBackgroundBlend.Clear();
            table.Menu.FileBackgroundBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
                new DevComponents.DotNetBar.BackgroundColorBlend(factory.GetColor(metroColors.CanvasColorLighterShade), 0F),
                new DevComponents.DotNetBar.BackgroundColorBlend(factory.GetColor(metroColors.CanvasColorLighterShade), 1F)});
            table.Menu.FileContainerBorder = factory.GetColor(metroColors.CanvasColorLightShade);
            table.Menu.FileContainerBorderLight = Color.Transparent;
            table.Menu.FileColumnOneBackground = factory.GetColor(metroColors.CanvasColor);
            table.Menu.FileColumnOneBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.Menu.FileColumnTwoBackground = factory.GetColor(metroColors.CanvasColor);
            table.Menu.FileBottomContainerBackgroundBlend.Clear();
            //table.Menu.FileBottomContainerBackgroundBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            //    new DevComponents.DotNetBar.BackgroundColorBlend(factory.GetColor(0xEBF3FC), 0F),
            //    new DevComponents.DotNetBar.BackgroundColorBlend(factory.GetColor(0xEBF3FC), 1F)});
            #endregion

            #region ComboBox
            table.ComboBox.Default.Background = factory.GetColor(metroColors.CanvasColor);
            table.ComboBox.Default.Border = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.ComboBox.Default.ExpandBackground = new LinearGradientColorTable();
            table.ComboBox.Default.ExpandBorderInner = new LinearGradientColorTable();
            table.ComboBox.Default.ExpandBorderOuter = new LinearGradientColorTable();
            table.ComboBox.Default.ExpandText = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.ComboBox.DefaultStandalone.Background = factory.GetColor(metroColors.CanvasColor);
            table.ComboBox.DefaultStandalone.Border = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.ComboBox.DefaultStandalone.ExpandBackground = LinearGradientColorTable.Empty;
            table.ComboBox.DefaultStandalone.ExpandBorderInner = LinearGradientColorTable.Empty;
            table.ComboBox.DefaultStandalone.ExpandBorderOuter = LinearGradientColorTable.Empty;
            table.ComboBox.DefaultStandalone.ExpandText = factory.GetColor(metroColors.TextColor);
            table.ComboBox.MouseOver.Background = factory.GetColor(metroColors.CanvasColor);
            table.ComboBox.MouseOver.Border = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.ComboBox.MouseOver.ExpandBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientStart), factory.GetColor(metroColors.BaseButtonGradientEnd), 90);
            table.ComboBox.MouseOver.ExpandBorderInner = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDarker));
            table.ComboBox.MouseOver.ExpandBorderOuter = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor), Color.Empty, 90);
            table.ComboBox.MouseOver.ExpandText = factory.GetColor(metroColors.TextColor);
            table.ComboBox.DroppedDown.Background = factory.GetColor(metroColors.CanvasColor);
            table.ComboBox.DroppedDown.Border = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.ComboBox.DroppedDown.ExpandBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight), factory.GetColor(metroColors.BaseColorDark), 90);
            table.ComboBox.DroppedDown.ExpandBorderInner = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDarker));
            table.ComboBox.DroppedDown.ExpandBorderOuter = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor), Color.Empty, 90);
            table.ComboBox.DroppedDown.ExpandText = factory.GetColor(metroColors.TextColor);
            #endregion

            #region Dialog Launcher
            table.DialogLauncher.Default.DialogLauncher = factory.GetColor(metroColors.TextInactiveColor);
            table.DialogLauncher.Default.DialogLauncherShade = Color.Empty; // factory.GetColor(64, 0xFFFFFF);

            table.DialogLauncher.MouseOver.DialogLauncher = factory.GetColor(metroColors.BaseColor);
            table.DialogLauncher.MouseOver.DialogLauncherShade = Color.Empty; // Color.FromArgb(128, factory.GetColor(0xFFFFFF));
            table.DialogLauncher.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));
            table.DialogLauncher.MouseOver.BottomBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));
            table.DialogLauncher.MouseOver.InnerBorder = LinearGradientColorTable.Empty;// new LinearGradientColorTable(factory.GetColor(Color.FromArgb(128, Color.White)));
            table.DialogLauncher.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest), Color.Empty);

            table.DialogLauncher.Pressed.DialogLauncher = factory.GetColor(metroColors.BaseColor);
            table.DialogLauncher.Pressed.DialogLauncherShade = Color.Empty; // Color.FromArgb(48, factory.GetColor(0xFFFFFF));
            table.DialogLauncher.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            table.DialogLauncher.Pressed.BottomBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            table.DialogLauncher.Pressed.InnerBorder = LinearGradientColorTable.Empty;//new LinearGradientColorTable(factory.GetColor(Color.FromArgb(48, Color.White)));
            table.DialogLauncher.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight), Color.Empty);
            #endregion

            #region System Button, Form
            // Default state no background
            table.SystemButton.Default = new Office2007SystemButtonStateColorTable();
            table.SystemButton.Default.Foreground = new LinearGradientColorTable(factory.GetColor(metroColors.TextInactiveColor));
            table.SystemButton.Default.LightShade = Color.Empty;
            table.SystemButton.Default.DarkShade = Color.Empty;

            // Mouse over state
            table.SystemButton.MouseOver = new Office2007SystemButtonStateColorTable();
            table.SystemButton.MouseOver.Foreground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            table.SystemButton.MouseOver.LightShade = Color.Empty;
            table.SystemButton.MouseOver.DarkShade = Color.Empty;
            table.SystemButton.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));
            table.SystemButton.MouseOver.BottomBackground = null;
            table.SystemButton.MouseOver.TopHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xFBFCFF), Color.Transparent);
            table.SystemButton.MouseOver.BottomHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xFBFCFF), Color.Transparent);
            table.SystemButton.MouseOver.OuterBorder = null;
            table.SystemButton.MouseOver.InnerBorder = null;

            // Pressed
            table.SystemButton.Pressed = new Office2007SystemButtonStateColorTable();
            table.SystemButton.Pressed.Foreground = new LinearGradientColorTable(factory.GetColor(metroColors.TextColor));
            table.SystemButton.Pressed.LightShade = Color.Empty;
            table.SystemButton.Pressed.DarkShade = Color.Empty;
            table.SystemButton.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            table.SystemButton.Pressed.TopHighlight = null;
            table.SystemButton.Pressed.BottomBackground = null;
            table.SystemButton.Pressed.BottomHighlight = null;
            table.SystemButton.Pressed.OuterBorder = null;
            table.SystemButton.Pressed.InnerBorder = null;

            // CLOSE Default state no background
            table.SystemButtonClose = new Office2007SystemButtonColorTable();
            table.SystemButtonClose.Default = new Office2007SystemButtonStateColorTable();
            table.SystemButtonClose.Default.Foreground = new LinearGradientColorTable(factory.GetColor(metroColors.TextInactiveColor));
            table.SystemButtonClose.Default.LightShade = Color.Empty;
            table.SystemButtonClose.Default.DarkShade = Color.Empty;

            // Mouse over state
            table.SystemButtonClose.MouseOver = new Office2007SystemButtonStateColorTable();
            table.SystemButtonClose.MouseOver.Foreground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            table.SystemButtonClose.MouseOver.LightShade = Color.Empty;
            table.SystemButtonClose.MouseOver.DarkShade = Color.Empty;
            table.SystemButtonClose.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));
            table.SystemButtonClose.MouseOver.BottomBackground = null;
            table.SystemButtonClose.MouseOver.TopHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xFBFCFF), Color.Transparent);
            table.SystemButtonClose.MouseOver.BottomHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xFBFCFF), Color.Transparent);
            table.SystemButtonClose.MouseOver.OuterBorder = null;
            table.SystemButtonClose.MouseOver.InnerBorder = null;

            // Pressed
            table.SystemButtonClose.Pressed = new Office2007SystemButtonStateColorTable();
            table.SystemButtonClose.Pressed.Foreground = new LinearGradientColorTable(factory.GetColor(metroColors.TextColor));
            table.SystemButtonClose.Pressed.LightShade = Color.Empty;
            table.SystemButtonClose.Pressed.DarkShade = Color.Empty;
            table.SystemButtonClose.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight)); ;
            table.SystemButtonClose.Pressed.TopHighlight = null;
            table.SystemButtonClose.Pressed.BottomBackground = null;
            table.SystemButtonClose.Pressed.BottomHighlight = null;
            table.SystemButtonClose.Pressed.OuterBorder = null;
            table.SystemButtonClose.Pressed.InnerBorder = null;

            // Form border
            table.Form.Active.BorderColors = new Color[] {
                factory.GetColor(metroColors.BaseColor),
                factory.GetColor(metroColors.CanvasColor),
                factory.GetColor(metroColors.CanvasColor),
                factory.GetColor(metroColors.CanvasColor),
                factory.GetColor(metroColors.CanvasColor)
            };

            table.Form.Inactive.BorderColors = new Color[] {
                factory.GetColor(metroColors.BaseColor),
                factory.GetColor(metroColors.CanvasColor),
                factory.GetColor(metroColors.CanvasColor),
                factory.GetColor(metroColors.CanvasColor),
                factory.GetColor(metroColors.CanvasColor)
            };

            // Form Caption Active
            table.Form.Active.CaptionTopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.Form.Active.CaptionBottomBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.Form.Active.CaptionBottomBorder = null;
            table.Form.Active.CaptionText = factory.GetColor(metroColors.TextLightColor);
            table.Form.Active.CaptionTextExtra = factory.GetColor(metroColors.TextInactiveColor);

            // Form Caption Inactive
            table.Form.Inactive.CaptionTopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.Form.Inactive.CaptionBottomBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.Form.Inactive.CaptionText = factory.GetColor(metroColors.CanvasColorLightShade);
            table.Form.Inactive.CaptionTextExtra = factory.GetColor(metroColors.CanvasColorLighterShade);

            table.Form.BackColor = factory.GetColor(metroColors.CanvasColor);
            table.Form.TextColor = factory.GetColor(metroColors.TextColor);
            table.Form.MdiClientBackgroundImage = null; // BarFunctions.LoadBitmap("SystemImages.Office2010SilverClientBackground.png");
            #endregion

            #region Quick Access Toolbar Background
            table.QuickAccessToolbar.Active.TopBackground = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(0xDEE7F4), factory.GetColor(0xE6EEF9));
            table.QuickAccessToolbar.Active.BottomBackground = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(0xDBE7F7), factory.GetColor(0xC9D9EE));
            table.QuickAccessToolbar.Active.OutterBorderColor = Color.Empty; // factory.GetColor(0xF6F9FC);
            table.QuickAccessToolbar.Active.MiddleBorderColor = Color.Empty; // factory.GetColor(0x9AB3D5);
            table.QuickAccessToolbar.Active.InnerBorderColor = Color.Empty; //  factory.GetColor(0xD2E3F9);

            table.QuickAccessToolbar.Inactive.TopBackground = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(0xE6ECF3), factory.GetColor(0xCED8E6));
            table.QuickAccessToolbar.Inactive.BottomBackground = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(0xCED8E6), factory.GetColor(0xC8D3E3));
            table.QuickAccessToolbar.Inactive.OutterBorderColor = Color.Empty; // factory.GetColor(0xF6F9FC);
            table.QuickAccessToolbar.Inactive.MiddleBorderColor = Color.Empty; // factory.GetColor(0x9AB3D5);
            table.QuickAccessToolbar.Inactive.InnerBorderColor = Color.Empty;

            table.QuickAccessToolbar.Standalone.TopBackground = new LinearGradientColorTable();
            table.QuickAccessToolbar.Standalone.BottomBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.QuickAccessToolbar.Standalone.OutterBorderColor = factory.GetColor(metroColors.CanvasColorLightShade);
            table.QuickAccessToolbar.Standalone.MiddleBorderColor = Color.Empty;
            table.QuickAccessToolbar.Standalone.InnerBorderColor = Color.Empty; // factory.GetColor(0xDCE8F7);

            table.QuickAccessToolbar.QatCustomizeMenuLabelBackground = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.QuickAccessToolbar.QatCustomizeMenuLabelText = factory.GetColor(metroColors.CanvasColorDarkShade);

            table.QuickAccessToolbar.Active.GlassBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(132, Color.Black)), Color.FromArgb(80, Color.Black));
            table.QuickAccessToolbar.Inactive.GlassBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(132, Color.Black)), Color.FromArgb(80, Color.Black));
            #endregion

            #region Tab Colors
            table.TabControl.Default = new Office2007TabItemStateColorTable();
            table.TabControl.Default.TopBackground = LinearGradientColorTable.Empty;
            table.TabControl.Default.BottomBackground = LinearGradientColorTable.Empty;
            table.TabControl.Default.InnerBorder = Color.Empty;
            table.TabControl.Default.OuterBorder = Color.Empty;
            table.TabControl.Default.Text = factory.GetColor(metroColors.TextInactiveColor);

            table.TabControl.MouseOver = new Office2007TabItemStateColorTable();
            table.TabControl.MouseOver.TopBackground = LinearGradientColorTable.Empty;
            table.TabControl.MouseOver.BottomBackground = LinearGradientColorTable.Empty;
            table.TabControl.MouseOver.InnerBorder = Color.Empty;
            table.TabControl.MouseOver.OuterBorder = Color.Empty;
            table.TabControl.MouseOver.Text = metroColors.BaseColor;

            table.TabControl.Selected = new Office2007TabItemStateColorTable();
            table.TabControl.Selected.TopBackground = LinearGradientColorTable.Empty;
            table.TabControl.Selected.BottomBackground = LinearGradientColorTable.Empty;
            table.TabControl.Selected.InnerBorder = Color.Empty;
            table.TabControl.Selected.OuterBorder = metroColors.CanvasColorLightShade;
            table.TabControl.Selected.Text = metroColors.BaseColor;

            table.TabControl.TabBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.TabControl.TabPanelBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), Color.Empty);
            table.TabControl.TabPanelBorder = metroColors.CanvasColorLightShade;
            #endregion

            #region CheckBoxItem
            table.CheckBoxItem = GetCheckBoxItem(factory, metroColors);

            Office2007CheckBoxColorTable chk = GetCheckBoxItem(factory, metroColors);
            chk.Default.Text = factory.GetColor(metroColors.BaseTextColor);
            chk.MouseOver.Text = factory.GetColor(metroColors.BaseTextColor);
            chk.Pressed.Text = factory.GetColor(metroColors.BaseTextColor);
            table.ContextualTables.Add(Office2007ColorTable.GetContextualKey(typeof(Office2007CheckBoxColorTable), typeof(Bar)) + "+StatusBar", chk);
            table.ContextualTables.Add(Office2007ColorTable.GetContextualKey(typeof(Office2007CheckBoxColorTable), typeof(MetroStatusBar)), chk);
            #endregion

            #region Scroll Bar Colors
            InitializeScrollBarColorTable(table, factory, metroColors);
            InitializeAppBlueScrollBarColorTable(table, factory, metroColors);
            #endregion

            #region ProgressBarItem
            Office2007ProgressBarColorTable pct = table.ProgressBarItem;
            pct.BackgroundColors = new GradientColorTable(metroColors.CanvasColor);
            pct.OuterBorder = factory.GetColor(metroColors.BaseColorDarker);
            pct.InnerBorder = Color.Empty;
            pct.Chunk = new GradientColorTable(metroColors.BaseColor);
            pct.ChunkOverlay = new GradientColorTable();
            //pct.ChunkOverlay.LinearGradientAngle = 90;
            //pct.ChunkOverlay.Colors.AddRange(new BackgroundColorBlend[] {
            //    new BackgroundColorBlend(Color.FromArgb(192, factory.GetColor(metroColors.BaseColorLight)), 0f),
            //    new BackgroundColorBlend(Color.FromArgb(128, factory.GetColor(metroColors.BaseColor)), .5f),
            //    new BackgroundColorBlend(Color.FromArgb(64, factory.GetColor(metroColors.BaseColor)), .5f),
            //    new BackgroundColorBlend(Color.Transparent, 1f),
            //});
            //pct.ChunkShadow = new GradientColorTable(0xB2B9C8, 0xD5DAE5, 0);
            pct.ChunkShadow = new GradientColorTable();

            // Paused State
            pct = table.ProgressBarItemPaused;
            pct.BackgroundColors = new GradientColorTable(0xEBEDF0, 0xD5D8DC);
            pct.OuterBorder = factory.GetColor(0x868B91);
            pct.InnerBorder = factory.GetColor(0xFFFFFF);
            pct.Chunk = new GradientColorTable(0xAEA700, 0xFFFDCD, 0);
            pct.ChunkOverlay = new GradientColorTable();
            pct.ChunkOverlay.LinearGradientAngle = 90;
            pct.ChunkOverlay.Colors.AddRange(new BackgroundColorBlend[] {
                new BackgroundColorBlend(Color.FromArgb(192, factory.GetColor(0xFFFBA3)), 0f),
                new BackgroundColorBlend(Color.FromArgb(128, factory.GetColor(0xD2CA00)), .5f),
                new BackgroundColorBlend(Color.FromArgb(64, factory.GetColor(0xFEF400)), .5f),
                new BackgroundColorBlend(Color.Transparent, 1f),
            });
            pct.ChunkShadow = new GradientColorTable(0xB2B9C8, 0xD5DAE5, 0);

            // Error State
            pct = table.ProgressBarItemError;
            pct.BackgroundColors = new GradientColorTable(0xEBEDF0, 0xD5D8DC);
            pct.OuterBorder = factory.GetColor(0x868B91);
            pct.InnerBorder = factory.GetColor(0xFFFFFF);
            pct.Chunk = new GradientColorTable(0xD20000, 0xFFCDCD, 0);
            pct.ChunkOverlay = new GradientColorTable();
            pct.ChunkOverlay.LinearGradientAngle = 90;
            pct.ChunkOverlay.Colors.AddRange(new BackgroundColorBlend[] {
                new BackgroundColorBlend(Color.FromArgb(192, factory.GetColor(0xFF8F8F)), 0f),
                new BackgroundColorBlend(Color.FromArgb(128, factory.GetColor(0xD20000)), .5f),
                new BackgroundColorBlend(Color.FromArgb(64, factory.GetColor(0xFE0000)), .5f),
                new BackgroundColorBlend(Color.Transparent, 1f),
            });
            pct.ChunkShadow = new GradientColorTable(0xB2B9C8, 0xD5DAE5, 0);
            #endregion

            #region Gallery
            Office2007GalleryColorTable gallery = table.Gallery;
            gallery.GroupLabelBackground = factory.GetColor(metroColors.CanvasColorLighterShade);
            gallery.GroupLabelText = factory.GetColor(metroColors.CanvasColorDarkShade);
            gallery.GroupLabelBorder = Color.Empty;
            #endregion

            #region Legacy Colors
            table.LegacyColors.BarBackground = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.BarBackground2 = Color.Empty;
            table.LegacyColors.BarStripeColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.LegacyColors.BarCaptionBackground = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.BarCaptionBackground2 = Color.Empty;
            table.LegacyColors.BarCaptionInactiveBackground = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.BarCaptionInactiveBackground2 = Color.Empty;
            table.LegacyColors.BarCaptionInactiveText = factory.GetColor(metroColors.TextDisabledColor);
            table.LegacyColors.BarCaptionText = factory.GetColor(metroColors.TextInactiveColor);
            table.LegacyColors.BarFloatingBorder = factory.GetColor(metroColors.BaseColor);
            table.LegacyColors.BarPopupBackground = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.BarPopupBorder = factory.GetColor(metroColors.BaseColor);
            table.LegacyColors.ItemBackground = Color.Empty;
            table.LegacyColors.ItemBackground2 = Color.Empty;
            table.LegacyColors.ItemCheckedBackground = Color.Empty;
            table.LegacyColors.ItemCheckedBackground2 = Color.Empty;
            table.LegacyColors.ItemCheckedBorder = factory.GetColor(metroColors.BaseColor);
            table.LegacyColors.ItemCheckedText = factory.GetColor(metroColors.TextColor);
            table.LegacyColors.ItemDisabledBackground = Color.Empty;
            table.LegacyColors.ItemDisabledText = factory.GetColor(metroColors.TextDisabledColor);
            table.LegacyColors.ItemExpandedShadow = Color.Empty;
            table.LegacyColors.ItemExpandedBackground = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.ItemExpandedBackground2 = Color.Empty;
            table.LegacyColors.ItemExpandedText = factory.GetColor(metroColors.TextColor);
            table.LegacyColors.ItemExpandedBorder = Color.Empty;
            table.LegacyColors.ItemHotBackground = factory.GetColor(metroColors.CanvasColorLightShade);
            table.LegacyColors.ItemHotBackground2 = Color.Empty;
            table.LegacyColors.ItemHotBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.LegacyColors.ItemHotText = factory.GetColor(metroColors.TextColor);
            table.LegacyColors.ItemPressedBackground = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.LegacyColors.ItemPressedBackground2 = Color.Empty;
            table.LegacyColors.ItemPressedBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.LegacyColors.ItemPressedText = factory.GetColor(metroColors.TextColor);
            table.LegacyColors.ItemSeparator = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.LegacyColors.ItemSeparatorShade = Color.Empty;
            table.LegacyColors.ItemText = factory.GetColor(metroColors.TextInactiveColor); // SystemColors.ControlTet;
            table.LegacyColors.MenuBackground = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.MenuBackground2 = Color.Empty; // Color.White;
            table.LegacyColors.MenuBarBackground = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.MenuBorder = factory.GetColor(metroColors.BaseColor);
            table.LegacyColors.MenuSide = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.MenuSide2 = Color.Empty; // factory.GetColor(0xDDE0E8);
            table.LegacyColors.MenuUnusedBackground = table.LegacyColors.MenuBackground;
            table.LegacyColors.MenuUnusedSide = Color.Empty;
            table.LegacyColors.MenuUnusedSide2 = Color.Empty;// System.Windows.Forms.ControlPaint.Light(table.LegacyColors.MenuSide2);
            table.LegacyColors.ItemDesignTimeBorder = Color.Black;
            table.LegacyColors.BarDockedBorder = Color.Empty;
            table.LegacyColors.DockSiteBackColor = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.DockSiteBackColor2 = Color.Empty;
            table.LegacyColors.CustomizeBackground = Color.Empty;
            table.LegacyColors.CustomizeBackground2 = Color.Empty;
            table.LegacyColors.CustomizeText = factory.GetColor(metroColors.TextInactiveColor);
            table.LegacyColors.PanelBackground = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.LegacyColors.PanelBackground2 = Color.Empty;
            table.LegacyColors.PanelText = factory.GetColor(metroColors.TextColor);
            table.LegacyColors.PanelBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.LegacyColors.ExplorerBarBackground = factory.GetColor(metroColors.CanvasColor);
            table.LegacyColors.ExplorerBarBackground2 = Color.Empty;
            table.LegacyColors.SplitterBackground = table.LegacyColors.PanelBackground;
            table.LegacyColors.SplitterBackground2 = table.LegacyColors.PanelBackground2;
            table.LegacyColors.SplitterText = table.LegacyColors.PanelText;
            table.LegacyColors.SplitterBorder = table.LegacyColors.PanelBorder;
            #endregion

            #region Navigation Pane
            table.NavigationPane.ButtonBackground = new GradientColorTable();
            table.NavigationPane.ButtonBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.CanvasColor), 0));
            //table.NavigationPane.ButtonBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.CanvasColorLightShade), 1));
            #endregion

            #region SuperTooltip
            table.SuperTooltip.BackgroundColors = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade), factory.GetColor(metroColors.CanvasColor));
            table.SuperTooltip.TextColor = factory.GetColor(metroColors.TextColor);
            #endregion

            #region Slider
            Office2007SliderColorTable sl = table.Slider;
            sl.Default.LabelColor = factory.GetColor(metroColors.TextColor);
            sl.Default.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.Default.PartBackground = new GradientColorTable();
            sl.Default.PartBorderColor = Color.Empty;
            sl.Default.PartBorderLightColor = Color.Empty;
            sl.Default.PartForeColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.Default.PartForeLightColor = Color.Empty;
            sl.Default.TrackLineColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.Default.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));

            sl.MouseOver.LabelColor = factory.GetColor(metroColors.TextColor);
            sl.MouseOver.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.MouseOver.PartBackground = new GradientColorTable();
            sl.MouseOver.PartBorderColor = Color.Empty;
            sl.MouseOver.PartBorderLightColor = Color.Empty;
            sl.MouseOver.PartForeColor = factory.GetColor(metroColors.TextColor);
            sl.MouseOver.PartForeLightColor = Color.Empty;
            sl.MouseOver.TrackLineColor = factory.GetColor(metroColors.BaseColorDark);
            sl.MouseOver.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));

            sl.Pressed.LabelColor = factory.GetColor(metroColors.TextColor);
            sl.Pressed.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.Pressed.PartBackground = new GradientColorTable();
            sl.Pressed.PartBorderColor = Color.Empty;
            sl.Pressed.PartBorderLightColor = Color.Empty;
            sl.Pressed.PartForeColor = factory.GetColor(metroColors.TextColor);
            sl.Pressed.PartForeLightColor = Color.Empty;
            sl.Pressed.TrackLineColor = factory.GetColor(metroColors.TextColor);
            sl.Pressed.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));

            ColorBlendFactory df = new ColorBlendFactory(ColorScheme.GetColor(0xCFCFCF));
            sl.Disabled.LabelColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.Disabled.SliderLabelColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.Disabled.PartBackground = new GradientColorTable();
            sl.Disabled.PartBorderColor = df.GetColor(sl.Default.PartBorderColor);
            sl.Disabled.PartBorderLightColor = df.GetColor(sl.Default.PartBorderLightColor);
            sl.Disabled.PartForeColor = df.GetColor(sl.Default.PartForeColor);
            sl.Disabled.PartForeLightColor = df.GetColor(sl.Default.PartForeLightColor);
            sl.Disabled.TrackLineColor = df.GetColor(sl.Default.TrackLineColor);
            sl.Disabled.TrackLineLightColor = df.GetColor(sl.Default.TrackLineLightColor);

            sl.TrackPart = new Office2007SliderPartColorTable();
            sl.TrackPart.Default.LabelColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.TrackPart.Default.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.TrackPart.Default.PartBackground = new GradientColorTable(factory.GetColor(metroColors.TextDisabledColor));
            sl.TrackPart.Default.PartBorderColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.TrackPart.Default.PartBorderLightColor = Color.Empty;
            sl.TrackPart.Default.PartForeColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.TrackPart.Default.PartForeLightColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.TrackPart.Default.TrackLineColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.TrackPart.Default.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));
            sl.TrackPart.MouseOver.LabelColor = factory.GetColor(metroColors.TextColor);
            sl.TrackPart.MouseOver.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.TrackPart.MouseOver.PartBackground = new GradientColorTable(factory.GetColor(metroColors.TextColor));
            sl.TrackPart.MouseOver.PartBorderColor = factory.GetColor(metroColors.TextColor);
            sl.TrackPart.MouseOver.PartBorderLightColor = Color.Empty;
            sl.TrackPart.MouseOver.PartForeColor = factory.GetColor(metroColors.TextColor);
            sl.TrackPart.MouseOver.PartForeLightColor = factory.GetColor(metroColors.TextColor);
            sl.TrackPart.MouseOver.TrackLineColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.TrackPart.MouseOver.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));
            sl.TrackPart.Pressed = sl.TrackPart.MouseOver;

            // Contextual Table when on StatusBar
            sl = new Office2007SliderColorTable();
            sl.Default.LabelColor = factory.GetColor(metroColors.TextColor);
            sl.Default.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.Default.PartBackground = new GradientColorTable();
            sl.Default.PartBorderColor = Color.Empty;
            sl.Default.PartBorderLightColor = Color.Empty;
            sl.Default.PartForeColor = factory.GetColor(metroColors.CanvasColor);
            sl.Default.PartForeLightColor = factory.GetColor(metroColors.BaseColorDark);
            sl.Default.TrackLineColor = factory.GetColor(metroColors.BaseColorDark);
            sl.Default.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));

            sl.MouseOver.LabelColor = factory.GetColor(metroColors.TextColor);
            sl.MouseOver.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.MouseOver.PartBackground = new GradientColorTable();
            sl.MouseOver.PartBorderColor = Color.Empty;
            sl.MouseOver.PartBorderLightColor = Color.Empty;
            sl.MouseOver.PartForeColor = factory.GetColor(metroColors.CanvasColor);
            sl.MouseOver.PartForeLightColor = factory.GetColor(metroColors.BaseColorDark);
            sl.MouseOver.TrackLineColor = factory.GetColor(metroColors.BaseColorDark);
            sl.MouseOver.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));

            sl.Pressed.LabelColor = factory.GetColor(metroColors.TextColor);
            sl.Pressed.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.Pressed.PartBackground = new GradientColorTable();
            sl.Pressed.PartBorderColor = Color.Empty;
            sl.Pressed.PartBorderLightColor = Color.Empty;
            sl.Pressed.PartForeColor = factory.GetColor(metroColors.CanvasColorLighterShade);
            sl.Pressed.PartForeLightColor = factory.GetColor(metroColors.BaseColorDark);
            sl.Pressed.TrackLineColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            sl.Pressed.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));

            df = new ColorBlendFactory(ColorScheme.GetColor(0xCFCFCF));
            sl.Disabled.LabelColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.Disabled.SliderLabelColor = factory.GetColor(metroColors.TextDisabledColor);
            sl.Disabled.PartBackground = new GradientColorTable();
            foreach (BackgroundColorBlend b in sl.Default.PartBackground.Colors)
                sl.Disabled.PartBackground.Colors.Add(new BackgroundColorBlend(df.GetColor(b.Color), b.Position));
            sl.Disabled.PartBorderColor = df.GetColor(sl.Default.PartBorderColor);
            sl.Disabled.PartBorderLightColor = df.GetColor(sl.Default.PartBorderLightColor);
            sl.Disabled.PartForeColor = df.GetColor(sl.Default.PartForeColor);
            sl.Disabled.PartForeLightColor = df.GetColor(sl.Default.PartForeLightColor);
            sl.Disabled.TrackLineColor = df.GetColor(sl.Default.TrackLineColor);
            sl.Disabled.TrackLineLightColor = df.GetColor(sl.Default.TrackLineLightColor);
            
            sl.TrackPart = new Office2007SliderPartColorTable();
            sl.TrackPart.Default.LabelColor = factory.GetColor(metroColors.BaseTextColor);
            sl.TrackPart.Default.SliderLabelColor = factory.GetColor(metroColors.TextColor);
            sl.TrackPart.Default.PartBackground = new GradientColorTable(factory.GetColor(metroColors.CanvasColor));
            sl.TrackPart.Default.PartBorderColor = factory.GetColor(metroColors.BaseColorDarker);
            sl.TrackPart.Default.PartBorderLightColor = Color.Empty;
            sl.TrackPart.Default.PartForeColor = factory.GetColor(metroColors.CanvasColor);
            sl.TrackPart.Default.PartForeLightColor = factory.GetColor(metroColors.CanvasColor);
            sl.TrackPart.Default.TrackLineColor = factory.GetColor(metroColors.BaseColorDark);
            sl.TrackPart.Default.TrackLineLightColor = factory.GetColor(Color.FromArgb(48, Color.White));
            sl.TrackPart.MouseOver = sl.TrackPart.Default;
            sl.TrackPart.Pressed = sl.TrackPart.Default;

            table.ContextualTables.Add(Office2007ColorTable.GetContextualKey(typeof(Office2007SliderColorTable), typeof(Bar)) + "+StatusBar", sl);
            table.ContextualTables.Add(Office2007ColorTable.GetContextualKey(typeof(Office2007SliderColorTable), typeof(MetroStatusBar)), sl);
            #endregion

            #region ListViewEx
            table.ListViewEx.Border = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.ListViewEx.ColumnBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade));
            table.ListViewEx.ColumnSeparator = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.ListViewEx.SelectionBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor), Color.Empty);
            table.ListViewEx.SelectionBorder = Color.Empty;
            table.ListViewEx.SelectionForeColor = factory.GetColor(metroColors.BaseTextColor);
            #endregion

            #region DataGridView
            table.DataGridView.BackgroundColor = factory.GetColor(metroColors.CanvasColor);
            table.DataGridView.DefaultCellBackground = factory.GetColor(metroColors.CanvasColor);
            table.DataGridView.DefaultCellText = factory.GetColor(metroColors.TextColor);
            table.DataGridView.ColumnHeaderNormalBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.DataGridView.ColumnHeaderNormalBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade));
            table.DataGridView.ColumnHeaderNormalText = factory.GetColor(metroColors.TextColor);
            table.DataGridView.ColumnHeaderSelectedBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLighterShade));
            table.DataGridView.ColumnHeaderSelectedText = factory.GetColor(metroColors.TextColor);
            table.DataGridView.ColumnHeaderSelectedBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.DataGridView.ColumnHeaderSelectedMouseOverBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            table.DataGridView.ColumnHeaderSelectedMouseOverBorder = factory.GetColor(metroColors.BaseColorDark);
            table.DataGridView.ColumnHeaderMouseOverBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            table.DataGridView.ColumnHeaderMouseOverBorder = factory.GetColor(metroColors.BaseColorDark);
            table.DataGridView.ColumnHeaderPressedBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            table.DataGridView.ColumnHeaderPressedBorder = factory.GetColor(metroColors.BaseColorDark);

            table.DataGridView.RowNormalBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade));
            table.DataGridView.RowNormalBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.DataGridView.RowSelectedBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLighterShade));
            table.DataGridView.RowSelectedBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.DataGridView.RowSelectedMouseOverBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            table.DataGridView.RowSelectedMouseOverBorder = factory.GetColor(metroColors.BaseColorDark);
            table.DataGridView.RowMouseOverBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            table.DataGridView.RowMouseOverBorder = factory.GetColor(metroColors.BaseColorDark);
            table.DataGridView.RowPressedBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            table.DataGridView.RowPressedBorder = factory.GetColor(metroColors.BaseColorDark);

            table.DataGridView.GridColor = factory.GetColor(metroColors.CanvasColorDarkShade);

            table.DataGridView.SelectorBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade));
            table.DataGridView.SelectorBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.DataGridView.SelectorBorderDark = Color.Empty;// factory.GetColor(0xC3C3C3);
            table.DataGridView.SelectorBorderLight = Color.Empty;// factory.GetColor(0xF9F9F9);
            table.DataGridView.SelectorSign = new LinearGradientColorTable(factory.GetColor(metroColors.TextColor));

            table.DataGridView.SelectorMouseOverBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            table.DataGridView.SelectorMouseOverBorder = factory.GetColor(metroColors.BaseColorDark);
            table.DataGridView.SelectorMouseOverBorderDark = Color.Empty;// factory.GetColor(0xB0CFF7);
            table.DataGridView.SelectorMouseOverBorderLight = Color.Empty; // factory.GetColor(0xD5E4F2);
            table.DataGridView.SelectorMouseOverSign = new LinearGradientColorTable(factory.GetColor(metroColors.TextColor));
            #endregion

            #region SideBar
            table.SideBar.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            table.SideBar.Border = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SideBar.SideBarPanelItemText = factory.GetColor(metroColors.TextColor);
            table.SideBar.SideBarPanelItemDefault = new GradientColorTable();
            table.SideBar.SideBarPanelItemDefault.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.CanvasColor), 0));
            table.SideBar.SideBarPanelItemDefault.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.CanvasColor), 1));
            // Expanded
            table.SideBar.SideBarPanelItemExpanded = new GradientColorTable();
            table.SideBar.SideBarPanelItemExpanded.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.BaseColor), 0));
            table.SideBar.SideBarPanelItemExpanded.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.BaseColor), 1));
            // MouseOver
            table.SideBar.SideBarPanelItemMouseOver = new GradientColorTable();
            table.SideBar.SideBarPanelItemMouseOver.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.BaseColor), 0));
            table.SideBar.SideBarPanelItemMouseOver.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.BaseColorDark), 1));
            // Pressed
            table.SideBar.SideBarPanelItemPressed = new GradientColorTable();
            table.SideBar.SideBarPanelItemPressed.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.BaseColorDark), 0));
            table.SideBar.SideBarPanelItemPressed.Colors.Add(new BackgroundColorBlend(factory.GetColor(metroColors.BaseColorDark), 1));
            #endregion

            #region AdvTree
#if !NOTREE
            table.AdvTree = new DevComponents.AdvTree.Display.TreeColorTable();
            CreateAdvTreeColorTable(table.AdvTree, factory, metroColors);
#endif
            #endregion

            #region CrumbBar
            table.CrumbBarItemView = new CrumbBarItemViewColorTable();
            CrumbBarItemViewStateColorTable crumbBarViewTable = new CrumbBarItemViewStateColorTable();
            table.CrumbBarItemView.Default = crumbBarViewTable;
            crumbBarViewTable.Foreground = factory.GetColor(metroColors.TextColor);
            crumbBarViewTable = new CrumbBarItemViewStateColorTable();
            table.CrumbBarItemView.MouseOver = crumbBarViewTable;
            crumbBarViewTable.Foreground = factory.GetColor(metroColors.BaseTextColor);
            crumbBarViewTable.Background = new BackgroundColorBlendCollection();
            crumbBarViewTable.Background.AddRange(new BackgroundColorBlend[]{
                new BackgroundColorBlend(factory.GetColor(metroColors.BaseColor), 0f),
                new BackgroundColorBlend(factory.GetColor(metroColors.BaseColorDark), 1f)});
            crumbBarViewTable.Border = factory.GetColor(metroColors.BaseColorDark);
            crumbBarViewTable.BorderLight = Color.Empty; // factory.GetColor("90FFFFFF");
            crumbBarViewTable = new CrumbBarItemViewStateColorTable();
            table.CrumbBarItemView.MouseOverInactive = crumbBarViewTable;
            crumbBarViewTable.Foreground = factory.GetColor(metroColors.BaseTextColor);
            crumbBarViewTable.Background = new BackgroundColorBlendCollection();
            crumbBarViewTable.Background.AddRange(new BackgroundColorBlend[]{
                new BackgroundColorBlend(factory.GetColor(metroColors.BaseColor), 0f),
                new BackgroundColorBlend(factory.GetColor(metroColors.BaseColor), 1f)});
            crumbBarViewTable.Border = factory.GetColor(metroColors.BaseColorDark);
            crumbBarViewTable.BorderLight = Color.Empty; // factory.GetColor("90FFFFFF");
            crumbBarViewTable = new CrumbBarItemViewStateColorTable();
            table.CrumbBarItemView.Pressed = crumbBarViewTable;
            crumbBarViewTable.Foreground = factory.GetColor(metroColors.BaseTextColor);
            crumbBarViewTable.Background = new BackgroundColorBlendCollection();
            crumbBarViewTable.Background.AddRange(new BackgroundColorBlend[]{
                new BackgroundColorBlend(factory.GetColor(metroColors.BaseColorDark), 0f),
                new BackgroundColorBlend(factory.GetColor(metroColors.BaseColorDark), 1f)});
            crumbBarViewTable.Border = factory.GetColor(metroColors.BaseColorDark);
            crumbBarViewTable.BorderLight = Color.Empty; // factory.GetColor("408B7654");

            #endregion

            #region WarningBox
            table.WarningBox.BackColor = factory.GetColor(factory.GetColor(metroColors.CanvasColor));
            table.WarningBox.WarningBorderColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.WarningBox.WarningBackColor1 = factory.GetColor(metroColors.CanvasColor);
            table.WarningBox.WarningBackColor2 = factory.GetColor(metroColors.CanvasColor);
            #endregion

            #region CalendarView

            #region WeekDayViewColors

            table.CalendarView.WeekDayViewColors = new ColorDef[]
            {
                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // DayViewBorder
                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // DayHeaderForeground

                new ColorDef(new Color[] {factory.GetColor(metroColors.CanvasColor)},
                new float[] {0f}, 90f),             // DayHeaderBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // DayHeaderBorder

                new ColorDef(factory.GetColor(metroColors.CanvasColor)),           // DayWorkHoursBackground
                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // DayAllDayEventBackground
                new ColorDef(factory.GetColor(metroColors.CanvasColorLighterShade)),           // DayOffWorkHoursBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // DayHourBorder
                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // DayHalfHourBorder

                new ColorDef(factory.GetColor(metroColors.BaseColor)),           // SelectionBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // OwnerTabBorder

                new ColorDef(factory.GetColor(metroColors.CanvasColor)),    // OwnerTabBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // OwnerTabForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColor)),           // OwnerTabContentBackground
                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // OwnerTabSelectedForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColorLighterShade)),           // OwnerTabSelectionBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColor)),           // CondensedViewBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // NowDayViewBorder
                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // NowDayHeaderForeground - 0x15428B

                new ColorDef(factory.GetColor(metroColors.CanvasColorLighterShade)),       // NowDayHeaderBackground
                
                new ColorDef(new Color[] {factory.GetColor(0xFFED79), factory.GetColor(0xFFD86B), factory.GetColor(0xFFBB00), factory.GetColor(0xFFEA77)},
                new float[] {0f, .55f ,58f, 1f}, 90f),              // TimeIndicator

                new ColorDef(factory.GetColor(0xEB8900)),           // TimeIndicatorBorder
            };

            #endregion

            #region HourRulerColors

            table.CalendarView.TimeRulerColors = new ColorDef[]
            {
                new ColorDef(factory.GetColor(metroColors.CanvasColor)),           // TimeRulerBackground
                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // TimeRulerForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // TimeRulerBorder
                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // TimeRulerTickBorder

                new ColorDef(new Color[] {factory.GetColor(0xFFED79), factory.GetColor(0xFFD86B), factory.GetColor(0xFFBB00), factory.GetColor(0xFFEA77)},
                new float[] {0f, .55f ,58f, 1f}, 90f),              // TimeRulerIndicator

                new ColorDef(factory.GetColor(0xEB8900)),           // TimeRulerIndicatorBorder
            };

            #endregion

            #region MonthViewColors

            table.CalendarView.MonthViewColors = new ColorDef[]
            {
              new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // DayOfWeekHeaderBorder

                new ColorDef(factory.GetColor(metroColors.CanvasColor)),                    // DayOfWeekHeaderBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // DayOfWeekHeaderForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // SideBarBorder

                new ColorDef(factory.GetColor(metroColors.CanvasColor)),                   // SideBarBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // SideBarForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // DayHeaderBorder

                new ColorDef(factory.GetColor(metroColors.CanvasColor)),                    // DayHeaderBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // DayHeaderForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // DayContentBorder
                new ColorDef(factory.GetColor(metroColors.BaseColor)),           // DayContentSelectionBackground
                new ColorDef(factory.GetColor(metroColors.CanvasColor)),           // DayContentActiveDayBackground
                new ColorDef(factory.GetColor(metroColors.CanvasColor)),           // DayContentInactiveDayBackground

                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // OwnerTabBorder

                new ColorDef(factory.GetColor(metroColors.CanvasColor)),

                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),           // OwnerTabForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColor)),           // OwnerTabContentBackground
                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // OwnerTabSelectedForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColor)),           // OwnerTabSelectionBackground

                new ColorDef(factory.GetColor(metroColors.BaseColor)),           // NowDayViewBorder
                new ColorDef(factory.GetColor(metroColors.TextColor)),           // NowDayHeaderForeground - 0x15428B

                new ColorDef(factory.GetColor(metroColors.CanvasColorLighterShade)),              // NowDayHeaderBackground

                new ColorDef(factory.GetColor(metroColors.TextColor)),   // ContentLinkForeground - DayHeaderForeground
                new ColorDef(factory.GetColor(metroColors.CanvasColorLightShade)),            // ContentLinkBackground - DayContentActiveDayBackground
            };

            #endregion

            #region AppointmentColors

            table.CalendarView.AppointmentColors = new ColorDef[]
            {
                new ColorDef(factory.GetColor(metroColors.CanvasColorDarkShade)),           // DefaultBorder

                new ColorDef(new Color[] {factory.GetColor(metroColors.BaseButtonGradientStart), factory.GetColor(metroColors.BaseButtonGradientEnd)},
                             new float[] {0f, 1f}, 90f),            // DefaultBackground

                new ColorDef(factory.GetColor(0x28518E)),           // BlueBorder

                new ColorDef(new Color[] {factory.GetColor(0xB1C5EC), factory.GetColor(0x759DDA)}, 
                             new float[] {0f, 1f}, 90f),            // BlueBackground

                new ColorDef(factory.GetColor(0x2C6524)),           // GreenBorder

                new ColorDef(new Color[] {factory.GetColor(0xC2E8BC), factory.GetColor(0x84D17B)},
                             new float[] {0f, 1f}, 90f),            // GreenBackground

                new ColorDef(factory.GetColor(0x8B3E0A)),           // OrangeBorder

                new ColorDef(new Color[] {factory.GetColor(0xF9C7A0), factory.GetColor(0xF49758)},
                             new float[] {0f, 1f}, 90f),            // OrangeBackground

                new ColorDef(factory.GetColor(0x3E2771)),           // PurpleBorder

                new ColorDef(new Color[] {factory.GetColor(0xC5B5E6), factory.GetColor(0x957BD2)},
                             new float[] {0f, 1f}, 90f),            // PurpleBackground

                new ColorDef(factory.GetColor(0x86171C)),           // RedBorder

                new ColorDef(new Color[] {factory.GetColor(0xF1AAAC), factory.GetColor(0xE5676E)},
                             new float[] {0f, 1f}, 90f),            // RedBackground

                new ColorDef(factory.GetColor(0x7C7814)),           // YellowBorder

                new ColorDef(new Color[] {factory.GetColor(0xFFFCAA), factory.GetColor(0xFFF958)},
                             new float[] {0f, 1f}, 90f),            // YellowBackground

                new ColorDef(factory.GetColor(-1)),                 // BusyTimeMarker
                new ColorDef(factory.GetColor(0xFFFFFF)),           // FreeTimeMarker
                new ColorDef(factory.GetColor(0x800080))            // OutOfOfficeTimeMarker
            };

            #endregion

            #endregion

            #region SuperTab

            #region SuperTab

            table.SuperTab.Background = new SuperTabLinearGradientColorTable(
                factory.GetColor(metroColors.CanvasColor), Color.Empty);

            table.SuperTab.InnerBorder = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.SuperTab.OuterBorder = factory.GetColor(metroColors.CanvasColorDarkShade);

            table.SuperTab.ControlBoxDefault.Image = factory.GetColor(metroColors.CanvasColorDarkShade);

            table.SuperTab.ControlBoxMouseOver.Background = factory.GetColor(metroColors.CanvasColorLightShade);
            table.SuperTab.ControlBoxMouseOver.Border = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SuperTab.ControlBoxMouseOver.Image = factory.GetColor(metroColors.TextColor);

            table.SuperTab.ControlBoxPressed.Background = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SuperTab.ControlBoxPressed.Border = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SuperTab.ControlBoxPressed.Image = factory.GetColor(metroColors.TextColor);

            table.SuperTab.InsertMarker = factory.GetColor(metroColors.BaseColor);

            #endregion

            #region SuperTabItem

            // Top Default

            table.SuperTabItem.Default.Normal.Background = new SuperTabLinearGradientColorTable(factory.GetColor(factory.GetColor(metroColors.CanvasColor)));

            table.SuperTabItem.Default.Normal.InnerBorder = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.SuperTabItem.Default.Normal.OuterBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SuperTabItem.Default.Normal.Text = factory.GetColor(metroColors.TextInactiveColor);
            table.SuperTabItem.Default.Normal.CloseMarker = factory.GetColor(metroColors.CanvasColorLightShade);

            // Disabled
            table.SuperTabItem.Default.Disabled.Text = factory.GetColor(metroColors.TextDisabledColor);
            table.SuperTabItem.Default.Disabled.Background.AdaptiveGradient = false;
            table.SuperTabItem.Default.Disabled.CloseMarker = factory.GetColor(metroColors.TextDisabledColor);

            // Top Selected

            table.SuperTabItem.Default.Selected.Background = new SuperTabLinearGradientColorTable(
                new Color[] { factory.GetColor(metroColors.CanvasColorLighterShade), factory.GetColor(metroColors.CanvasColorLightShade), factory.GetColor(metroColors.CanvasColorLightShade), factory.GetColor(metroColors.CanvasColor) },
                new float[] { 0, .5f, .5f, 1 });

            table.SuperTabItem.Default.Selected.InnerBorder = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.SuperTabItem.Default.Selected.OuterBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SuperTabItem.Default.Selected.Text = factory.GetColor(metroColors.TextColor);
            table.SuperTabItem.Default.Selected.CloseMarker = factory.GetColor(metroColors.CanvasColorDarkShade);

            // Top SelectedMouseOver

            table.SuperTabItem.Default.SelectedMouseOver.Background = new SuperTabLinearGradientColorTable(
                new Color[] { factory.GetColor(metroColors.CanvasColor), factory.GetColor(metroColors.CanvasColorLightShade) },
                new float[] { 0, 1 });

            table.SuperTabItem.Default.SelectedMouseOver.InnerBorder = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.SuperTabItem.Default.SelectedMouseOver.OuterBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SuperTabItem.Default.SelectedMouseOver.Text = factory.GetColor(metroColors.TextColor);
            table.SuperTabItem.Default.SelectedMouseOver.CloseMarker = factory.GetColor(metroColors.CanvasColorDarkShade);

            // Top MouseOver

            table.SuperTabItem.Default.MouseOver.Background = new SuperTabLinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), metroColors.CanvasColorLightShade);

            table.SuperTabItem.Default.MouseOver.InnerBorder = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.SuperTabItem.Default.MouseOver.OuterBorder = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SuperTabItem.Default.MouseOver.Text = factory.GetColor(metroColors.TextColor);
            table.SuperTabItem.Default.MouseOver.CloseMarker = factory.GetColor(metroColors.CanvasColorDarkShade);

            // Left, Bottom, Right

            table.SuperTabItem.Left = table.SuperTabItem.Default;
            table.SuperTabItem.Bottom = table.SuperTabItem.Default;
            table.SuperTabItem.Right = table.SuperTabItem.Default;

            #endregion

            #region SuperTabPanel

            table.SuperTabPanel.Default.Background = new SuperTabLinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), Color.Empty);
            table.SuperTabPanel.Default.InnerBorder = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.SuperTabPanel.Default.OuterBorder = factory.GetColor(metroColors.CanvasColorDarkShade);

            table.SuperTabPanel.Left = table.SuperTabPanel.Default;
            table.SuperTabPanel.Bottom = table.SuperTabPanel.Default;
            table.SuperTabPanel.Right = table.SuperTabPanel.Default;

            #endregion

            #endregion

            #region Backstage

            #region Backstage
            SuperTabStyleColorFactory.GetMetroBackstageColorTable(table.Backstage, factory, metroColors);
            #endregion

            #region BackstageItem
            SuperTabStyleColorFactory.GetMetroBackstageItemColorTable(table.BackstageItem, factory, metroColors);
            #endregion

            #region BackstagePanel
            SuperTabStyleColorFactory.GetMetroBackstagePanelColorTable(table.BackstagePanel, factory, metroColors);
            #endregion

            #endregion

            #region SwitchButton
            SwitchButtonColorTable sbt = new SwitchButtonColorTable();
            sbt.BorderColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            sbt.OffBackColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            sbt.OffTextColor = factory.GetColor(metroColors.TextColor);
            sbt.OnBackColor = factory.GetColor(metroColors.BaseColor);
            sbt.OnTextColor = factory.GetColor(metroColors.BaseTextColor);
            sbt.SwitchBackColor = factory.GetColor(metroColors.TextColor);
            sbt.SwitchBorderColor = Color.Empty; // factory.GetColor(metroColors.CanvasColorLightShade);
            sbt.TextColor = factory.GetColor(metroColors.TextColor);
            table.SwitchButton = new SwitchButtonColors();
            table.SwitchButton.Default = sbt;
            table.SwitchButton.Disabled.BorderColor = factory.GetColor(metroColors.CanvasColorLightShade);
            table.SwitchButton.Disabled.SwitchBorderColor = Color.Empty;
            table.SwitchButton.Disabled.OffTextColor = table.CheckBoxItem.Disabled.Text;
            table.SwitchButton.Disabled.OnTextColor = table.SwitchButton.Disabled.OffTextColor;
            table.SwitchButton.Disabled.TextColor = table.SwitchButton.Disabled.OffTextColor;
            table.SwitchButton.Disabled.SwitchBackColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            table.SwitchButton.Disabled.OffBackColor = factory.GetColor(metroColors.CanvasColorLightShade);
            table.SwitchButton.Disabled.OnBackColor = factory.GetColor(metroColors.CanvasColorLightShade);
            #endregion

            #region ElementStyle Classes
            table.StyleClasses.Clear();
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonGalleryContainerKey;
            style.BorderColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            style.Border = eStyleBorderType.Solid;
            style.BorderWidth = 1;
            style.CornerDiameter = 0;
            style.CornerType = eCornerType.Square;
            style.BackColor = factory.GetColor(metroColors.CanvasColor);
            table.StyleClasses.Add(style.Class, style);
            // FileMenuContainer
            style = GetFileMenuContainerStyle(table);
            table.StyleClasses.Add(style.Class, style);
            // Two Column File Menu Container
            style = GetTwoColumnMenuContainerStyle(table);
            table.StyleClasses.Add(style.Class, style);
            // Column one File Menu Container
            style = GetMenuColumnOneContainerStyle(table);
            table.StyleClasses.Add(style.Class, style);
            // Column two File Menu Container
            style = GetMenuColumnTwoContainerStyle(table);
            table.StyleClasses.Add(style.Class, style);
            // Bottom File Menu Container
            style = GetMenuBottomContainer(table);
            table.StyleClasses.Add(style.Class, style);
            // TextBox border
            style = Office2007ColorTableFactory.GetTextBoxStyle(factory.GetColor(metroColors.CanvasColorDarkShade));
            table.StyleClasses.Add(style.Class, style);
            // RichTextBox border
            style = Office2007ColorTableFactory.GetRichTextBoxStyle(factory.GetColor(metroColors.CanvasColorDarkShade));
            table.StyleClasses.Add(style.Class, style);
            // ItemPanel
            style = Office2007ColorTableFactory.GetItemPanelStyle(factory.GetColor(metroColors.CanvasColorDarkShade), factory.GetColor(metroColors.CanvasColor));
            table.StyleClasses.Add(style.Class, style);
            // DateTimeInput background
            style = Office2007ColorTableFactory.GetDateTimeInputBackgroundStyle(factory.GetColor(metroColors.CanvasColorDarkShade), factory.GetColor(metroColors.CanvasColor));
            table.StyleClasses.Add(style.Class, style);
            // Ribbon Client Panel
            style = Office2010BlueFactory.GetRibbonClientPanelStyle(factory, eOffice2010ColorScheme.Silver);
            table.StyleClasses.Add(style.Class, style);
            // ListView Border
            style = Office2007ColorTableFactory.GetListViewBorderStyle(table.ListViewEx);
            table.StyleClasses.Add(style.Class, style);
            style = Office2007ColorTableFactory.GetStatusBarAltStyle(table.Bar);
            table.StyleClasses.Add(style.Class, style);
#if !NOTREE
            // Tree Border/Background
            style = Office2007ColorTableFactory.GetAdvTreeStyle(factory.GetColor(metroColors.CanvasColorDarkShade), factory.GetColor(metroColors.CanvasColor));
            table.StyleClasses.Add(style.Class, style);
            style = Office2007ColorTableFactory.GetAdvTreeColumnsHeaderStyle(factory.GetColor(metroColors.CanvasColorLighterShade), Color.Empty, factory.GetColor(metroColors.CanvasColorDarkShade));
            table.StyleClasses.Add(style.Class, style);
            style = Office2007ColorTableFactory.GetAdvTreeNodesColumnsHeaderStyle(factory.GetColor(metroColors.CanvasColorLighterShade), Color.Empty, factory.GetColor(metroColors.CanvasColorLighterShade));
            table.StyleClasses.Add(style.Class, style);
            style = Office2007ColorTableFactory.GetAdvTreeColumnStyle(factory.GetColor(metroColors.TextColor));
            table.StyleClasses.Add(style.Class, style);
            // CrumbBar
            style = Office2007ColorTableFactory.GetCrumbBarBackgroundStyle(factory.GetColor(metroColors.CanvasColor), factory.GetColor(metroColors.CanvasColorLightShade), factory.GetColor(metroColors.CanvasColorDarkShade));
            table.StyleClasses.Add(style.Class, style);
#endif
            // DataGridView border
            style = Office2007ColorTableFactory.GetDataGridViewStyle();
            table.StyleClasses.Add(style.Class, style);
            // DataGridViewDateTime border
            style = Office2007ColorTableFactory.GetDataGridViewDateTimeStyle();
            table.StyleClasses.Add(style.Class, style);
            // DataGridViewNumeric border
            style = Office2007ColorTableFactory.GetDataGridViewNumericStyle();
            table.StyleClasses.Add(style.Class, style);
            // DataGridViewIpAddress border
            style = Office2007ColorTableFactory.GetDataGridViewIpAddressStyle();
            table.StyleClasses.Add(style.Class, style);

            // Slide-out button
            style = GetSlideOutButtonStyle(metroColors.ComplementColor);
            table.StyleClasses.Add(style.Class, style);

            // MetroTilePanel
            style = Office2007ColorTableFactory.GetMetroTilePanelStyle(factory.GetColor(metroColors.CanvasColor));
            table.StyleClasses.Add(style.Class, style);

            // MetroTileGroup
            style = Office2007ColorTableFactory.GetMetroTileGroupStyle(factory.GetColor(metroColors.TextColor));
            table.StyleClasses.Add(style.Class, style);

            // MonthCalendarAdv
            style = Office2007ColorTableFactory.GetMonthCalendarStyle(factory.GetColor(metroColors.CanvasColor));
            table.StyleClasses.Add(style.Class, style);
            #endregion

            #region Contextual Label Colors
            table.LabelItemColors.Clear();
            table.LabelItemColors.Add(typeof(MetroStatusBar), new LabelColors(factory.GetColor(metroColors.BaseTextColor), factory.GetColor(metroColors.TextDisabledColor)));
            table.LabelItemColors.Add(typeof(Bar), new LabelColors(factory.GetColor(metroColors.BaseTextColor), factory.GetColor(metroColors.TextDisabledColor)));
            #endregion

            InitAppButtonColors(table, factory, metroColors);

            #region StepIndicator
            table.StepIndicator.BackgroundColor = factory.GetColor(metroColors.CanvasColorLighterShade);
            table.StepIndicator.IndicatorColor = factory.GetColor(Color.FromArgb(128, metroColors.ComplementColorLight));
            #endregion

            #region RadialMenu
            table.RadialMenu = new RadialMenuColorTable();
            table.RadialMenu.CircularBackColor = factory.GetColor(metroColors.ComplementColor);
            table.RadialMenu.CircularBorderColor = factory.GetColor(0xFFFFFF);
            table.RadialMenu.CircularForeColor = factory.GetColor(0xFFFFFF);
            table.RadialMenu.RadialMenuBackground = factory.GetColor(metroColors.CanvasColor);
            table.RadialMenu.RadialMenuBorder = factory.GetColor(metroColors.BaseColor);
            table.RadialMenu.RadialMenuButtonBackground = factory.GetColor(metroColors.CanvasColor);
            table.RadialMenu.RadialMenuButtonBorder = factory.GetColor(metroColors.BaseColor);
            table.RadialMenu.RadialMenuExpandForeground = factory.GetColor(metroColors.CanvasColor);
            table.RadialMenu.RadialMenuInactiveBorder = Color.FromArgb(128, table.RadialMenu.RadialMenuBorder);
            table.RadialMenu.RadialMenuItemForeground = factory.GetColor(metroColors.BaseColor);
            table.RadialMenu.RadialMenuItemMouseOverBackground = Color.FromArgb(72, table.RadialMenu.RadialMenuItemForeground);
            table.RadialMenu.RadialMenuItemMouseOverForeground = factory.GetColor(metroColors.BaseColor);
            table.RadialMenu.RadialMenuMouseOverBorder = Color.FromArgb(200, table.RadialMenu.RadialMenuBorder);
            #endregion
        }
        #region Application Button Colors
        private static Office2007ButtonItemColorTable FallBackAppButtonColorTable = null;
        internal static Office2007ButtonItemColorTable GetAppFallBackColorTable()
        {
            if (FallBackAppButtonColorTable != null) return FallBackAppButtonColorTable;

            MetroPartColors metroColors = MetroColorTableInitializer.CreateMetroPartColors(MetroColorGeneratorParameters.Default.CanvasColor, MetroColorGeneratorParameters.Default.BaseColor);
            // Blue default
            Office2007ButtonItemColorTable table = new Office2007ButtonItemColorTable();
            table.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Orange);

            Office2007ButtonItemStateColorTable ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(metroColors.BaseColor);
            ct.BottomBackgroundHighlight = LinearGradientColorTable.Empty; 
            ct.OuterBorder = LinearGradientColorTable.Empty;
            ct.InnerBorder = LinearGradientColorTable.Empty;
            ct.Text = metroColors.BaseTextColor;
            table.Default = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(metroColors.BaseColorLight1);
            ct.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            ct.OuterBorder = LinearGradientColorTable.Empty;
            ct.InnerBorder = LinearGradientColorTable.Empty; 
            ct.Text = metroColors.BaseTextColor;
            table.MouseOver = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(metroColors.BaseColorDark);
            ct.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            ct.OuterBorder = LinearGradientColorTable.Empty;
            ct.InnerBorder = LinearGradientColorTable.Empty;
            ct.Text = metroColors.BaseTextColor;
            table.Pressed = ct;

            table.Expanded = table.Pressed;
            table.Checked = table.Pressed;

            FallBackAppButtonColorTable = table;

            return table;
        }
        internal static void InitAppButtonColors(Office2007ColorTable colorTable, ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTableCollection colors = colorTable.ApplicationButtonColors;
            colors.Clear();

            // Blue default
            Office2007ButtonItemColorTable table = new Office2007ButtonItemColorTable();
            table.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Orange);

            Office2007ButtonItemStateColorTable ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            ct.BottomBackgroundHighlight = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(32, 0xFFFFFF), Color.Transparent);
            ct.OuterBorder = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            ct.InnerBorder = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(16, 0xFFFFFF));
            ct.Text = factory.GetColor(metroColors.BaseTextColor);
            table.Default = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight1));
            ct.BottomBackgroundHighlight = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(82, 0xFFFFFF), Color.Transparent);
            ct.OuterBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            ct.InnerBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(72, 0xFFFFFF), factory.GetColor(48, 0xFFFFFF)); //new LinearGradientColorTable(factory.GetColor(0x55A1F3), factory.GetColor(0x4F9EEE));
            ct.Text = factory.GetColor(metroColors.BaseTextColor);
            table.MouseOver = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            ct.BottomBackgroundHighlight = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(82, 0xFFFFFF), Color.Transparent);
            ct.OuterBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDarker));
            ct.InnerBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(72, 0xFFFFFF), factory.GetColor(48, 0xFFFFFF)); //new LinearGradientColorTable(factory.GetColor(0x55A1F3), factory.GetColor(0x4F9EEE));
            ct.Text = factory.GetColor(metroColors.BaseTextColor);
            table.Pressed = ct;

            table.Expanded = table.Pressed;
            table.Checked = table.Pressed;

            colors.Add(table);

            // Magenta
            table = new Office2007ButtonItemColorTable();
            table.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Magenta);

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0xCC256B), factory.GetColor(0xB10851));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(64, 0xFFFFFF), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8F013C), factory.GetColor(0x940741));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(32, 0xFFFFFF));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.Default = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0xD63272), factory.GetColor(0xB10B52));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(82, 0xFFFFFF), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8F013C), factory.GetColor(0x950942));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(72, 0xFFFFFF), factory.GetColor(48, 0xFFFFFF)); //new LinearGradientColorTable(factory.GetColor(0x55A1F3), factory.GetColor(0x4F9EEE));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.MouseOver = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0xB50C53), factory.GetColor(0xB00B52));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(82, 0xFFFFFF), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8F013D), factory.GetColor(0x950741));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(72, 0xFFFFFF), factory.GetColor(48, 0xFFFFFF)); //new LinearGradientColorTable(factory.GetColor(0x55A1F3), factory.GetColor(0x4F9EEE));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.Pressed = ct;

            table.Expanded = table.Pressed;
            table.Checked = table.Pressed;

            colors.Add(table);

            // Orange
            table = new Office2007ButtonItemColorTable();
            table.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Blue);

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0xF27350), factory.GetColor(0xE5552F));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xF89E42), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xCC2B12), factory.GetColor(0xCF3415));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xF68954), factory.GetColor(0xF78B3E));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.Default = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0xF87E4D), factory.GetColor(0xE6552E));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xFBBD5E), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xCA2810), factory.GetColor(0xCD3217));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xFBAA5A), factory.GetColor(0xFCB857)); //new LinearGradientColorTable(factory.GetColor(0x55A1F3), factory.GetColor(0x4F9EEE));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.MouseOver = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0xE3531D), factory.GetColor(0xE04E19));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xFDAF4C), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xCA2810), factory.GetColor(0xCD3013));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xE86229), factory.GetColor(0xFB8D44)); //new LinearGradientColorTable(factory.GetColor(0x55A1F3), factory.GetColor(0x4F9EEE));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.Pressed = ct;

            table.Expanded = table.Pressed;
            table.Checked = table.Pressed;

            colors.Add(table);

            // Green
            table = new Office2007ButtonItemColorTable();
            table.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.BlueWithBackground);

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0x459731), factory.GetColor(0x2B7F2C));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0x6BCA45), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x186337), factory.GetColor(0x1E6A39));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0x4CA231), factory.GetColor(0x53B331));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.Default = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0x469734), factory.GetColor(0x267C2B));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0x89D668), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x196437), factory.GetColor(0x216B3C));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0x65B943), factory.GetColor(0x70CC4A));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.MouseOver = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0x2F822A), factory.GetColor(0x2A7E2C));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0x68CB38), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x186437), factory.GetColor(0x1D6A38));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0x368F2B), factory.GetColor(0x59BD2D)); //new LinearGradientColorTable(factory.GetColor(0x55A1F3), factory.GetColor(0x4F9EEE));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.Pressed = ct;

            table.Expanded = table.Pressed;
            table.Checked = table.Pressed;

            colors.Add(table);

            // Teal
            table = new Office2007ButtonItemColorTable();
            table.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.MagentaWithBackground);

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0x159795), factory.GetColor(0x018281));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0x20B7B4), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x055E5E), factory.GetColor(0x076464));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0x1BA29D), factory.GetColor(0x1FB1A9));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.Default = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0x1F9C99), factory.GetColor(0x038584));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0x36CDCA), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x055E5E), factory.GetColor(0x096767));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0x30B8B3), factory.GetColor(0x34C2BE));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.MouseOver = ct;

            ct = new Office2007ButtonItemStateColorTable();
            ct.Background = new LinearGradientColorTable(factory.GetColor(0x028482), factory.GetColor(0x028081));
            ct.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0x2CC2BE), Color.Transparent);
            ct.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x055E5E), factory.GetColor(0x086565));
            ct.InnerBorder = new LinearGradientColorTable(factory.GetColor(0x028F8D), factory.GetColor(0x1DB4AD)); //new LinearGradientColorTable(factory.GetColor(0x55A1F3), factory.GetColor(0x4F9EEE));
            ct.Text = factory.GetColor(0xFFFFFF);
            table.Pressed = ct;

            table.Expanded = table.Pressed;
            table.Checked = table.Pressed;

            colors.Add(table);
        }
        #endregion

        #region RibbonBar
        public static Office2007RibbonBarStateColorTable GetRibbonBar(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007RibbonBarStateColorTable rb = new Office2007RibbonBarStateColorTable();
            rb.TopBackgroundHeight = 0.8f;
            rb.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade));
            rb.InnerBorder = LinearGradientColorTable.Empty;// new LinearGradientColorTable(factory.GetColor(factory.GetColor(180, Color.White)), factory.GetColor(120, 0xFFFFFF));
            rb.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            rb.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor(0xF6F7F8), factory.GetColor(0xE5E9EE));
            rb.TitleBackground = null;// new LinearGradientColorTable(factory.GetColor(0xC2D8F1), factory.GetColor(0xC0D8EF));
            rb.TitleText = factory.GetColor(metroColors.TextColor);
            return rb;
        }

        public static Office2007RibbonBarStateColorTable GetRibbonBarMouseOver(ColorFactory factory, MetroPartColors metroColors)
        {
            return GetRibbonBar(factory, metroColors);
            //Office2007RibbonBarStateColorTable rb = new Office2007RibbonBarStateColorTable();
            //rb.TopBackgroundHeight = 0.8f;
            //rb.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xEFF1F2), factory.GetColor(0xCDD2D7));
            //rb.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xFFFFFF), factory.GetColor(120, 0xFFFFFF));
            //rb.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFEFEFE), factory.GetColor(0xDEE4EB));
            //rb.BottomBackground = null;// new LinearGradientColorTable(factory.GetColor(0xC7D8ED), factory.GetColor(0xD8E8F5));
            //rb.TitleBackground = new LinearGradientColorTable(factory.GetColor(0xF8FAFB), Color.Transparent);
            //rb.TitleText = factory.GetColor(0x565F6D);
            //return rb;
        }

        public static Office2007RibbonBarStateColorTable GetRibbonBarExpanded(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007RibbonBarStateColorTable rb = new Office2007RibbonBarStateColorTable();
            rb.TopBackgroundHeight = 15;
            rb.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), factory.GetColor(metroColors.CanvasColorLightShade));
            rb.InnerBorder = new LinearGradientColorTable(factory.GetColor(factory.GetColor(180, Color.White)), factory.GetColor(120, 0xFFFFFF));
            rb.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            rb.BottomBackground = null;// new LinearGradientColorTable(factory.GetColor(0xC7D8ED), factory.GetColor(0xD8E8F5));
            rb.TitleBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLighterShade), Color.Transparent);
            rb.TitleText = Color.Empty;
            return rb;
        }
        #endregion

        #region CheckBoxItem
        private static Office2007CheckBoxColorTable GetCheckBoxItem(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007CheckBoxColorTable chk = new Office2007CheckBoxColorTable();

            chk.Default.CheckBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), Color.Empty);
            chk.Default.CheckBorder = factory.GetColor(metroColors.TextDisabledColor);
            chk.Default.CheckInnerBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            chk.Default.CheckInnerBorder = factory.GetColor(metroColors.CanvasColor);
            chk.Default.CheckSign = new LinearGradientColorTable(factory.GetColor(metroColors.TextLightColor), Color.Empty);
            chk.Default.Text = factory.GetColor(metroColors.TextColor);

            chk.MouseOver.CheckBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest), Color.Empty);
            chk.MouseOver.CheckBorder = factory.GetColor(metroColors.BaseColorLight);
            chk.MouseOver.CheckInnerBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));
            chk.MouseOver.CheckInnerBorder = factory.GetColor(metroColors.BaseColorLightest);
            chk.MouseOver.CheckSign = new LinearGradientColorTable(factory.GetColor(metroColors.TextColor), Color.Empty);
            chk.MouseOver.Text = factory.GetColor(metroColors.TextColor);

            chk.Pressed.CheckBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            chk.Pressed.CheckBorder = factory.GetColor(metroColors.BaseColorLight);
            chk.Pressed.CheckInnerBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            chk.Pressed.CheckInnerBorder = factory.GetColor(metroColors.BaseColorLight);
            chk.Pressed.CheckSign = new LinearGradientColorTable(factory.GetColor(metroColors.TextColor), Color.Empty);
            chk.Pressed.Text = factory.GetColor(metroColors.TextColor);

            chk.Disabled.CheckBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor), Color.Empty);
            chk.Disabled.CheckBorder = factory.GetColor(metroColors.CanvasColorLightShade);
            chk.Disabled.CheckInnerBackground = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            chk.Disabled.CheckInnerBorder = Color.Empty; // factory.GetColor(metroColors.CanvasColorLightShade);
            chk.Disabled.CheckSign = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorDarkShade), Color.Empty);
            chk.Disabled.Text = factory.GetColor(metroColors.CanvasColorDarkShade);

            return chk;
        }
        #endregion

        #region Buttons
        private static Office2007ButtonItemColorTable GetButtonItemBackstageDefault(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Blue);
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.Text = factory.GetColor(metroColors.BaseTextColor);

            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.MouseOver.InnerBorder = LinearGradientColorTable.Empty;
            cb.MouseOver.TopBackground = null;
            cb.MouseOver.TopBackgroundHighlight = null;
            cb.MouseOver.BottomBackground = null;
            cb.MouseOver.BottomBackgroundHighlight = null;
            cb.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.MouseOver.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark), Color.Empty);
            cb.MouseOver.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.MouseOver.Text = factory.GetColor(metroColors.BaseTextColor);

            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x527DE0));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(192, 0xFFFFFF));
            cb.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(32, metroColors.BaseColor));
            cb.Pressed.TopBackgroundHighlight = LinearGradientColorTable.Empty; //new LinearGradientColorTable(Color.FromArgb(192, Color.White), Color.Transparent);
            cb.Pressed.BottomBackground = new LinearGradientColorTable(factory.GetColor(32, metroColors.BaseColor));
            cb.Pressed.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor), Color.Empty);
            cb.Pressed.SplitBorderLight = new LinearGradientColorTable(factory.GetColor(135, 0xFFFFFF), Color.Empty);
            cb.Pressed.Text = factory.GetColor(metroColors.TextColor);

            // Checked
            cb.Checked = cb.Pressed;

            // Expanded button
            cb.Expanded = cb.MouseOver;

            SetBlueExpandColors(cb, factory);

            return cb;
        }
        public static Office2007ButtonItemColorTable GetButtonItemDefault(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.Text = factory.GetColor(metroColors.TextColor);

            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.TopBackground = null;
            cb.MouseOver.TopBackgroundHighlight = null;
            cb.MouseOver.BottomBackground = null;
            cb.MouseOver.BottomBackgroundHighlight = null;
            cb.MouseOver.Background = new LinearGradientColorTable(metroColors.BaseColorLightest);
            cb.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));// LinearGradientColorTable.Empty;
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));
            cb.MouseOver.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.Text = factory.GetColor(metroColors.BaseColorLightText);
            //cb.MouseOver = new Office2007ButtonItemStateColorTable();
            //cb.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientStart));
            //cb.MouseOver.TopBackgroundHighlight = LinearGradientColorTable.Empty;
            //cb.MouseOver.BottomBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientEnd));
            //cb.MouseOver.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            //cb.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDarker));
            //cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            //cb.MouseOver.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            //cb.MouseOver.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            //cb.MouseOver.Text = factory.GetColor(metroColors.TextColor);


            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Pressed.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Pressed.TopBackground = null;
            cb.Pressed.TopBackgroundHighlight = null;
            cb.Pressed.BottomBackground = null;
            cb.Pressed.BottomBackgroundHighlight = null;
            cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Pressed.SplitBorderLight = LinearGradientColorTable.Empty;//(factory.GetColor(metroColors.BaseColorLight), Color.Empty);
            cb.Pressed.Text = factory.GetColor(metroColors.TextColor);
            //cb.Pressed = new Office2007ButtonItemStateColorTable();
            //cb.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            //cb.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDarker));
            //cb.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            //cb.Pressed.TopBackgroundHighlight = LinearGradientColorTable.Empty;
            //cb.Pressed.BottomBackground = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            //cb.Pressed.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(Color.FromArgb(48, Color.White)), Color.Transparent);
            //cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            //cb.Pressed.SplitBorderLight = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight), Color.Empty);
            //cb.Pressed.Text = factory.GetColor(metroColors.BaseTextColor);

            // Checked
            cb.Checked = new Office2007ButtonItemStateColorTable();
            cb.Checked.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLighter));
            cb.Checked.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLighter));
            cb.Checked.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLighter));
            cb.Checked.TopBackground = null; //new LinearGradientColorTable(metroColors.BaseColorLight);
            cb.Checked.TopBackgroundHighlight = null; //LinearGradientColorTable.Empty;
            cb.Checked.BottomBackground = null; // new LinearGradientColorTable(metroColors.BaseColorLight);
            cb.Checked.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor("20FFFFFF"), Color.Transparent);
            cb.Checked.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.Text = factory.GetColor(metroColors.BaseColorLightText);
            //cb.Checked = new Office2007ButtonItemStateColorTable();
            //cb.Checked.OuterBorder = new LinearGradientColorTable(metroColors.BaseColorDark);
            //cb.Checked.InnerBorder = new LinearGradientColorTable(metroColors.BaseColorDarker);
            //cb.Checked.TopBackground = new LinearGradientColorTable(metroColors.BaseColorLight);
            //cb.Checked.TopBackgroundHighlight = LinearGradientColorTable.Empty;
            //cb.Checked.BottomBackground = new LinearGradientColorTable(metroColors.BaseColorLight);
            //cb.Checked.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor("20FFFFFF"), Color.Transparent);
            //cb.Checked.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            //cb.Checked.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            //cb.Checked.Text = factory.GetColor(metroColors.BaseColorLightText);

            // Expanded button
            cb.Expanded = new Office2007ButtonItemStateColorTable();
            cb.Expanded.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Expanded.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Expanded.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Expanded.TopBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Expanded.TopBackgroundHighlight = null; // LinearGradientColorTable.Empty;
            cb.Expanded.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Expanded.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(92, Color.White)), Color.Transparent);
            cb.Expanded.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Expanded.SplitBorderLight = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight), Color.Empty);
            cb.Expanded.Text = factory.GetColor(metroColors.TextColor);

            SetBlueExpandColors(cb, factory);

            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemDefaultWithBackground(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = GetButtonItemDefault(factory, metroColors);

            cb.Default.TopBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientStart));
            cb.Default.TopBackgroundHighlight = null;// LinearGradientColorTable.Empty;
            cb.Default.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientEnd));
            cb.Default.BottomBackgroundHighlight = null; // LinearGradientColorTable.Empty;
            cb.Default.Background = new LinearGradientColorTable(metroColors.CanvasColor);
            cb.Default.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorDarkShade));
            cb.Default.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOverSplitInactive = cb.Default;

            // Same as default
            cb.Disabled = new Office2007ButtonItemStateColorTable();
            cb.Disabled.TopBackground = null;
            cb.Disabled.TopBackgroundHighlight = null; // new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.BottomBackground = null; // LinearGradientColorTable.Empty;
            cb.Disabled.BottomBackgroundHighlight = null; // new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            cb.Disabled.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade), Color.Empty);
            cb.Disabled.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Text = factory.GetColor(metroColors.CanvasColorLightShade);
            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemBlue(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.Text = factory.GetColor(metroColors.TextColor);

            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.TopBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.MouseOver.TopBackgroundHighlight = null;// LinearGradientColorTable.Empty;
            cb.MouseOver.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientEnd));
            cb.MouseOver.BottomBackgroundHighlight = null;// LinearGradientColorTable.Empty;
            cb.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.MouseOver.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.Text = factory.GetColor(metroColors.BaseColorLightText);

            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(metroColors.BaseColorDarker);
            cb.Pressed.InnerBorder = new LinearGradientColorTable(metroColors.BaseColorDark);
            cb.Pressed.TopBackground = null;// new LinearGradientColorTable(factory.GetColor("DAF6FF"));
            cb.Pressed.TopBackgroundHighlight = null; // LinearGradientColorTable.Empty;
            cb.Pressed.BottomBackground = null;// new LinearGradientColorTable(factory.GetColor("A7E8FF"));
            cb.Pressed.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(48, Color.White)), Color.Transparent);
            cb.Pressed.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Pressed.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.Pressed.Text = factory.GetColor(metroColors.TextColor);

            // Checked
            cb.Checked = new Office2007ButtonItemStateColorTable();
            cb.Checked.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Checked.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Checked.TopBackground = null;  // new LinearGradientColorTable(factory.GetColor("A7E8FF"));
            cb.Checked.TopBackgroundHighlight = null; // LinearGradientColorTable.Empty;
            cb.Checked.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor("A7E8FF"));
            cb.Checked.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor("20FFFFFF"), Color.Transparent);
            cb.Checked.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Checked.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.Text = factory.GetColor(metroColors.BaseTextColor);

            // Expanded button
            cb.Expanded = new Office2007ButtonItemStateColorTable();
            cb.Expanded.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Expanded.InnerBorder = LinearGradientColorTable.Empty;
            cb.Expanded.TopBackground = null; // new LinearGradientColorTable(factory.GetColor("DAF6FF"));
            cb.Expanded.TopBackgroundHighlight = null; // LinearGradientColorTable.Empty;
            cb.Expanded.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor("A7E8FF"));
            cb.Expanded.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(48, Color.White)), Color.Transparent);
            cb.Expanded.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Expanded.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Expanded.SplitBorderLight = LinearGradientColorTable.Empty; //(factory.GetColor("A000BFFF"), Color.Empty);
            cb.Expanded.Text = factory.GetColor(metroColors.BaseTextColor);

            SetBlueExpandColors(cb, factory);

            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemBlueWithBackground(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = GetButtonItemBlue(factory, metroColors);

            cb.Default.TopBackground = null; //new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientStart));
            cb.Default.TopBackgroundHighlight = null;// LinearGradientColorTable.Empty;
            cb.Default.BottomBackground = null;// new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientEnd));
            cb.Default.BottomBackgroundHighlight = null;// LinearGradientColorTable.Empty;
            cb.Default.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Default.InnerBorder = LinearGradientColorTable.Empty;
            cb.Default.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Default.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.Text = factory.GetColor(metroColors.BaseTextColor);
            cb.MouseOverSplitInactive = cb.Default;

            // Same as default
            cb.Disabled = new Office2007ButtonItemStateColorTable();
            cb.Disabled.TopBackground = null;
            cb.Disabled.TopBackgroundHighlight = null;
            cb.Disabled.BottomBackground = null;
            cb.Disabled.BottomBackgroundHighlight = null;
            cb.Disabled.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            cb.Disabled.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade), Color.Empty);
            cb.Disabled.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Text = factory.GetColor(metroColors.CanvasColorLightShade);
            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemMagenta(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.Text = factory.GetColor(metroColors.TextColor);

            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.TopBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.MouseOver.TopBackgroundHighlight = null;// LinearGradientColorTable.Empty;
            cb.MouseOver.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseButtonGradientEnd));
            cb.MouseOver.BottomBackgroundHighlight = null;// LinearGradientColorTable.Empty;
            cb.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColorLight));
            cb.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColorDark));
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColor));
            cb.MouseOver.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.Text = factory.GetColor(metroColors.ComplementColorLightText);

            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColor));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(metroColors.ComplementColorDarker);
            cb.Pressed.InnerBorder = new LinearGradientColorTable(metroColors.ComplementColorDark);
            cb.Pressed.TopBackground = null;// new LinearGradientColorTable(factory.GetColor("DAF6FF"));
            cb.Pressed.TopBackgroundHighlight = null; // LinearGradientColorTable.Empty;
            cb.Pressed.BottomBackground = null;// new LinearGradientColorTable(factory.GetColor("A7E8FF"));
            cb.Pressed.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(48, Color.White)), Color.Transparent);
            cb.Pressed.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColor));
            cb.Pressed.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.Pressed.Text = factory.GetColor(metroColors.TextColor);

            // Checked
            cb.Checked = new Office2007ButtonItemStateColorTable();
            cb.Checked.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColorDark));
            cb.Checked.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColorDark));
            cb.Checked.TopBackground = null;  // new LinearGradientColorTable(factory.GetColor("A7E8FF"));
            cb.Checked.TopBackgroundHighlight = null; // LinearGradientColorTable.Empty;
            cb.Checked.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor("A7E8FF"));
            cb.Checked.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor("20FFFFFF"), Color.Transparent);
            cb.Checked.Background = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColor));
            cb.Checked.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.Text = factory.GetColor(metroColors.ComplementColorText);

            // Expanded button
            cb.Expanded = new Office2007ButtonItemStateColorTable();
            cb.Expanded.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColor));
            cb.Expanded.InnerBorder = LinearGradientColorTable.Empty;
            cb.Expanded.TopBackground = null; // new LinearGradientColorTable(factory.GetColor("DAF6FF"));
            cb.Expanded.TopBackgroundHighlight = null; // LinearGradientColorTable.Empty;
            cb.Expanded.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor("A7E8FF"));
            cb.Expanded.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(48, Color.White)), Color.Transparent);
            cb.Expanded.Background = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColor));
            cb.Expanded.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColorDark));
            cb.Expanded.SplitBorderLight = LinearGradientColorTable.Empty; //(factory.GetColor("A000BFFF"), Color.Empty);
            cb.Expanded.Text = factory.GetColor(metroColors.ComplementColorText);

            SetBlueExpandColors(cb, factory);

            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemMagentaWithBackground(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = GetButtonItemMagenta(factory, metroColors);

            cb.Default.TopBackground = null;
            cb.Default.TopBackgroundHighlight = null;
            cb.Default.BottomBackground = null;
            cb.Default.BottomBackgroundHighlight = null;
            cb.Default.Background = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColor));
            cb.Default.InnerBorder = LinearGradientColorTable.Empty;
            cb.Default.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.ComplementColor));
            cb.Default.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.Text = factory.GetColor(metroColors.ComplementColorText);
            cb.MouseOverSplitInactive = cb.Default;

            // Same as default
            cb.Disabled = new Office2007ButtonItemStateColorTable();
            cb.Disabled.TopBackground = null;
            cb.Disabled.TopBackgroundHighlight = null;
            cb.Disabled.BottomBackground = null;
            cb.Disabled.BottomBackgroundHighlight = null;
            cb.Disabled.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            cb.Disabled.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColorLightShade), Color.Empty);
            cb.Disabled.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Text = factory.GetColor(metroColors.CanvasColorLightShade);
            return cb;
        }

        public static void SetBlueExpandColors(Office2007ButtonItemColorTable ct, ColorFactory factory)
        {
            Color cb = factory.GetColor(0x454F5A);
            Color cl = Color.FromArgb(192, Color.White);
            ct.Default.ExpandBackground = cb;
            ct.Default.ExpandLight = cl;

            ct.Checked.ExpandBackground = cb;
            ct.Checked.ExpandLight = cl;

            //ct.Disabled.ExpandBackground = factory.GetColor(0xB7B7B7);
            //ct.Disabled.ExpandLight = factory.GetColor(0xEDEDED);

            ct.Expanded.ExpandBackground = cb;
            ct.Expanded.ExpandLight = cl;

            ct.MouseOver.ExpandBackground = cb;
            ct.MouseOver.ExpandLight = cl;

            ct.Pressed.ExpandBackground = cb;
            ct.Pressed.ExpandLight = cl;
        }

        public static Office2007ButtonItemColorTable CreateBlueOrbColorTable(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.BlueOrb);

            // Default
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.TopBackground = null;
            cb.Default.TopBackgroundHighlight = null;
            cb.Default.BottomBackground = null;
            cb.Default.BottomBackgroundHighlight = null;
            cb.Default.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Default.InnerBorder = LinearGradientColorTable.Empty;
            cb.Default.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseTextColor));
            cb.Default.OuterBorderWidth = 2;
            cb.Default.SplitBorder = LinearGradientColorTable.Empty;
            cb.Default.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.Default.Text = factory.GetColor(metroColors.BaseTextColor);
            cb.MouseOverSplitInactive = cb.Default;

            
            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.TopBackground = null;
            cb.MouseOver.TopBackgroundHighlight = null;
            cb.MouseOver.BottomBackground = null;
            cb.MouseOver.BottomBackgroundHighlight = null;
            cb.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.MouseOver.InnerBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));// LinearGradientColorTable.Empty;
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));
            cb.MouseOver.OuterBorderWidth = 2;
            cb.MouseOver.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.Text = factory.GetColor(metroColors.BaseColorLightest);

            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Pressed.OuterBorderWidth = 2;
            cb.Pressed.InnerBorder = LinearGradientColorTable.Empty; // (factory.GetColor(metroColors.BaseColorLight));
            cb.Pressed.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Pressed.TopBackground = null;
            cb.Pressed.TopBackgroundHighlight = null;
            cb.Pressed.BottomBackground = null;
            cb.Pressed.BottomBackgroundHighlight = null;
            cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Pressed.SplitBorderLight = LinearGradientColorTable.Empty;//(factory.GetColor(metroColors.BaseColorLight), Color.Empty);
            cb.Pressed.Text = factory.GetColor(metroColors.BaseColorLight);

            // Checked
            cb.Checked = new Office2007ButtonItemStateColorTable();
            cb.Checked.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseTextColor));
            cb.Checked.OuterBorderWidth = 2;
            cb.Checked.InnerBorder = LinearGradientColorTable.Empty; // (factory.GetColor(metroColors.BaseTextColor));
            cb.Checked.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Checked.TopBackground = null; //new LinearGradientColorTable(metroColors.BaseColorLight);
            cb.Checked.TopBackgroundHighlight = null; //LinearGradientColorTable.Empty;
            cb.Checked.BottomBackground = null; // new LinearGradientColorTable(metroColors.BaseColorLight);
            cb.Checked.BottomBackgroundHighlight = null; // new LinearGradientColorTable(factory.GetColor("20FFFFFF"), Color.Transparent);
            cb.Checked.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.Text = factory.GetColor(metroColors.BaseTextColor);

            // Expanded button
            cb.Expanded = new Office2007ButtonItemStateColorTable();
            cb.Expanded.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLightest));
            cb.Expanded.OuterBorderWidth = 2;
            cb.Expanded.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Expanded.InnerBorder = LinearGradientColorTable.Empty;
            cb.Expanded.TopBackground = null; 
            cb.Expanded.TopBackgroundHighlight = null; 
            cb.Expanded.BottomBackground = null;
            cb.Expanded.BottomBackgroundHighlight = null;
            cb.Expanded.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight));
            cb.Expanded.SplitBorderLight = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight), Color.Empty);
            cb.Expanded.Text = factory.GetColor(metroColors.BaseColorLightest);

            // Same as default
            cb.Disabled = new Office2007ButtonItemStateColorTable();
            cb.Disabled.TopBackground = null;
            cb.Disabled.TopBackgroundHighlight = null; // new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.BottomBackground = null; // LinearGradientColorTable.Empty;
            cb.Disabled.BottomBackgroundHighlight = null; // new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColor));
            cb.Disabled.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight1), Color.Empty);
            cb.Disabled.OuterBorderWidth = 2;
            cb.Disabled.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Text = factory.GetColor(metroColors.BaseColorLight1);
            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemStatusBar(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.Text = factory.GetColor(metroColors.BaseTextColor);

            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.TopBackground = null;
            cb.MouseOver.TopBackgroundHighlight = null;
            cb.MouseOver.BottomBackground = null;
            cb.MouseOver.BottomBackgroundHighlight = null;
            cb.MouseOver.Background = new LinearGradientColorTable(metroColors.BaseColorLight1);
            cb.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight1));
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight1));
            cb.MouseOver.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOver.Text = factory.GetColor(metroColors.BaseTextColor);

            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorLight1));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Pressed.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Pressed.TopBackground = null;
            cb.Pressed.TopBackgroundHighlight = null;
            cb.Pressed.BottomBackground = null;
            cb.Pressed.BottomBackgroundHighlight = null;
            cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Pressed.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.Pressed.Text = factory.GetColor(metroColors.BaseTextColor);

            // Checked
            cb.Checked = new Office2007ButtonItemStateColorTable();
            cb.Checked.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Checked.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Checked.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Checked.TopBackground = null;
            cb.Checked.TopBackgroundHighlight = null;
            cb.Checked.BottomBackground = null;
            cb.Checked.BottomBackgroundHighlight = null;
            cb.Checked.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.Text = factory.GetColor(metroColors.BaseTextColor);

            // Expanded button
            cb.Expanded = new Office2007ButtonItemStateColorTable();
            cb.Expanded.OuterBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Expanded.Background = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Expanded.InnerBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Expanded.TopBackground = null;
            cb.Expanded.TopBackgroundHighlight = null; 
            cb.Expanded.BottomBackground = null; 
            cb.Expanded.BottomBackgroundHighlight = null;
            cb.Expanded.SplitBorder = new LinearGradientColorTable(factory.GetColor(metroColors.BaseColorDark));
            cb.Expanded.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.Expanded.Text = factory.GetColor(metroColors.BaseTextColor);

            SetBlueExpandColors(cb, factory);

            return cb;
        }
        #endregion

        #region RibbonTabItem
        public static Office2007RibbonTabItemColorTable GetRibbonTabItemBlueDefault(ColorFactory factory, MetroPartColors metroColors)
        {
            Office2007RibbonTabItemColorTable rt = new Office2007RibbonTabItemColorTable();
            rt.Default.Text = factory.GetColor(metroColors.TextLightColor);
            rt.CornerSize = 0;

            // Selected Tab
            rt.Selected = new Office2007RibbonTabItemStateColorTable();
            rt.Selected.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            rt.Selected.BackgroundHighlight = new LinearGradientColorTable();
            rt.Selected.InnerBorder = new LinearGradientColorTable();
            rt.Selected.OuterBorder = new LinearGradientColorTable(metroColors.CanvasColorLightShade);
            rt.Selected.Text = factory.GetColor(metroColors.BaseColor);

            // Selected Tab Mouse Over
            rt.SelectedMouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.SelectedMouseOver.Background = new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            rt.SelectedMouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.SelectedMouseOver.InnerBorder = LinearGradientColorTable.Empty;
            rt.SelectedMouseOver.OuterBorder = new LinearGradientColorTable(metroColors.CanvasColorLightShade);
            rt.SelectedMouseOver.Text = factory.GetColor(metroColors.BaseColor);

            // Tab Mouse Over
            rt.MouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.MouseOver.Background = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(metroColors.CanvasColor));
            rt.MouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.MouseOver.InnerBorder = LinearGradientColorTable.Empty;
            rt.MouseOver.OuterBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(metroColors.CanvasColorLightShade);
            rt.MouseOver.Text = factory.GetColor(metroColors.BaseColor);

            return rt;
        }

        public static Office2007RibbonTabItemColorTable GetRibbonTabItemBlueMagenta(ColorFactory factory)
        {
            Office2007RibbonTabItemColorTable rt = new Office2007RibbonTabItemColorTable();
            rt.Default.Text = factory.GetColor(0x3B3B3B);
            rt.CornerSize = 2;

            // Selected Tab
            rt.Selected = new Office2007RibbonTabItemStateColorTable();
            rt.Selected.Background = new LinearGradientColorTable(factory.GetColor(0xF4DEEE), factory.GetColor(0xFEFEFE));
            rt.Selected.BackgroundHighlight = new LinearGradientColorTable(Color.FromArgb(64, Color.White), Color.Transparent);
            rt.Selected.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xFFFFFF), Color.Transparent);
            rt.Selected.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE088C0), factory.GetColor(0xDF6DAA));
            rt.Selected.Text = factory.GetColor(0x3B3B3B);

            // Selected Tab Mouse Over
            rt.SelectedMouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.SelectedMouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xF4DEEE), factory.GetColor(0xFEFEFE));
            rt.SelectedMouseOver.BackgroundHighlight = new LinearGradientColorTable(Color.FromArgb(64, Color.White), Color.Transparent);
            rt.SelectedMouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xFFFFFF), Color.Transparent);
            rt.SelectedMouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE088C0), factory.GetColor(0xDF6DAA));
            rt.SelectedMouseOver.Text = factory.GetColor(0x3B3B3B);

            // Tab Mouse Over
            rt.MouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xEDB5D8), factory.GetColor(0xFCFBFB));
            rt.MouseOver.BackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xFCFBFB), Color.Transparent);
            rt.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xF9D7EE), factory.GetColor(0xFDEDF8));
            rt.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE33991));
            rt.MouseOver.Text = factory.GetColor(0x3B3B3B);

            return rt;
        }

        public static Office2007RibbonTabItemColorTable GetRibbonTabItemBlueGreen(ColorFactory factory)
        {
            Office2007RibbonTabItemColorTable rt = new Office2007RibbonTabItemColorTable();
            rt.Default.Text = factory.GetColor(0x3B3B3B);
            rt.CornerSize = 2;

            // Selected Tab
            rt.Selected = new Office2007RibbonTabItemStateColorTable();
            rt.Selected.Background = new LinearGradientColorTable(factory.GetColor(0xE4F3E0), factory.GetColor(0xE4F2DF));
            rt.Selected.BackgroundHighlight = new LinearGradientColorTable(Color.FromArgb(64, Color.White), Color.Transparent);
            rt.Selected.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xD5EAD3), factory.GetColor(0xB8DEB1));
            rt.Selected.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB9C1B7), factory.GetColor(0xBFC1BE));
            rt.Selected.Text = factory.GetColor(0x3B3B3B);

            // Selected Tab Mouse Over
            rt.SelectedMouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.SelectedMouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xE4F3E0), factory.GetColor(0xE4F2DF));
            rt.SelectedMouseOver.BackgroundHighlight = new LinearGradientColorTable(Color.FromArgb(32, Color.White), Color.Transparent);
            rt.SelectedMouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xD5EAD3), factory.GetColor(0xB8DEB1));
            rt.SelectedMouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB9C1B7), factory.GetColor(0xBFC1BE));
            rt.SelectedMouseOver.Text = factory.GetColor(0x3B3B3B);

            // Tab Mouse Over
            rt.MouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xB3DEE3), factory.GetColor(0x8ADE9F));
            rt.MouseOver.BackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xDEECFF), Color.Transparent);
            rt.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xE2EFFF), factory.GetColor(0xC7DFFF));
            rt.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xC1C8D1), factory.GetColor(0xC0C7D0));
            rt.MouseOver.Text = factory.GetColor(0x3B3B3B);

            return rt;
        }

        public static Office2007RibbonTabItemColorTable GetRibbonTabItemBlueOrange(ColorFactory factory)
        {
            Office2007RibbonTabItemColorTable rt = new Office2007RibbonTabItemColorTable();
            rt.Default.Text = factory.GetColor(0x3B3B3B);
            rt.CornerSize = 2;

            // Selected Tab
            rt.Selected = new Office2007RibbonTabItemStateColorTable();
            rt.Selected.Background = new LinearGradientColorTable(factory.GetColor(0xFFF59F), factory.GetColor(0xFFFAD5));
            rt.Selected.BackgroundHighlight = new LinearGradientColorTable(Color.FromArgb(64, Color.White), Color.Transparent);
            rt.Selected.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xFFFBD6), factory.GetColor(0xFDF28C));
            rt.Selected.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xCAC7AC), factory.GetColor(0xC2C2C2));
            rt.Selected.Text = factory.GetColor(0x3B3B3B);

            // Selected Tab Mouse Over
            rt.SelectedMouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.SelectedMouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xFFF59F), factory.GetColor(0xFFFAD5));
            rt.SelectedMouseOver.BackgroundHighlight = new LinearGradientColorTable(Color.FromArgb(32, Color.White), Color.Transparent);
            rt.SelectedMouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xFFFBD6), factory.GetColor(0xFDF28C));
            rt.SelectedMouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xCAC7AC), factory.GetColor(0xC2C2C2));
            rt.SelectedMouseOver.Text = factory.GetColor(0x3B3B3B);

            // Tab Mouse Over
            rt.MouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xCFE0E1), factory.GetColor(0xE9E799));
            rt.MouseOver.BackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xCBE2FF), Color.Transparent);
            rt.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xE2EFFF), factory.GetColor(0xC7DFFF));
            rt.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xC1C8D1), factory.GetColor(0xC0C7D0));
            rt.MouseOver.Text = factory.GetColor(0x3B3B3B);

            return rt;
        }
        #endregion

        #region Style Class Creation
        public static ElementStyle GetSlideOutButtonStyle(Color complementColor)
        {
            ElementStyle style = new ElementStyle(ElementStyleClassKeys.SlideOutButtonKey);
            style.BackColor = complementColor;
            return style;
        }

        public static ElementStyle GetFileMenuContainerStyle(Office2007ColorTable table)
        {
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonFileMenuContainerKey;
            Office2007MenuColorTable mc = table.Menu;

            style.PaddingBottom = 3;
            style.PaddingLeft = 0;
            style.PaddingRight = 0;
            style.PaddingTop = 22;
            style.Border = eStyleBorderType.Solid;
            style.CornerType = eCornerType.Rounded;
            style.BorderWidth = 1;
            style.BorderColor = Color.Transparent;
            style.CornerDiameter = 3;
            BackgroundColorBlend[] blend = new BackgroundColorBlend[mc.FileBackgroundBlend.Count];
            mc.FileBackgroundBlend.CopyTo(blend);
            style.BackColorBlend.Clear();
            style.BackColorBlend.AddRange(blend);
            style.BackColorGradientAngle = 90;
            return style;
        }

        public static ElementStyle GetTwoColumnMenuContainerStyle(Office2007ColorTable table)
        {
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonFileMenuTwoColumnContainerKey;
            Office2007MenuColorTable mc = table.Menu;

            style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Double;
            style.BorderBottomWidth = 0;
            style.BorderColor = mc.FileContainerBorder;
            //style.BorderColorLight = mc.FileContainerBorderLight;
            style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.None;
            style.BorderLeftWidth = 0;
            style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.None;
            style.BorderRightWidth = 0;
            style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            style.BorderTopWidth = 1;
            style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            style.BorderBottomWidth = 1;
            style.PaddingBottom = 0;
            style.PaddingLeft = 0;
            style.PaddingRight = 0;
            style.PaddingTop = 0;

            return style;
        }

        public static ElementStyle GetMenuColumnOneContainerStyle(Office2007ColorTable table)
        {
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonFileMenuColumnOneContainerKey;
            Office2007MenuColorTable mc = table.Menu;

            style.BackColor = mc.FileColumnOneBackground;
            style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            style.BorderRightColor = mc.FileColumnOneBorder;
            style.BorderRightWidth = 1;
            style.PaddingRight = 1;

            return style;
        }

        public static ElementStyle GetMenuColumnTwoContainerStyle(Office2007ColorTable table)
        {
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonFileMenuColumnTwoContainerKey;
            Office2007MenuColorTable mc = table.Menu;

            style.BackColor = mc.FileColumnTwoBackground;

            return style;
        }

        public static ElementStyle GetMenuBottomContainer(Office2007ColorTable table)
        {
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonFileMenuBottomContainerKey;
            Office2007MenuColorTable mc = table.Menu;

            //BackgroundColorBlend[] blend = new BackgroundColorBlend[mc.FileBottomContainerBackgroundBlend.Count];
            //mc.FileBottomContainerBackgroundBlend.CopyTo(blend);
            //style.BackColorBlend.Clear();
            //style.BackColorBlend.AddRange(blend);
            //style.BackColorGradientAngle = 90;
            style.MarginTop = 2;
            style.MarginRight = 2;
            return style;
        }
        #endregion

        #region Scroll Bar
        public static void InitializeScrollBarColorTable(Office2007ColorTable t, ColorFactory factory, MetroPartColors metroParts)
        {
            InitializeScrollBarColorTable(t.ScrollBar, factory, metroParts);
        }
        public static void InitializeScrollBarColorTable(Office2007ScrollBarColorTable scrollBarColorTable, ColorFactory factory, MetroPartColors metroParts)
        {
            Office2007ScrollBarStateColorTable sct = scrollBarColorTable.Default;
            sct.Background = new LinearGradientColorTable(factory.GetColor(metroParts.CanvasColor));
            sct.Border = LinearGradientColorTable.Empty;
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(metroParts.TextInactiveColor));
            sct.TrackBackground.Clear();
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = LinearGradientColorTable.Empty;
            sct.TrackSignBackground = new LinearGradientColorTable(factory.GetColor(metroParts.TextInactiveColor));

            // Mouse Over
            sct = scrollBarColorTable.MouseOver;
            sct.Background = scrollBarColorTable.Default.Background;
            sct.Border = scrollBarColorTable.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbInnerBorder = LinearGradientColorTable.Empty;
            sct.ThumbOuterBorder = LinearGradientColorTable.Empty;
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(metroParts.TextColor));
            sct.TrackBackground.Clear();
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = LinearGradientColorTable.Empty;
            sct.TrackSignBackground = new LinearGradientColorTable(factory.GetColor(metroParts.TextColor));

            // Control Mouse Over
            sct = scrollBarColorTable.MouseOverControl;
            sct.Background = scrollBarColorTable.Default.Background;
            sct.Border = scrollBarColorTable.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbInnerBorder = LinearGradientColorTable.Empty;
            sct.ThumbOuterBorder = new LinearGradientColorTable(factory.GetColor(metroParts.TextColor));
            sct.ThumbSignBackground = scrollBarColorTable.Default.ThumbSignBackground;
            sct.TrackBackground.Clear();
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = LinearGradientColorTable.Empty;
            sct.TrackSignBackground = new LinearGradientColorTable(factory.GetColor(metroParts.TextColor));

            // Pressed
            sct = scrollBarColorTable.Pressed;
            sct.Background = scrollBarColorTable.Default.Background;
            sct.Border = scrollBarColorTable.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbInnerBorder = LinearGradientColorTable.Empty;
            sct.ThumbOuterBorder = LinearGradientColorTable.Empty;
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(metroParts.TextColor));
            sct.TrackBackground.Clear();
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = LinearGradientColorTable.Empty;
            sct.TrackSignBackground = new LinearGradientColorTable(factory.GetColor(metroParts.TextColor));
            // Disabled
            sct = scrollBarColorTable.Disabled;
            sct.Background = scrollBarColorTable.Default.Background;
            sct.Border = scrollBarColorTable.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbInnerBorder = new LinearGradientColorTable();
            sct.ThumbOuterBorder = new LinearGradientColorTable();
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(metroParts.TextDisabledColor));
            sct.TrackBackground.Clear();
            sct.TrackInnerBorder = new LinearGradientColorTable();
            sct.TrackOuterBorder = new LinearGradientColorTable();
            sct.TrackSignBackground = new LinearGradientColorTable();
        }
        public static void InitializeAppBlueScrollBarColorTable(Office2007ColorTable t, ColorFactory factory, MetroPartColors metroParts)
        {
            InitializeScrollBarColorTable(t.AppScrollBar, factory, metroParts);
        }
        #endregion

        #region AdvTree
        public static void CreateAdvTreeColorTable(TreeColorTable ct, ColorFactory factory, MetroPartColors metroColors)
        {
            #region Tree Selection
            TreeSelectionColors treeSelection = new TreeSelectionColors();
            ct.Selection = treeSelection;
            // Highlight full row
            SelectionColorTable selColorTable = new SelectionColorTable();
            selColorTable.Fill = new SolidFill(factory.GetColor(metroColors.BaseColor));
            selColorTable.TextColor = factory.GetColor(metroColors.BaseTextColor);
            treeSelection.FullRowSelect = selColorTable;
            //  Highlight full row Inactive
            selColorTable = new SelectionColorTable();
            selColorTable.Fill = new SolidFill(metroColors.CanvasColorLightShade);
            treeSelection.FullRowSelectInactive = selColorTable;

            // Node Marker
            selColorTable = new SelectionColorTable();
            selColorTable.Fill = new SolidFill(factory.GetColor(metroColors.BaseColor));
            selColorTable.TextColor = factory.GetColor(metroColors.BaseTextColor);
            treeSelection.NodeMarker = selColorTable;
            // Node marker inactive
            selColorTable = new SelectionColorTable();
            selColorTable.Fill = new SolidFill(factory.GetColor(metroColors.CanvasColorLightShade));
            treeSelection.NodeMarkerInactive = selColorTable;

            // Cell selection
            selColorTable = new SelectionColorTable();
            selColorTable.Fill = new SolidFill(factory.GetColor(metroColors.BaseColor));
            selColorTable.TextColor = factory.GetColor(metroColors.BaseTextColor);
            treeSelection.HighlightCells = selColorTable;
            // Cell selection inactive
            selColorTable = new SelectionColorTable();
            selColorTable.Fill = new SolidFill(factory.GetColor(metroColors.CanvasColorLightShade));
            treeSelection.HighlightCellsInactive = selColorTable;

            selColorTable = new SelectionColorTable();
            selColorTable.Fill = new SolidFill(factory.GetColor(metroColors.BaseColor));
            selColorTable.TextColor = factory.GetColor(metroColors.BaseTextColor);
            treeSelection.NodeHotTracking = selColorTable;
            #endregion

            #region Expand Buttons
            TreeExpandColorTable expand = new TreeExpandColorTable();
            expand.CollapseBorder = new SolidBorder(factory.GetColor(metroColors.CanvasColorDarkShade), 1);
            expand.CollapseFill = new SolidFill(factory.GetColor(metroColors.CanvasColor));
            expand.CollapseMouseOverBorder = new SolidBorder(factory.GetColor(metroColors.TextColor), 1);
            expand.CollapseMouseOverFill = new SolidFill(factory.GetColor(metroColors.TextColor));
            expand.ExpandBorder = new SolidBorder(factory.GetColor(metroColors.CanvasColorDarkShade), 1);
            expand.ExpandFill = new SolidFill(factory.GetColor(metroColors.CanvasColor));
            expand.ExpandMouseOverBorder = new SolidBorder(factory.GetColor(metroColors.TextColor), 1);
            expand.ExpandMouseOverFill = new SolidFill(factory.GetColor(metroColors.CanvasColor));
            ct.ExpandTriangle = expand;
            // Rectangle
            expand = new TreeExpandColorTable();
            expand.CollapseForeground = new SolidFill(factory.GetColor(metroColors.CanvasColorDarkShade));
            expand.CollapseBorder = new SolidBorder(factory.GetColor(metroColors.TextColor), 1);
            expand.CollapseFill = new GradientFill(new ColorStop[]{
                new ColorStop(factory.GetColor(metroColors.CanvasColor), 0f), new ColorStop(factory.GetColor(metroColors.CanvasColor), .40f), new ColorStop(factory.GetColor(metroColors.CanvasColorLighterShade), 1f)}, 45);
            expand.CollapseMouseOverForeground = expand.CollapseForeground;
            expand.CollapseMouseOverBorder = expand.CollapseBorder;
            expand.CollapseMouseOverFill = expand.CollapseFill;
            expand.ExpandForeground = expand.CollapseForeground;
            expand.ExpandBorder = expand.CollapseBorder;
            expand.ExpandFill = expand.CollapseFill;
            expand.ExpandMouseOverForeground = expand.CollapseForeground;
            expand.ExpandMouseOverBorder = expand.CollapseBorder;
            expand.ExpandMouseOverFill = expand.CollapseFill;
            ct.ExpandRectangle = expand;
            ct.ExpandEllipse = expand;
            #endregion

            #region Misc Tree Color
            ct.GridLines = factory.GetColor(metroColors.CanvasColorLightShade);
            #endregion
        }
        #endregion

        #region RibbonTab Group
        public static Office2007RibbonTabGroupColorTable GetRibbonTabGroupDefault(ColorFactory factory)
        {
            Office2007RibbonTabGroupColorTable tg = new Office2007RibbonTabGroupColorTable();
            tg.Background = new LinearGradientColorTable(factory.GetColor(0xF6F1FC));
            tg.BackgroundHighlight = LinearGradientColorTable.Empty;
            tg.Text = factory.GetColor(0x935ED3);
            tg.Border = new LinearGradientColorTable(factory.GetColor(0x935ED3));

            return tg;
        }

        public static Office2007RibbonTabGroupColorTable GetRibbonTabGroupOrange(ColorFactory factory)
        {
            Office2007RibbonTabGroupColorTable tg = new Office2007RibbonTabGroupColorTable();
            tg.Background = new LinearGradientColorTable(factory.GetColor(0xFFF8ED));
            tg.BackgroundHighlight = LinearGradientColorTable.Empty;
            tg.Text = factory.GetColor(0xCF5C0A);
            tg.Border = new LinearGradientColorTable(factory.GetColor(0xFF9D00));

            return tg;
        }

        public static Office2007RibbonTabGroupColorTable GetRibbonTabGroupMagenta(ColorFactory factory)
        {
            Office2007RibbonTabGroupColorTable tg = new Office2007RibbonTabGroupColorTable();
            tg.Background = new LinearGradientColorTable(factory.GetColor(0xFCF0F7));
            tg.BackgroundHighlight = LinearGradientColorTable.Empty;
            tg.Text = factory.GetColor(0xC9599C);
            tg.Border = new LinearGradientColorTable(factory.GetColor(0xC9599C));

            return tg;
        }

        public static Office2007RibbonTabGroupColorTable GetRibbonTabGroupGreen(ColorFactory factory)
        {
            Office2007RibbonTabGroupColorTable tg = new Office2007RibbonTabGroupColorTable();
            tg.Background = new LinearGradientColorTable(factory.GetColor(0xE6F3E6));
            tg.BackgroundHighlight = LinearGradientColorTable.Empty;
            tg.Text = factory.GetColor(0x49A349);
            tg.Border = new LinearGradientColorTable(factory.GetColor(0x49A349));

            return tg;
        }
        #endregion
    }


}
