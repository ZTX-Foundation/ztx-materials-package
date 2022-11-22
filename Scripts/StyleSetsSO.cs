using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZTX.Materials
{
    [CreateAssetMenu(fileName = "Style Sets", menuName = "ZTX/Style Sets", order = 1)]
    public class StyleSetsSO : ScriptableObject
    {
        public MaterialStyleSetSO[] styleSets;

        // Swap all the relevant materials of target gameObject to the new material styleset
        public void SetMaterials(GameObject target, int styleSetIndex)
        {
            if (styleSetIndex > styleSets.Length)
            {
                Debug.LogError("styleSetIndex out of bounds");
            }

            MaterialStyleSetSO styleSet = styleSets[styleSetIndex];

            SkinnedMeshRenderer[] skinnedMeshRenderers = target.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                SkinnedMeshRenderer s = skinnedMeshRenderers[i];
                Material[] materials = s.materials;

                for (int j = 0; j < materials.Length; j++)
                {
                    Material m = materials[j];
                        
                    // Replace the material
                    Material mNew = SwapMaterial(m, styleSet);
                    if (mNew != null) 
                        m = mNew;
                    
                    materials[j] = m;
                }

                s.materials = materials;
            }
        }

        private Material SwapMaterial(Material m, MaterialStyleSetSO styleSet)
        {
            if (m.name.Contains("body"))
                return styleSet.body;
            if (m.name.Contains("eyelash"))
                return styleSet.eyelash;
            if (m.name.Contains("eye"))
                return styleSet.eye;
            if (m.name.Contains("hair"))
                return styleSet.hair;
            if (m.name.Contains("standardTransparent"))
                return styleSet.standardTransparent;
            if (m.name.Contains("standardTwoSide"))
                return styleSet.standardTwoSide;
            if (m.name.Contains("standard"))
                return styleSet.standard;
            if (m.name.Contains("fur"))
                return styleSet.fur;
            Debug.LogError($"No replacing material found for material {m.name} with shader {m.shader.name}");
            return null;
        }
    }
}