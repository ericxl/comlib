using UnityEngine;
namespace VirtusArts
{
    public abstract class DerivableSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _baseInstance;

        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The instance.</value>
        public static T BaseInstance
        {
            get { return _baseInstance ?? (_baseInstance = FindObjectOfType<T>()); }
        }

        protected virtual void Awake()
        {
            _baseInstance = this as T;
        }   

        protected virtual void OnDestroy()
        {
            _baseInstance = null;
        }
    }
}
