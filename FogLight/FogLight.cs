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
        public override string ModAuthor => "Leaxx, itisgt";
        public override string ModDescription => "Adds optional extra fog lights for the Laika.";
        public override string ModVersion => "2.0";
        public override string GitHubLink => "https://github.com/Jalopy-Mods/FogLight";
        public override WhenToInit WhenToInit => WhenToInit.InGame;
        public override List<(string, string, string)> Dependencies => new List<(string, string, string)>()
        {
            ("JaLoader", "Leaxx", "2.0.1")
        };

        public override bool UseAssets => true;

        private GameObject redFogLight;
        private GameObject whiteFogLight;

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

            redFogLight = LoadAsset<GameObject>("foglights", "RedFogLight", "", ".prefab");
            whiteFogLight = LoadAsset<GameObject>("foglights", "WhiteFogLight", "", ".prefab");

            redFogLight.GetComponent<MeshRenderer>().material = whiteFogLight.GetComponent<MeshRenderer>().material = ModHelper.Instance.defaultEngineMaterial;
            redFogLight = Instantiate(redFogLight, ModHelper.Instance.laika.transform.Find("TweenHolder/Frame"));
            whiteFogLight = Instantiate(whiteFogLight, ModHelper.Instance.laika.transform.Find("TweenHolder/Frame"));

            redFogLight.transform.localPosition = new Vector3(-5.7f, -3.9f, -2);
            whiteFogLight.transform.localPosition = new Vector3(-5.7f, -3.9f, 2);
            redFogLight.transform.localEulerAngles = whiteFogLight.transform.localEulerAngles = new Vector3(-90, 0, 0);
            redFogLight.transform.localScale = whiteFogLight.transform.localScale = new Vector3(35f, 35f, 35f);

            ModHelper.Instance.CreateIconForExtra(redFogLight, new Vector3(), redFogLight.transform.localScale, new Vector3(-100, 0, -60), "RedFogLight");
            ModHelper.Instance.CreateIconForExtra(whiteFogLight, new Vector3(), whiteFogLight.transform.localScale, new Vector3(-100, 0, -60), "WhiteFogLight");

            CustomObjectsManager.Instance.RegisterObject(ModHelper.Instance.CreateExtraObject(redFogLight, BoxSizes.Small, "Red Fog Light", "A red fog light. Helps visibility in foggy or dark situations.", 30, 1, "RedFogLight", AttachExtraTo.Body), "RedFogLight");
            CustomObjectsManager.Instance.RegisterObject(ModHelper.Instance.CreateExtraObject(whiteFogLight, BoxSizes.Small, "White Fog Light", "A white fog light. Helps visibility in foggy or dark situations.", 30, 1, "WhiteFogLight", AttachExtraTo.Body), "WhiteFogLight");
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
}
