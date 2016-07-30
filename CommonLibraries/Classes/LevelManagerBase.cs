using UnityEngine;
using ItemSystem;

namespace VirtusArts
{
    public abstract class LevelManagerBase : MonoBehaviour
    {
        [SerializeField]
        private AudioClip SceneClip;

        public virtual void Awake()
        {
            ItemDatabase.Load();
            UserData.Load();

        }

        public virtual void Start()
        {
            SoundManager.Instance.PlaySoundtrack(SceneClip);
        }

        protected virtual void OnApplicationQuit()
        {
            UserData.Save();
        }
    }
}
