# Coding Interview Challenge: Doubly Linked List

## Problem Description

You are tasked with implementing a custom **Doubly Linked List** in C#.

Unlike a standard Singly Linked List where nodes only point to the next element, a Doubly Linked List node maintains references to both its **Next** and **Previous** nodes.

This allows for efficient traversal in both directions and $O(1)$ complexity for adding or removing elements from both the beginning and the end of the list.

## Requirements

Your implementation must satisfy the following constraints and functional requirements:

**Properties:**
* **`Count`**: Returns the current number of nodes in the list.
* **`First`**: Returns the first node in the list.
* **`Last`**: Returns the last node in the list.
* *Constraint:* These properties must be publicly readable but **cannot be overwritten directly** from outside the class.

**Constructors:**
* **Single Item:** A constructor that accepts a single data item `T` to initialize the list.
* **Collection:** A constructor that accepts an `IEnumerable<T>` to populate the list.

**Methods:**
* **`AddFirst(T data)`**: Adds a new node containing `data` to the **beginning** of the list. Returns the created `Node`.
* **`AddLast(T data)`**: Adds a new node containing `data` to the **end** of the list. Returns the created `Node`.
* **`RemoveFirst()`**: Removes the first node from the list. If the list is empty, throw an `InvalidOperationException`.
* **`RemoveLast()`**: Removes the last node from the list. If the list is empty, throw an `InvalidOperationException`.
* **`GetData()`**: Returns an `IEnumerable<T>` containing the data from all nodes in order (from First to Last).

### Edge Cases
* Ensure the `Count` is updated correctly on all additions and removals.
* Handle scenarios where the list becomes empty (First and Last should be null).
* Handle scenarios where the list has only one item and it is removed.

---

## Evaluation Criteria

1.  **Correctness:** Do the pointers (`Next` and `Previous`) update correctly when adding/removing?
2.  **Edge Case Handling:** Does the code crash if I remove from a list with 1 item? Does it crash if I remove from an empty list?
3.  **Code Quality:** Is the code clean and readable?
4.  **Encapsulation:** Are the `Count`, `First`, and `Last` properties protected from external modification?