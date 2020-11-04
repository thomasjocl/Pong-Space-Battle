namespace Assets.Common
{
    public static class AppHelper
    { 
        public static void Quit()
        { 
             UnityEditor.EditorApplication.isPlaying = false;  
        }
    }
}
