using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase {
    public class FrogSpawner : MonoBehaviour {
        public GameObject[] frogPrefab; // Префаб объекта
        public Transform[] bezierPoints; // Массив контрольных точек
        public FrogTrigger frogTrigger; // Ссылка на компонент FrogTrigger
        public Transform[] targetTransforms;
        private List<Transform> freeTransforms;
       

        private void Start() {
            freeTransforms = new List<Transform>(targetTransforms);
            StartCoroutine(SpawnFrog());
        }



        public void StartSpawnFrog() {
            StartCoroutine(SpawnFrog());

        }
        public IEnumerator SpawnFrog() {
            while (true) {
                GameObject newFrog = Instantiate(frogPrefab[Random.Range(0,frogPrefab.Length)], bezierPoints[0].position, Quaternion.identity);
                BezierCurve curve = newFrog.AddComponent<BezierCurve>();
                curve.point0 = bezierPoints[0];
                curve.point1 = bezierPoints[1];
                curve.point2 = bezierPoints[2];
                curve.point3 = bezierPoints[3];

                FrogBehavior frogBehavior = newFrog.AddComponent<FrogBehavior>();
                frogBehavior.Initialize(frogTrigger);


               

                yield return new WaitForSeconds(3.5f);
            }
        }
    }
}