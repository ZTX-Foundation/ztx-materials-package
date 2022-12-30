using System;
using UnityEngine;
using UnityEngine.InputSystem;
// using ZTX.Materials;

public class StyleChangeHotkey : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            // RuntimeMaterialReplacer.Instance.NextMaterialSet(ZtxPlayersManager.instance.LocalPlayer.gameObject);
        }
    }
#endif
}
