using UnityEngine;
using UnityEngine.InputSystem;

namespace ValheimChet
{
    internal class UserInput
    {
        public static void PollInput() 
        {
            // Here you add your own keybinds for your own build
            // See Vars.cs under `bools` for options
            /*
              Ex:
              if (Input.GetKeyDown(Key.F))
              {
                  Vars.esp_toggle = !Vars.esp_toggle;
              }
             */

            if (Input.GetKeyDown(KeyCode.Insert))
            {
                Vars.menu_toggle = !Vars.menu_toggle;
            }

            if (Input.GetKey(KeyCode.End))
            {
                Loader.Unload();
            }

        }
    }
}
