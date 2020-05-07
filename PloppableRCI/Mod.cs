using ICities;
using CitiesHarmony.API;


namespace PloppableRICO
{
    public class PloppableRICOMod : IUserMod
    {
        public static string version = "2.0.1";

        public string Name => "RICO Revisited " + version;

        public string Description => Translations.GetTranslation("Allows Plopping of RICO Buildings (fork of AJ3D's original with bugfixes and new features)");


        public void OnEnabled()
        {
            // Apply Harmony patches via Cities Harmony.
            // Called here instead of OnCreated to allow the auto-downloader to do its work prior to launch.
            HarmonyHelper.EnsureHarmonyInstalled();
        }

        public void OnDisabled()
        {
            // Unapply Harmony patches via Cities Harmony.
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.UnpatchAll();
        }
    }
}
