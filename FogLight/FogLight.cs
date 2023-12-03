using JaLoader;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace FogLight
{
    public class FogLight : Mod
    {
        public override string ModID => "FogLight";
        public override string ModName => "Fog Light";
        public override string ModAuthor => "Leaxx";
        public override string ModDescription => "Adds an optional extra fog light for the Laika.";
        public override string ModVersion => "1.0";
        public override string GitHubLink => "https://github.com/Jalopy-Mods/FogLight";
        public override WhenToInit WhenToInit => WhenToInit.InGame;
        public override List<(string, string, string)> Dependencies => new List<(string, string, string)>()
        {
            ("JaLoader", "Leaxx", "2.0.1")
        };

        public override bool UseAssets => true;

        private Material lightMaterial;
        private GameObject lightObject;

        public override void EventsDeclaration()
        {
            base.EventsDeclaration();
        }

        public override void SettingsDeclaration()
        {
            base.SettingsDeclaration();
        }

        public override void CustomObjectsRegistration()
        {
            base.CustomObjectsRegistration();
            lightObject = LoadAsset<GameObject>("foglight", "fogLightFinal", "", ".prefab");
            lightMaterial = LoadAsset<Material>("foglight", "LightMaterial", "", ".mat");

            lightMaterial = new Material(lightMaterial);
            lightObject.GetComponent<MeshRenderer>().materials[0] = lightObject.GetComponent<MeshRenderer>().materials[1] = ModHelper.Instance.defaultEngineMaterial;
            lightObject = Instantiate(lightObject, ModHelper.Instance.laika.transform.Find("TweenHolder/Frame"));
            lightObject.transform.localPosition = new Vector3(-5.625f, -3.85f, 2f);
            lightObject.transform.localEulerAngles = new Vector3(85, 95, -90);
            lightObject.transform.localScale = new Vector3(3f, 8f, 12f);

            var script = lightObject.AddComponent<FogLightObject>();
            script.lightMaterial = lightMaterial;

            ModHelper.Instance.CreateIconForExtra(lightObject, new Vector3(), lightObject.transform.localScale, new Vector3(70, 0, -60), "FogLight");

            CustomObjectsManager.Instance.RegisterObject(ModHelper.Instance.CreateExtraObject(lightObject, BoxSizes.Small, "Fog Light", "A fog light. Helps visibility in foggy or dark situations.", 50, 1, "FogLight", AttachExtraTo.Body), "FogLight");
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }

    public class FogLightObject : MonoBehaviour
    {
        private CarLogicC carLogic;
        public Material lightMaterial;
        private MeshRenderer renderer;

        private void Start()
        {
            carLogic = FindObjectOfType<CarLogicC>();
            renderer = gameObject.GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (carLogic.headlightsOn)
            {
                Material[] mats = renderer.materials;
                mats[1] = lightMaterial;
                renderer.materials = mats;
                gameObject.transform.Find("LightHolder").gameObject.SetActive(true);
            }
            else
            {
                Material[] mats = renderer.materials;
                mats[1] = renderer.materials[0];
                renderer.materials = mats;
                gameObject.transform.Find("LightHolder").gameObject.SetActive(false);
            }
        }
    }
}
