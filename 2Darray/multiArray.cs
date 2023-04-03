using System;
using System.Collections;
namespace ConsoleEnum
{
    public class multiArray : IComparable
    {
        // Beginning of nested classes.
        // Nested class to do ascending sort on year property.
        private class SortYearAscendingHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                multiArray c1 = (multiArray)a;
                multiArray c2 = (multiArray)b;

                if (c1.Name > c2.Name)
                    return 1;

                if (c1.Name < c2.Name)
                    return -1;

                else
                    return 0;
            }
        }