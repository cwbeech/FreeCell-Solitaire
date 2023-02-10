/* Game.cs
 * Author: Rod Howell
 * Modified by: Calvin Beechner
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using System.CodeDom.Compiler;

namespace Ksu.Cis300.FreeCell
{
    /// <summary>
    /// Conrols the game logic for a FreeCell game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The number of free cells.
        /// </summary>
        private const int _freeCellCount = 4;

        /// <summary>
        /// The number of home cells.
        /// </summary>
        private const int _homeCellCount = 4;

        /// <summary>
        /// The number of tableau columns.
        /// </summary>
        private const int _tableauColumnCount = 8;

        /// <summary>
        /// Gets the free cells.
        /// </summary>
        public CardLocation[] FreeCells { get; } = new CardLocation[_freeCellCount];

        /// <summary>
        /// Gets the home cells.
        /// </summary>
        public CardLocation[] HomeCells { get; } = new CardLocation[_homeCellCount];

        /// <summary>
        /// Gets the tableau columns.
        /// </summary>
        public TableauColumn[] TableauColumns { get; } = new TableauColumn[_tableauColumnCount];

        /// <summary>
        /// Constructs a Game.
        /// </summary>
        public Game() 
        { 
            for (int i = 0; i < FreeCells.Length; i++)
            {
                FreeCells[i] = new CardLocation();
            }
            for (int i = 0; i < HomeCells.Length; i++)
            {
                HomeCells[i] = new CardLocation();
            }
            for (int i = 0; i < TableauColumns.Length; i++)
            {
                TableauColumns[i] = new TableauColumn();
            }
        }

        /// <summary>
        /// Starts a new game using the given seed.
        /// </summary>
        /// <param name="seed">The seed to use to shuffle the cards.</param>
        public void StartNewGame(int seed)
        {
            ClearCardLocation(FreeCells);
            ClearCardLocation(HomeCells);
            ClearTableauColumns();
            DealCards(seed);
            InitializeStack();
        }

        /// <summary>
        /// Reacts to the user's click on a free cell.
        /// </summary>
        /// <param name="loc">The chosen free cell.</param>
        public void ClickFreeCell(CardLocation loc)
        {
            if (_selectedFreeCell != null) //1
            {
                CardLocation temp = new CardLocation();
                temp = _selectedFreeCell;
                bool isEmpty = false;
                for (int i = 0; i < _emptyFreeCells.Count; i++)
                {
                    if (loc == _emptyFreeCells.ToArray()[i]) //determines if selection is empty. If passed, the turn is legal.
                    {
                        isEmpty = true;
                    }
                }
                if (isEmpty)
                {
                    AddCardToCardLocation(temp.Card, loc);
                    RemoveCardFromCardLocation(temp); //this probably isn't necessary
                }
                DeselectFreeCell();
            }
            else if (_tableauColumnSelected != null) //2
            {
                TableauColumn temp = new TableauColumn();
                temp = _tableauColumnSelected;
                if (temp.NumberSelected <= _emptyFreeCells.Count)
                {
                    for (int i = 0; i < temp.NumberSelected; i++)
                    {
                        AddCardToCardLocation(temp.Column.Pop(), loc);

                    }
                }
                DeselectTableauColumn();
            }
            else //3
            {
                bool isEmpty = false;
                if (loc.Card != null)
                {
                    for (int i = 0; i < _emptyFreeCells.Count; i++) //Checks if selected valid free cell
                    {
                        if (loc == _emptyFreeCells.ToArray()[i]) //determines if selection has card.
                        {
                            isEmpty = true;
                        }
                    }
                    if (!isEmpty)
                    {
                        SelectFreeCell(loc);
                    }
                }
            }
        }

        /// <summary>
        /// Reacts to the user's click on a tableau column.
        /// </summary>
        /// <param name="col">The column clicked.</param>
        /// <param name="n">The number of cards chosen.</param>
        public void ClickColumn(TableauColumn col, int n)
        {

            if (_selectedFreeCell != null) //1
            {
                CardLocation temp = new CardLocation();
                temp = _selectedFreeCell;
                bool isEmpty = false;
                for (int i = 0; i < _emptyFreeCells.Count; i++) //Checks if selected valid free cell
                {
                    
                    if (temp == _emptyFreeCells.ToArray()[i]) //determines if selection has card.
                    {
                        isEmpty = true;
                    }
                }
                if (!isEmpty) //if SelectedFreecell is valid
                {
                    if (col.Column.Count == 0)
                    {
                        AddCardToTableauColumn(temp.Card, col); //adds card
                        RemoveCardFromCardLocation(temp);
                    }
                    else if (CanBeStackedOnTableau(col.Column.Peek(), temp.Card)) //if SelectedFreeCell can be added to col
                    {
                        AddCardToTableauColumn(temp.Card, col); //adds card
                        RemoveCardFromCardLocation(temp);
                    }
                }
                DeselectFreeCell();
            }
            else if (_tableauColumnSelected != null) //2
            {
                TableauColumn temp = new TableauColumn();
                temp = _tableauColumnSelected;
                /*CanBeStackedOnTableau(col.Column.Peek(), temp.Column.ToArray()[temp.NumberSelected-1])*/
                if (CanColumnBePlacedOnTableauColumn(temp, col, temp.NumberSelected))
                { //checks if card can be stacked on Tableau
                    MoveCardsTableauColumn(temp, col, temp.NumberSelected);
                }
                DeselectTableauColumn();
            }
            else //3
            {
                if (n != 0)
                {
                    SelectTableauColumn(col, n);
                }
            }

        }

        /// <summary>
        /// Reacts to the user's clicking a home cell.
        /// </summary>
        /// <param name="loc">The home cell that was clicked.</param>
        public void ClickHomeCell(CardLocation loc)
        {
            if (_selectedFreeCell != null) //1
            {
                CardLocation temp = new CardLocation();
                temp = _selectedFreeCell;
                bool isEmpty = false;
                
                for (int i = 0; i < _emptyFreeCells.Count; i++) //Checks if selected valid free cell
                {
                    if (loc == _emptyFreeCells.ToArray()[i]) //determines if selection has card.
                    {
                        isEmpty = true;
                    }
                }
                if (!isEmpty && CanBePlacedOnHomeCell(temp.Card, loc)) //if SelectedFreecell is valid
                {
                    AddCardToCardLocation(temp.Card, loc);
                    RemoveCardFromCardLocation(temp);
                }
                DeselectFreeCell();
            }
            else if (_tableauColumnSelected != null) //2
            {
                TableauColumn temp = new TableauColumn();
                temp = _tableauColumnSelected;
                for (int i = 0; i < temp.NumberSelected; i++)
                {
                    if (CanBePlacedOnHomeCell(temp.Column.Peek(), loc))
                    {
                        AddCardToCardLocation(temp.Column.Pop(), loc);
                    }
                }
                DeselectTableauColumn();
            }
            /*
            TableauColumn temp = new TableauColumn();
            temp = TableauColumnSelected;

            DeselectTableauColumn();
            */
        }

        /// <summary>
        /// Moves all possible cards to home cells.
        /// </summary>
        public void MoveAll()
        {
            if (_selectedFreeCell != null) //1
            {
                DeselectFreeCell();
            }
            if (_tableauColumnSelected != null) //2
            {
                DeselectTableauColumn();
            }
            bool couldMove = true;
            while(couldMove)
            {
                if (!MoveCardFromFreeToHome() && !MoveCardFromTableauToHHome())
                {
                    couldMove = false;
                }
                
            }
        }

        /// <summary>
        /// The number of milliseconds to pause when making an animated update to the GUI
        /// </summary>
        private const int _numberOfMilliseconds = 35;

        /// <summary>
        /// Stack used to keep track of empty cells
        /// </summary>
        private Stack<CardLocation> _emptyFreeCells = new Stack<CardLocation>();

        /// <summary>
        /// CardLocation used to keep track of the selected free cell
        /// </summary>
        private CardLocation _selectedFreeCell = null;
        //SelectedFreeCell = null;

        /// <summary>
        /// TableauColumn used to keep track of the tableau column on which cards are selected
        /// </summary>
        private TableauColumn _tableauColumnSelected = null;
        //TableauColumnSelected = null;

        /// <summary>
        /// Clears stack and initializes stack to contain all the free cells.
        /// </summary>
        private void InitializeStack()
        {
            //clears stack
            while (_emptyFreeCells.Count > 0)
            {
                _emptyFreeCells.Pop();
            }
            //initializes stack
            for (int i = 0; i < _freeCellCount; i++)
            {
                _emptyFreeCells.Push(FreeCells[i]);
            }
        }

        /// <summary>
        /// Checks if CardLocation exhists and if it does it is removed.
        /// </summary>
        /// /// <param name="loc">The CardLocation that needs to be removed.</param>
        private void RemoveFreeCell(CardLocation loc)
        {
            Stack<CardLocation> temp = new Stack<CardLocation>();
            while (_emptyFreeCells.Count > 0)
            {
                temp.Push(_emptyFreeCells.Pop());
            }
            while (temp.Count > 0)
            {
                if (loc == temp.Peek())
                {
                    temp.Pop();
                }
                else
                {
                    _emptyFreeCells.Push(temp.Pop());
                }
            }
        }

        /// <summary>
        /// takes in control and refreshes it, and pauses for the allocated amount of milliseconds
        /// </summary>
        /// <param name="c">The control that needs to be redrawn</param>
        private static void RedrawControl(Control c)
        {
            c.Refresh();
            Thread.Sleep(_numberOfMilliseconds);
        }

        /// <summary>
        /// Adds a Card to CardLocation.
        /// </summary>
        /// <param name="c">The Card being added.</param>
        /// <param name="cl">The CardLocation the Card is being added to.</param>
        private void AddCardToCardLocation(Card c, CardLocation cl)
        {
            cl.Card = c;
            RedrawControl(cl);
            RemoveFreeCell(cl);
        }

        /// <summary>
        /// Adds a Card to TableauColumn.
        /// </summary>
        /// <param name="c">The Card being added.</param>
        /// <param name="tc">The TableauColumn the Card is being added to.</param>
        private static void AddCardToTableauColumn(Card c, TableauColumn tc)
        {
            tc.Column.Push(c);
            RedrawControl(tc);
        }

        /// <summary>
        /// Removes a Card from CardLocation.
        /// </summary>
        /// <param name="cl">The CardLocation the Card is being removed from.</param>
        /// <returns>The Card being removed.</returns>
        private Card RemoveCardFromCardLocation(CardLocation cl)
        {
            Card result = cl.Card;
            cl.Card = null;
            RedrawControl(cl);
            _emptyFreeCells.Push(cl);
            return result;
        }

        /// <summary>
        /// Removes a Card from TableauColumn.
        /// </summary>
        /// <param name="tc">The TableauColumn the Card is being removed from.</param>
        /// <returns>The Card being removed.</returns>
        private static Card RemoveCardFromTableauColumn(TableauColumn tc)
        {
            Card result = tc.Column.Pop();
            RedrawControl(tc);
            return result;
        }

        /// <summary>
        /// Clears a CardLocation[].
        /// </summary>
        /// <param name="cl">An array of CardLocations.</param>
        private static void ClearCardLocation(CardLocation[] cl)
        {
            for (int i = 0; i < cl.Length; i++)
            {
                cl[i].Card = null;
                cl[i].Refresh();
            }
        }

        /// <summary>
        /// Clears the TableauColumns.
        /// </summary>
        private void ClearTableauColumns()
        {
            for (int i = 0; i < TableauColumns.Length; i++)
            {
                while (TableauColumns[i].Column.Count > 0)
                {
                    TableauColumns[i].Column.Pop();
                }
                TableauColumns[i].Refresh();
            }
        }
        /// <summary>
        /// Deals the Cards.
        /// </summary>
        /// <param name="seed">Number used to dertermine how cards are shuffled.</param>
        private void DealCards(int seed)
        {
            Stack<Card> deck = Shuffler.GetShuffledDeck(seed);
            while (deck.Count > 0)
            {
                for (int i = 0; i < _tableauColumnCount; i++)
                {
                    if (deck.Count != 0)
                    {
                        TableauColumns[i].Column.Push(deck.Pop());
                        TableauColumns[i].Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Selects a free cell.
        /// </summary>
        /// <param name="cl"></param>
        private void SelectFreeCell(CardLocation cl)
        {
            cl.IsSelected = true;
            _selectedFreeCell = cl;
            cl.Refresh();
        }

        /// <summary>
        /// Selects a TableauColumn.
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="number"></param>
        private void SelectTableauColumn(TableauColumn tc, int number)
        {
            tc.NumberSelected = number;
            _tableauColumnSelected = tc;
            tc.Refresh();
        }
        /// <summary>
        /// Deselects a free cell.
        /// </summary>
        /// <returns>Returns the CardLocation of the freecell that was deselected</returns>
        private CardLocation DeselectFreeCell()
        {
            CardLocation temp = new CardLocation();
            _selectedFreeCell.IsSelected = false;
            temp = _selectedFreeCell; //saves the value of the selected free cell.
            _selectedFreeCell.Refresh();
            _selectedFreeCell = null;
            return temp;
        }

        /// <summary>
        /// Deselects TableauColumn.
        /// </summary>
        /// <returns>Returns Deselected TableauColomn</returns>
        private TableauColumn DeselectTableauColumn()
        {
            TableauColumn temp = new TableauColumn();
            _tableauColumnSelected.NumberSelected = 0;
            temp = _tableauColumnSelected;
            _tableauColumnSelected.Refresh();
            _tableauColumnSelected = null;
            return temp;
        }
        /// <summary>
        /// Determines whether a card can or cannot be stacked on tableau.
        /// </summary>
        /// <param name="tableau">Card variable representing the bottom card.</param>
        /// <param name="top">Card variable representing the top card.</param>
        /// <returns>Returns a boolean representing whether or not the move is legal.</returns>
        private static bool CanBeStackedOnTableau(Card tableau, Card top)
        {
            bool result = false;
            if (tableau == null)
            {
                result = true;
            }
            else if (top.Rank +1 == tableau.Rank)
            {
                if (top.Color != tableau.Color)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Determines whether or not a card can be placed on a home cell
        /// </summary>
        /// <param name="c"></param>
        /// <param name="cl"></param>
        /// <returns>Returns a boolean value representing whether or not the move is legal.</returns>
        private static bool CanBePlacedOnHomeCell(Card c, CardLocation cl)
        {
            bool result = false;
            if (cl.Card != null) {
                if (cl.Card.Rank == c.Rank - 1 && cl.Card.Suit == c.Suit)
                {
                    result = true;
                }
            }
            else
            {
                if (c.Rank == 1)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Determines whether or not a column can be placed on a TableauColumn.
        /// </summary>
        /// <param name="top">Cards to be moved and placed on top.</param>
        /// <param name="bottom">The column on which the Cards will be placed.</param>
        /// <param name="number">The number of Cards being moved.</param>
        /// <returns>Returns a boolean variable representing whether or not the move is legal.</returns>
        private bool CanColumnBePlacedOnTableauColumn(TableauColumn top, TableauColumn bottom, int number)
        {
            bool result = false;
            if (_emptyFreeCells.Count >= number-1) //1
            {
                bool properlyStacked = true;
                for (int i = 0; i < number - 1; i++) //2
                {
                    if (CanBeStackedOnTableau(top.Column.ToArray()[i + 1], top.Column.ToArray()[i]) == false)
                    {
                        properlyStacked = false;
                    }
                }
                if (properlyStacked) //3
                {
                    if (bottom.Column.Count == 0) //empty bottom
                    {
                        result = true;
                    }
                    else if (top.NumberSelected > 1)
                    {
                        if (CanBeStackedOnTableau(bottom.Column.Peek(), top.Column.ToArray()[number - 1]))
                        {
                            result = true;
                        }
                    }
                    else if (top.NumberSelected <= 1)
                    {
                        if (CanBeStackedOnTableau(bottom.Column.Peek(), top.Column.Peek()))
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Moves a column of one or more cards from one tableau column to another.
        /// </summary>
        /// <param name="moved">A TableauColumn containing the Column containing the Cards to be moved.</param>
        /// <param name="placed">A TableauColumn containing the Column on which the cards are to be placed</param>
        /// <param name="number">The number of Cards being moved.</param>
        private void MoveCardsTableauColumn(TableauColumn moved, TableauColumn placed, int number)
        {
            Stack<CardLocation> tempStack = new Stack<CardLocation>();
            for (int i = 0; i < number - 1; i++) //1
            {
                _emptyFreeCells.Peek().Card = moved.Column.Pop();
                tempStack.Push(_emptyFreeCells.Peek());
                RemoveFreeCell(_emptyFreeCells.Peek());
                for (int ii = 0; ii < _freeCellCount; ii++)
                {
                    FreeCells[ii].Refresh();
                }
            }
            AddCardToTableauColumn(moved.Column.Peek(), placed); //2
            RemoveCardFromTableauColumn(moved);
            while (tempStack.Count > 0)  //3
            {
                AddCardToTableauColumn(tempStack.Peek().Card, placed);
                RemoveCardFromCardLocation(tempStack.Pop());
            }
        }

        /// <summary>
        /// Checks if a card can be moved from free to home and if it can it is moved.
        /// </summary>
        /// <returns>A boolean representing whether or not a card was succesfully moved.</returns>
        private bool MoveCardFromFreeToHome()
        {
            bool result = false;
            for (int i = 0; i < FreeCells.Length; i++)
            {
                if (FreeCells[i].Card != null)
                {
                    for (int ii = 0; ii < HomeCells.Length; ii++)
                    {
                        if (!result)
                        {
                            if (CanBePlacedOnHomeCell(FreeCells[i].Card, HomeCells[ii]))
                            {
                                AddCardToCardLocation(FreeCells[i].Card, HomeCells[ii]);
                                RemoveCardFromCardLocation(FreeCells[i]);
                                result = true;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if a card can be moved from Tableau to home and if it can it is moved.
        /// </summary>
        /// <returns>A boolean representing whether or not a card was succesfully moved.</returns>
        private bool MoveCardFromTableauToHHome()
        {
            bool result = false;
            for (int i = 0; i < TableauColumns.Length; i++)
            {
                if (TableauColumns[i].Column.Count != 0)
                {
                    for (int ii = 0; ii < HomeCells.Length; ii++)
                    {
                        if (!result)
                        {
                            if (CanBePlacedOnHomeCell(TableauColumns[i].Column.Peek(), HomeCells[ii]))
                            {
                                AddCardToCardLocation(TableauColumns[i].Column.Peek(), HomeCells[ii]);
                                RemoveCardFromTableauColumn(TableauColumns[i]);
                                result = true;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
