# CCO '18 P1 - Geese vs. Hawks

- https://dmoj.ca/problem/cco18p1
- Points: 10 (partial)
- Time limit: 0.6s
- Memory limit: 1G
- Author: azneye
- Problem type: Dynamic Programming
- Competition: Canadian Computing Olympiad: 2018 Day 1, Problem 1

Troy and JP are big hockey fans. Every hockey team played `N` games this season. Each game was between two teams and the team that scored more points won. No game ended in a tie.

Troy's favourite team is the Waterloo Geese and he recorded the outcome of all their games as a string `S`. `S~i = W` if the Geese won their `i`-th game; otherwise `S~i = L` if the Geese lost their `i`-th game. He also recorded that they scored `A~i` points in their `i`-th game.

JP's favourite team is the Laurier Hawks and he recorded the outcome of all their games as a string `T`. `T~j = W` if the Hawks won their `j`-th game; otherwise `T~j = L` if the Hawks lost their `j`-th game. He also recorded that they scored `B~j` points on their `j`-th game.

Troy and JP recorded wins/losses and points in the order that their favourite teams played.

A *rivalry* game is one where the Geese and Hawks played each other. Since neither Troy nor JP recorded the opponents their favourite teams faced, they are not sure which games, if any, were rivalry games. They wonder what is the maximum possible sum of points scored by both their teams in rivalry games that matches the information they recorded.

## Input Specification

  1. The first line contains one integer `N`, between 1 and 1,000 inclusive.
  2. The second line contains string `S` of length `N` consisting of characters "W" and "L".
  3. The third line contains `N` integers `A~1, …, A~N`, between 1 and 1,000,000 inclusive.
  4. The fourth line contains string `T` of length `N` consisting of characters "W" and "L".
  5. The fifth line contains `N` integers `B~1, …, B~N`, between 1 and 1,000,000 inclusive.

For 10 of the 25 available marks, `N ≤ 10`.

## Output Specification

Print one line with one integer, the maximum possible sum of points scored in potential rivalry games.

## Sample Input 1

    1
    W
    2
    W
    3

## Output for Sample Input 1

    0

Since both the Geese and Hawks won all their games, there could not have been any rivalry games.

## Sample Input 2

    4
    WLLW
    1 2 3 4
    LWWL
    6 5 3 2

## Output for Sample Input 2

    14

The fourth game each team played could have been a rivalry game where Geese won with 4 points to the Hawk's 2 points. The third game the Geese played and the second game the Hawks played could have been a rivalry game where the Hawks won with 5 points compared to 3 points of the Geese. The points scored by both teams is `4 + 2 + 5 + 3 = 14` and this is the maximum possible.

Note that the first game played by the Geese was a win where they scored 1 goal: this game cannot be against the Hawks, since there is no game where the Hawks scored 0 goals. Similarly, the first game played by the Hawks cannot be used, since the Hawks lost and scored 6 goals, and the Geese never had a game where they scored at least 7 goals.
