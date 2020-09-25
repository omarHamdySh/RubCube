using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;

namespace SA.iOS
{
    class ISN_ReplayKitUI : ISN_ServiceSettingsUI
    {
        public override void OnAwake()
        {
            base.OnAwake();

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.ios-native/wiki/Getting-Started-(Replay-Kit)");
            AddFeatureUrl("Capturing & Sharing", "https://github.com/StansAssets/com.stansassets.ios-native/wiki/Capturing-&-Sharing");
        }

        public override string Title => "Replay Kit";

        public override string Description => "Record video from the screen, and audio from the app and microphone.";

        protected override Texture2D Icon => SA_EditorAssets.GetTextureAtPath(ISN_Skin.IconsPath + "ReplayKit_icon.png");

        public override SA_iAPIResolver Resolver => ISN_Preprocessor.GetResolver<ISN_ReplayKitResolver>();

        protected override IEnumerable<string> SupportedPlatforms => new List<string>() { "iOS" };

        protected override void OnServiceUI() { }
    }
}
