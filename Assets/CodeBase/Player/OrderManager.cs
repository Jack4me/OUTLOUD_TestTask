using System.Collections.Generic;
using UnityEngine;

namespace CodeBase {
    public class OrderManager : MonoBehaviour
    {
        public List<string> recipes;

        private void Awake()
        {
            // Примеры рецептов
            recipes = new List<string> { "Пончики", "Коктейль" };
        }

        public string GetRandomRecipe()
        {
            if (recipes.Count > 0)
            {
                int randomIndex = Random.Range(0, recipes.Count);
                Debug.Log("INDEX" + randomIndex);
                return recipes[randomIndex];
            }
            return null;
        }
    }
}