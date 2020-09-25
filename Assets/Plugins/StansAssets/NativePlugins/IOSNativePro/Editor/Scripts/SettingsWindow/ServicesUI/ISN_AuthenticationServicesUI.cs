using System.Collections.Generic;
using SA.Foundation.Editor;
using UnityEngine;

namespace SA.iOS
{
    class ISN_AuthenticationServicesUI : ISN_ServiceSettingsUI
    {
        public override void OnAwake()
        {
            base.OnAwake();
            AddFeatureUrl("Sign in with Apple", "https://github.com/StansAssets/com.stansassets.ios-native/wiki/Sign-in-with-Apple");
        }

        public override string Title => "Authentication Services";

        public override string Description => "Make it easy for users to log into apps and services.";

        protected override Texture2D Icon => SA_EditorAssets.GetTextureAtPath(ISN_Skin.IconsPath + "CoreLocation_icon.png");

        public override SA_iAPIResolver Resolver => ISN_Preprocessor.GetResolver<ISN_AuthenticationServicesResolver>();

        protected override IEnumerable<string> SupportedPlatforms => new List<string>() { "iOS", "tvOS" };

        protected override void OnServiceUI() { }
    }
}
