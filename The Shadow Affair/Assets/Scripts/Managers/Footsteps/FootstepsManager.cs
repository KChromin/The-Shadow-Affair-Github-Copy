using System;
using System.Collections.Generic;
using SmugRag.Templates.Singletons;
using UnityEngine;
using UnityEngine.Serialization;

namespace SmugRag.Managers.Footsteps
{
    public class FootstepsManager : SingletonPersistentManager<FootstepsManager>
    {
        public TextureLists textureLists;

        public TextureNameLists Names { get; private set; }

        [Serializable]
        public struct TextureLists
        {
            [Header("Concrete")]
            public List<Texture> concreteTextures;

            [Header("Wood")]
            public List<Texture> woodTextures;

            [Header("Grass")]
            public List<Texture> grassTextures;

            [Header("Gravel")]
            public List<Texture> gravelTextures;

            [Header("Stone")]
            public List<Texture> stoneTextures;
        }

        public class TextureNameLists
        {
            [Header("Concrete")]
            public List<string> ConcreteTextures = new List<string>();

            [Header("Wood")]
            public List<string> WoodTextures = new List<string>();

            [Header("Grass")]
            public List<string> GrassTextures = new List<string>();

            [Header("Gravel")]
            public List<string> GravelTextures = new List<string>();

            [Header("Stone")]
            public List<string> StoneTextures = new List<string>();
        }

        protected override void Awake()
        {
            base.Awake();

            MaterialToMaterialNames();
        }

        private void MaterialToMaterialNames()
        {
            Names = new TextureNameLists();

            foreach (Texture concrete in textureLists.concreteTextures)
            {
                Names.ConcreteTextures.Add(concrete.name);
            }

            foreach (Texture wood in textureLists.woodTextures)
            {
                Names.WoodTextures.Add(wood.name);
            }

            foreach (Texture grass in textureLists.grassTextures)
            {
                Names.GrassTextures.Add(grass.name);
            }

            foreach (Texture gravel in textureLists.gravelTextures)
            {
                Names.GravelTextures.Add(gravel.name);
            }

            foreach (Texture stone in textureLists.stoneTextures)
            {
                Names.StoneTextures.Add(stone.name);
            }
        }
    }
}