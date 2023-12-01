using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Model;
using ClassesTools;
using UnityEngine;

namespace UniRxTask
{
    public class CardAnimator : MonoBehaviourExtBind
    {
        /// <summary>
        /// Тестовая версия для создания анимации появления новой карты в нижней коллекции
        /// </summary>
        [SerializeField] private Transform _bottomCollection;

        [SerializeField] private CollectionObject _prefab;

        private List<CollectionObject> _bottomCollectionCards;

        [OnAwake]
        private void CustomAwake()
        {
            _bottomCollectionCards = new List<CollectionObject>();
        }

        [Bind]
        private Transform CreateNewCard()
        {
            return Instantiate(_prefab, Vector3.zero, Quaternion.identity, _bottomCollection).transform;
        }
    }
}