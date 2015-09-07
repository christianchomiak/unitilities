// <summary>
/// ApplicationHelper v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Shortcuts for common queries to Unity's Application.
/// </summary>

using UnityEngine;

namespace Unitilities
{

    public static class ApplicationHelper
    {
        
        #region Platforms

        public static bool PlatformIsDesktop
        {
            get
            {
                return ApplicationHelper.PlatformIsEditor ||
                    ApplicationHelper.PlatformIsDesktopStandalone ||
                    ApplicationHelper.PlatformIsWeb;
            }
        }

        public static bool PlatformIsDesktopStandalone
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsPlayer ||
                   Application.platform == RuntimePlatform.LinuxPlayer ||
                   Application.platform == RuntimePlatform.OSXPlayer ||

                    #if UNITY_5
                        Application.platform == RuntimePlatform.WSAPlayerX86 ||
                        Application.platform == RuntimePlatform.WSAPlayerX64;
                    #else
                        Application.platform == RuntimePlatform.MetroPlayerX86||
                        Application.platform == RuntimePlatform.MetroPlayerX64;
                    #endif

            }
        }

        public static bool PlatformIsEditor
        {
            get
            {
                return Application.isEditor;

                /*return Application.platform == RuntimePlatform.WindowsEditor ||
                   Application.platform == RuntimePlatform.OSXEditor;*/
            }
        }

        public static bool PlatformIsWeb
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsWebPlayer ||
                        #if UNITY_5
                         Application.platform == RuntimePlatform.WebGLPlayer ||
                        #endif
                    Application.platform == RuntimePlatform.OSXWebPlayer;
            }
        }

        public static bool PlatformIsMobile
        {
            get
            {
                return Application.platform == RuntimePlatform.Android ||
                       Application.platform == RuntimePlatform.IPhonePlayer ||
                       Application.platform == RuntimePlatform.BlackBerryPlayer ||
                       Application.platform == RuntimePlatform.WP8Player ||
                       
                       #if UNITY_5
                        Application.platform == RuntimePlatform.WSAPlayerARM;
                       #else
                        Application.platform == RuntimePlatform.MetroPlayerARM;
                       #endif
            }
        }



        #endregion

    }

}