# Draughts / Checkers Kata

:warning: **WIP!**


### TO-DO

- [x] Add Piece to Board
- [x] Move UpLeft / UpRight
- [ ] Illegal Move Off Board
- [ ] Capture Piece

### Thoughts

- "Up" is _RELATIVE_ to the Player - Should we change the language?

### Object Model Ideas

- Game
    - Board
        - 8x8 Squares
            - Black / White
            - Piece?
        - Size
    - 2 Players
        - Player
    - State
        - "Player selection" (coin toss to pick black)
        - "Starting" (new game) - Black player first
        - "In Play" (Both Players Have Pieces & Can Move)
        - "Ended" (Player has won)

- Piece     - Black or White     - IsKing?     - Move()     - Capture()

