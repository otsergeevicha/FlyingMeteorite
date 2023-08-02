#if UNITY_EDITOR
using System.Collections.Generic;
using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure
{
    public class DeleteSpecificComponentsFromBuild : MonoCache
    {
        [SerializeField]
        private List<Component> _componentsToDelete = new List<Component>();

        public void DeleteComponents()
        {
            foreach (Component componentToDelete in _componentsToDelete)
                DestroyImmediate(componentToDelete);

            DestroyImmediate(this);
        }
    }
}
#endif