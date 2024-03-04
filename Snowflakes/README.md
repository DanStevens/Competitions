# CCO '07 P2 - Snowflakes

- https://dmoj.ca/problem/cco07p2
- Points: 10
- Time limit: 1.0s
- Memory limit: 64M

**Canadian Computing Competition: 2007 Stage 2, Day 1, Problem 2**

You may have heard that no two snowflakes are alike. Your task is to write a program to determine whether this is really true. Your program will read information about a collection of snowflakes, and search for a pair that may be identical.

Each snowflake has six arms. For each snowflake, your program will be provided with a measurement of the length of each of the six arms. Any pair of snowflakes which have the same lengths of corresponding arms should be flagged by your program as possibly identical.

**Note: The original CCO data were weak and have been augmented with some custom test cases.**

## Input Specification

The first line of input will contain a single integer *n*, `0` < *n* ≤ `100 000`, the number of snowflakes to follow. This will be followed by *n* lines, each describing a snowflake. Each snowflake will be described by a line containing six integers (each integer is at least `0` and less than `10 000 000`), the lengths of the arms of the snowflake. The lengths of the arms will be given in order around the snowflake (either clockwise or counterclockwise), but they may begin with any of the six arms. For example, the same snowflake could be described as `1 2 3 4 5 6` or `4 3 2 1 6 5`.

## Output Specification

If all of the snowflakes are distinct, your program should print the message: `No two snowflakes are alike.`

If there is a pair of possibly identical snowflakes, your program should print the message: `Twin snowflakes found.`

## Sample Input

    2
    1 2 3 4 5 6
    4 3 2 1 6 5

## Output for Sample Input

    Twin snowflakes found.

## Submissions

Personal best:
- Resources: 2.570s, 31.73 MB
- Maximum single-case runtime: 0.239s
- Final score: 100/100 (10.0/10 points) 

https://dmoj.ca/problem/cco07p2/submissions/MajeureX/

## My comments

Interesting problem. I came across after reading *Algorithmic Thinking* by Starch Press. In that book, the solution is given in C, but I decided to try my hand at in C# as that is my primary language.

**Spoiler alert**

I solved the problem in much the same way as in the book, that is finding a way determine if a Snowflake is 'like' another by moving the second through the first. I then compared each Snowflake with every other Snowflake using nested `for` loops, but that exceeded the time allowed due to the quadratic complexity. I resolved this in the way the book suggested, calculating the the 'size' of Snowflake (that is the sum of all the arm lengths), and using the size as a key to a `Dictionary` (.NET's hash table type). Instead of comparing an incoming Snowflake with every other Snowflake, we only compare it with Snowflakes of the same size. The Dictionary's value type is a collection of Snowflakes as it's possible for there to be multiple Snowflakes with the same size. This resulted in an accepted solution.

I further optimized the run time and memory usage with the following changes:
  - Replaced the `LinkedList` use to store Snowflakes of the same size with a `List` (.NET's dynamic array type). This saved a modest 0.04s and 2.6 MB.
  - Assumed that most Snowflakes were of a unique size, so made the value of the Dictionary a `(Snowflake, ICollection<Snowflake>)` tuple and only creating the `List` object if a second Snowflake of a given size is found. This made the comparison of two Snowflakes of a given size more complex, but overall saved 0.4s and 7.7 MB, beating the previous C# top submission by *lexnext1* of 2.94s and 44.5 MB.
  - Make `Snowflake` class into a `struct`, saving 0.05s and 12.2 MB.