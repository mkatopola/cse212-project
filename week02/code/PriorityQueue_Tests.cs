using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from empty queue
    // Expected Result: Throws InvalidOperationException
    // Defect(s) Found: None (existing empty check works)
    public void Dequeue_EmptyQueue_ThrowsException()
    {
        var priorityQueue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue single item and dequeue
    // Expected Result: Dequeues successfully
    // Defect(s) Found: Dequeue doesn't remove items
    public void EnqueueOneItem_DequeueReturnsThatItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        Assert.AreEqual("A", priorityQueue.Dequeue());
        // Verify item was removed
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Multiple items with same priority (FIFO order)
    // Expected Result: Dequeues in insertion order
    // Defect(s) Found: Dequeue doesn't remove items
    public void EnqueueTwoItemsSamePriority_DequeueReturnsFirstEnqueued()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 1);
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Highest priority at end of queue
    // Expected Result: Correctly dequeues highest priority item
    // Defect(s) Found: Loop doesn't check last element
    public void Enqueue_HighestPriorityAtEnd_DequeuesHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 1);
        priorityQueue.Enqueue("C", 3); // Highest priority at end
        Assert.AreEqual("C", priorityQueue.Dequeue());
        // Verify remaining items
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Multiple high priority items (should pick first occurrence)
    // Expected Result: Dequeues first highest priority item
    // Defect(s) Found: Priority comparison uses >= (should use >)
    public void Enqueue_MultipleHighPriority_DequeuesFirstOccurrence()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 3); // First high priority
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 3); // Later high priority
        priorityQueue.Enqueue("D", 1);
        Assert.AreEqual("A", priorityQueue.Dequeue()); // Should pick first 3
        Assert.AreEqual("C", priorityQueue.Dequeue()); // Then second 3
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Complex priority distribution
    // Expected Result: Dequeues in correct order
    // Defect(s) Found: Multiple defects (removal, loop bounds, comparison)
    public void Enqueue_ComplexPriorities_DequeuesInCorrectOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 3); // First highest
        priorityQueue.Enqueue("C", 1);
        priorityQueue.Enqueue("D", 3); // Second highest
        priorityQueue.Enqueue("E", 2);

        // First: B (first 3)
        Assert.AreEqual("B", priorityQueue.Dequeue());
        // Second: D (second 3)
        Assert.AreEqual("D", priorityQueue.Dequeue());
        // Third: A (first 2)
        Assert.AreEqual("A", priorityQueue.Dequeue());
        // Fourth: E (second 2)
        Assert.AreEqual("E", priorityQueue.Dequeue());
        // Fifth: C (1)
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }
}