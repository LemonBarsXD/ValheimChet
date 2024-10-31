using UnityEngine;

namespace ValheimChet
{
    internal class UserInput
    {
        public static void PollInput() 
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                Vars.menu_toggle = !Vars.menu_toggle;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Vars.killauraEnabled = !Vars.killauraEnabled;
            }

            if (Input.GetKey(KeyCode.End))
            {
                Loader.Unload();
            }

        }
    }
}
