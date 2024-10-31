// Author: Upwn
// Ver: 1.0.0


using UnityEngine;

namespace ValheimChet
{
    public class Loader
    {
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<Chet>();
            Loader.Load.name = "Chet";
            UnityEngine.GameObject.DontDestroyOnLoad(Loader.Load);
            PatchEntryPoint.Init();
        }

        public static void Unload()
        {
            _Unload();
        }

        private static void _Unload()
        {
            GameObject.Destroy(Loader.Load);
        }

        private static GameObject Load;
    }

}
