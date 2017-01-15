﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEditor;
using UnityEngine;

namespace Tiled2Unity
{
    // Handled a texture being imported
    partial class ImportTiled2Unity
    {
        public void TextureImported(string texturePath)
        {
            // This is a fixup method due to materials and textures, under some conditions, being imported out of order
            Texture2D texture2d = AssetDatabase.LoadAssetAtPath(texturePath, typeof(Texture2D)) as Texture2D;
            Material material = AssetDatabase.LoadAssetAtPath(GetMaterialAssetPath(texturePath), typeof(Material)) as Material;
            if (material == null)
            {
                Debug.LogError(String.Format("Error importing texture '{0}'. Could not find material. Try re-importing Tiled2Unity/Imported/[MapName].tiled2unity.xml file", texturePath));
            }
            else
            {
                material.SetTexture("_MainTex", texture2d);
            }
        }
    }
}
