using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar.Schedule;

namespace DevComponents.DotNetBar.Rendering
{
    internal static class VisualStudio2010Factory
    {
        public static void InitializeColorTable(Office2007ColorTable table, ColorFactory factory)
        {
            #region RibbonControl Start Images
            table.RibbonControl.StartButtonDefault = BarFunctions.LoadBitmap("SystemImages.BlankOffice2010NormalSilver.png");
            table.RibbonControl.StartButtonMouseOver = BarFunctions.LoadBitmap("SystemImages.BlankOffice2010HotSilver.png");
            table.RibbonControl.StartButtonPressed = BarFunctions.LoadBitmap("SystemImages.BlankOffice2010PressedSilver.png");
            #endregion

            #region RibbonControl
            table.RibbonControl.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8894A8));
            table.RibbonControl.InnerBorder = null; // new LinearGradientColorTable(factory.GetColor(0xC9D2E1));
            table.RibbonControl.TabsBackground = new LinearGradientColorTable(factory.GetColor(0xBCC7D8));
            table.RibbonControl.TabsGlassBackground = new LinearGradientColorTable(Color.Transparent, factory.GetColor(0xBCC7D8));
            table.RibbonControl.TabDividerBorder = Color.Empty; // factory.GetColor(0xA7BAD1);
            table.RibbonControl.TabDividerBorderLight = Color.Empty; // factory.GetColor(0xF4F8FD);
            table.RibbonControl.CornerSize = 1;
            table.RibbonControl.PanelTopBackgroundHeight = 0;
            table.RibbonControl.PanelTopBackground = new LinearGradientColorTable(factory.GetColor(0xCAD3E2), factory.GetColor(0xAEB9CD));
            table.RibbonControl.PanelBottomBackground = null; // new LinearGradientColorTable(factory.GetColor(0xE7F1FA), factory.GetColor(0xD8E4F2));
            #endregion

            #region Item Group
            table.ItemGroup.OuterBorder = LinearGradientColorTable.Empty;
            table.ItemGroup.InnerBorder = LinearGradientColorTable.Empty;
            table.ItemGroup.TopBackground = LinearGradientColorTable.Empty;
            table.ItemGroup.BottomBackground = LinearGradientColorTable.Empty;
            table.ItemGroup.ItemGroupDividerDark = Color.Empty;
            table.ItemGroup.ItemGroupDividerLight = Color.Empty;
            #endregion

            #region RibbonBar
            table.RibbonBar.Default = GetRibbonBar(factory);
            table.RibbonBar.MouseOver = GetRibbonBarMouseOver(factory);
            table.RibbonBar.Expanded = GetRibbonBarExpanded(factory);
            #endregion

