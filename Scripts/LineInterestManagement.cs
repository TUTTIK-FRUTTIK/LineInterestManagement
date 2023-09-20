using System.Collections.Generic;
using UnityEngine;
using Mirror;

[AddComponentMenu("Network/ Interest Management/ Line/Line Interest Management")]
public class LineInterestManagement : InterestManagement
{
    [Tooltip("Rebuild all every 'rebuildInterval' seconds.")]
    public float rebuildInterval = 0.1f;
    double lastRebuildTime;

    public LayerMask layerMask;
    public QueryTriggerInteraction queryTriggerInteraction;

    List<NetworkLine> checkedObjects = new List<NetworkLine>();


    [ServerCallback]
    public override bool OnCheckObserver(NetworkIdentity identity, NetworkConnectionToClient newObserver)
    {
        return true;
    }


    [ServerCallback]
    public override void OnRebuildObservers(NetworkIdentity identity, HashSet<NetworkConnectionToClient> newObservers)
    {
        if(identity.TryGetComponent(out NetworkLine checkedObject))
        {
            foreach(Transform linePoint in checkedObject.linePoints)
            {
                foreach(NetworkLine enemyObject in checkedObjects)
                {
                    if (enemyObject.gameObject == identity.gameObject) continue;

                    //Debug.DrawLine(linePoint.position, enemyObject.transform.position, Color.green, Time.deltaTime, true);
                    RaycastHit hitInfo;
                    if(Physics.Linecast(linePoint.position, enemyObject.transform.position, out hitInfo, layerMask, queryTriggerInteraction))
                    {
                        if(hitInfo.collider.TryGetComponent(out NetworkLine Player))
                        {
                            if (enemyObject.gameObject.GetComponent<NetworkIdentity>().connectionToClient == null) continue;
                            newObservers.Add(enemyObject.gameObject.GetComponent<NetworkIdentity>().connectionToClient);
                        }
                    }
                    else
                    {
                        if (enemyObject.gameObject.GetComponent<NetworkIdentity>().connectionToClient == null) continue;
                        newObservers.Add(enemyObject.gameObject.GetComponent<NetworkIdentity>().connectionToClient);
                    }
                }
            }
        }
        else
        {
            foreach(NetworkConnectionToClient conn in NetworkServer.connections.Values)
                newObservers.Add(conn);
        }
        
    }

    [ServerCallback]
    public override void OnSpawned(NetworkIdentity identity)
    {
        if(identity.TryGetComponent(out NetworkLine checkedObject))
            checkedObjects.Add(checkedObject);
    }

    [ServerCallback]
    public override void OnDestroyed(NetworkIdentity identity)
    {
        if (identity.TryGetComponent(out NetworkLine checkedObject))
            checkedObjects.Remove(checkedObject);
    }


    [ServerCallback]
    public override void SetHostVisibility(NetworkIdentity identity, bool visible)
    {
        base.SetHostVisibility(identity, visible);
    }


    [ServerCallback]
    public override void Reset() { lastRebuildTime = 0D; }

    [ServerCallback]
    internal void Update()
    {
        if (NetworkTime.localTime >= lastRebuildTime + rebuildInterval)
        {
            RebuildAll();
            lastRebuildTime = NetworkTime.localTime;
        }
    }
}