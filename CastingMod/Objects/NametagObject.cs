using System;
using TMPro;
using UnityEngine;

namespace vynscastingmod.Objects
{
    public class NametagObject : MonoBehaviour
    {
        public void Awake()
        {
            attachedRig = GetComponentInParent<VRRig>();
            textObj = new GameObject().AddComponent<TextMeshPro>();
            textObj.font = GameObject.FindObjectOfType<TMP_FontAsset>(); // should just get default gorilla tag font, fingers crossed
            textObj.fontStyle = FontStyles.Bold;
            textObj.fontSize = 8;
            textObj.alignment = TextAlignmentOptions.CenterGeoAligned;
            textObj.transform.localScale = Vector3.one * 0.2f;
        }

        public void OnDestroy()
        {
            Destroy(textObj.gameObject);
        }

        public void LateUpdate()
        {
            if (!Main.instance.nametagsEnabled)
            {
                Destroy(textObj.gameObject);
                Destroy(this);
            }
            textObj.transform.position = attachedRig.transform.position + (Vector3.up * 0.4f);
            textObj.transform.rotation = Main.instance.camera.transform.rotation;
            textObj.text = attachedRig.playerNameVisible;
            textObj.color = attachedRig.playerColor;
        }

        public TextMeshPro textObj;
        public VRRig attachedRig;
    }
}