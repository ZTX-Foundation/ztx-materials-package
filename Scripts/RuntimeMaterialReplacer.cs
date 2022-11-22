using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZTX.Materials
{
    public class RuntimeMaterialReplacer : MonoBehaviour
    {
        [SerializeField] private StyleSetsSO styleSets;

        public static RuntimeMaterialReplacer Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void SetInitialMaterials(GameObject target)
        {
            SkinnedMeshRenderer[] skinnedMeshRenderers = target.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                SkinnedMeshRenderer s = skinnedMeshRenderers[i];
                Material[] materials = s.materials;

                for (int j = 0; j < materials.Length; j++)
                {
                    Material m = materials[j];
                        
                    // Replace the material
                    Material mNew = SwapMaterial(m, styleSets.styleSets[0]);
                    if (mNew != null) 
                        m = mNew;

                    materials[j] = m;
                }

                s.materials = materials;
            }
        }
        
        private Material SwapMaterial(Material m, MaterialStyleSetSO styleSet)
        {
            Debug.Log($"{m.shader.name}");
            if (m.shader.name.ToLower().Contains("skin"))
                return styleSet.body;
            if (m.shader.name.ToLower().Contains("softedge"))
                return styleSet.eyelash;
            if (m.shader.name.ToLower().Contains("eye"))
                return styleSet.eye;
            if (m.shader.name.ToLower().Contains("hair"))
                return styleSet.hair;
            if (m.shader.name.ToLower().Contains("standardtransparent"))
                return styleSet.standardTransparent;
            if (m.shader.name.ToLower().Contains("2s"))
                return styleSet.standardTwoSide;
            if (m.shader.name.ToLower().Contains("standard"))
                return styleSet.standard;
            if (m.shader.name.ToLower().Contains("velvet"))
                return styleSet.standard;
            if (m.shader.name.ToLower().Contains("fur"))
                return styleSet.fur;
            Debug.LogError("No replacing material found");
            return null;
        }
    }
}
