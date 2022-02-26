using System.Collections.Generic;
using System.Linq;

namespace PracticeSolution.CoreLogic.SortingAlgorithms
{
    public class Sorter
    {
        #region Bubble sort

        /// <summary>
        /// Each iteration, the first element of the currently sorted sequence is floated to the top.
        /// </summary>
        /// <param name="array">Sortable array</param>
        public static void BubbleSort(IList<int> array)
        {
            var arrayLenghtWithOffset = array.Count - 1;
            for (var i = 0; i < arrayLenghtWithOffset; i++)
            {
                for (var j = 0; j < arrayLenghtWithOffset - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }
        }

        #endregion
        
        #region Count sort

        /// <summary>
        /// Parses the array into the elements it contains and their number, and then sorts exactly the set of elements.
        /// </summary>
        /// <param name="array">Sortable array</param>
        public void CountSort(IList<int> array)
        {
            var itemSet = new Dictionary<int, int>();
            foreach (var item in array)
            {
                if (itemSet.ContainsKey(item))
                {
                    itemSet[item]++;
                }
                else
                {
                    itemSet[item] = 1;
                }
            }

            var itemArray = itemSet.Keys.ToArray();
            BubbleSort(itemArray);
            var arrayIndex = 0;
            foreach (var t in itemArray)
            {
                var count = itemSet[t];
                for (var j = 0; j < count; j++)
                {
                    array[arrayIndex] = t;
                    arrayIndex++;
                }
            }
        }

        #endregion

        #region Insertion sort

        /// <summary>
        /// Splits an array into a sorted sequence and an unsorted sequence,
        /// then takes the first element from the unsorted sequence and inserts it in the correct place in the sorted sequence.
        /// </summary>
        /// <param name="array">Sortable array</param>
        public void InsertionSort(IList<int> array)
        {
            int startIndexUnsortedSequence = 1;
            for(int i = 0; i < array.Count - 1; i++)
            {
                if(array[startIndexUnsortedSequence] > array[i])
                {
                    startIndexUnsortedSequence++;
                    continue;
                }
                break;
            }

            for (var i = startIndexUnsortedSequence; i < array.Count; i++)
            {
                var j = i;
                while (j > 0 && array[j - 1] > array[j])
                {
                    (array[j], array[j - 1]) = (array[j - 1], array[j]);
                    j -= 1;
                }
            }
        }
        
        #endregion
        
        #region Selection sort

        /// <summary>
        /// At each iteration, finds the worst element of the unsorted sequence and puts it at the end of the sorted sequence.
        /// </summary>
        /// <param name="array">Sortable array</param>
        public void SelectionSort(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var min = array[i];
                var indexMin = i;
                for (var j = i + 1; j < array.Length; j++)
                {
                    if (min <= array[j]) continue;
                    min = array[j];
                    indexMin = j;
                }
                (array[i], array[indexMin]) = (array[indexMin], array[i]);
            }
        }

        #endregion
        
        #region Merge sort
        
        /// <summary>
        /// NOT READY
        /// </summary>
        /// <param name="array">Sortable array</param>
        private void MergeSort(int[] array)
        {
            if (array.Length > 1)
            {
                MergeSort(array, 0, array.Length - 1);
            }
        }

        private void MergeSort(int[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                int middleIndex = (leftIndex + rightIndex) / 2;
                MergeSort(array, leftIndex, middleIndex);
                MergeSort(array, middleIndex + 1, rightIndex);
                Merge(array, leftIndex, middleIndex, rightIndex);
            }
        }

        private void Merge(int[] array, int leftIndex, int middleIndex, int rightIndex)
        {
            
        }
        
        #endregion
    }
}