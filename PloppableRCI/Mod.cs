using ICities;
using ColossalFramework.UI;
using CitiesHarmony.API;


namespace PloppableRICO
{
    /// <summary>
    /// The base mod class for instantiation by the game.
    /// </summary>
    public class PloppableRICOMod : IUserMod
    {
        public static string ModName => "RICO Revisited";
        public static string Version => "2.2.2";

        public string Name => ModName + " " + Version;
        public string Description => Translations.Translate("PRR_DESCRIPTION");


        /// <summary>
        /// Called by the game when the mod is enabled.
        /// </summary>
        public void OnEnabled()
        {
            // Apply Harmony patches via Cities Harmony.
            // Called here instead of OnCreated to allow the auto-downloader to do its work prior to launch.
            HarmonyHelper.DoOnHarmonyReady(() => Patcher.PatchAll());
        }


        /// <summary>
        /// Called by the game when the mod is disabled.
        /// </summary>
        public void OnDisabled()
        {
            // Unapply Harmony patches via Cities Harmony.
            if (HarmonyHelper.IsHarmonyInstalled)
            {
                Patcher.UnpatchAll();
            }
        }


        /// <summary>
        /// Called by the game when the mod options panel is setup.
        /// </summary>
        public void OnSettingsUI(UIHelperBase helper)
        {
            // Read configuration file.
            SettingsFile settingsFile = Configuration<SettingsFile>.Load();

            // General options.
            UIHelperBase otherGroup = helper.AddGroup(" ");

            UIDropDown translationDropDown = (UIDropDown)otherGroup.AddDropdown(Translations.Translate("TRN_CHOICE"), Translations.LanguageList, Translations.Index, (value) =>
            {
                Translations.Index = value;
                Configuration<SettingsFile>.Save();
            });
            translationDropDown.autoSize = false;
            translationDropDown.width = 270f;

            // Add logging checkbox.
            otherGroup.AddCheckbox(Translations.Translate("PRR_OPTION_MOREDEBUG"), settingsFile.DebugLogging, isChecked =>
            {
                Settings.debugLogging = isChecked;
                settingsFile.DebugLogging = isChecked;
                Configuration<SettingsFile>.Save();
            });

            // Add reset on load checkbox.
            otherGroup.AddCheckbox(Translations.Translate("PRR_OPTION_FORCERESET"), settingsFile.ResetOnLoad, isChecked =>
            {
                settingsFile.ResetOnLoad = isChecked;
                Configuration<SettingsFile>.Save();
            });

            // Add thumbnail background dropdown.
            otherGroup.AddDropdown(Translations.Translate("PRR_OPTION_THUMBACK"), Settings.ThumbBackNames, settingsFile.ThumbBacks, (value) =>
            {
                Settings.thumbBacks = value;
                settingsFile.ThumbBacks = value;
                Configuration<SettingsFile>.Save();
            });

            // Add regenerate thumbnails button.
            otherGroup.AddButton(Translations.Translate("PRR_OPTION_REGENTHUMBS"), () => PloppableTool.Instance.RegenerateThumbnails());

            // Add speed boost checkbox.
            UIHelperBase speedGroup = helper.AddGroup(Translations.Translate("PRR_OPTION_SPDHDR"));
            speedGroup.AddCheckbox(Translations.Translate("PRR_OPTION_SPEED"), settingsFile.SpeedBoost, isChecked =>
            {
                Settings.speedBoost = isChecked;
                settingsFile.SpeedBoost = isChecked;
                Configuration<SettingsFile>.Save();
            });

            // Add fast thumbnails checkbox.
            UIHelperBase fastGroup = helper.AddGroup(Translations.Translate("PRR_OPTION_FASTHDR"));
            fastGroup.AddCheckbox(Translations.Translate("PRR_OPTION_FASTHUMB"), settingsFile.FastThumbs, isChecked =>
            {
                Settings.fastThumbs = isChecked;
                settingsFile.FastThumbs = isChecked;
                Configuration<SettingsFile>.Save();
            });
        }
    }
}
