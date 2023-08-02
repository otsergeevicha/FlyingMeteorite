﻿#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class DeleteComponentsFromBuild : IProcessSceneWithReport
    {
        public int callbackOrder { get { return 0; } }

        private readonly string[] _uselessComponentNames = new string[]
        {
            "PolybrushMesh",
            "SplineComputer",
            "SplineMesh",
            "AnotherUselessComponentThatMightNotExist",
            "TweakThisListOfComponentsInCodeDirectly"
        };

        public void OnProcessScene(Scene scene, BuildReport report)
        {
            foreach (GameObject rootGameObject in scene.GetRootGameObjects())
            foreach(string uselessComponentName in _uselessComponentNames)
                DeleteComponents(rootGameObject.transform, uselessComponentName);

            Object[] deletionComponents = Resources.FindObjectsOfTypeAll(typeof(DeleteSpecificComponentsFromBuild));
            
            foreach (DeleteSpecificComponentsFromBuild deletionComponent in deletionComponents)
                deletionComponent.DeleteComponents();
        }

        private void DeleteComponents(Transform transform, string componentName)
        {
            var uselessComponent = transform.GetComponent(componentName);
            if (uselessComponent != null)
                Object.DestroyImmediate(uselessComponent);

            foreach (Transform childTransform in transform)
                DeleteComponents(childTransform, componentName);
        }
    }
}
#endif