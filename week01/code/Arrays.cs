using System;
using System.Collections.Generic;

public static class Arrays 
{ 
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'. For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}. Assume that length is a positive 
    /// integer greater than 0. 
    /// </summary> 
    /// <returns>array of doubles that are the multiples of the supplied number</returns> 
    public static double[] MultiplesOf(double number, int length) 
    { 
        // ============================= PLAN (before coding) =============================
        // 1) We need to create an array of doubles with exactly 'length' slots.
        // 2) The first element should be the starting 'number'.
        // 3) Each subsequent element should be the next multiple of 'number'.
        //    In other words, element at index i (0-based) should be: number * (i + 1).
        // 4) Fill the array using a for-loop from i = 0 to i < length, computing number * (i + 1).
        // 5) Return the filled array.
        // 6) Edge considerations: The problem guarantees length > 0. If number is 0, all entries will be 0.
        // ==============================================================================

        // Implementation following the plan:
        var result = new double[length];
        for (int i = 0; i < length; i++)
        {
            // i = 0  -> number * 1
            // i = 1  -> number * 2
            // ...
            result[i] = number * (i + 1);
        }
        return result; 
    } 

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'. For example, if the data is
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}. The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary> 
    public static void RotateListRight(List<int> data, int amount) 
    { 
        // ============================= PLAN (before coding) =============================
        // We will implement rotation using slicing with GetRange and AddRange for clarity.
        // 1) Handle trivial cases: if the list is null or has 0 or 1 element, there's nothing to rotate.
        // 2) The amount might be equal to data.Count (which means no change) or even larger in other contexts;
        //    normalize it with modulo to be safe: amount = amount % data.Count. If it becomes 0, return.
        // 3) Compute the split index where the list should be cut: split = data.Count - amount.
        //    Example: data = [1,2,3,4,5,6,7,8,9], amount = 3 => split = 6.
        //    Tail  = data[split..end] = [7,8,9]; Head = data[0..split] = [1,2,3,4,5,6].
        // 4) Rebuild the original list in-place: clear then add Tail followed by Head.
        // ==============================================================================

        if (data == null || data.Count <= 1)
        {
            return; // Nothing to rotate
        }

        // Normalize the rotation amount to [0, data.Count - 1]
        amount %= data.Count; 
        if (amount == 0) 
        {
            return; // Rotating by a multiple of the length yields the same list
        }

        int split = data.Count - amount;

        // Slice the list into two parts: [0..split) and [split..Count)
        List<int> tail = data.GetRange(split, amount);           // rightmost 'amount' elements
        List<int> head = data.GetRange(0, split);                // the rest at the front

        // Rebuild 'data' in-place: tail + head
        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);
    } 
}
