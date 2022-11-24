using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZTX.Materials
{
    [CreateAssetMenu(fileName = "Material Style Set", menuName = "ZTX/Material Style Set", order = 2)]
    public class MaterialStyleSetSO : ScriptableObject
    {
        [Header("Body")]
        public Material body;
        public Material eye;
        public Material eyelash;
        public Material hair;
        
        [Header("Clothing")]
        public Material standard;
        public Material standardTransparent;
        public Material standardTwoSide;
        public Material fur;

        [Header("Shader details")] public string mainTextureName;
        public string mainColorName;
    }
}
