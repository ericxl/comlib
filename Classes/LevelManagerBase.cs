using UnityEngine;

namespace VirtusArts
{
    public abstract class LevelManagerBase<T> : Singleton<T> where T:MonoBehaviour
    {
        [SerializeField]
        private AudioClip SceneClip;

        public virtual void Awake()
        {
        }

        public virtual void Start()
        {
            SoundManager.PlaySoundtrack(SceneClip);
        }

        protected virtual void OnApplicationQuit()
        {

        }
    }
}
