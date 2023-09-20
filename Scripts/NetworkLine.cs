using UnityEngine;

namespace Mirror
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/ Interest Management/ Line/Network Line")]
    public class NetworkLine : NetworkBehaviour
    {
        public Transform[] linePoints;
    }
}