            #region ButtonItem Colors Initialization
            table.RibbonButtonItemColors.Clear();
            table.ButtonItemColors.Clear();
            table.MenuButtonItemColors.Clear();
            // Orange
            Office2007ButtonItemColorTable cb = GetButtonItemBlueOrange(factory);
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Orange);
            table.ButtonItemColors.Add(cb);
            // Orange with background
            cb = GetButtonItemBlueOrangeWithBackground(factory);
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.OrangeWithBackground);
            table.ButtonItemColors.Add(cb);
            // Blue
            cb = Office2007ColorTableFactory.GetButtonItemBlueBlue(factory);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Blue);
            table.ButtonItemColors.Add(cb);
            // Blue with background
            cb = GetButtonItemBlueBlueWithBackground(factory);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.MouseOverSplitInactive = cb.Default;
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.BlueWithBackground);
            table.ButtonItemColors.Add(cb);
            // Magenta
            cb = Office2007ColorTableFactory.GetButtonItemBlueMagenta(factory);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Magenta);
            table.ButtonItemColors.Add(cb);
            // Blue with background
            cb = GetButtonItemBlueMagentaWithBackground(factory);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.MouseOverSplitInactive = cb.Default;
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.MagentaWithBackground);
            table.ButtonItemColors.Add(cb);

            cb = GetButtonItemOffice2007WithBackground(factory);
            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Office2007WithBackground);
            table.ButtonItemColors.Add(cb);

            table.ButtonItemColors.Add(ButtonItemStaticColorTables.CreateBlueOrbColorTable(factory));

            table.BackstageButtonItemColors.Add(GetButtonItemBackstageDefault(factory));
            #endregion

            #region NavigationPane ButtonItem Initialization
            table.NavigationPaneButtonItemColors.Clear();
            table.NavigationPaneButtonItemColors.Add(GetNavigationPaneButtonItemTable(factory));
            #endregion

            #region RibbonTabItem Colors Initialization
            table.RibbonTabItemColors.Clear();
            Office2007RibbonTabItemColorTable rt = GetRibbonTabItemBlueDefault(factory);
            rt.Name = Enum.GetName(typeof(eRibbonTabColor), eRibbonTabColor.Default);
            table.RibbonTabItemColors.Add(rt);

            // Magenta
            rt = GetRibbonTabItemBlueMagenta(factory);
            rt.CornerSize = 2;
            rt.Name = Enum.GetName(typeof(eRibbonTabColor), eRibbonTabColor.Magenta);
            table.RibbonTabItemColors.Add(rt);

            // Green
            rt = GetRibbonTabItemBlueGreen(factory);
            rt.CornerSize = 2;
            rt.Name = Enum.GetName(typeof(eRibbonTabColor), eRibbonTabColor.Green);
            table.RibbonTabItemColors.Add(rt);

            // Orange
            rt = GetRibbonTabItemBlueOrange(factory);
            rt.CornerSize = 2;
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
            table.Bar.ToolbarTopBackground = new LinearGradientColorTable(factory.GetColor(0xBCC7D8));
            table.Bar.ToolbarBottomBackground = new LinearGradientColorTable(factory.GetColor(0xBCC7D8));
            table.Bar.ToolbarBottomBorder = factory.GetColor(0x9CAAC1);
            table.Bar.PopupToolbarBackground = new LinearGradientColorTable(factory.GetColor(0xE7EAED), Color.Empty);
            table.Bar.PopupToolbarBorder = factory.GetColor(0x9BA7B7);
            table.Bar.StatusBarTopBorder = factory.GetColor(0x9CAAC1);
            table.Bar.StatusBarTopBorderLight = Color.Empty;
            table.Bar.StatusBarAltBackground.Clear();
            table.Bar.StatusBarAltBackground.Add(new BackgroundColorBlend(factory.GetColor(0x9CAAC1), 0f));
            table.Bar.StatusBarAltBackground.Add(new BackgroundColorBlend(factory.GetColor(0x9CAAC1), 1f));
            #endregion

            #region Menu
            table.Menu.Background = new LinearGradientColorTable(factory.GetColor(0xE9ECEE), factory.GetColor(0xD0D7E2));
            table.Menu.Border = new LinearGradientColorTable(factory.GetColor(0x9BA7B7), Color.Empty);
            table.Menu.Side = new LinearGradientColorTable(factory.GetColor(0xE9ECEE), Color.Empty);
            table.Menu.SideBorder = LinearGradientColorTable.Empty;
            table.Menu.SideBorderLight = LinearGradientColorTable.Empty;
            table.Menu.SideUnused = new LinearGradientColorTable(factory.GetColor(0xBCC7D8), Color.Empty);
            table.Menu.FileBackgroundBlend.Clear();
            table.Menu.FileBackgroundBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
                new DevComponents.DotNetBar.BackgroundColorBlend(factory.GetColor(0xE9ECEE), 0F),
                new DevComponents.DotNetBar.BackgroundColorBlend(factory.GetColor(0xD0D7E2), 1F)});
            table.Menu.FileContainerBorder = factory.GetColor(0x9BA7B7);
            table.Menu.FileContainerBorderLight = Color.Transparent;
            table.Menu.FileColumnOneBackground = factory.GetColor(0xE9ECEE);
            table.Menu.FileColumnOneBorder = factory.GetColor(0xBEC3CB);
            table.Menu.FileColumnTwoBackground = Color.Empty;
            table.Menu.FileBottomContainerBackgroundBlend.Clear();
            //table.Menu.FileBottomContainerBackgroundBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            //    new DevComponents.DotNetBar.BackgroundColorBlend(factory.GetColor(0xEBF3FC), 0F),
            //    new DevComponents.DotNetBar.BackgroundColorBlend(factory.GetColor(0xEBF3FC), 1F)});
            #endregion

            #region ComboBox
            table.ComboBox.Default.Background = factory.GetColor(0xF1F3F8);
            int editBorderColor = 0x8591A2;
            table.ComboBox.Default.Border = factory.GetColor(editBorderColor);
            table.ComboBox.Default.ExpandBackground = new LinearGradientColorTable();
            table.ComboBox.Default.ExpandBorderInner = new LinearGradientColorTable();
            table.ComboBox.Default.ExpandBorderOuter = new LinearGradientColorTable();
            table.ComboBox.Default.ExpandText = factory.GetColor(0x5A5D61);
            table.ComboBox.DefaultStandalone.Background = factory.GetColor(0xFFFFFF);
            table.ComboBox.DefaultStandalone.Border = factory.GetColor(editBorderColor);
            table.ComboBox.DefaultStandalone.ExpandBackground = new LinearGradientColorTable(factory.GetColor(0xCAD3E2), factory.GetColor(0xAEB9CD), 90);
            table.ComboBox.DefaultStandalone.ExpandBorderInner = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(0xFFFFFF));
            table.ComboBox.DefaultStandalone.ExpandBorderOuter = new LinearGradientColorTable(factory.GetColor(0xABBAD0), Color.Empty, 90);
            table.ComboBox.DefaultStandalone.ExpandText = factory.GetColor(0x1B293E);
            table.ComboBox.MouseOver.Background = factory.GetColor(0xFFFFFF);
            table.ComboBox.MouseOver.Border = factory.GetColor(0xE5C365);
            table.ComboBox.MouseOver.ExpandBackground = new LinearGradientColorTable(factory.GetColor(0xFFFCF2), factory.GetColor(0xFFECB5), 90);
            table.ComboBox.MouseOver.ExpandBorderInner = LinearGradientColorTable.Empty;
            table.ComboBox.MouseOver.ExpandBorderOuter = new LinearGradientColorTable(factory.GetColor(0xE5C365), Color.Empty, 90);
            table.ComboBox.MouseOver.ExpandText = factory.GetColor(0x000000);
            table.ComboBox.DroppedDown.Background = factory.GetColor(0xFFFFFF);
            table.ComboBox.DroppedDown.Border = factory.GetColor(0xE5C365);
            table.ComboBox.DroppedDown.ExpandBackground = new LinearGradientColorTable(factory.GetColor(0xFFE8A6), factory.GetColor(0xFFE8A6), 90);
            table.ComboBox.DroppedDown.ExpandBorderInner = LinearGradientColorTable.Empty;
            table.ComboBox.DroppedDown.ExpandBorderOuter = new LinearGradientColorTable(factory.GetColor(0xE5C365));
            table.ComboBox.DroppedDown.ExpandText = factory.GetColor(0x000000);
            #endregion

            #region Dialog Launcher
            table.DialogLauncher.Default.DialogLauncher = factory.GetColor(0x5D6570);
            table.DialogLauncher.Default.DialogLauncherShade = Color.Empty;// factory.GetColor(172, 0xFFFFFF);

            table.DialogLauncher.MouseOver.DialogLauncher = factory.GetColor(0x5D6570);
            table.DialogLauncher.MouseOver.DialogLauncherShade = Color.Empty; // Color.FromArgb(192, factory.GetColor(0xFFFFFF));
            table.DialogLauncher.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFFFBF1), factory.GetColor(0xFFEFC1));
            table.DialogLauncher.MouseOver.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xFFEFC1), factory.GetColor(0xFFECB5));
            table.DialogLauncher.MouseOver.InnerBorder = LinearGradientColorTable.Empty;
            table.DialogLauncher.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE5C365), Color.Empty);

            table.DialogLauncher.Pressed.DialogLauncher = factory.GetColor(0x5D6570);
            table.DialogLauncher.Pressed.DialogLauncherShade = Color.Empty; // Color.FromArgb(128, factory.GetColor(0xFFFFFF));
            table.DialogLauncher.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(0xE4C8A5), factory.GetColor(0xEDC891));
            table.DialogLauncher.Pressed.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xEEBC62), factory.GetColor(0xEEE48B));
            table.DialogLauncher.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xD4BFA8), factory.GetColor(0xEFF1C2));
            table.DialogLauncher.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xC2923B), factory.GetColor(0xC2B462));
            #endregion

            #region System Button, Form
            // Default state no background
            table.SystemButton.Default = new Office2007SystemButtonStateColorTable();
            table.SystemButton.Default.Foreground = new LinearGradientColorTable(factory.GetColor(0xEAF0F6));
            table.SystemButton.Default.LightShade = Color.Empty;
            table.SystemButton.Default.DarkShade = factory.GetColor(0x525565);

            // Mouse over state
            table.SystemButton.MouseOver = new Office2007SystemButtonStateColorTable();
            table.SystemButton.MouseOver.Foreground = new LinearGradientColorTable(factory.GetColor(0xEDF2F9));
            table.SystemButton.MouseOver.LightShade = Color.Empty;
            table.SystemButton.MouseOver.DarkShade = factory.GetColor(0x525565);
            table.SystemButton.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(0xCAD3E2), factory.GetColor(0xBDC7D7));
            table.SystemButton.MouseOver.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xBDC7D7), factory.GetColor(0xAEB9CD));
            table.SystemButton.MouseOver.TopHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xFBFCFF), Color.Transparent);
            table.SystemButton.MouseOver.BottomHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xFBFCFF), Color.Transparent);
            table.SystemButton.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x9BA7B7));
            table.SystemButton.MouseOver.InnerBorder = LinearGradientColorTable.Empty;

            // Pressed
            table.SystemButton.Pressed = new Office2007SystemButtonStateColorTable();
            table.SystemButton.Pressed.Foreground = new LinearGradientColorTable(factory.GetColor(0xE9EFF6));
            table.SystemButton.Pressed.LightShade = Color.Empty;
            table.SystemButton.Pressed.DarkShade = factory.GetColor(0x525565);
            table.SystemButton.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(0xAEB9CD));
            table.SystemButton.Pressed.TopHighlight = null;// new LinearGradientColorTable(factory.GetColor(0xB8CEE9), Color.Transparent);
            table.SystemButton.Pressed.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xAEB9CD));
            table.SystemButton.Pressed.BottomHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xC6EAFD), Color.Transparent);
            table.SystemButton.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8FA5BF));
            table.SystemButton.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xADC1DA));

            // CLOSE Default state no background
            table.SystemButtonClose = new Office2007SystemButtonColorTable();
            table.SystemButtonClose.Default = new Office2007SystemButtonStateColorTable();
            table.SystemButtonClose.Default.Foreground = new LinearGradientColorTable(factory.GetColor(0xEAF0F6));
            table.SystemButtonClose.Default.LightShade = Color.Empty;
            table.SystemButtonClose.Default.DarkShade = factory.GetColor(0x525565);

            // Mouse over state
            table.SystemButtonClose.MouseOver = new Office2007SystemButtonStateColorTable();
            table.SystemButtonClose.MouseOver.Foreground = new LinearGradientColorTable(factory.GetColor(0xFCD7D7));
            table.SystemButtonClose.MouseOver.LightShade = Color.Empty;
            table.SystemButtonClose.MouseOver.DarkShade = factory.GetColor(0x9B3D3D);
            table.SystemButtonClose.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFF8482), factory.GetColor(0xFB7F7E));
            table.SystemButtonClose.MouseOver.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xF67979), factory.GetColor(0xE36162));
            table.SystemButtonClose.MouseOver.TopHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xFBFCFF), Color.Transparent);
            table.SystemButtonClose.MouseOver.BottomHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xFBFCFF), Color.Transparent);
            table.SystemButtonClose.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x9B3D3D));
            table.SystemButtonClose.MouseOver.InnerBorder = LinearGradientColorTable.Empty;

            // Pressed
            table.SystemButtonClose.Pressed = new Office2007SystemButtonStateColorTable();
            table.SystemButtonClose.Pressed.Foreground = new LinearGradientColorTable(factory.GetColor(0xF8D4D5));
            table.SystemButtonClose.Pressed.LightShade = Color.Empty;
            table.SystemButtonClose.Pressed.DarkShade = factory.GetColor(0x9B3D3D);
            table.SystemButtonClose.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(0xF27776));
            table.SystemButtonClose.Pressed.TopHighlight = null;// new LinearGradientColorTable(factory.GetColor(0xB8CEE9), Color.Transparent);
            table.SystemButtonClose.Pressed.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xF27776));
            table.SystemButtonClose.Pressed.BottomHighlight = null; // new LinearGradientColorTable(factory.GetColor(0xC6EAFD), Color.Transparent);
            table.SystemButtonClose.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x9B3D3D));
            table.SystemButtonClose.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xDA6163));

            // Form border
            table.Form.Active.BorderColors = new Color[] {
                factory.GetColor(0x909AA6),
                factory.GetColor(0xD4E6F5),
                factory.GetColor(0xBBCEE6),
                factory.GetColor(0xBBCEE6),
                factory.GetColor(0xBBCEE6)
            };

            table.Form.Inactive.BorderColors = new Color[] {
                factory.GetColor(0xA2ADB9),
                factory.GetColor(0xBBCEE6),
                factory.GetColor(0xBBCEE6),
                factory.GetColor(0xBBCEE6),
                factory.GetColor(0xBBCEE6)
            };

            // Form Caption Active
            table.Form.Active.CaptionTopBackground = new LinearGradientColorTable(factory.GetColor(0xBCC7D8), factory.GetColor(0xBCC7D8));
            table.Form.Active.CaptionBottomBackground = new LinearGradientColorTable(factory.GetColor(0xBCC7D8), factory.GetColor(0xBCC7D8));
            table.Form.Active.CaptionBottomBorder = new Color[] { factory.GetColor(0x9CAAC1), factory.GetColor(0xC9D2E1) };
            table.Form.Active.CaptionText = factory.GetColor(0x1B293E);
            table.Form.Active.CaptionTextExtra = factory.GetColor(0x1B293E);

            // Form Caption Inactive
            table.Form.Inactive.CaptionTopBackground = new LinearGradientColorTable(factory.GetColor(0xC9D2E1));
            table.Form.Inactive.CaptionBottomBackground = new LinearGradientColorTable(factory.GetColor(0xC9D2E1));
            table.Form.Inactive.CaptionText = factory.GetColor(0x56465A);
            table.Form.Inactive.CaptionTextExtra = factory.GetColor(0x56465A);

            table.Form.BackColor = factory.GetColor(0xD0D7E2);
            table.Form.TextColor = factory.GetColor(0x1B293E);
            table.Form.MdiClientBackgroundImage = BarFunctions.LoadBitmap("SystemImages.VS2010ClientBackground.png");
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
            table.QuickAccessToolbar.Standalone.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xBCC7D8));
            table.QuickAccessToolbar.Standalone.OutterBorderColor = factory.GetColor(0x9CAAC1);
            table.QuickAccessToolbar.Standalone.MiddleBorderColor = Color.Empty; //factory.GetColor(0xC9D2E1);
            table.QuickAccessToolbar.Standalone.InnerBorderColor = Color.Empty; // factory.GetColor(0xDCE8F7);

            table.QuickAccessToolbar.QatCustomizeMenuLabelBackground = factory.GetColor(0xCAD3E2);
            table.QuickAccessToolbar.QatCustomizeMenuLabelText = factory.GetColor(0x1B293E);

            table.QuickAccessToolbar.Active.GlassBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(132, Color.Black)), Color.FromArgb(80, Color.Black));
            table.QuickAccessToolbar.Inactive.GlassBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(Color.FromArgb(132, Color.Black)), Color.FromArgb(80, Color.Black));
            #endregion

            #region Tab Colors
            table.TabControl.Default = new Office2007TabItemStateColorTable();
            table.TabControl.Default.TopBackground = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(0xC8DCF4), factory.GetColor(0xAFD2FE));
            table.TabControl.Default.BottomBackground = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(0x96BFF3), factory.GetColor(0xB5CCE9));
            table.TabControl.Default.InnerBorder = Color.Empty; // factory.GetColor(0xE3EFFF);
            table.TabControl.Default.OuterBorder = Color.Empty; // factory.GetColor(0x8591A2);
            table.TabControl.Default.Text = factory.GetColor(0xFFFFFF);

            table.TabControl.MouseOver = new Office2007TabItemStateColorTable();
            table.TabControl.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(0x6F7776), factory.GetColor(0x616C75));
            table.TabControl.MouseOver.BottomBackground = new LinearGradientColorTable(factory.GetColor(0x5D6975), factory.GetColor(0x4F5F74));
            table.TabControl.MouseOver.InnerBorder = Color.Empty; // factory.GetColor(0xFFFFFF);
            table.TabControl.MouseOver.OuterBorder = factory.GetColor(0x9BA7B7);
            table.TabControl.MouseOver.Text = factory.GetColor(0xFFFFFF);

            table.TabControl.Selected = new Office2007TabItemStateColorTable();
            table.TabControl.Selected.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFFFCF2), factory.GetColor(0xFFF3CF));
            table.TabControl.Selected.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xFFE8A6), factory.GetColor(0xFFE8A6));
            table.TabControl.Selected.InnerBorder = Color.Empty;
            table.TabControl.Selected.OuterBorder = Color.Empty; // factory.GetColor(0x293955);
            table.TabControl.Selected.Text = factory.GetColor(0x000000);

            table.TabControl.TabBackground = LinearGradientColorTable.Empty;// new LinearGradientColorTable(factory.GetColor(0x2B3C59), Color.Empty);
            table.TabControl.TabBackgroundImage = BarFunctions.LoadBitmap("SystemImages.VS2010ClientBackground.png");
            table.TabControl.TabPanelBackground = new LinearGradientColorTable(factory.GetColor(0xFFFFFF), Color.Empty);
            table.TabControl.TabPanelBorder = factory.GetColor(editBorderColor);
            #endregion

            #region CheckBoxItem
            Office2007CheckBoxColorTable chk = table.CheckBoxItem;
            chk.Default.CheckBackground = new LinearGradientColorTable(factory.GetColor(0xF4F4F4), Color.Empty);
            chk.Default.CheckBorder = factory.GetColor(0xABC1DE);
            chk.Default.CheckInnerBackground = new LinearGradientColorTable(factory.GetColor(0xE8E9EB), factory.GetColor(0xF2F2F3));
            chk.Default.CheckInnerBorder = factory.GetColor(0xBEC4CC);
            chk.Default.CheckSign = new LinearGradientColorTable(factory.GetColor(0x4A6B96), Color.Empty);
            chk.Default.Text = factory.GetColor(0x1B293E);

            chk.MouseOver.CheckBackground = new LinearGradientColorTable(factory.GetColor(0xFCF1C2), Color.Empty);
            chk.MouseOver.CheckBorder = factory.GetColor(0xCF9037);
            chk.MouseOver.CheckInnerBackground = new LinearGradientColorTable(factory.GetColor(0xFAECC8), factory.GetColor(0xFDF6E4));
            chk.MouseOver.CheckInnerBorder = factory.GetColor(0xFCAD5D);
            chk.MouseOver.CheckSign = new LinearGradientColorTable(factory.GetColor(0x4A6B96), Color.Empty);
            chk.MouseOver.Text = factory.GetColor(0x1B293E);

            chk.Pressed.CheckBackground = new LinearGradientColorTable(factory.GetColor(0xFCE7BD));
            chk.Pressed.CheckBorder = factory.GetColor(0xC26D1D);
            chk.Pressed.CheckInnerBackground = new LinearGradientColorTable(factory.GetColor(0xFAE0A2), factory.GetColor(0xFDF2D7));
            chk.Pressed.CheckInnerBorder = factory.GetColor(0xFCA558);
            chk.Pressed.CheckSign = new LinearGradientColorTable(factory.GetColor(0x4A6B96), Color.Empty);
            chk.Pressed.Text = factory.GetColor(0x1B293E);

            chk.Disabled.CheckBackground = new LinearGradientColorTable(factory.GetColor(0xFFFFFF), Color.Empty);
            chk.Disabled.CheckBorder = factory.GetColor(0xB6B8BA);
            chk.Disabled.CheckInnerBackground = new LinearGradientColorTable(factory.GetColor(0xF8F8F9));
            chk.Disabled.CheckInnerBorder = factory.GetColor(0xDFE0E0);
            chk.Disabled.CheckSign = new LinearGradientColorTable(factory.GetColor(0xB1B3B4), Color.Empty);
            chk.Disabled.Text = factory.GetColor(0x87929F);
            #endregion

            #region Scroll Bar Colors
            InitializeScrollBarColorTable(table, factory);
            InitializeAppBlueScrollBarColorTable(table, factory);
            #endregion

            #region ProgressBarItem
            Office2007ProgressBarColorTable pct = table.ProgressBarItem;
            pct.BackgroundColors = new GradientColorTable(0xEBEDF0, 0xD5D8DC);
            pct.OuterBorder = factory.GetColor(0x6E97CC);
            pct.InnerBorder = factory.GetColor(0xFFFFFF);
            pct.Chunk = new GradientColorTable(0x69922A, 0xE7F2D4, 0);
            pct.ChunkOverlay = new GradientColorTable();
            pct.ChunkOverlay.LinearGradientAngle = 90;
            pct.ChunkOverlay.Colors.AddRange(new BackgroundColorBlend[] {
                new BackgroundColorBlend(Color.FromArgb(192, factory.GetColor(0xEEFFD7)), 0f),
                new BackgroundColorBlend(Color.FromArgb(128, factory.GetColor(0x8DB254)), .5f),
                new BackgroundColorBlend(Color.FromArgb(64, factory.GetColor(0x69922B)), .5f),
                new BackgroundColorBlend(Color.Transparent, 1f),
            });
            pct.ChunkShadow = new GradientColorTable(0xB2B9C8, 0xD5DAE5, 0);

            // Paused State
            pct = table.ProgressBarItemPaused;
            pct.BackgroundColors = new GradientColorTable(0xEBEDF0, 0xD5D8DC);
            pct.OuterBorder = factory.GetColor(0x6E97CC);
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
            pct.OuterBorder = factory.GetColor(0x6E97CC);
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
            gallery.GroupLabelBackground = factory.GetColor(0xF0F2F5);
            gallery.GroupLabelText = factory.GetColor(0x1B293E);
            gallery.GroupLabelBorder = factory.GetColor(0xF0F2F5);
            #endregion

            #region Legacy Colors
            table.LegacyColors.BarBackground = factory.GetColor(0xBCC7D8);
            table.LegacyColors.BarBackground2 = Color.Empty; // factory.GetColor(0xB3C4D8);
            table.LegacyColors.BarStripeColor = factory.GetColor(0x60728C);
            table.LegacyColors.BarCaptionBackground = factory.GetColor(0xFFFCF2);
            table.LegacyColors.BarCaptionBackground2 = factory.GetColor(0xFFE8A6);
            table.LegacyColors.BarCaptionText = factory.GetColor(0x000000);
            table.LegacyColors.BarCaptionInactiveBackground = factory.GetColor(0x4D6082);
            table.LegacyColors.BarCaptionInactiveBackground2 = factory.GetColor(0x3D5277);
            table.LegacyColors.BarCaptionInactiveText = factory.GetColor(0xFFFFFF);
            table.LegacyColors.BarFloatingBorder = factory.GetColor(0x2B3C59);
            table.LegacyColors.BarPopupBackground = factory.GetColor(0xFFFFFF);
            table.LegacyColors.BarPopupBorder = factory.GetColor(0x9BA7B7);
            table.LegacyColors.ItemBackground = Color.Empty;
            table.LegacyColors.ItemBackground2 = Color.Empty;
            table.LegacyColors.ItemCheckedBackground = factory.GetColor(0xFFEFBB);
            table.LegacyColors.ItemCheckedBackground2 = factory.GetColor(0xFFEFBB);
            table.LegacyColors.ItemCheckedBorder = factory.GetColor(0xE5C365);
            table.LegacyColors.ItemCheckedText = factory.GetColor(0x1B293E);
            table.LegacyColors.ItemDisabledBackground = Color.Empty;
            table.LegacyColors.ItemDisabledText = factory.GetColor(0x8D8D8D);
            table.LegacyColors.ItemExpandedShadow = Color.Empty;
            table.LegacyColors.ItemExpandedBackground = factory.GetColor(0xE9ECEE); //factory.GetColor(0xFFE8A6);
            table.LegacyColors.ItemExpandedBackground2 = Color.Empty;// factory.GetColor(0xFFE8A6);
            table.LegacyColors.ItemExpandedText = factory.GetColor(0x1B293E);
            table.LegacyColors.ItemExpandedBorder = factory.GetColor(0x1B293E);
            table.LegacyColors.ItemHotBackground = factory.GetColor(0xFFFCF2);
            table.LegacyColors.ItemHotBackground2 = factory.GetColor(0xFFECB5);
            table.LegacyColors.ItemHotBorder = factory.GetColor(0x9BA7B7);
            table.LegacyColors.ItemHotText = factory.GetColor(0x1B293E);
            table.LegacyColors.ItemPressedBackground = factory.GetColor(0xFFFCF2);
            table.LegacyColors.ItemPressedBackground2 = factory.GetColor(0xFFECB5);
            table.LegacyColors.ItemPressedBorder = factory.GetColor(0xE5C365);
            table.LegacyColors.ItemPressedText = factory.GetColor(0x1B293E);
            table.LegacyColors.ItemSeparator = factory.GetColor(0xBEC3CB);
            table.LegacyColors.ItemSeparatorShade = Color.Empty; // Color.FromArgb(128, factory.GetColor(0xFFFFFF));
            table.LegacyColors.ItemText = factory.GetColor(0x1B293E);
            table.LegacyColors.MenuBackground = factory.GetColor(0xE9ECEE);
            table.LegacyColors.MenuBackground2 = factory.GetColor(0xD0D7E2);
            table.LegacyColors.MenuBarBackground = factory.GetColor(0xCAD3E2);
            table.LegacyColors.MenuBarBackground2 = factory.GetColor(0xAEB9CD);
            table.LegacyColors.MenuBorder = factory.GetColor(0x9BA7B7);
            table.LegacyColors.ItemExpandedBorder = table.LegacyColors.MenuBorder;
            table.LegacyColors.MenuSide = factory.GetColor(0xE9ECEE);
            table.LegacyColors.MenuSide2 = Color.Empty; // factory.GetColor(0xDDE0E8);
            table.LegacyColors.MenuUnusedBackground = table.LegacyColors.MenuBackground;
            table.LegacyColors.MenuUnusedSide = factory.GetColor(0xBCC7D8);
            table.LegacyColors.MenuUnusedSide2 = Color.Empty;// System.Windows.Forms.ControlPaint.Light(table.LegacyColors.MenuSide2);
            table.LegacyColors.ItemDesignTimeBorder = Color.DarkGray;
            table.LegacyColors.BarDockedBorder = factory.GetColor(0x9CAAC1);
            table.LegacyColors.DockSiteBackColor = factory.GetColor(0x9CAAC1);
            table.LegacyColors.DockSiteBackColor2 = Color.Empty;
            table.LegacyColors.CustomizeBackground = factory.GetColor(0xD5DCE8);
            table.LegacyColors.CustomizeBackground2 = Color.Empty; // factory.GetColor(0xB9CEE6);
            table.LegacyColors.CustomizeText = factory.GetColor(0x1B293E);
            table.LegacyColors.PanelBackground = factory.GetColor(0xCAD3E2);
            table.LegacyColors.PanelBackground2 = factory.GetColor(0xAEB9CD);
            table.LegacyColors.PanelText = factory.GetColor(0x1B293E);
            table.LegacyColors.PanelBorder = factory.GetColor(editBorderColor);
            table.LegacyColors.ExplorerBarBackground = factory.GetColor(0x4D6082);
            table.LegacyColors.ExplorerBarBackground2 = factory.GetColor(0x3D5277);
            table.LegacyColors.AutoHidePanelBackgroundImage = table.Form.MdiClientBackgroundImage;
            table.LegacyColors.AutoHidePanelBackground = Color.Transparent;
            table.LegacyColors.AutoHideTabBackground = factory.GetColor(0x4B5E81);
            table.LegacyColors.AutoHideTabText = factory.GetColor(0xFFFFFF);
            table.LegacyColors.AutoHideTabBorder = Color.Transparent;
            table.LegacyColors.AutoHideSelectedTabBackground = factory.GetColor(0x4C5C74);
            table.LegacyColors.AutoHideSelectedTabBackground2 = factory.GetColor(0x6F7776);
            table.LegacyColors.AutoHideSelectedTabText = factory.GetColor(0xFFFFFF);
            table.LegacyColors.AutoHideSelectedTabBorder = factory.GetColor(0x9BA7B7);
            table.LegacyColors.SplitterBackground = factory.GetColor(0xDBEBFF);
            table.LegacyColors.SplitterBackground2 = factory.GetColor(0xB4C4D6);
            table.LegacyColors.SplitterBorder = factory.GetColor(0x859EBF);
            table.LegacyColors.SplitterText = table.LegacyColors.SplitterBorder;
            #endregion

            #region Navigation Pane
            table.NavigationPane.ButtonBackground = new GradientColorTable();
            table.NavigationPane.ButtonBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xBCC7D8), 0));
            #endregion

            #region SuperTooltip
            table.SuperTooltip.BackgroundColors = new LinearGradientColorTable(factory.GetColor(0xFDFDFE), factory.GetColor(0xE4E5F0));
            table.SuperTooltip.TextColor = factory.GetColor(0x4C4C4C);
            #endregion

            #region Slider
            Office2007SliderColorTable sl = table.Slider;
            sl.Default.LabelColor = factory.GetColor(0x1B293E);
            sl.Default.SliderLabelColor = factory.GetColor(0x1B293E);
            sl.Default.PartBackground = new GradientColorTable();
            sl.Default.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xF7FBFE), 0));
            sl.Default.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xE3ECF8), .5f));
            sl.Default.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xBCCADD), 1f));
            sl.Default.PartBorderColor = factory.GetColor(0x76869D);
            sl.Default.PartBorderLightColor = Color.FromArgb(96, factory.GetColor(0xFFFFFF));
            sl.Default.PartForeColor = factory.GetColor(0x7E8287);
            sl.Default.PartForeLightColor = Color.Empty;
            sl.Default.TrackLineColor = factory.GetColor(0x8A9CB8);
            sl.Default.TrackLineLightColor = factory.GetColor(0xDEEAF7);

            sl.MouseOver.LabelColor = factory.GetColor(0x1B293E);
            sl.MouseOver.SliderLabelColor = factory.GetColor(0x1B293E);
            sl.MouseOver.PartBackground = new GradientColorTable();
            sl.MouseOver.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xF6E49C), 0));
            sl.MouseOver.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xF6E59A), .5f));
            sl.MouseOver.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFAF5D5), 1f));
            sl.MouseOver.PartBorderColor = factory.GetColor(0xEDC23E);
            sl.MouseOver.PartBorderLightColor = Color.FromArgb(96, factory.GetColor(0xFFFFFF));
            sl.MouseOver.PartForeColor = factory.GetColor(0x7E8287);
            sl.MouseOver.PartForeLightColor = Color.Empty;
            sl.MouseOver.TrackLineColor = factory.GetColor(0x8A9CB8);
            sl.MouseOver.TrackLineLightColor = factory.GetColor(0xDEEAF7);

            sl.Pressed.LabelColor = factory.GetColor(0x1B293E);
            sl.Pressed.SliderLabelColor = factory.GetColor(0x1B293E);
            sl.Pressed.PartBackground = new GradientColorTable();
            sl.Pressed.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFBD677), 0));
            sl.Pressed.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFFE489), .5f));
            sl.Pressed.PartBackground.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFFE489), 1f));
            sl.Pressed.PartBorderColor = factory.GetColor(0xC27D36);
            sl.Pressed.PartBorderLightColor = factory.GetColor(0xF1C15F);
            sl.Pressed.PartForeColor = factory.GetColor(0x7E8287);
            sl.Pressed.PartForeLightColor = Color.Empty;
            sl.Pressed.TrackLineColor = factory.GetColor(0x8A9CB8);
            sl.Pressed.TrackLineLightColor = factory.GetColor(0xDEEAF7);

            ColorBlendFactory df = new ColorBlendFactory(ColorScheme.GetColor(0xCFCFCF));
            sl.Disabled.LabelColor = factory.GetColor(0x8D8D8D);
            sl.Disabled.SliderLabelColor = factory.GetColor(0x8D8D8D);
            sl.Disabled.PartBackground = new GradientColorTable();
            foreach (BackgroundColorBlend b in sl.Default.PartBackground.Colors)
                sl.Disabled.PartBackground.Colors.Add(new BackgroundColorBlend(df.GetColor(b.Color), b.Position));
            sl.Disabled.PartBorderColor = df.GetColor(sl.Default.PartBorderColor);
            sl.Disabled.PartBorderLightColor = df.GetColor(sl.Default.PartBorderLightColor);
            sl.Disabled.PartForeColor = df.GetColor(sl.Default.PartForeColor);
            sl.Disabled.PartForeLightColor = df.GetColor(sl.Default.PartForeLightColor);
            sl.Disabled.TrackLineColor = df.GetColor(sl.Default.TrackLineColor);
            sl.Disabled.TrackLineLightColor = df.GetColor(sl.Default.TrackLineLightColor);
            #endregion

            #region ListViewEx
            table.ListViewEx.Border = factory.GetColor(editBorderColor);
            table.ListViewEx.ColumnBackground = new LinearGradientColorTable(factory.GetColor(0xEFF5FB), factory.GetColor(0xE1ECFA));
            table.ListViewEx.ColumnSeparator = factory.GetColor(editBorderColor);
            table.ListViewEx.SelectionBackground = new LinearGradientColorTable(factory.GetColor(0xA7CDF0), Color.Empty);
            table.ListViewEx.SelectionBorder = factory.GetColor(0xE3EFFF);
            #endregion

            #region DataGridView
            table.DataGridView.ColumnHeaderNormalBorder = factory.GetColor(editBorderColor);
            table.DataGridView.ColumnHeaderNormalBackground = new LinearGradientColorTable(factory.GetColor(0xEFF5FB), factory.GetColor(0xE1ECFA), 90);
            table.DataGridView.ColumnHeaderSelectedBackground = new LinearGradientColorTable(factory.GetColor(0xF9D99F), factory.GetColor(0xF1C15F), 90);
            table.DataGridView.ColumnHeaderSelectedBorder = factory.GetColor(0xF29536);
            table.DataGridView.ColumnHeaderSelectedMouseOverBackground = new LinearGradientColorTable(factory.GetColor(0xFFD58D), factory.GetColor(0xF2923A), 90);
            table.DataGridView.ColumnHeaderSelectedMouseOverBorder = factory.GetColor(0xF29536);
            table.DataGridView.ColumnHeaderMouseOverBackground = new LinearGradientColorTable(factory.GetColor(0xD4E4F7), factory.GetColor(0xC3D6EC), 90);
            table.DataGridView.ColumnHeaderMouseOverBorder = factory.GetColor(editBorderColor);
            table.DataGridView.ColumnHeaderPressedBackground = new LinearGradientColorTable(factory.GetColor(0xB5CBE3), factory.GetColor(0xA5BCD7), 90);
            table.DataGridView.ColumnHeaderPressedBorder = factory.GetColor(editBorderColor);

            table.DataGridView.RowNormalBackground = new LinearGradientColorTable(factory.GetColor(0xE4ECF7));
            table.DataGridView.RowNormalBorder = factory.GetColor(editBorderColor);
            table.DataGridView.RowSelectedBackground = new LinearGradientColorTable(factory.GetColor(0xFFD58D));
            table.DataGridView.RowSelectedBorder = factory.GetColor(0xF29536);
            table.DataGridView.RowSelectedMouseOverBackground = new LinearGradientColorTable(factory.GetColor(0xF1C05C));
            table.DataGridView.RowSelectedMouseOverBorder = factory.GetColor(0xF29536);
            table.DataGridView.RowMouseOverBackground = new LinearGradientColorTable(factory.GetColor(0xF1C05C));
            table.DataGridView.RowMouseOverBorder = factory.GetColor(0xF29536);
            table.DataGridView.RowPressedBackground = new LinearGradientColorTable(factory.GetColor(0xB5CBE3), factory.GetColor(0xA5BCD7), 90);
            table.DataGridView.RowPressedBorder = factory.GetColor(editBorderColor);

            table.DataGridView.GridColor = factory.GetColor(0xAAAAAA);

            table.DataGridView.SelectorBackground = new LinearGradientColorTable(factory.GetColor(0xA5BCD7));
            table.DataGridView.SelectorBorder = factory.GetColor(editBorderColor);
            table.DataGridView.SelectorBorderDark = factory.GetColor(0xC3C3C3);
            table.DataGridView.SelectorBorderLight = factory.GetColor(0xF9F9F9);
            table.DataGridView.SelectorSign = new LinearGradientColorTable(factory.GetColor(0xF2F2F2));

            table.DataGridView.SelectorMouseOverBackground = new LinearGradientColorTable(factory.GetColor(0x8BA0B5));
            table.DataGridView.SelectorMouseOverBorder = factory.GetColor(0x9EB6CE);
            table.DataGridView.SelectorMouseOverBorderDark = factory.GetColor(0xB0CFF7);
            table.DataGridView.SelectorMouseOverBorderLight = factory.GetColor(0xD5E4F2);
            table.DataGridView.SelectorMouseOverSign = new LinearGradientColorTable(factory.GetColor(0xF9FAFB), factory.GetColor(0xD7DAE2));
            #endregion

            #region SideBar
            table.SideBar.Background = new LinearGradientColorTable(factory.GetColor(Color.White));
            table.SideBar.Border = factory.GetColor(editBorderColor);
            table.SideBar.SideBarPanelItemText = factory.GetColor(0x1B293E);
            table.SideBar.SideBarPanelItemDefault = new GradientColorTable();
            table.SideBar.SideBarPanelItemDefault.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xEDF5FD), 0));
            table.SideBar.SideBarPanelItemDefault.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xD8E4F2), 1));
            // Expanded
            table.SideBar.SideBarPanelItemExpanded = new GradientColorTable();
            table.SideBar.SideBarPanelItemExpanded.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFBDBB5), 0));
            table.SideBar.SideBarPanelItemExpanded.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFEC778), .4f));
            table.SideBar.SideBarPanelItemExpanded.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFEB456), .4f));
            table.SideBar.SideBarPanelItemExpanded.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFDEB9F), 1));
            // MouseOver
            table.SideBar.SideBarPanelItemMouseOver = new GradientColorTable();
            table.SideBar.SideBarPanelItemMouseOver.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFFFCD9), 0));
            table.SideBar.SideBarPanelItemMouseOver.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFFE78D), .4f));
            table.SideBar.SideBarPanelItemMouseOver.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFFD748), .4f));
            table.SideBar.SideBarPanelItemMouseOver.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFFE793), 1));
            // Pressed
            table.SideBar.SideBarPanelItemPressed = new GradientColorTable();
            table.SideBar.SideBarPanelItemPressed.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xF8B869), 0));
            table.SideBar.SideBarPanelItemPressed.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFDA361), .4f));
            table.SideBar.SideBarPanelItemPressed.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFB8A3C), .4f));
            table.SideBar.SideBarPanelItemPressed.Colors.Add(new BackgroundColorBlend(factory.GetColor(0xFEBB60), 1));
            #endregion

            #region AdvTree
