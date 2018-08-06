// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Joyces.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

		#endregion

		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

        public static string RefreshToken
        {
            get { return AppSettings.GetValueOrDefault("RefreshToken", null); }
            set { AppSettings.AddOrUpdateValue("RefreshToken", value); }
        }

        public static string AccessToken
        {
            get { return AppSettings.GetValueOrDefault("AccessToken", null); }
            set { AppSettings.AddOrUpdateValue("AccessToken", value); }
        }

        public static string UserEmail
        {
            get { return AppSettings.GetValueOrDefault("UserEmail", null); }
            set { AppSettings.AddOrUpdateValue("UserEmail", value); }
        }
        //How long in seconds until the access token expires (string convert to int)
        public static string AccessTokenExpiration
        {
            get { return AppSettings.GetValueOrDefault("TokenExpires", null); }
            set { AppSettings.AddOrUpdateValue("TokenExpires", value); }
        }

        public static string UserAccountNo
        {
            get { return AppSettings.GetValueOrDefault("UserAccountNo", null); }
            set { AppSettings.AddOrUpdateValue("UserAccountNo", value); }
        }

        public static string PushDeviceToken
        {
            get { return AppSettings.GetValueOrDefault("PushDeviceToken", null); }
            set { AppSettings.AddOrUpdateValue("PushDeviceToken", value); }
        }

        //Temporary without db to keep variables
        public static string MoreJson
        {
            get { return AppSettings.GetValueOrDefault("MoreJson", null); }
            set { AppSettings.AddOrUpdateValue("MoreJson", value); }
        }
        public static string OfferJson
        {
            get { return AppSettings.GetValueOrDefault("OfferJson", null); }
            set { AppSettings.AddOrUpdateValue("OfferJson", value); }
        }
        public static string NewsJson
        {
            get { return AppSettings.GetValueOrDefault("NewsJson", null); }
            set { AppSettings.AddOrUpdateValue("NewsJson", value); }
        }

        public static string CustomerJson
        {
            get { return AppSettings.GetValueOrDefault("CustomerJson", null); }
            set { AppSettings.AddOrUpdateValue("CustomerJson", value); }
        }

        public static string MainFont
        {
            get { return AppSettings.GetValueOrDefault("MainFont", null); }
            set { AppSettings.AddOrUpdateValue("MainFont", value); }
        }
    }
}