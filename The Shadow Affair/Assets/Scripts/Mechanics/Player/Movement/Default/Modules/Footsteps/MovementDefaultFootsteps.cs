using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using SmugRag.Mechanics.Tags;
using SmugRag.Managers.Footsteps;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultFootsteps
    {
        private MovementDefaultActions _actions;
        private MovementDefaultFootstepsData _data;
        private Transform _footstepsOrigin;
        private MovementDefaultCasesData _cases;
        private LayerMask _groundLayers;
        private RaycastHit _hit;

        public void Setup(MovementDefaultActions actions, MovementDefaultFootstepsData data, Transform footstepsOrigin, MovementDefaultCasesData cases, LayerMask groundLayers)
        {
            _actions = actions;
            _data = data;
            _footstepsOrigin = footstepsOrigin;
            _cases = cases;
            _groundLayers = groundLayers;

            SubscribeToEvents();
        }

        private void ExecuteLanding(float intensity)
        {
            Vector3 castPosition = _footstepsOrigin.position;
            PlayFootstep(castPosition, GetCurrentSoundLanding(GetCurrentGroundType(castPosition)));
        }

        private void ExecuteFootstep(bool rightFoot)
        {
            Vector3 castPosition = _footstepsOrigin.position;

            if (rightFoot)
            {
                  castPosition += _data.footstepsOffsetXZ;
            }
            else
            {
                  castPosition -= _data.footstepsOffsetXZ;
            }

            PlayFootstep(castPosition, GetCurrentSoundFootsteps(GetCurrentGroundType(castPosition), _cases.isRunning));
        }

        private FootstepsGroundType GetCurrentGroundType(Vector3 castPosition)
        {
            FootstepsGroundType groundType;

            if (Physics.Raycast(castPosition, Vector3.down, out _hit, 0.2f, _groundLayers, QueryTriggerInteraction.Ignore))
            {
                if (_hit.transform.TryGetComponent(out FootstepsTypeTag tagComponent))
                {
                    groundType = tagComponent.groundType;
                }
                else if (_hit.transform.TryGetComponent(out Terrain hitTerrain))
                {
                    groundType = CheckFootstepsManager(DetectTextureOnTerrain(hitTerrain, _hit.point));
                }
                else
                {
                    groundType = FootstepsGroundType.Default;
                }
            }
            else
            {
                groundType = FootstepsGroundType.Default;
            }

            return groundType;
        }

        private FootstepsGroundType CheckFootstepsManager(string materialName)
        {
            if (FootstepsManager.Instance.Names.ConcreteTextures.Contains(materialName))
            {
                return FootstepsGroundType.Concrete;
            }

            if (FootstepsManager.Instance.Names.WoodTextures.Contains(materialName))
            {
                return FootstepsGroundType.Wood;
            }

            if (FootstepsManager.Instance.Names.GrassTextures.Contains(materialName))
            {
                return FootstepsGroundType.Grass;
            }

            if (FootstepsManager.Instance.Names.GravelTextures.Contains(materialName))
            {
                return FootstepsGroundType.Gravel;
            }

            if (FootstepsManager.Instance.Names.StoneTextures.Contains(materialName))
            {
                return FootstepsGroundType.Stone;
            }

            return FootstepsGroundType.Default;
        }

        private EventInstance GetCurrentSoundFootsteps(FootstepsGroundType groundType, bool isRunning)
        {
            if (isRunning)
            {
                switch (groundType)
                {
                    case FootstepsGroundType.Default:
                        return RuntimeManager.CreateInstance(_data.footstepsDefault);

                    case FootstepsGroundType.Concrete:
                        return RuntimeManager.CreateInstance(_data.footstepsConcreteRun);

                    case FootstepsGroundType.Wood:
                        return RuntimeManager.CreateInstance(_data.footstepsWoodRun);

                    case FootstepsGroundType.Grass:
                        return RuntimeManager.CreateInstance(_data.footstepsGrass);

                    case FootstepsGroundType.Gravel:
                        return RuntimeManager.CreateInstance(_data.footstepsGravel);

                    case FootstepsGroundType.Stone:
                        return RuntimeManager.CreateInstance(_data.footstepsStone);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(groundType), groundType, null);
                }
            }
            else
            {
                switch (groundType)
                {
                    case FootstepsGroundType.Default:
                        return RuntimeManager.CreateInstance(_data.footstepsDefault);

                    case FootstepsGroundType.Concrete:
                        return RuntimeManager.CreateInstance(_data.footstepsConcreteWalk);

                    case FootstepsGroundType.Wood:
                        return RuntimeManager.CreateInstance(_data.footstepsWoodWalk);

                    case FootstepsGroundType.Grass:
                        return RuntimeManager.CreateInstance(_data.footstepsGrass);

                    case FootstepsGroundType.Gravel:
                        return RuntimeManager.CreateInstance(_data.footstepsGravel);

                    case FootstepsGroundType.Stone:
                        return RuntimeManager.CreateInstance(_data.footstepsStone);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(groundType), groundType, null);
                }
            }
        }

        private EventInstance GetCurrentSoundLanding(FootstepsGroundType groundType)
        {
            switch (groundType)
            {
                case FootstepsGroundType.Default:
                    return RuntimeManager.CreateInstance(_data.landingDefault);

                case FootstepsGroundType.Concrete:
                    return RuntimeManager.CreateInstance(_data.landingConcrete);

                case FootstepsGroundType.Wood:
                    return RuntimeManager.CreateInstance(_data.landingWood);

                case FootstepsGroundType.Grass:
                    return RuntimeManager.CreateInstance(_data.landingGrass);

                case FootstepsGroundType.Gravel:
                    return RuntimeManager.CreateInstance(_data.landingGravel);

                case FootstepsGroundType.Stone:
                    return RuntimeManager.CreateInstance(_data.landingStone);

                default:
                    throw new ArgumentOutOfRangeException(nameof(groundType), groundType, null);
            }
        }

        private void PlayFootstep(Vector3 soundPosition, EventInstance footstepSound)
        {
            footstepSound.set3DAttributes(soundPosition.To3DAttributes());
            footstepSound.start();
            footstepSound.release();
        }

        #region Get Material From Raycast

        // ReSharper disable Unity.PerformanceAnalysis
        private string DetectTextureOnTerrain(Terrain terrain, Vector3 hitPoint)
        {
            Vector3 terrainPosition = hitPoint - terrain.transform.position;
            var terrainData = terrain.terrainData;
            Vector3 splatMapPosition = new Vector3((terrainPosition.x / terrainData.size.x), 0, (terrainPosition.z / terrainData.size.z));

            int x = Mathf.FloorToInt(splatMapPosition.x * terrainData.alphamapWidth);
            int z = Mathf.FloorToInt(splatMapPosition.z * terrainData.alphamapHeight);

            float[,,] alphaMap = terrainData.GetAlphamaps(x, z, 1, 1);

            int primaryIndex = 0;
            for (int i = 0; i < alphaMap.Length; i++)
            {
                if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex])
                {
                    primaryIndex = i;
                }
            }

            return terrainData.terrainLayers[primaryIndex].diffuseTexture.name;
        }

        #endregion Get Material From Raycast

        private void SubscribeToEvents()
        {
            _actions.OnFootstepAction += ExecuteFootstep;
            _actions.OnLandingAction += ExecuteLanding;
        }

        public void UnsubscribeFromEvents()
        {
            _actions.OnFootstepAction -= ExecuteFootstep;
            _actions.OnLandingAction -= ExecuteLanding;
        }
    }
}