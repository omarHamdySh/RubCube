using UnityEngine;

namespace SA.iOS.AVFoundation.Internal
{
    /// <summary>
    /// This class is for plugin internal use only
    /// </summary>
    static class ISN_AVLib
    {
        static ISN_iAVAPI s_Api;

        public static ISN_iAVAPI Api
        {
            get
            {
                if (s_Api == null)
                {
                    if (Application.isEditor)
                        s_Api = new ISN_AVEditorAPI();
                    else
                        s_Api = ISN_AVNativeAPI.Instance;
                }

                return s_Api;
            }
        }
    }
}
