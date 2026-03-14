using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue on an empty queue.
    // Expected Result: Throws InvalidOperationException with message "The queue is empty.".
    // Defect(s) Found: N/A (guard clause present). Test should PASS.
    public void Dequeue_EmptyQueue_Throws()
    {
        var pq = new PriorityQueue();
        var ex = Assert.ThrowsException<InvalidOperationException>(() => pq.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Dequeue returns highest-priority item's value when different priorities exist.
    // Expected Result: Returns value with the highest numeric priority.
    // Defect(s) Found: Previously, Dequeue ignored the last element due to an off-by-one loop bound and did not remove items. Fixed loop and removal. Test should PASS.
    public void Dequeue_ReturnsHighestPriority()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("low", 1);
        pq.Enqueue("high", 5);
        pq.Enqueue("mid", 3);
        var first = pq.Dequeue();
        Assert.AreEqual("high", first);
    }

    [TestMethod]
    // Scenario: FIFO tie-break when multiple items share the highest priority.
    // Expected Result: Among equal highest priorities, dequeue returns the earliest enqueued item first.
    // Defect(s) Found: Previously, ties were broken by picking the last equal-priority item (used ">="). Changed to strict ">" to preserve FIFO. Test should PASS.
    public void Dequeue_TieBreaksFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 2);
        pq.Enqueue("B", 2);
        pq.Enqueue("C", 2);
        Assert.AreEqual("A", pq.Dequeue());
        Assert.AreEqual("B", pq.Dequeue());
        Assert.AreEqual("C", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue always adds to the back; verify order among equal priorities across time.
    // Expected Result: If D with same priority is enqueued after A and B, it should be served after them when all share the highest priority.
    // Defect(s) Found: Combined defects (no removal, wrong tie-break) previously violated expected order. With fixes, test should PASS.
    public void Enqueue_AddsToBack_VerifiedViaEqualPriorityOrder()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 10);
        pq.Enqueue("B", 10);
        // Interleave different priorities
        pq.Enqueue("X", 1);
        pq.Enqueue("Y", 5);
        // Enqueue another highest-priority later
        pq.Enqueue("C", 10);

        // All highest-priority (10) items should come out in FIFO order among themselves
        Assert.AreEqual("A", pq.Dequeue());
        Assert.AreEqual("B", pq.Dequeue());
        Assert.AreEqual("C", pq.Dequeue());

        // Then the remaining ones by their priorities
        Assert.AreEqual("Y", pq.Dequeue());
        Assert.AreEqual("X", pq.Dequeue());

        // Now queue is empty
        Assert.ThrowsException<InvalidOperationException>(() => pq.Dequeue());
    }
}
