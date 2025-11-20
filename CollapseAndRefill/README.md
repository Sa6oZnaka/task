# Technical Interview: Cascading Slot Mechanism

## Objective
You are tasked with implementing the core engine for a "Cascading Spin" slot machine game. 

Unlike traditional slots, this game uses a gravity-based mechanic similar to *Tetris* or *Candy Crush*. When a winning combination occurs, those symbols disappear, existing symbols fall into the empty spaces, and new symbols drop in from the top to fill the grid.

## Function Signature
You must implement the following function:

```csharp
public static byte[][] Spin(int N, int M, List<string> symbols)
{
    // Implementation needed
}
```

## Input Parameters

1.  **`N`** (Integer): The number of rows in the grid.
2.  **`M`** (Integer): The number of columns in the grid.
3.  **`symbols`** (List\<string>): A collection of strings representing the game data. Each string contains space-separated numbers (0-255).

---

## Logic & Rules

### 1. Initialization
The `symbols` list serves two purposes: defining the initial state and providing the supply for future refills.

* **Initial Grid:** The first **`N`** strings in the `symbols` list represent the initial rows of the game matrix. You must parse these to build the starting $N \times M$ grid.
* **Refill Supply:** All remaining strings in the list (after index `N-1`) represent the "supply" of new symbols waiting to drop in.

**Refill Parsing Logic:**
The supply strings represent **rows**.
* The value at `Index 0` of a supply string belongs to **Column 0**.
* The value at `Index 1` of a supply string belongs to **Column 1**.
* *and so on...*

You should treat these as First-In-First-Out (FIFO) queues for each individual column.

### 2. The Cascade Loop
Once the grid is initialized, the "Spin" logic begins. This is an iterative process that continues until a termination condition is met.

**A. Check for Matches**
* Count the frequency of every symbol currently on the grid.
* A match occurs if a specific symbol appears **8 or more times** anywhere on the grid.

**B. Eliminate and Gravity**
* If matches exist, remove **all** instances of the matching symbols.
* Symbols above the empty spots must fall down (gravity) to fill the gaps.

**C. Refill**
* Empty spaces created at the top of the columns must be filled.
* Retrieve the next available symbol for that specific column from the **Refill Supply**.
* *Note:* If Column 0 needs a refill, take the next value destined for Column 0. This operates independently of whether Column 1 needs a refill.

### 3. Termination Conditions
The process stops immediately if any of the following occur:

1.  **No Matches:** No symbol appears 8 or more times on the grid.
2.  **Max Cascades:** You have performed **10** consecutive cascade iterations.
3.  **Supply Exhaustion:** The grid needs a refill for a specific column, but that column's supply queue is empty.
    * *Note:* If Column A is empty but we don't need to refill it (because no matches occurred in Column A), the game continues. The stop condition triggers only if we **need** a symbol but cannot get one.

---

## Constraints

* `3 <= N <= 10`
* `3 <= M <= 10`
* `0 <= S <= 255` (Symbol values are bytes)
* You are guaranteed enough symbols in the supply to complete at least one full refill if needed, but you must handle the exhaustion case for subsequent cascades.
* There are no strict time or memory constraints, but efficiency is appreciated.

## Deliverables
* Return the final state of the `byte[][]` matrix after the cascading process terminates.