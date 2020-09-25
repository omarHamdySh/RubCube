using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;

namespace SA.iOS
{
    class ISN_PhotosUI : ISN_ServiceSettingsUI
    {
        public override void OnAwake()
        {
            base.OnAwake();

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ios-native/wiki/Getting-Started-(Photos)");
            AddFeatureUrl("Photos Permissions", "https://github.com/StansAssets/com.stansassets.ios-native/wiki/Photos-Permissions");
            AddFeatureUrl("API Reference", "https://api.stansassets.com/ios-native/SA.iOS.Photos.html");
        }

        public override string Title => "Photos";

        public override string Description => "Work with image and video assets managed by the Photos app.";

        protected override Texture2D Icon => SA_EditorAssets.GetTextureAtPath(ISN_Skin.IconsPath + "Photos_icon.png");

        public override SA_iAPIResolver Resolver => ISN_Preprocessor.GetResolver<ISN_PhotosResolver>();

        protected override IEnumerable<string> SupportedPlatforms => new List<string>() { "iOS", "Unity Editor" };

        protected override void OnServiceUI() { }
    }
}