#if !NOTREE
            table.AdvTree = new DevComponents.AdvTree.Display.TreeColorTable();
            DevComponents.AdvTree.Display.ColorTableInitializer.InitOffice2007Blue(table.AdvTree, factory);
#endif
            #endregion

            #region CrumbBar
            table.CrumbBarItemView = new CrumbBarItemViewColorTable();
            CrumbBarItemViewStateColorTable crumbBarViewTable = new CrumbBarItemViewStateColorTable();
            table.CrumbBarItemView.Default = crumbBarViewTable;
            crumbBarViewTable.Foreground = factory.GetColor(0x1B293E);
            crumbBarViewTable = new CrumbBarItemViewStateColorTable();
            table.CrumbBarItemView.MouseOver = crumbBarViewTable;
            crumbBarViewTable.Foreground = factory.GetColor(0x1B293E);
            crumbBarViewTable.Background = new BackgroundColorBlendCollection();
            crumbBarViewTable.Background.AddRange(new BackgroundColorBlend[]{
                new BackgroundColorBlend(factory.GetColor("F9E8A1"), 0f),
                new BackgroundColorBlend(factory.GetColor("FAF2CD"), 1f)});
            crumbBarViewTable.Border = factory.GetColor("EEC955");
            crumbBarViewTable.BorderLight = factory.GetColor("90FFFFFF");
            crumbBarViewTable = new CrumbBarItemViewStateColorTable();
            table.CrumbBarItemView.MouseOverInactive = crumbBarViewTable;
            crumbBarViewTable.Foreground = factory.GetColor(0x1B293E);
            crumbBarViewTable.Background = new BackgroundColorBlendCollection();
            crumbBarViewTable.Background.AddRange(new BackgroundColorBlend[]{
                new BackgroundColorBlend(factory.GetColor("FFFFFDEC"), 0f),
                new BackgroundColorBlend(factory.GetColor("FFFFF4CA"), .4f),
                new BackgroundColorBlend(factory.GetColor("FFFFEBA6"), .4f),
                new BackgroundColorBlend(factory.GetColor("FFFFF2C5"), 1f)});
            crumbBarViewTable.Border = factory.GetColor("FF8E8F8F");
            crumbBarViewTable.BorderLight = factory.GetColor("90FFFFFF");
            crumbBarViewTable = new CrumbBarItemViewStateColorTable();
            table.CrumbBarItemView.Pressed = crumbBarViewTable;
            crumbBarViewTable.Foreground = factory.GetColor(0x1B293E);
            crumbBarViewTable.Background = new BackgroundColorBlendCollection();
            crumbBarViewTable.Background.AddRange(new BackgroundColorBlend[]{
                new BackgroundColorBlend(factory.GetColor("FFC59B61"), 0f),
                new BackgroundColorBlend(factory.GetColor("FFEEB469"), .1f),
                new BackgroundColorBlend(factory.GetColor("FFFCA060"), .6f),
                new BackgroundColorBlend(factory.GetColor("FFFB8E3D"), .6f),
                new BackgroundColorBlend(factory.GetColor("FFFEBB60"), 1f)});
            crumbBarViewTable.Border = factory.GetColor("FF8B7654");
            crumbBarViewTable.BorderLight = factory.GetColor("408B7654");

            #endregion

            #region WarningBox
            table.WarningBox.BackColor = factory.GetColor(factory.GetColor(0xCDDFF5));
            table.WarningBox.WarningBorderColor = factory.GetColor(editBorderColor);
            table.WarningBox.WarningBackColor1 = factory.GetColor(0xC7D6E9);
            table.WarningBox.WarningBackColor2 = factory.GetColor(0xAFC3DC);
            #endregion

            #region CalendarView

            #region WeekDayViewColors

            table.CalendarView.WeekDayViewColors = new ColorDef[]
            {
                new ColorDef(factory.GetColor(editBorderColor)),           // DayViewBorder
                new ColorDef(factory.GetColor(0x1B293E)),           // DayHeaderForeground

                new ColorDef(factory.GetColor(0xA5BFE1)),           // DayHeaderBackground

                new ColorDef(factory.GetColor(0x8DAED9)),           // DayHeaderBorder

                new ColorDef(factory.GetColor(0xFFFFFF)),           // DayWorkHoursBackground
                new ColorDef(factory.GetColor(0xA5BFE1)),           // DayAllDayEventBackground
                new ColorDef(factory.GetColor(0xE6EDF7)),           // DayOffWorkHoursBackground

                new ColorDef(factory.GetColor(0xA5BFE1)),           // DayHourBorder
                new ColorDef(factory.GetColor(0xD5E1F1)),           // DayHalfHourBorder

                new ColorDef(factory.GetColor(0x294C7A)),           // SelectionBackground

                new ColorDef(factory.GetColor(0x9199A4)),           // OwnerTabBorder

                new ColorDef(new Color[] {factory.GetColor(0xCFD2D8), factory.GetColor(0xB0B6BE)},    // OwnerTabBackground
                new float[] {0f, 1f}, 90f),

                new ColorDef(factory.GetColor(0x000000)),           // OwnerTabForeground
                new ColorDef(factory.GetColor(0xB0B6BE)),           // OwnerTabContentBackground
                new ColorDef(factory.GetColor(0x000000)),           // OwnerTabSelectedForeground
                new ColorDef(factory.GetColor(0xFFFFFF)),           // OwnerTabSelectionBackground

                new ColorDef(factory.GetColor(0xFFFFFF)),           // CondensedViewBackground

                new ColorDef(factory.GetColor(0xEB8900)),           // NowDayViewBorder
                new ColorDef(factory.GetColor(0x000000)),           // NowDayHeaderForeground - 0x15428B

                new ColorDef(new Color[] {factory.GetColor(0xFFED79), factory.GetColor(0xFFD86B), factory.GetColor(0xFFBB00), factory.GetColor(0xFFEA77)},
                new float[] {0f, .55f ,58f, 1f}, 90f),              // NowDayHeaderBackground
                
                new ColorDef(new Color[] {factory.GetColor(0xFFED79), factory.GetColor(0xFFD86B), factory.GetColor(0xFFBB00), factory.GetColor(0xFFEA77)},
                new float[] {0f, .55f ,58f, 1f}, 90f),              // TimeIndicator

                new ColorDef(factory.GetColor(0xEB8900)),           // TimeIndicatorBorder
            };

            #endregion

            #region HourRulerColors

            table.CalendarView.TimeRulerColors = new ColorDef[]
            {
                new ColorDef(factory.GetColor(0xFFFFFF)),           // TimeRulerBackground
                new ColorDef(factory.GetColor(0x1B293E)),           // TimeRulerForeground
                new ColorDef(factory.GetColor(editBorderColor)),           // TimeRulerBorder
                new ColorDef(factory.GetColor(editBorderColor)),           // TimeRulerTickBorder

                new ColorDef(new Color[] {factory.GetColor(0xFFED79), factory.GetColor(0xFFD86B), factory.GetColor(0xFFBB00), factory.GetColor(0xFFEA77)},
                new float[] {0f, .55f ,58f, 1f}, 90f),              // TimeRulerIndicator

                new ColorDef(factory.GetColor(0xEB8900)),           // TimeRulerIndicatorBorder
            };

            #endregion

            #region MonthViewColors

            table.CalendarView.MonthViewColors = new ColorDef[]
            {
              new ColorDef(factory.GetColor(editBorderColor)),           // DayOfWeekHeaderBorder

                new ColorDef(factory.GetColor(0xFFFFFF)),         // DayOfWeekHeaderBackground

                new ColorDef(factory.GetColor(0x1B293E)),           // DayOfWeekHeaderForeground
                new ColorDef(factory.GetColor(0x8DAED9)),           // SideBarBorder

                new ColorDef(factory.GetColor(0xE6EDF7)),           // SideBarBackground

                new ColorDef(factory.GetColor(0x000000)),           // SideBarForeground
                new ColorDef(factory.GetColor(editBorderColor)),           // DayHeaderBorder

                new ColorDef(factory.GetColor(0xA5BFE1)),           // DayHeaderBackground

                new ColorDef(factory.GetColor(0x1F3979)),           // DayHeaderForeground
                new ColorDef(factory.GetColor(0x8DAED9)),           // DayContentBorder
                new ColorDef(factory.GetColor(0xE6EDF7)),           // DayContentSelectionBackground
                new ColorDef(factory.GetColor(0xFFFFFF)),           // DayContentActiveDayBackground
                new ColorDef(factory.GetColor(0xA5BFE1)),           // DayContentInactiveDayBackground

                new ColorDef(factory.GetColor(0x9199A4)),           // OwnerTabBorder

                new ColorDef(new Color[] {factory.GetColor(0xCFD2D8), factory.GetColor(0xB0B6BE)},    // OwnerTabBackground
                new float[] {0f, 1f}, 90f),

                new ColorDef(factory.GetColor(0x000000)),           // OwnerTabForeground
                new ColorDef(factory.GetColor(0xB0B6BE)),           // OwnerTabContentBackground
                new ColorDef(factory.GetColor(0x000000)),           // OwnerTabSelectedForeground
                new ColorDef(factory.GetColor(0xFFFFFF)),           // OwnerTabSelectionBackground
 
                new ColorDef(factory.GetColor(0xEB8900)),           // NowDayViewBorder
                new ColorDef(factory.GetColor(0x000000)),           // NowDayHeaderForeground - 0x15428B

                new ColorDef(new Color[] {factory.GetColor(0xFFED79), factory.GetColor(0xFFD86B), factory.GetColor(0xFFBB00), factory.GetColor(0xFFEA77)},
                new float[] {0f, .55f ,58f, 1f}, 90f),              // NowDayHeaderBackground

                new ColorDef(factory.GetColor(0x1F3979)),           // ContentLinkForeground - DayHeaderForeground
                new ColorDef(factory.GetColor(0xFFFFFF)),           // ContentLinkBackground - DayContentActiveDayBackground

           };

            #endregion

            #region AppointmentColors

            table.CalendarView.AppointmentColors = new ColorDef[]
            {
                new ColorDef(factory.GetColor(0x8DAED9)),           // DefaultBorder

                new ColorDef(new Color[] {factory.GetColor(0xE0E9F6), factory.GetColor(0xB3C9E6)},
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

            table.SuperTab.Background = new SuperTabLinearGradientColorTable(factory.GetColor(0xBBCEE6));
            table.SuperTab.InnerBorder = factory.GetColor(Color.Transparent);
            table.SuperTab.OuterBorder = factory.GetColor(0x9FB2C7);

            table.SuperTab.ControlBoxDefault.Image = factory.GetColor(0x1B293E);

            table.SuperTab.ControlBoxMouseOver.Background = factory.GetColor(0xCEEDFA);
            table.SuperTab.ControlBoxMouseOver.Border = factory.GetColor(0x3399FF);
            table.SuperTab.ControlBoxMouseOver.Image = factory.GetColor(0xFF, 0x1B293E);

            table.SuperTab.ControlBoxPressed.Background = factory.GetColor(0xB7CAE0);
            table.SuperTab.ControlBoxPressed.Border = factory.GetColor(0x3399FF);
            table.SuperTab.ControlBoxPressed.Image = factory.GetColor(0xFF, 0x1B293E);

            table.SuperTab.InsertMarker = factory.GetColor(0xFF, 0x000000);

            #endregion

            #region SuperTabItem

            // Top Default

            table.SuperTabItem.Default.Normal.Background = new SuperTabLinearGradientColorTable();
            table.SuperTabItem.Default.Normal.Text = factory.GetColor(0x1B293E);
            table.SuperTabItem.Default.Normal.CloseMarker = factory.GetColor(0x1B293E);

            // Top Selected
            table.SuperTabItem.Default.Selected.Background = new SuperTabLinearGradientColorTable(
                new Color[] { factory.GetColor(0xF5FAFF), factory.GetColor(0xEFF6FD) },
                new float[] { 0, 1 });

            table.SuperTabItem.Default.Selected.InnerBorder = Color.Transparent;
            table.SuperTabItem.Default.Selected.OuterBorder = factory.GetColor(0x9FB2C7);
            table.SuperTabItem.Default.Selected.Text = factory.GetColor(0x1B293E);
            table.SuperTabItem.Default.Selected.CloseMarker = factory.GetColor(0x1B293E);

            // Disabled
            table.SuperTabItem.Default.Disabled.Text = factory.GetColor(0x87929f);
            table.SuperTabItem.Default.Disabled.Background.AdaptiveGradient = false;
            table.SuperTabItem.Default.Disabled.CloseMarker = factory.GetColor(0x87929f);

            // Top SelectedMouseOver

            table.SuperTabItem.Default.SelectedMouseOver.Background = new SuperTabLinearGradientColorTable(
                new Color[] { factory.GetColor(0xF5FAFF), factory.GetColor(0xEFF6FD) },
                new float[] { 0, 1 });

            table.SuperTabItem.Default.SelectedMouseOver.InnerBorder = Color.Transparent;
            table.SuperTabItem.Default.SelectedMouseOver.OuterBorder = factory.GetColor(0x9FB2C7);
            table.SuperTabItem.Default.SelectedMouseOver.Text = factory.GetColor(0x1B293E);
            table.SuperTabItem.Default.SelectedMouseOver.CloseMarker = factory.GetColor(0x1B293E);

            // Top MouseOver

            table.SuperTabItem.Default.MouseOver.Background = new SuperTabLinearGradientColorTable(
                new Color[] { factory.GetColor(48, 0xFFFFFF), factory.GetColor(180, 0xFFFFFF) },
                new float[] { 0, 1 });

            table.SuperTabItem.Default.MouseOver.InnerBorder = factory.GetColor(190, 0xFFFFFF);
            table.SuperTabItem.Default.MouseOver.OuterBorder = factory.GetColor(0xB1B5BA);
            table.SuperTabItem.Default.MouseOver.Text = factory.GetColor(0x1B293E);
            table.SuperTabItem.Default.MouseOver.CloseMarker = factory.GetColor(0x1B293E);

            // Left, Bottom, Right

            table.SuperTabItem.Left = table.SuperTabItem.Default;
            table.SuperTabItem.Bottom = table.SuperTabItem.Default;
            table.SuperTabItem.Right = table.SuperTabItem.Right;

            #endregion

            #region SuperTabPanel

            table.SuperTabPanel.Default.Background = new SuperTabLinearGradientColorTable(factory.GetColor(0xEFF6FD));
            table.SuperTabPanel.Default.OuterBorder = factory.GetColor(0x9FB2C7);
            table.SuperTabPanel.Default.InnerBorder = factory.GetColor(Color.Transparent);

            table.SuperTabPanel.Left = table.SuperTabPanel.Default;
            table.SuperTabPanel.Bottom = table.SuperTabPanel.Default;
            table.SuperTabPanel.Right = table.SuperTabPanel.Default;

            #endregion

            #endregion

            #region Backstage

            #region Backstage
            SuperTabStyleColorFactory.GetVS2010BackstageBlueColorTable(table.Backstage, factory);
            #endregion

            #region BackstageItem
            SuperTabStyleColorFactory.GetOffice2010BackstageBlueItemColorTable(table.BackstageItem, factory);
            #endregion

            #region BackstagePanel
            SuperTabStyleColorFactory.GetOffice2010BackstageBluePanelColorTable(table.BackstagePanel, factory);
            #endregion

            #endregion

            #region SwitchButton
            table.SwitchButton = new SwitchButtonColors();
            table.SwitchButton.Disabled.BorderColor = table.CheckBoxItem.Disabled.CheckBorder;
            table.SwitchButton.Disabled.SwitchBorderColor = table.SwitchButton.Disabled.BorderColor;
            table.SwitchButton.Disabled.OffTextColor = table.CheckBoxItem.Disabled.Text;
            table.SwitchButton.Disabled.OnTextColor = table.SwitchButton.Disabled.OffTextColor;
            table.SwitchButton.Disabled.TextColor = table.SwitchButton.Disabled.OffTextColor;
            table.SwitchButton.Disabled.SwitchBackColor = table.CheckBoxItem.Disabled.CheckInnerBackground.Start;
            table.SwitchButton.Disabled.OffBackColor = table.CheckBoxItem.Disabled.CheckInnerBackground.Start;
            table.SwitchButton.Disabled.OnBackColor = table.SwitchButton.Disabled.OffBackColor;
            #endregion

            #region ElementStyle Classes
            table.StyleClasses.Clear();
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonGalleryContainerKey;
            style.BorderColor = factory.GetColor(0x8894A8);
            style.Border = eStyleBorderType.Solid;
            style.BorderWidth = 1;
            style.CornerDiameter = 2;
            style.CornerType = eCornerType.Rounded;
            style.BackColor = factory.GetColor(0xF1F3F8);
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
            style = Office2007ColorTableFactory.GetTextBoxStyle(factory.GetColor(editBorderColor));
            table.StyleClasses.Add(style.Class, style);
            // RichTextBox border
            style = Office2007ColorTableFactory.GetRichTextBoxStyle(factory.GetColor(editBorderColor));
            table.StyleClasses.Add(style.Class, style);
            // ItemPanel
            style = Office2007ColorTableFactory.GetItemPanelStyle(factory.GetColor(editBorderColor));
            table.StyleClasses.Add(style.Class, style);
            // DateTimeInput background
            style = Office2007ColorTableFactory.GetDateTimeInputBackgroundStyle(factory.GetColor(editBorderColor), SystemColors.Window);
            table.StyleClasses.Add(style.Class, style);
            // Ribbon Client Panel
            style = GetRibbonClientPanelStyle(factory, eOffice2010ColorScheme.Blue);
            table.StyleClasses.Add(style.Class, style);
            // ListView Border
            style = Office2007ColorTableFactory.GetListViewBorderStyle(table.ListViewEx);
            table.StyleClasses.Add(style.Class, style);
            style = Office2007ColorTableFactory.GetStatusBarAltStyle(table.Bar);
            table.StyleClasses.Add(style.Class, style);
#if !NOTREE
            // Tree Border/Background
            style = Office2007ColorTableFactory.GetAdvTreeStyle(factory.GetColor(editBorderColor), Color.Empty);
            table.StyleClasses.Add(style.Class, style);
            style = Office2007ColorTableFactory.GetAdvTreeColumnsHeaderStyle(factory.GetColor(0xF9FCFD), factory.GetColor(0xD3DBE9), factory.GetColor(0x9EB6CE));
            table.StyleClasses.Add(style.Class, style);
            style = Office2007ColorTableFactory.GetAdvTreeNodesColumnsHeaderStyle(factory.GetColor(0xF9FCFD), factory.GetColor(0xD3DBE9), factory.GetColor(0x9EB6CE));
            table.StyleClasses.Add(style.Class, style);
            style = Office2007ColorTableFactory.GetAdvTreeColumnStyle(factory.GetColor(0x000000));
            table.StyleClasses.Add(style.Class, style);
            // CrumbBar
            style = Office2007ColorTableFactory.GetCrumbBarBackgroundStyle(factory.GetColor(Color.White), factory.GetColor("FF567DB0"), factory.GetColor("FF2F578D"));
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

            // Slide-out Button
            style = Office2007ColorTableFactory.GetSlideOutButtonStyle(table.CheckBoxItem.Default.CheckBorder);
            table.StyleClasses.Add(style.Class, style);
            // MetroTilePanel
            style = Office2007ColorTableFactory.GetMetroTilePanelStyle(factory.GetColor(Color.White));
            table.StyleClasses.Add(style.Class, style);
            // MetroTileGroup
            style = Office2007ColorTableFactory.GetMetroTileGroupStyle(factory.GetColor(Color.DarkGray));
            table.StyleClasses.Add(style.Class, style);
            // MonthCalendarAdv
            style = Office2007ColorTableFactory.GetMonthCalendarStyle(SystemColors.Window);
            table.StyleClasses.Add(style.Class, style);
            #endregion

            #region StepIndicator
            table.StepIndicator.BackgroundColor = factory.GetColor(ColorFunctions.GetShade(table.Form.BackColor, 10));
            table.StepIndicator.IndicatorColor = factory.GetColor(0xA4CC28);
            #endregion

            #region RadialMenu
            table.RadialMenu = new RadialMenuColorTable();
            table.RadialMenu.CircularBackColor = factory.GetColor(0xD44F2E);
            table.RadialMenu.CircularBorderColor = factory.GetColor(0xFFFFFF);
            table.RadialMenu.CircularForeColor = factory.GetColor(0xFFFFFF);
            table.RadialMenu.RadialMenuBackground = factory.GetColor(0xFFFFFF);
            table.RadialMenu.RadialMenuBorder = factory.GetColor(0x535F74);
            table.RadialMenu.RadialMenuButtonBackground = factory.GetColor(0xFFFFFF);
            table.RadialMenu.RadialMenuButtonBorder = factory.GetColor(0x535F74);
            table.RadialMenu.RadialMenuExpandForeground = factory.GetColor(0xFFFFFF);
            table.RadialMenu.RadialMenuInactiveBorder = Color.FromArgb(128, table.RadialMenu.RadialMenuBorder);
            table.RadialMenu.RadialMenuItemForeground = factory.GetColor(0x535F74);
            table.RadialMenu.RadialMenuItemMouseOverBackground = Color.FromArgb(72, table.RadialMenu.RadialMenuItemForeground);
            table.RadialMenu.RadialMenuItemMouseOverForeground = factory.GetColor(0x535F74);
            table.RadialMenu.RadialMenuMouseOverBorder = Color.FromArgb(200, table.RadialMenu.RadialMenuBorder);
            #endregion
        }

        #region RibbonBar
        public static Office2007RibbonBarStateColorTable GetRibbonBar(ColorFactory factory)
        {
            Office2007RibbonBarStateColorTable rb = new Office2007RibbonBarStateColorTable();
            rb.TopBackgroundHeight = 0.8f;
            rb.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x9CAAC1));
            rb.InnerBorder = new LinearGradientColorTable();
            rb.TopBackground = new LinearGradientColorTable(factory.GetColor(0xCAD3E2), factory.GetColor(0xAEB9CD));
            rb.BottomBackground = null; // new LinearGradientColorTable(factory.GetColor(0xE7F1FA), factory.GetColor(0xD8E4F2));
            rb.TitleBackground = null;// new LinearGradientColorTable(factory.GetColor(0xC2D8F1), factory.GetColor(0xC0D8EF));
            rb.TitleText = factory.GetColor(0x384E73);
            return rb;
        }

        public static Office2007RibbonBarStateColorTable GetRibbonBarMouseOver(ColorFactory factory)
        {
            return GetRibbonBar(factory);
            //Office2007RibbonBarStateColorTable rb = new Office2007RibbonBarStateColorTable();
            //rb.TopBackgroundHeight = 0.8f;
            //rb.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE2ECF7), factory.GetColor(0x93A7C3));
            //rb.InnerBorder = new LinearGradientColorTable(factory.GetColor(40, 0xFFFFFF), factory.GetColor(120, 0xFFFFFF));
            //rb.TopBackground = new LinearGradientColorTable(factory.GetColor(0xEFF6FD), factory.GetColor(0xE7F1FA));
            //rb.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xE7F1FA), factory.GetColor(0xD8E4F2));
            //rb.TitleBackground = null;// new LinearGradientColorTable(factory.GetColor(0xC2D8F1), factory.GetColor(0xC0D8EF));
            //rb.TitleText = factory.GetColor(0x384E73);
            //return rb;
        }

        public static Office2007RibbonBarStateColorTable GetRibbonBarExpanded(ColorFactory factory)
        {
            Office2007RibbonBarStateColorTable rb = new Office2007RibbonBarStateColorTable();
            rb.TopBackgroundHeight = 0.8f;
            rb.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE2ECF7), factory.GetColor(0x93A7C3));
            rb.InnerBorder = new LinearGradientColorTable(factory.GetColor(40, 0xFFFFFF), factory.GetColor(120, 0xFFFFFF));
            rb.TopBackground = new LinearGradientColorTable(factory.GetColor(0xEFF6FD), factory.GetColor(0xE7F1FA));
            rb.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xE7F1FA), factory.GetColor(0xD8E4F2));
            rb.TitleBackground = null;// new LinearGradientColorTable(factory.GetColor(0xC2D8F1), factory.GetColor(0xC0D8EF));
            rb.TitleText = Color.Empty;
            return rb;
        }
        #endregion

        #region Buttons
        public static Office2007ButtonItemColorTable GetButtonItemBlueOrange(ColorFactory factory)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.Text = factory.GetColor(0x1B293E);

            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE5C365));
            cb.MouseOver.InnerBorder = LinearGradientColorTable.Empty;
            cb.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFFFCF2), factory.GetColor(0xFFF3CF));
            cb.MouseOver.TopBackgroundHighlight = LinearGradientColorTable.Empty; //new LinearGradientColorTable(Color.FromArgb(192, Color.White), Color.Transparent);
            cb.MouseOver.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xFFECB5), factory.GetColor(0xFFECB5));
            cb.MouseOver.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.MouseOver.SplitBorder = new LinearGradientColorTable(factory.GetColor(0xE5C365), Color.Empty);
            cb.MouseOver.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.MouseOver.Text = factory.GetColor(0x1B293E);

            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xEEC757));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE5C365));
            cb.Pressed.InnerBorder = LinearGradientColorTable.Empty;
            cb.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFFE8A6));
            cb.Pressed.TopBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.Pressed.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xFFE8A6));
            cb.Pressed.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(0xE5C365), Color.Empty);
            cb.Pressed.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.Pressed.Text = factory.GetColor(0x1B293E);

            // Checked
            cb.Checked = new Office2007ButtonItemStateColorTable();
            cb.Checked.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE5C365));
            cb.Checked.InnerBorder = LinearGradientColorTable.Empty;
            cb.Checked.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFFEFBB));
            cb.Checked.TopBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.Checked.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xFFEFBB));
            cb.Checked.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.Checked.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Checked.Text = factory.GetColor(0x1B293E);

            // Expanded button
            cb.Expanded = new Office2007ButtonItemStateColorTable();
            cb.Expanded.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE5C365));
            cb.Expanded.InnerBorder = LinearGradientColorTable.Empty;
            cb.Expanded.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFFE8A6));
            cb.Expanded.TopBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.Expanded.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xFFE8A6));
            cb.Expanded.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.Expanded.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Expanded.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Expanded.Text = factory.GetColor(0x1B293E);

            SetBlueExpandColors(cb, factory);

            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemBlueOrangeWithBackground(ColorFactory factory)
        {
            Office2007ButtonItemColorTable cb = GetButtonItemBlueOrange(factory);

            cb.Default.TopBackground = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            cb.Default.TopBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xAEB9CD));
            cb.Default.BottomBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8894A8));
            cb.Default.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOverSplitInactive = cb.Default;

            // Same as default
            cb.Disabled = new Office2007ButtonItemStateColorTable();
            cb.Disabled.TopBackground = new LinearGradientColorTable(factory.GetColor(0xBCC7D8));
            cb.Disabled.TopBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xBCC7D8));
            cb.Disabled.BottomBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x9CAAC1), Color.Empty);
            cb.Disabled.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Text = factory.GetColor(0xA7A7A7);
            return cb;
        }
        public static Office2007ButtonItemColorTable GetButtonItemOffice2007WithBackground(ColorFactory factory)
        {
            Office2007ButtonItemColorTable cb = GetButtonItemBlueOrange(factory);

            cb.Default.Text = factory.GetColor(0x262626);
            cb.Checked.Text = cb.Default.Text;
            cb.Expanded.Text = cb.Default.Text;
            cb.MouseOver.Text = cb.Default.Text;
            cb.MouseOverSplitInactive.Text = cb.Default.Text;
            cb.Pressed.Text = cb.Default.Text;

            cb.Default.TopBackground = new LinearGradientColorTable(factory.GetColor(0xFFFFFF), factory.GetColor(0xFCFCFD));
            cb.Default.TopBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xFCFCFD), factory.GetColor(0xEDEFF1));
            cb.Default.BottomBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xDEDFE0), factory.GetColor(0xA1A2A4));
            cb.Default.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.MouseOverSplitInactive = cb.Default;

            // Same as default
            cb.Disabled = new Office2007ButtonItemStateColorTable();
            cb.Disabled.TopBackground = new LinearGradientColorTable(factory.GetColor(0xEDF5FD));
            cb.Disabled.TopBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xEDF5FD));
            cb.Disabled.BottomBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xCEDDED), Color.Empty);
            cb.Disabled.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Text = factory.GetColor(0xA7A7A7);
            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemBlueBlueWithBackground(ColorFactory factory)
        {
            Office2007ButtonItemColorTable cb = Office2007ColorTableFactory.GetButtonItemBlueBlue(factory);

            cb.Default.TopBackground = new LinearGradientColorTable(factory.GetColor(0x3E7DDB), factory.GetColor(0x2B63B5));
            cb.Default.TopBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.BottomBackground = new LinearGradientColorTable(factory.GetColor(0x2960B3), factory.GetColor(0x2D70CF));
            cb.Default.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0x4588DE), Color.Transparent);
            cb.Default.InnerBorder = new LinearGradientColorTable(factory.GetColor(0x4387E4), Color.Empty);
            cb.Default.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x1F48A1), factory.GetColor(0x244FA6));
            cb.Default.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.Text = factory.GetColor(0xFFFFFF);
            cb.MouseOverSplitInactive = cb.Default;

            // Same as default
            cb.Disabled = new Office2007ButtonItemStateColorTable();
            cb.Disabled.TopBackground = new LinearGradientColorTable(factory.GetColor(0xEDF5FD));
            cb.Disabled.TopBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xEDF5FD));
            cb.Disabled.BottomBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xCEDDED), Color.Empty);
            cb.Disabled.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Text = factory.GetColor(0xA7A7A7);

            return cb;
        }

        public static Office2007ButtonItemColorTable GetButtonItemBlueMagentaWithBackground(ColorFactory factory)
        {
            Office2007ButtonItemColorTable cb = Office2007ColorTableFactory.GetButtonItemBlueMagenta(factory);
            cb.Default.TopBackground = new LinearGradientColorTable(factory.GetColor(0xB50C53), factory.GetColor(0xAF074D));
            cb.Default.TopBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xAF074D), factory.GetColor(0xE8447F));
            cb.Default.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xE4417C), Color.Transparent);
            cb.Default.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xBF185E), factory.GetColor(0xE84683));
            cb.Default.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8E023A), factory.GetColor(0x92033D));
            cb.Default.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Default.Text = factory.GetColor(0xFFFFFF);

            // Same as default
            cb.Disabled = new Office2007ButtonItemStateColorTable();
            cb.Disabled.TopBackground = new LinearGradientColorTable(factory.GetColor(0xEDF5FD));
            cb.Disabled.TopBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.BottomBackground = new LinearGradientColorTable(factory.GetColor(0xEDF5FD));
            cb.Disabled.BottomBackgroundHighlight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.InnerBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xCEDDED), Color.Empty);
            cb.Disabled.SplitBorder = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.SplitBorderLight = new LinearGradientColorTable(Color.Empty, Color.Empty);
            cb.Disabled.Text = factory.GetColor(0xA7A7A7);

            return cb;
        }

        public static void SetBlueExpandColors(Office2007ButtonItemColorTable ct, ColorFactory factory)
        {
            Color cb = factory.GetColor(0x1B293E);
            Color cl = Color.Empty;// Color.FromArgb(192, Color.White);
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

        public static Office2007ButtonItemColorTable GetNavigationPaneButtonItemTable(ColorFactory factory)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Orange);
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.Text = factory.GetColor(0x001938);

            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x859EBF));
            cb.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xF7FFFF));
            cb.MouseOver.TopBackground = LinearGradientColorTable.Empty;
            cb.MouseOver.TopBackgroundHighlight = LinearGradientColorTable.Empty; //new LinearGradientColorTable(Color.FromArgb(192, Color.White), Color.Transparent);
            cb.MouseOver.BottomBackground = LinearGradientColorTable.Empty;
            cb.MouseOver.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xE5F1FB), Color.Transparent);
            cb.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xE5F1FC), factory.GetColor(0xC2DBF1));
            cb.MouseOver.SplitBorder = LinearGradientColorTable.Empty;
            cb.MouseOver.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.MouseOver.Text = factory.GetColor(0x001938);

            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xB1BAC4));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x758EAB));
            cb.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(0x8AA3C2));
            cb.Pressed.TopBackground = LinearGradientColorTable.Empty;
            cb.Pressed.TopBackgroundHighlight = LinearGradientColorTable.Empty; //new LinearGradientColorTable(Color.FromArgb(192, Color.White), Color.Transparent);
            cb.Pressed.BottomBackground = LinearGradientColorTable.Empty;
            cb.Pressed.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0x9FB7D6), Color.Transparent);
            cb.Pressed.Background = new LinearGradientColorTable(factory.GetColor(0x9FB7D6));
            cb.Pressed.SplitBorder = LinearGradientColorTable.Empty;
            cb.Pressed.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.Pressed.Text = factory.GetColor(0x001938);

            // Checked
            cb.Checked = new Office2007ButtonItemStateColorTable();
            cb.Checked.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x859EBF));
            cb.Checked.InnerBorder = new LinearGradientColorTable(factory.GetColor(0xDDE9F7));
            cb.Checked.TopBackground = LinearGradientColorTable.Empty;
            cb.Checked.TopBackgroundHighlight = LinearGradientColorTable.Empty; //new LinearGradientColorTable(Color.FromArgb(192, Color.White), Color.Transparent);
            cb.Checked.BottomBackground = LinearGradientColorTable.Empty;
            cb.Checked.BottomBackgroundHighlight = new LinearGradientColorTable(factory.GetColor(0xDCEBF6), Color.Transparent);
            cb.Checked.Background = new LinearGradientColorTable(factory.GetColor(0xC8DBEF), factory.GetColor(0xB4CBE5));
            cb.Checked.SplitBorder = LinearGradientColorTable.Empty;
            cb.Checked.SplitBorderLight = LinearGradientColorTable.Empty;
            cb.Checked.Text = factory.GetColor(0x001938);

            // Expanded button
            cb.Expanded = cb.Checked;

            return cb;
        }
        #endregion

        #region RibbonTabItem
        public static Office2007RibbonTabItemColorTable GetRibbonTabItemBlueDefault(ColorFactory factory)
        {
            Office2007RibbonTabItemColorTable rt = new Office2007RibbonTabItemColorTable();
            rt.Default.Text = factory.GetColor(0x1B293E);
            rt.CornerSize = 2;

            // Selected Tab
            rt.Selected = new Office2007RibbonTabItemStateColorTable();
            rt.Selected.Background = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            rt.Selected.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.Selected.InnerBorder = LinearGradientColorTable.Empty;
            rt.Selected.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8894A8));
            rt.Selected.Text = factory.GetColor(0x1B293E);

            // Selected Tab Mouse Over
            rt.SelectedMouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.SelectedMouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            rt.SelectedMouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.SelectedMouseOver.InnerBorder = LinearGradientColorTable.Empty;
            rt.SelectedMouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8894A8));
            rt.SelectedMouseOver.Text = factory.GetColor(0x1B293E);

            // Tab Mouse Over
            rt.MouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(48, 0xFFFFFF), factory.GetColor(180, 0xFFFFFF));
            rt.MouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(190, 0xFFFFFF), Color.Empty);
            rt.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x8894A8), Color.Empty);
            rt.MouseOver.Text = factory.GetColor(0x1B293E);

            return rt;
        }

        public static Office2007RibbonTabItemColorTable GetRibbonTabItemBlueMagenta(ColorFactory factory)
        {
            Office2007RibbonTabItemColorTable rt = new Office2007RibbonTabItemColorTable();
            rt.Default.Text = factory.GetColor(0x1B293E);
            rt.CornerSize = 2;

            // Selected Tab
            rt.Selected = new Office2007RibbonTabItemStateColorTable();
            rt.Selected.Background = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            rt.Selected.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.Selected.InnerBorder = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(0xEEF5FD), Color.Transparent);
            rt.Selected.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE58ABD), factory.GetColor(0xDE66A6));
            rt.Selected.Text = factory.GetColor(0x1B293E);

            // Selected Tab Mouse Over
            rt.SelectedMouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.SelectedMouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            rt.SelectedMouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.SelectedMouseOver.InnerBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(0xEEF5FD), Color.Transparent);
            rt.SelectedMouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE58ABD), factory.GetColor(0xDE66A6));
            rt.SelectedMouseOver.Text = factory.GetColor(0x1B293E);

            // Tab Mouse Over
            rt.MouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(48, 0xE33991), factory.GetColor(180, 0xFFFFFF));
            rt.MouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(190, 0xFAD8F0), Color.Empty);
            rt.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xE33991), Color.Empty);
            rt.MouseOver.Text = factory.GetColor(0x1B293E);

            return rt;
        }

        public static Office2007RibbonTabItemColorTable GetRibbonTabItemBlueGreen(ColorFactory factory)
        {
            Office2007RibbonTabItemColorTable rt = new Office2007RibbonTabItemColorTable();
            rt.Default.Text = factory.GetColor(0x1B293E);
            rt.CornerSize = 2;

            // Selected Tab
            rt.Selected = new Office2007RibbonTabItemStateColorTable();
            rt.Selected.Background = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            rt.Selected.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.Selected.InnerBorder = LinearGradientColorTable.Empty; // new LinearGradientColorTable(factory.GetColor(0xEEF5FD), Color.Transparent);
            rt.Selected.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x64BF5F), factory.GetColor(0x53C24E));
            rt.Selected.Text = factory.GetColor(0x1B293E);

            // Selected Tab Mouse Over
            rt.SelectedMouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.SelectedMouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            rt.SelectedMouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.SelectedMouseOver.InnerBorder = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(0xEEF5FD), Color.Transparent);
            rt.SelectedMouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x64BF5F), factory.GetColor(0x53C24E));
            rt.SelectedMouseOver.Text = factory.GetColor(0x1B293E);

            // Tab Mouse Over
            rt.MouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(48, 0x26B023), factory.GetColor(180, 0xFFFFFF));
            rt.MouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(190, 0xC8F8B1), Color.Empty);
            rt.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x26B023), Color.Empty);
            rt.MouseOver.Text = factory.GetColor(0x1B293E);

            return rt;
        }

        public static Office2007RibbonTabItemColorTable GetRibbonTabItemBlueOrange(ColorFactory factory)
        {
            Office2007RibbonTabItemColorTable rt = new Office2007RibbonTabItemColorTable();
            rt.Default.Text = factory.GetColor(0x1B293E);
            rt.CornerSize = 2;

            // Selected Tab
            rt.Selected = new Office2007RibbonTabItemStateColorTable();
            rt.Selected.Background = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            rt.Selected.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.Selected.InnerBorder = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(0xEDF5FD), Color.Transparent);
            rt.Selected.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xF2A765), factory.GetColor(0xF2A000));
            rt.Selected.Text = factory.GetColor(0x1B293E);

            // Selected Tab Mouse Over
            rt.SelectedMouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.SelectedMouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xCAD3E2));
            rt.SelectedMouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.SelectedMouseOver.InnerBorder = LinearGradientColorTable.Empty; //new LinearGradientColorTable(factory.GetColor(0xEDF5FD), Color.Transparent);
            rt.SelectedMouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xF2A765), factory.GetColor(0xF2A000));
            rt.SelectedMouseOver.Text = factory.GetColor(0x1B293E);

            // Tab Mouse Over
            rt.MouseOver = new Office2007RibbonTabItemStateColorTable();
            rt.MouseOver.Background = new LinearGradientColorTable(factory.GetColor(0xF5D290), factory.GetColor(180, 0xFFFFFF));
            rt.MouseOver.BackgroundHighlight = LinearGradientColorTable.Empty;
            rt.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(190, 0xFBE66F), Color.Empty);
            rt.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0xF07D06), factory.GetColor(0xEF7E08));
            rt.MouseOver.Text = factory.GetColor(0x1B293E);

            return rt;
        }

        public static Office2007RibbonTabGroupColorTable GetRibbonTabGroupDefault(ColorFactory factory)
        {
            Office2007RibbonTabGroupColorTable tg = new Office2007RibbonTabGroupColorTable();
            tg.Background = new LinearGradientColorTable(factory.GetColor(0x4488E5), Color.Transparent);
            tg.BackgroundHighlight = LinearGradientColorTable.Empty;
            tg.Text = factory.GetColor(0x000000);
            tg.Border = new LinearGradientColorTable(factory.GetColor(0x1F48A1), Color.Transparent);

            return tg;
        }

        public static Office2007RibbonTabGroupColorTable GetRibbonTabGroupMagenta(ColorFactory factory)
        {
            Office2007RibbonTabGroupColorTable tg = new Office2007RibbonTabGroupColorTable();
            tg.Background = new LinearGradientColorTable(factory.GetColor(0xC55790), Color.Transparent);
            tg.BackgroundHighlight = LinearGradientColorTable.Empty;
            tg.Text = factory.GetColor(0x000000);
            tg.Border = new LinearGradientColorTable(factory.GetColor(0xC90077), Color.Transparent);

            return tg;
        }

        public static Office2007RibbonTabGroupColorTable GetRibbonTabGroupGreen(ColorFactory factory)
        {
            Office2007RibbonTabGroupColorTable tg = new Office2007RibbonTabGroupColorTable();
            tg.Background = new LinearGradientColorTable(factory.GetColor(192, 0x26B023), Color.Transparent);
            tg.BackgroundHighlight = LinearGradientColorTable.Empty;
            tg.Text = factory.GetColor(0x000000);
            tg.Border = new LinearGradientColorTable(factory.GetColor(0x21941D), Color.Transparent);

            return tg;
        }

        public static Office2007RibbonTabGroupColorTable GetRibbonTabGroupOrange(ColorFactory factory)
        {
            Office2007RibbonTabGroupColorTable tg = new Office2007RibbonTabGroupColorTable();
            tg.Background = new LinearGradientColorTable(factory.GetColor(192, 0xFF9D00), Color.Transparent);
            tg.BackgroundHighlight = LinearGradientColorTable.Empty;
            tg.Text = factory.GetColor(0x000000);
            tg.Border = new LinearGradientColorTable(factory.GetColor(0xD67519), Color.Transparent);

            return tg;
        }
        #endregion

        #region Style Class Creation
        public static ElementStyle GetFileMenuContainerStyle(Office2007ColorTable table)
        {
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonFileMenuContainerKey;
            Rendering.Office2007MenuColorTable mc = table.Menu;

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
            Rendering.Office2007MenuColorTable mc = table.Menu;

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
            Rendering.Office2007MenuColorTable mc = table.Menu;

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
            Rendering.Office2007MenuColorTable mc = table.Menu;

            style.BackColor = mc.FileColumnTwoBackground;

            return style;
        }

        public static ElementStyle GetMenuBottomContainer(Office2007ColorTable table)
        {
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonFileMenuBottomContainerKey;
            Rendering.Office2007MenuColorTable mc = table.Menu;

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
        public static void InitializeAppBlueScrollBarColorTable(Office2007ColorTable t, ColorFactory factory)
        {
            Office2007ScrollBarStateColorTable sct = t.AppScrollBar.Default;
            sct.Background = new LinearGradientColorTable(factory.GetColor(0xA9BED5), factory.GetColor(0xBBCEE4), 0);
            sct.Border = new LinearGradientColorTable(factory.GetColor(0x728EAD), Color.Empty);
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(0x6E7EA6), factory.GetColor(0x424B63), 90);
            sct.TrackBackground.Clear();
            sct.TrackBackground.AddRange(new BackgroundColorBlend[] { new BackgroundColorBlend(factory.GetColor(0xB9CEE6),0f), 
                new BackgroundColorBlend(factory.GetColor(0x91ABC9),1f)});
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = new LinearGradientColorTable(factory.GetColor(0x5C7796), Color.Empty);
            sct.TrackSignBackground = new LinearGradientColorTable(Color.FromArgb(180, factory.GetColor(0x8F8F8F)), Color.FromArgb(64, Color.White));

            // Mouse Over
            sct = t.AppScrollBar.MouseOver;
            sct.Background = t.AppScrollBar.Default.Background;
            sct.Border = t.AppScrollBar.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbBackground.AddRange(new BackgroundColorBlend[] { new BackgroundColorBlend(factory.GetColor(0xC1D7F0),0f), 
                new BackgroundColorBlend(factory.GetColor(0xB4CDE7),0.5f),
                new BackgroundColorBlend(factory.GetColor(0xA3BEDB),1f)});
            sct.ThumbInnerBorder = LinearGradientColorTable.Empty;
            sct.ThumbOuterBorder = new LinearGradientColorTable(factory.GetColor(0x5C7796));
            sct.ThumbSignBackground = t.AppScrollBar.Default.ThumbSignBackground;
            sct.TrackBackground.Clear();
            sct.TrackBackground.AddRange(new BackgroundColorBlend[] { new BackgroundColorBlend(factory.GetColor(0xC1D7F0),0f), 
                new BackgroundColorBlend(factory.GetColor(0xB4CDE7),0.5f),
                new BackgroundColorBlend(factory.GetColor(0xA3BEDB),1f)});
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = new LinearGradientColorTable(factory.GetColor(0x5C7796), Color.Empty);
            sct.TrackSignBackground = new LinearGradientColorTable(factory.GetColor(0x8F8F8F));

            // Control Mouse Over
            sct = t.AppScrollBar.MouseOverControl;
            sct.Background = t.AppScrollBar.Default.Background;
            sct.Border = t.AppScrollBar.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbBackground.AddRange(new BackgroundColorBlend[] { new BackgroundColorBlend(factory.GetColor(0xB9CEE6),0f), 
                new BackgroundColorBlend(factory.GetColor(0xA9C1DB),0.5f),
                new BackgroundColorBlend(factory.GetColor(0x91ABC9),1f)});
            sct.ThumbInnerBorder = LinearGradientColorTable.Empty;
            sct.ThumbOuterBorder = new LinearGradientColorTable(factory.GetColor(0x5C7796));
            sct.ThumbSignBackground = t.AppScrollBar.Default.ThumbSignBackground;
            sct.TrackBackground.Clear();
            sct.TrackBackground.AddRange(new BackgroundColorBlend[] { new BackgroundColorBlend(factory.GetColor(0xB9CEE6),0f), 
                new BackgroundColorBlend(factory.GetColor(0xA9C1DB),0.5f),
                new BackgroundColorBlend(factory.GetColor(0x91ABC9),1f)});
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = new LinearGradientColorTable(factory.GetColor(0x5C7796), Color.Empty);
            sct.TrackSignBackground = new LinearGradientColorTable(factory.GetColor(0x8F8F8F));

            // Pressed
            sct = t.AppScrollBar.Pressed;
            sct.Background = t.AppScrollBar.Default.Background;
            sct.Border = t.AppScrollBar.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbBackground.AddRange(new BackgroundColorBlend[] { 
                new BackgroundColorBlend(factory.GetColor(0xB0C7DE),0f), 
                new BackgroundColorBlend(factory.GetColor(0x89A4C1),1f)
            });
            sct.ThumbInnerBorder = LinearGradientColorTable.Empty;
            sct.ThumbOuterBorder = new LinearGradientColorTable(factory.GetColor(0x5C7796));
            sct.ThumbSignBackground = t.AppScrollBar.Default.ThumbSignBackground;
            sct.TrackBackground.Clear();
            sct.TrackBackground.AddRange(new BackgroundColorBlend[] { 
                new BackgroundColorBlend(factory.GetColor(0xB0C7DE),0f), 
                new BackgroundColorBlend(factory.GetColor(0x89A4C1),1f)
            });
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = new LinearGradientColorTable(factory.GetColor(0x5C7796), Color.Empty);
            sct.TrackSignBackground = t.AppScrollBar.Default.TrackSignBackground;

            // Disabled
            sct = t.AppScrollBar.Disabled;
            sct.Background = t.AppScrollBar.Default.Background;
            sct.Border = t.AppScrollBar.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbInnerBorder = new LinearGradientColorTable();
            sct.ThumbOuterBorder = new LinearGradientColorTable();
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(0xBFCFF7), factory.GetColor(0x727C94));
            sct.TrackBackground.Clear();
            sct.TrackInnerBorder = new LinearGradientColorTable();
            sct.TrackOuterBorder = new LinearGradientColorTable();
            sct.TrackSignBackground = new LinearGradientColorTable();
        }

        public static void InitializeScrollBarColorTable(Office2007ColorTable t, ColorFactory factory)
        {
            Office2007ScrollBarStateColorTable sct = t.ScrollBar.Default;
            sct.Background = new LinearGradientColorTable(factory.GetColor(0xF4F4F4), factory.GetColor(0xFCFCFC), 0);
            sct.Border = new LinearGradientColorTable(factory.GetColor(0xE6ECF2));
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(0x747576), factory.GetColor(0x454647), 90);
            sct.TrackBackground.Clear();
            sct.TrackBackground.AddRange(new BackgroundColorBlend[] { 
                new BackgroundColorBlend(factory.GetColor(0xF0F4F8),0f), 
                new BackgroundColorBlend(factory.GetColor(0xEAEEF3),.8f),
                new BackgroundColorBlend(factory.GetColor(0xE0E4E9),.8f),
                new BackgroundColorBlend(factory.GetColor(0xD7DBE1),1f)});
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = new LinearGradientColorTable(factory.GetColor(0x8A9199), Color.Empty);
            sct.TrackSignBackground = new LinearGradientColorTable(factory.GetColor(0x7D7D7D));

            // Mouse Over
            sct = t.ScrollBar.MouseOver;
            sct.Background = t.ScrollBar.Default.Background;
            sct.Border = t.ScrollBar.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbBackground.AddRange(new BackgroundColorBlend[] { 
                new BackgroundColorBlend(factory.GetColor(0xFCFDFE),0f), 
                new BackgroundColorBlend(factory.GetColor(0xF3F6F9),0.8f),
                new BackgroundColorBlend(factory.GetColor(0xEAEDF1),0.8f),
                new BackgroundColorBlend(factory.GetColor(0xE4E8EC),1f)});
            sct.ThumbInnerBorder = new LinearGradientColorTable(factory.GetColor(0xFCFDFE), factory.GetColor(0xF1F3F5));
            sct.ThumbOuterBorder = new LinearGradientColorTable(factory.GetColor(0x8A9199), Color.Empty);
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(0x747576), factory.GetColor(0x454647), 90);
            sct.TrackBackground.Clear();
            sct.TrackBackground.CopyFrom(sct.ThumbBackground);
            sct.TrackInnerBorder = sct.ThumbInnerBorder;
            sct.TrackOuterBorder = sct.ThumbOuterBorder;
            sct.TrackSignBackground = new LinearGradientColorTable(factory.GetColor(0x7D7D7D));

            // Control Mouse Over
            sct = t.ScrollBar.MouseOverControl;
            sct.Background = t.ScrollBar.Default.Background;
            sct.Border = t.ScrollBar.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbBackground.CopyFrom(t.ScrollBar.Default.TrackBackground);
            sct.ThumbInnerBorder = t.ScrollBar.Default.TrackInnerBorder;
            sct.ThumbOuterBorder = t.ScrollBar.Default.TrackOuterBorder;
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(0x747576), factory.GetColor(0x454647));
            sct.TrackBackground.Clear();
            sct.TrackBackground.CopyFrom(t.ScrollBar.Default.TrackBackground);
            sct.TrackInnerBorder = t.ScrollBar.Default.TrackInnerBorder;
            sct.TrackOuterBorder = t.ScrollBar.Default.TrackOuterBorder;
            sct.TrackSignBackground = t.ScrollBar.Default.TrackSignBackground;

            // Pressed
            sct = t.ScrollBar.Pressed;
            sct.Background = t.ScrollBar.Default.Background;
            sct.Border = t.ScrollBar.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbBackground.AddRange(new BackgroundColorBlend[] { 
                new BackgroundColorBlend(factory.GetColor(0xEBEFF3),0f), 
                new BackgroundColorBlend(factory.GetColor(0xE5E8ED),0.8f),
                new BackgroundColorBlend(factory.GetColor(0xDADEE3),0.8f),
                new BackgroundColorBlend(factory.GetColor(0xD0D4DA),1f)});
            sct.ThumbInnerBorder = LinearGradientColorTable.Empty;
            sct.ThumbOuterBorder = new LinearGradientColorTable(factory.GetColor(0x8A9199), Color.Empty);
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(0x6E7EA6), factory.GetColor(0x424B63), 90);
            sct.TrackBackground.Clear();
            sct.TrackBackground.CopyFrom(sct.ThumbBackground);
            sct.TrackInnerBorder = LinearGradientColorTable.Empty;
            sct.TrackOuterBorder = new LinearGradientColorTable(factory.GetColor(0x8A9199), Color.Empty);
            sct.TrackSignBackground = t.ScrollBar.Default.TrackSignBackground;
            // Disabled
            sct = t.ScrollBar.Disabled;
            sct.Background = t.ScrollBar.Default.Background;
            sct.Border = t.ScrollBar.Default.Border;
            sct.ThumbBackground.Clear();
            sct.ThumbInnerBorder = new LinearGradientColorTable();
            sct.ThumbOuterBorder = new LinearGradientColorTable();
            sct.ThumbSignBackground = new LinearGradientColorTable(factory.GetColor(0xBFCFF7), factory.GetColor(0x727C94));
            sct.TrackBackground.Clear();
            sct.TrackInnerBorder = new LinearGradientColorTable();
            sct.TrackOuterBorder = new LinearGradientColorTable();
            sct.TrackSignBackground = new LinearGradientColorTable();
        }
        #endregion

        #region RibbonClientPanel

        public static ElementStyle GetRibbonClientPanelStyle(ColorFactory f, eOffice2010ColorScheme cs)
        {
            ElementStyle style = new ElementStyle();
            style.Class = ElementStyleClassKeys.RibbonClientPanelKey;

            style.BackColorGradientAngle = 90;

            if (cs == eOffice2010ColorScheme.Blue)
            {
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0xC7DAF0), 0));
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0x9AB3CF), .7f));
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0x85A1C0), 1));
            }
            else if (cs == eOffice2010ColorScheme.Silver)
            {
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0xCDD1D6), 0));
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0xC0C4C8), .7f));
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0xBABEC2), 1));
            }
            else if (cs == eOffice2010ColorScheme.Black)
            {
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0x6F6F6F), 0));
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0x4F4F4F), .7f));
                style.BackColorBlend.Add(new BackgroundColorBlend(f.GetColor(0x3F3F3F), 1));
            }

            return style;
        }
        #endregion

        #region Backstage Buttons
        public static Office2007ButtonItemColorTable GetButtonItemBackstageDefault(ColorFactory factory)
        {
            Office2007ButtonItemColorTable cb = new Office2007ButtonItemColorTable();
            cb.Name = Enum.GetName(typeof(eButtonColor), eButtonColor.Blue);
            cb.Default = new Office2007ButtonItemStateColorTable();
            cb.Default.Text = factory.GetColor(0x000000);

            // Button mouse over
            cb.MouseOver = new Office2007ButtonItemStateColorTable();
            cb.MouseOver.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x527DE0));
            cb.MouseOver.InnerBorder = new LinearGradientColorTable(factory.GetColor(135, 0xFFFFFF));
            cb.MouseOver.TopBackground = new LinearGradientColorTable(factory.GetColor(32, 0x007FFF));
            cb.MouseOver.TopBackgroundHighlight = LinearGradientColorTable.Empty; //new LinearGradientColorTable(Color.FromArgb(192, Color.White), Color.Transparent);
            cb.MouseOver.BottomBackground = new LinearGradientColorTable(factory.GetColor(32, 0x007FFF));
            cb.MouseOver.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.MouseOver.SplitBorder = new LinearGradientColorTable(factory.GetColor(0x527DE0), Color.Empty);
            cb.MouseOver.SplitBorderLight = new LinearGradientColorTable(factory.GetColor(135, 0xFFFFFF), Color.Empty);
            cb.MouseOver.Text = factory.GetColor(0x000000);

            cb.MouseOverSplitInactive = new Office2007ButtonItemStateColorTable();
            cb.MouseOverSplitInactive.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x527DE0));

            // Pressed
            cb.Pressed = new Office2007ButtonItemStateColorTable();
            cb.Pressed.OuterBorder = new LinearGradientColorTable(factory.GetColor(0x527DE0));
            cb.Pressed.InnerBorder = new LinearGradientColorTable(factory.GetColor(192, 0xFFFFFF));
            cb.Pressed.TopBackground = new LinearGradientColorTable(factory.GetColor(32, 0x007FFF));
            cb.Pressed.TopBackgroundHighlight = LinearGradientColorTable.Empty; //new LinearGradientColorTable(Color.FromArgb(192, Color.White), Color.Transparent);
            cb.Pressed.BottomBackground = new LinearGradientColorTable(factory.GetColor(32, 0x007FFF));
            cb.Pressed.BottomBackgroundHighlight = LinearGradientColorTable.Empty;
            cb.Pressed.SplitBorder = new LinearGradientColorTable(factory.GetColor(0x527DE0), Color.Empty);
            cb.Pressed.SplitBorderLight = new LinearGradientColorTable(factory.GetColor(135, 0xFFFFFF), Color.Empty);
            cb.Pressed.Text = factory.GetColor(0x000000);

            // Checked
            cb.Checked = cb.Pressed;

            // Expanded button
            cb.Expanded = cb.MouseOver;

            SetBlueExpandColors(cb, factory);

            return cb;
        }
        #endregion
    }
}
