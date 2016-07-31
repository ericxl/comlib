using UnityEngine;
namespace VirtusArts
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<T>()); }
        }

        protected virtual void Awake()
        {
            _instance = this as T;
        }   

        protected virtual void OnDestroy()
        {
            _instance = null;
        }
    }
}
