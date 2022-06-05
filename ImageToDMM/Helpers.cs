using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToDMM
{
    class Helpers
    {
        //Takes a List filled with 2D int arrays and gives back one array filled with List<int>
        public List<int>[,] MergeArrays(List<int[,]> arrays)
        {
            int arrayHeight = arrays[0].GetLength(0);
            int arrayWidth = arrays[0].GetLength(1);
            List<int>[,] returnArray = new List<int>[arrayHeight, arrayWidth];
            foreach (int[,] currentArray in arrays)
            {
                for (int currentHeight = 0; currentHeight < arrayHeight; currentHeight++)
                {
                    for (int currentWidth = 0; currentWidth < arrayWidth; currentWidth++)
                    {
                        if (returnArray[currentHeight, currentWidth] == null)
                            returnArray[currentHeight, currentWidth] = new List<int>();
                        returnArray[currentHeight, currentWidth].Add(currentArray[currentHeight, currentWidth]);
                    }
                }
            }
            return returnArray;
        }

        public List<List<string>> ArrayUniqueLists (List<string>[,] arrayLists)
        {
            int arrayHeight = arrayLists.GetLength(0);
            int arrayWidth = arrayLists.GetLength(1);
            List<List<string>> returnList = new List<List<string>>();

            for (int currentHeight = 0; currentHeight < arrayHeight; currentHeight++)
            {
                for (int currentWidth = 0; currentWidth < arrayWidth; currentWidth++)
                {
                    if (!returnList.Any(a => a.SequenceEqual(arrayLists[currentHeight, currentWidth])))
                        returnList.Add(arrayLists[currentHeight, currentWidth]);
                }
            }
            return returnList;

        }
    }
}
