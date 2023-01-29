# tiebreaker

This is a tie breaker game that was given to me as a challenge. You may run it using the abc.txt file fount in the project folder.

The rules are as follows:

We've created a simple multiplayer card game where:
• 6 players are dealt 5 cards from two 52 card decks, and the winner is the one with the highest score.
The score for each player is calculated by adding the highest 3 card values for each player, where the
number cards have their face value, J = 11, Q = 12, K = 13 and A = 11 (not 1).
•
In the event of a tie, the scores are recalculated for only the tied players by calculating a "suit score" for
each player to see if the tie can be broken (it may not).
Each card is given a score based on its suit, with clubs = 1, diamonds = 2, hearts = 3 and spades
= 4, and the player's score is the suit value of the player’s highest card.
o
•

You are required to write a command line application using C# or JavaScript (Node application) that needs to do
the following:
• Run on Windows.
• Be invoked with the name of the input and output text files.
• Read the data from the input file, find the winner(s) and write them to the output file.
• Handle any problems with the input or input file contents.

Output File Structure
The output file should contain a single line, with one of the following 3 possibilities:
The name of the winner and their score (Face Value OR Face + Suit Value) (colon separated).
o Example: Player1:35
•
A comma separated list of winners in the case of a tie and the score (Face Value OR Face + Suit Value)
(colon separated).
o Example: Player1,Player2:35
•

"Exception:[reason]" if the input file or its contents had any issue.
o Example: Exception:Some reason why the input is wrong.
