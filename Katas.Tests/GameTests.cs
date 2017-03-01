using NUnit.Framework;

namespace Katas.Tests
{
    public class GameTests
    {
        [Test]
        public void Map_TwoFactoriesWithSingleLink_CreatesMap()
        {
            // TODO (RC): Complete Map of Factory <> Factory Links
            // e.g. Factory[1].Links returns ALL Linked Factories
        }

        // TODO (RC): Build Dictionary of Player Bases > Linked Factories

        /* IDEAS

        GameState - Balance of Power
        Projection - Move vs. GameState = Power Shift
        Strategy? KILL vs. CAPTURE?
            <= Troops than enemy? CAPTURE
            > Troops than enemy? KILL

        Possible Options for moves (Player Owned > Links > Bases)
        Each Option will have:
        - Source / Target Factories
        - Turns Required
        - Troop Count on Arrival (Turns Required * Target Production)
        - Expected Losses (Availability - (Troops on Arrival + Troops Inbound)

        Risk - Ability for Enemy Player to Attack

        Balance of Power - Power Shift over Turns >> Move Results
        Want maximum return over shortest turns

        Growth ability - capturing nodes with more links = better growth.

        Also need to think defense - if there are troops incoming, then we need to weigh up production losses
        */
    }
}