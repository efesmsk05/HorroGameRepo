using UnityEngine;

public class TeleportableObject : MonoBehaviour
{
    public Camera playerCamera;
    public Collider roomBounds;

    private bool isInView = true;

    void Update()
    {
        if (playerCamera == null || roomBounds == null)
            return;

        bool currentlyVisible = IsInView();

        if (isInView && !currentlyVisible)
        {
            TeleportToNewPosition();
        }

        isInView = currentlyVisible;
    }

    bool IsInView()
    {
        Vector3 viewportPos = playerCamera.WorldToViewportPoint(transform.position);

        float margin = 0.1f; // Görüþ kenarlarýndan biraz uzaklaþýldýðýnda sayýlacak

        return viewportPos.z > 0 &&
               viewportPos.x >= -margin && viewportPos.x <= 1 + margin &&
               viewportPos.y >= -margin && viewportPos.y <= 1 + margin;
    }


    void TeleportToNewPosition()
    {
        Vector3 newPos = GetRandomPositionInRoom();

        // Obje yerden yukarýda olsun diye Y koordinatýný koruyabiliriz
        newPos.y = transform.position.y;

        transform.position = newPos;
    }

    Vector3 GetRandomPositionInRoom()
    {
        Bounds bounds = roomBounds.bounds;
        Vector3 candidatePos = transform.position;

        int maxAttempts = 30;
        float minDistanceToWall = 0.2f; // Duvarlara yakýnlýk
        float minDistanceToOtherObjects = 1.0f; // Diðer objelerle çakýþmama

        int attempts = 0;

        while (attempts < maxAttempts)
        {
            float randX = Random.Range(bounds.min.x + minDistanceToWall, bounds.max.x - minDistanceToWall);
            float randZ = Random.Range(bounds.min.z + minDistanceToWall, bounds.max.z - minDistanceToWall);
            candidatePos = new Vector3(randX, transform.position.y, randZ);

            // Kamera arkasý kontrolü
            Vector3 dirToCandidate = (candidatePos - playerCamera.transform.position).normalized;
            float dot = Vector3.Dot(playerCamera.transform.forward, dirToCandidate);

            if (dot > -0.1f) // oyuncunun çok arkasý deðilse atlama
            {
                attempts++;
                continue;
            }

            // Yakýnýnda baþka obje var mý? (çarpýþmayý önlemek için)
            Collider[] hits = Physics.OverlapSphere(candidatePos, minDistanceToOtherObjects);
            bool hasOtherObjects = false;

            foreach (var hit in hits)
            {
                if (hit.gameObject != this.gameObject && hit.CompareTag("Teleportable"))
                {
                    hasOtherObjects = true;
                    break;
                }
            }

            if (hasOtherObjects)
            {
                attempts++;
                continue;
            }

            return candidatePos;
        }

        return transform.position;
    }



}
