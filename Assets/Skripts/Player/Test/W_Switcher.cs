using UnityEngine;

public class W_Switcher : MonoBehaviour
{
    private MonoBehaviour[] weaponScripts; // Array der Skripte f�r die verschiedenen Waffen
    private int currentWeaponIndex = 0; // Index des aktuellen Waffen-Skripts

    void Start()
    {
        // Hole alle Skripte vom aktuellen GameObject
        weaponScripts = GetComponents<MonoBehaviour>();

        // Deaktiviere alle Waffen-Skripte au�er dem ersten
        for (int i = 1; i < weaponScripts.Length; i++)
        {
            DisableScript(weaponScripts[i]);
        }
    }

    void Update()
    {
        // Waffenwechsel mit den Tasten 1, 2, 3, ...
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weaponScripts.Length > 1)
        {
            SwitchWeapon(1);
        }
        // F�ge hier weitere Tasten f�r zus�tzliche Waffen hinzu

        // Beispiel: Waffenwechsel mit dem Mausrad
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // Wenn das Mausrad nach oben gedreht wird, wechsle zur n�chsten Waffe
            if (scroll > 0f)
            {
                SwitchWeapon((currentWeaponIndex + 1) % weaponScripts.Length);
            }
            // Wenn das Mausrad nach unten gedreht wird, wechsle zur vorherigen Waffe
            else if (scroll < 0f)
            {
                SwitchWeapon((currentWeaponIndex - 1 + weaponScripts.Length) % weaponScripts.Length);
            }
        }

        // Weitere Logik f�r die Waffensteuerung hier hinzuf�gen...
    }

    void SwitchWeapon(int newIndex)
    {
        // Deaktiviere das aktuelle Waffen-Skript
        DisableScript(weaponScripts[currentWeaponIndex]);

        // Aktiviere das neue Waffen-Skript
        EnableScript(weaponScripts[newIndex]);

        // Setze den Index des aktuellen Waffen-Skripts auf den neuen Index
        currentWeaponIndex = newIndex;
    }

    void DisableScript(MonoBehaviour script)
    {
        script.enabled = false;
    }

    void EnableScript(MonoBehaviour script)
    {
        script.enabled = true;
    }
}