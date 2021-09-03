using UnityEngine;
using System.Collections.Generic;

namespace Wanderer.Utils
{
    public class Utils
    {
        public static Vector3 GetWorldPositionWithZ(Vector3 screenPos, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPos);
            return worldPosition;
        }
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0;
            return vec;
        }

        public static bool RandomBool(float trueChance)
        {
            return Random.Range(0f, 100f) <= trueChance;
        }

        public static bool RandomBool()
        {
            return RandomBool(50f);
        }
        public static float RandomizeParams(float min, float max)
        {
            return Random.Range(min, max);
        }

        public static int RandomObjectInCollection(int collectionLenght)
        {
           return Random.Range(0, collectionLenght );  
        }
        public static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
        {
            return collider.OverlapPoint(point);
        }

        public static Vector3 RandomPointInBounds(Bounds bounds, float margin = 0)
        {
            return new Vector3(
               UnityEngine.Random.Range(bounds.min.x + margin, bounds.max.x - margin),
                UnityEngine.Random.Range(bounds.min.y + margin, bounds.max.y - margin),
                UnityEngine.Random.Range(bounds.min.z + margin, bounds.max.z - margin)
            );
        }

        public static bool isClipPlaying(string name, Animator animator)
        {
            var animClip = animator.runtimeAnimatorController.animationClips;
            foreach (var clip in animClip)
            {
                if (clip.name == name)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName(name))
                        return true;
                }
            }
            return false;
        }

        public static AnimationClip GetAnimationClip(string name, Animator animator)
        {
            var animClip = animator.runtimeAnimatorController.animationClips;
            foreach (var clip in animClip)
            {
                if (clip.name == name)
                {
                    return clip;
                }
            }
            return null;
        }

        public static float GetAnimationClipDurantion(string name, Animator animator, float timeToRemove = 0f)
        {
            var clip = Utils.GetAnimationClip(name, animator);

            if (clip == null)
            {
                Debug.LogWarning("l'animation : " + name + " n'existe pas pour " + animator.gameObject);
                return 0f;
            }
                

            return clip.length - timeToRemove;
        }




        public static void AddAnimationEvent(string name, string functionName, Animator animator, float time = 0, float param = 0f)
        {
            AnimationClip Clip = null;
            var animClip = animator.runtimeAnimatorController.animationClips;
            foreach (var clip in animClip)
            {
                if (clip.name == name)
                {
                    Clip = clip;
                    break;
                }
            }
            if (Clip == null)
            {
                Debug.LogWarning("You are missing " + name + "animation for " + animator.gameObject.name);
                return;
            }


            var _aEvents = new AnimationEvent();
            _aEvents.functionName = functionName;

            if (time != 0)
                _aEvents.time = time;
            else
                _aEvents.time = Clip.length;

            if (param != 0f)
                _aEvents.floatParameter = param;

            Clip.AddEvent(_aEvents);
            
        }


        #region Particule System
        public static void TogglePs(ParticleSystem ps, bool enabled)
        {
            var psEmission = ps.emission;
            psEmission.enabled = enabled;
        }

        #endregion

        #region GameObject 

        public static GameObject FindGameObjectInChildWithTag(GameObject _parent, string _tag)
        {
            Transform transform = _parent.transform;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.tag == _tag) return transform.GetChild(i).gameObject;
            }

            return null;
        }

        public static List<GameObject> FindGameObjectsInChildWithTag(GameObject _parent, string _tag)
        {
            Transform transform = _parent.transform;
            List<GameObject> gameObjectsList = new List<GameObject>();

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.tag == _tag)
                {
                    gameObjectsList.Add(transform.GetChild(i).gameObject);
                }
            }

            return gameObjectsList;
        }


        #endregion
    }


}

