using System.Collections;
using UnityEngine;

namespace Game
{
    public class DamagePopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject popupPrefab;

        public GameObject Create(Transform parent, Combat.Result result)
        {
            GameObject popupObject = Instantiate(popupPrefab);
            popupObject.transform.position = parent.position;

            DamagePopup popup = popupObject.GetComponent<DamagePopup>();
            popup.SetResult(result);

            StartCoroutine(SetPopupLifetime(popupObject));

            return popupObject;
        }

        private IEnumerator SetPopupLifetime(GameObject popupObject)
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(popupObject);
        }
    }
}