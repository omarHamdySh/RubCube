using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SA.Foundation.Editor;

namespace SA.iOS
{
    class ISN_AVKitUI : ISN_ServiceSettingsUI
    {
        public override void OnAwake()
        {
            base.OnAwake();

            AddFeatureUrl("AVPlayer View", "https://github.com/StansAssets/com.stansassets.ios-native/wiki/AVPlayerViewController");
            AddFeatureUrl("Video Player", "https://github.com/StansAssets/com.stansassets.ios-native/wiki/AVPlayerViewController");
        }

        public override string Title => "AVKit";

        public override string Description => " The AVKit framework provides a high-level interface for playing video content..";

        protected override Texture2D Icon => SA_EditorAssets.GetTextureAtPath(ISN_Skin.IconsPath + "AVKit_icon.png");

        public override SA_iAPIResolver Resolver => ISN_Preprocessor.GetResolver<ISN_AVKitResolver>();

        protected override void OnServiceUI() { }
    }
}
