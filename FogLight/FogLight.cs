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
        public override string GitHubLink => "";
        public override WhenToInit WhenToInit => WhenToInit.InGame;
        public override List<(string, string, string)> Dependencies => new List<(string, string, string)>()
        {
            ("JaLoader", "Leaxx", "2.0.0")
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

            ModHelper.Instance.CreateIconForExtra(lightObject, new Vector3(), lightObject.transform.localScale, new Vector3(70, 0, -60), "FogLight");

            CustomObjectsManager.Instance.RegisterObject(ModHelper.Instance.CreateExtraObject(lightObject, BoxSizes.Small, "Fog Light", "Fog light", 50, 1, "FogLight", AttachExtraTo.Body), "FogLight");
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

            if (Input.GetKeyDown(KeyCode.V))
            {
                Console.Instance.Log(lightMaterial.name);
                Console.Instance.Log(lightMaterial.shader.name);
                Material[] mats = lightObject.GetComponent<MeshRenderer>().materials;
                mats[1] = lightMaterial;
                lightObject.GetComponent<MeshRenderer>().materials = mats;
                lightObject.transform.Find("LightHolder").gameObject.SetActive(true);
            }
            
            if(Input.GetKeyUp(KeyCode.V))
            {
                Material[] mats = lightObject.GetComponent<MeshRenderer>().materials;
                mats[1] = lightObject.GetComponent<MeshRenderer>().materials[0];
                lightObject.GetComponent<MeshRenderer>().materials = mats;
                lightObject.transform.Find("LightHolder").gameObject.SetActive(false);
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
