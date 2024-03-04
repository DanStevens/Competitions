# LKP '18 Contest 2 P1 - Food Lines

- https://dmoj.ca/problem/lkp18c2p1
- Points: 3
- Time limit: 3.0s
- Memory limit: 64M
- Author: KevinWan
- Problem type: Implementation

After many years of continuous warfare, the country of Collea was left in shambles. This has caused food shortages and famines all across the country. In an attempt to distribute the little food that was produced and prevent food hoarding, the Collean government limited the amount of food that any one person can buy. Soon, Collean citizens are forced to wait in long lines in order to obtain the food they needed. There are currently *N* such food lines in the city of Lachtin, the *i*th of which has *aᵢ* people in it. *M* people are going to enter one of the lines in the next hour, where they each enter the shortest line they see. Since Phreia plans to enter the line, she wants to know the length of the line that each person decides to join.

## Constraints

  - 1 ≤ *N* ≤ 100
  - 1 ≤ *aᵢ* ≤ 100 for *i* in 1, 2, …, *N*
  - 1 ≤ *M* ≤ 100

## Input Specification

  1. The first line contains two positive integers, *N* and *M* .
  2. The second line contains *N* positive integers, a~1, a~2, …, a~N.

## Output Specification

Print *M* lines, the *i*th of which being the length of the line that the *i*th person joined.

## Sample Input

    5 3
    2 2 3 3 3

## Sample Output

    2
    2
    3
