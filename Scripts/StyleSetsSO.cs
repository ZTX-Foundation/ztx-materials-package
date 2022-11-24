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

            MaterialStyleSetSO styleset = styleSets[styleSetIndex];

            SkinnedMeshRenderer[] skinnedMeshRenderers = target.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                SkinnedMeshRenderer s = skinnedMeshRenderers[i];
                Material[] materials = s.materials;

                for (int j = 0; j < materials.Length; j++)
                {
                    Material m = materials[j];
                    
                    // Replace the material
                    Material mNew = SwapMaterial(m, styleset);
                    if (mNew != null)
                    {
                        if (m.HasProperty("_MainTex"))
                        {
                            Texture tex = m.GetTexture("_MainTex");
                            mNew.SetTexture(styleset.mainTextureName, tex);
                        }
                        else if (m.HasProperty("_BaseTex"))
                        {
                            Texture tex = m.GetTexture("_BaseTex");
                            mNew.SetTexture(styleset.mainTextureName, tex);
                        }

                        if (m.HasProperty("_Color"))
                        {
                            Color color = m.GetColor("_Color");
                            mNew.SetColor(styleset.mainColorName, color);
                        }
                        else if (m.HasProperty("_BaseColor"))
                        {
                            Color color = m.GetColor("_BaseColor");
                            mNew.SetColor(styleset.mainColorName, color);
                        }

                        m = mNew;
                    }
                    
                    materials[j] = m;
                }

                s.materials = materials;
            }
        }

        private Material SwapMaterial(Material m, MaterialStyleSetSO styleSet)
        {
            if (m.name.ToLower().Contains("skin"))
                return styleSet.body;
            if (m.shader.name.ToLower().Contains("softedge"))
                return styleSet.eyelash;
            if (m.name.ToLower().Contains("eye"))
                return styleSet.eye;
            if (m.shader.name.ToLower().Contains("hair"))
                return styleSet.hair;
            if (m.shader.name.ToLower().Contains("transparent"))
                return styleSet.standardTransparent;
            if (m.shader.name.ToLower().Contains("2s"))
                return styleSet.standardTwoSide;
            if (m.shader.name.ToLower().Contains("standard"))
                return styleSet.standard;
            if (m.shader.name.ToLower().Contains("velvet"))
                return styleSet.standard;
            if (m.shader.name.ToLower().Contains("fur"))
                return styleSet.fur;
            Debug.LogError($"No replacing material found for material {m.name} with shader {m.shader.name}");
            return null;
        }
    }
}