# WC '18 Contest 1 S3 - Reach for the Top

 - https://dmoj.ca/problem/wc18c1s3
- Points: 12 (partial)
- Time limit: 1.8s
- Memory limit: 128M
- Author: SourSpinach
- Problem types: Dynamic Programming, Graph Theory

**Woburn Challenge 2018-19 Round 1 - Senior Division**

It's time for Bob to face his biggest fear at H.S. High School: the dreaded gym class rope climb. In this brutal test of strength and endurance, Bob is tasked with ascending part of the way up an infinitely long vertical rope. He begins by grabbing onto the bottom of the rope, at a height of *0*, and must reach any height of `H` (`1 ≤ H ≤ 1,000,000`) or greater (measured in metres).

To make matters even worse than usual, Alice has pranked Bob by spreading itching powder on some sections of the rope, which he'll need to avoid along the way! She's done so for `N` (`0 ≤ N ≤ H − 1`) sections, the `i`-th of which covers all heights from `A~i` to `B~i`  inclusive (`1 ≤ A~i ≤ B~i ≤ H − 1`). No two sections overlap with one another, even at their endpoints.

Bob's rope-climbing style is… unusual, to say the least, which may come in handy for avoiding Alice's itching powder. At any point, given that he's currently holding onto the rope at some height `h~1` , he may only perform one of the following two possible actions:

 1. Jump upwards by a height of exactly `J` (`1 ≤ J ≤ H`) , such that his new height is `h~2 = h~1 + J`, but only if the rope is not covered in itching powder at height `h~2` .
 2. Drop downwards by any height, such that his new height is any integer `h~2` (`0 ≤ h~2 < h~1`), but only if the rope is not covered in itching powder at height `h~2` .

Both types of actions involved in this technique are understandably quite tiring, so Bob would like to avoid performing more of them than necessary. Help him determine the minimum number of actions he must perform to reach a height of at least H metres, or determine that it's sadly impossible for him to ever reach such a height.

## Subtasks

 - In test cases worth 8/27 of the points, `H ≤ 1,000` and `J ≤ 2` .
 - In test cases worth another 12/27 of the points, `H ≤ 1,000`.

## Input Specification

 - The first line of input consists of three space-separated integers, `H`, `J`, and `N`.
 - `N` lines follow, the `i`-th of which consists of two space-separated integers, `A~i` and `B~i`, for `i = 1…N`.

## Output Specification

Output a single integer, either the minimum number of actions required for Bob to reach a height of at least `H` metres, or *-1* if it's impossible for him to do so.

## Sample Input and Output

### Case 1

Input:

    12 5 2
    2 4
    10 10

Output:

    5

Bob can jump up to a height of *5*, drop down to a height of *1*, jump up to a height of *6*, jump up to a height of *11*, and finally jump up to a height of *16*. This is the only strategy which involves *5* actions, which is the minimum possible number of actions.

### Case 2

Input:

    5 2 2
    1 1
    4 4

Output:

    -1

Bob must start by jumping up to a height of *2*. From there, he may not jump upwards to a height of *4* (as the rope is covered in itching powder there), and similarly may not drop down to a height of *1*, meaning that he can never grab the rope at any height aside from *0* or *2*.
