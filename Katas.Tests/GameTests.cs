using NUnit.Framework;

namespace Katas.Tests
{
    public class GameTests
    {
        // TODO (RC): Build Picture of "Game" (pieces in play)..

        // TODO (RC): Ensure First Turn < 100ms

        // TODO (RC): Ensure Other Turns < 50ms

        // TODO (RC): GameState - Balance of Power

        // TODO (RC): Projection - Move vs. GameState = Power Shift

        // TODO (RC): First Up - Limited Set of Moves I Can Make - i.e. Currently Owned

        // TODO (RC): Strategy? KILL vs. CAPTURE?
        // <= Troops than enemy? CAPTURE
        // > Troops than enemy? KILL

        // TODO (RC): Determine Possible Options for moves (Player Owned > Links > Bases)
        // Each Option will have:
        // Source / Target Factories
        // Turns Required
        // Troop Count on Arrival (Turns Required * Target Production)
        // Expected Losses (Availability - (Troops on Arrival + Troops Inbound)

        // TODO (RC): Risk - Ability for Enemy Player to Attack

        // TODO (RC): Balance of Power - Power Shift over Turns >> Move Results
        // Want maximum return over shortest turns

        // TODO (RC): Growth ability - capturing nodes with more links = better growth.


        // TODO (RC): Find Quickest Move that Captures Most Production w/ Least Losses

        // TODO (RC): Also need to think defense - if there are troops incoming, then we need to weigh up production losses

    }
}