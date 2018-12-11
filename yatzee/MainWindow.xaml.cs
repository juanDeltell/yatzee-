using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;

namespace yatzee
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string actualPlay = "";
        int cont = 3;
        int points = 0;
        bool alreadySelected = false;
        int[] orderArray = new int[6];



        public MainWindow()
        {
            InitializeComponent();
            checkFirstDice.IsChecked = true;
            checkSecondDice.IsChecked = true;
            checkThirdDice.IsChecked = true;
            checkFourthDice.IsChecked = true;
            checkFifthDice.IsChecked = true;

            closeMoves();
        }

        private void ThrowDices_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            cont--;

            timesLeft.Text = cont.ToString();
            timesLeft.UpdateLayout();


            int dice1 = rnd.Next(1, 7);
            int dice2 = rnd.Next(1, 7);
            int dice3 = rnd.Next(1, 7);
            int dice4 = rnd.Next(1, 7);
            int dice5 = rnd.Next(1, 7);

            if (checkFirstDice.IsChecked == true)
            {
                diceTextOne.Text = dice1.ToString();
                diceTextOne.UpdateLayout();
            }
            if (checkSecondDice.IsChecked == true)
            {
                diceTextTwo.Text = dice2.ToString();
                diceTextTwo.UpdateLayout();
            }
            if (checkThirdDice.IsChecked == true)
            {
                diceTextThree.Text = dice3.ToString();
                diceTextThree.UpdateLayout();
            }
            if (checkFourthDice.IsChecked == true)
            {
                diceTextFour.Text = dice4.ToString();
                diceTextFour.UpdateLayout();
            }
            if (checkFifthDice.IsChecked == true)
            {
                diceTextFive.Text = dice5.ToString();
                diceTextFive.UpdateLayout();
            }


            checkFirstDice.IsChecked = false;
            checkSecondDice.IsChecked = false;
            checkThirdDice.IsChecked = false;
            checkFourthDice.IsChecked = false;
            checkFifthDice.IsChecked = false;

            if (cont == 0)//round finish
            {
                throwDices.IsEnabled = false;
                cont = 3;
                results.Text = results.Text + diceTextOne.Text.ToString() + " "
                     + diceTextTwo.Text.ToString() + " "
                      + diceTextThree.Text.ToString() + " "
                       + diceTextFour.Text.ToString() + " "
                        + diceTextFive.Text.ToString() + "\n\n ";

                results.UpdateLayout();

                checkFirstDice.IsChecked = true;
                checkSecondDice.IsChecked = true;
                checkThirdDice.IsChecked = true;
                checkFourthDice.IsChecked = true;
                checkFifthDice.IsChecked = true;

                openPosibleNextMove("");

                actualPlay = checkPlay();
  

                results.Text = results.Text + " You got " + actualPlay + " .\n";
                alreadySelected = false;
            }
            
        }



        private int[] countDicesAndPoints()
        {
            
            int contOnes = contHowManyDicesHave(1);
            int contTwoes = contHowManyDicesHave(2);
            int contThrees = contHowManyDicesHave(3);
            int contFours = contHowManyDicesHave(4);
            int contFives = contHowManyDicesHave(5);
            int contSixes = contHowManyDicesHave(6);

            for (var i = 1; i < 6; i ++)
            {
                contOnes = contHowManyDicesHave(i) ;
                orderArray[i] = i;
            }
            return orderArray;
        }


        private String checkPlay()
        {
            string actualPlay = "", actualRound ="";

            //checking how many numbers we have of each
            int contOnes = contHowManyDicesHave(1);
            int contTwoes = contHowManyDicesHave(2);
            int contThrees = contHowManyDicesHave(3);
            int contFours = contHowManyDicesHave(4);
            int contFives = contHowManyDicesHave(5);
            int contSixes = contHowManyDicesHave(6);

            
            //check game
            if (contOnes == 5 || contTwoes == 5 || contThrees == 5 || contFours == 5 || contFives == 5 || contSixes == 5)//yatzee!
            {
                actualPlay = "Yatzee!";
            }
            else if (contOnes == 4 || contTwoes == 4 || contThrees == 4 || contFours == 4 || contFives == 4 || contSixes == 4)//four of a kind
            {
                actualPlay = "Four of a Kind!";
            }
            else if (contOnes == 3 || contTwoes == 3 || contThrees == 3 || contFours == 3 || contFives == 3 || contSixes == 3)//it could be 3 of a kind or fullhouse
            {
                if (contOnes == 2 || contTwoes == 2 || contThrees == 2 || contFours == 2 || contFives == 2 || contSixes == 2)//FullHouse
                {
                    actualPlay = "Full House!";
                }
                else
                {
                    actualPlay = "Three of a Kind!";
                }
            }
            else if (contOnes == 2 || contTwoes == 2 || contThrees == 2 || contFours == 2 || contFives == 2 || contSixes == 2)//it could be pair or two pairs
            {//there is at leats one pair
                if ((contOnes == 2 && contTwoes == 2) || (contOnes == 2 && contThrees == 2) || (contOnes == 2 && contFours == 2) || (contOnes == 2 && contFives == 2) || (contOnes == 2 && contSixes == 2) ||
                    (contTwoes == 2 && contThrees == 2) || (contTwoes == 2 && contFours == 2) || (contTwoes == 2 && contFives == 2) || (contTwoes == 2 && contSixes == 2) ||
                    (contThrees == 2 && contFours == 2) || (contThrees == 2 && contFives == 2) || (contThrees == 2 && contSixes == 2) ||
                    (contFours == 2 && contFives == 2) || (contFours == 2 && contSixes == 2) ||
                    (contFives == 2 && contSixes == 2))
                {
                    actualPlay = "Doble Pair!";
                }
                else
                {
                    actualPlay = "Pair!";
                }
            }
            else if (contOnes == 1 && contTwoes == 1 && contThrees == 1 && contFours == 1 && contFives == 1)//small strait
            {
                actualPlay = "Small Straight!";
            }
            else if (contTwoes == 1 && contThrees == 1 && contFours == 1 && contFives == 1 && contSixes == 1)
            {
                actualPlay = "Big Straight!";
            }
            else
            {
                actualPlay = "chance";
            }

            openPosibleNextMove(actualPlay);

            actualRound = selectOption(actualPlay);//wait untill 1 click

            

            return actualRound;
        }
        

        private int getPoints(string selectedGame)
        {
            int puntos = 111, aux;
            if (selectedGame == "Aces")
            {
                puntos = 1 * contHowManyDicesHave(1);
            }
            else if (selectedGame == "Twos")
            {
                puntos = 2 * contHowManyDicesHave(2);
            }
            else if (selectedGame == "Threes")
            {
                puntos = 3 * contHowManyDicesHave(3);
            }
            else if (selectedGame == "Fours")
            {
                puntos = 4 * contHowManyDicesHave(4);
            }
            else if (selectedGame == "Fives")
            {
                puntos = 5 * contHowManyDicesHave(5);
            }
            else if (selectedGame == "Sixes")
            {
                puntos = 6 * contHowManyDicesHave(6);
            }
            else if (selectedGame == "Yatzee!")
            {
                if (checkPlay() == "Yatzee!")
                {
                    puntos = 50;
                }
                else
                {
                    puntos = 0;
                }
            }
            else if (selectedGame == "Full House!")
            {
                if(checkPlay() == "Full House!")
                {
                    puntos = ((6 * contHowManyDicesHave(6)) + (5 * contHowManyDicesHave(5)) + (4 * contHowManyDicesHave(4)) + (3 * contHowManyDicesHave(3)) + (2 * contHowManyDicesHave(2)) + (1 * contHowManyDicesHave(1)));
                }
                else
                {
                    puntos = 0;
                }
            }
            else if (selectedGame == "Small Straight!")
            {
                if (checkPlay() == "Small Straight!")
                {
                    puntos = ((6 * contHowManyDicesHave(6)) + (5 * contHowManyDicesHave(5)) + (4 * contHowManyDicesHave(4)) + (3 * contHowManyDicesHave(3)) + (2 * contHowManyDicesHave(2)) + (1 * contHowManyDicesHave(1)));
                }
                else
                {
                    puntos = 0;
                }
            }
            else if (selectedGame == "Big Straight!")
            {
                if (checkPlay() == "Big Straight!")
                {
                    puntos = ((6 * contHowManyDicesHave(6)) + (5 * contHowManyDicesHave(5)) + (4 * contHowManyDicesHave(4)) + (3 * contHowManyDicesHave(3)) + (2 * contHowManyDicesHave(2)) + (1 * contHowManyDicesHave(1)));
                }
                else
                {
                    puntos = 0;
                }
            }
            else if( selectedGame == "chance")
            {
                puntos = ((6 * contHowManyDicesHave(6)) + (5 * contHowManyDicesHave(5)) + (4 * contHowManyDicesHave(4)) + (3 * contHowManyDicesHave(3)) + (2 * contHowManyDicesHave(2)) + (1 * contHowManyDicesHave(1)));
            }
            else if (selectedGame == "Four of a Kind!")
            {
                if (checkPlay() == "Four of a Kind!")
                {
                    //hay que comprobar si se tiene la jugada
                    aux = whichIsTheMostRepeated();
                    puntos = aux * contHowManyDicesHave(aux);
                }
                else
                {
                    puntos = 0;
                }

            }
            else if (selectedGame == "Three of a Kind!")
            {
                if (checkPlay() == "Three of a Kind!")
                {
                    //hay que comprobar si se tiene la jugada
                    aux = whichIsTheMostRepeated();
                    puntos = aux * contHowManyDicesHave(aux);
                }
                else
                {
                    puntos = 0;
                }
            }
            else if (selectedGame == "Doble Pair!")
            {
                if (checkPlay() == "Doble Pair!")
                {
                    //hay que comprobar si se tiene la jugada
                    puntos = letsCalculateTwoPairsPoints();
                }
                else
                {
                    puntos = 0;
                }
            }
            else if (selectedGame == "Pair!")
            {
                if (checkPlay() == "Pair!")
                {
                    //hay que comprobar si se tiene la jugada
                    aux = whichIsTheMostRepeated();
                    puntos = aux * contHowManyDicesHave(aux);
                }
                else
                {
                    puntos = 0;
                }
            }

            return puntos;
        }



        private int whichIsTheMostRepeated()
        {
            int aux, higher = 0;

            for(int i = 1; i < 7; i++)
            {
                aux = contHowManyDicesHave(i);

                if (aux*aux > higher*higher)//si solo es una pareja cogemos la mas alta
                {
                    higher = i;//i
                }
            }

            return higher;
        }

        private int letsCalculateTwoPairsPoints()
        {
            int aux = 0, points=0;

            for (int i = 1; i < 7; i++)
            {
                aux = contHowManyDicesHave(i);

                if (aux == 2)//es una pareja
                {
                    points = points + i + i;
                    
                }
                
            }

            return points;
        }

        private int contHowManyDicesHave(int thisNum)
        {
            int cont = 0;
            if (int.Parse(diceTextOne.Text) == thisNum)
            {
                cont++;
            }
            if (int.Parse(diceTextTwo.Text) == thisNum)
            {
                cont++;
            }
            if (int.Parse(diceTextThree.Text) == thisNum)
            {
                cont++;
            }
            if (int.Parse(diceTextFour.Text) == thisNum)
            {
                cont++;
            }
            if (int.Parse(diceTextFive.Text) == thisNum)
            {
                cont++;
            }
            

            return cont;
        }



        private void selectGameTable(string selectedGame, TextBox textBoxToAddPoints)
        {

            results.Text = results.Text +  "\n\nYou have selected " + selectedGame + "\n";

            points = getPoints(selectedGame);

            
            textBoxToAddPoints.Text = points.ToString();
            textBoxToAddPoints.Background = Brushes.Green;

            results.UpdateLayout();

            textBoxToAddPoints.UpdateLayout();

            //hay q comprobar que si son  vacio sumar cero......
            subtotalPlayer1.Text = ( int.Parse(AcesPlayer1.Text) + int.Parse(twosPlayer1.Text) + int.Parse(threesPlayer1.Text) + int.Parse(foursPlayer1.Text) + int.Parse(fivesPlayer1.Text) + int.Parse(sixesPlayer1.Text) ).ToString();
            
            closeMoves();
            throwDices.IsEnabled = true;
        }



        private int calculeSubtotal()
        {
            int suma = 0;

            return suma;
        }



        private void openPosibleNextMove(string jugada)
        {
            if (AcesPlayer1.Text == "")
            {
                AcesPlayer1.IsReadOnly = false;
                AcesPlayer1.Background = Brushes.Yellow;
                AcesPlayer1.UpdateLayout();
            }

            if (twosPlayer1.Text == "")
            {
                twosPlayer1.IsReadOnly = false;
                twosPlayer1.Background = Brushes.Yellow;
                twosPlayer1.UpdateLayout();
            }

            if (threesPlayer1.Text == "")
            {
                threesPlayer1.IsReadOnly = false;
                threesPlayer1.Background = Brushes.Yellow;
                threesPlayer1.UpdateLayout();
            }

            if (foursPlayer1.Text == "")
            {
                foursPlayer1.IsReadOnly = false;
                foursPlayer1.Background = Brushes.Yellow;
                foursPlayer1.UpdateLayout();
            }

            if (fivesPlayer1.Text == "")
            {
                fivesPlayer1.IsReadOnly = false;
                fivesPlayer1.Background = Brushes.Yellow;
                fivesPlayer1.UpdateLayout();
            }

            if (sixesPlayer1.Text == "")
            {
                sixesPlayer1.IsReadOnly = false;
                sixesPlayer1.Background = Brushes.Yellow;
                sixesPlayer1.UpdateLayout();
            }

            if (jugada == "Yatzee!" && yatzeePlayer1.Text == "")
            {
                yatzeePlayer1.IsReadOnly = false;
                yatzeePlayer1.Background = Brushes.Yellow;
                yatzeePlayer1.UpdateLayout();
            }
            else if (jugada == "Four of a Kind!" && fourKindPlayer1.Text == "")
            {
                fourKindPlayer1.IsReadOnly = false;
                fourKindPlayer1.Background = Brushes.Yellow;
                fourKindPlayer1.UpdateLayout();
            }
            else if (jugada == "Full House!" && fullHousePlayer1.Text == "")
            {
                fullHousePlayer1.IsReadOnly = false;
                fullHousePlayer1.Background = Brushes.Yellow;
                fullHousePlayer1.UpdateLayout();
            }
            else if (jugada == "Three of a Kind!" && threeKindPlayer1.Text == "")
            {
                threeKindPlayer1.IsReadOnly = false;
                threeKindPlayer1.Background = Brushes.Yellow;
                threeKindPlayer1.UpdateLayout();
            }
            else if (jugada == "Doble Pair!" && doblePairPlayer1.Text == "")
            {
                doblePairPlayer1.IsReadOnly = false;
                doblePairPlayer1.Background = Brushes.Yellow;
                doblePairPlayer1.UpdateLayout();
            }
            else if (jugada == "Pair!" && pairPlayer1.Text == "")
            {
                pairPlayer1.IsReadOnly = false;
                pairPlayer1.Background = Brushes.Yellow;
                pairPlayer1.UpdateLayout();
            }
            else if (jugada == "Small Straight!" && smallStrightPlayer1.Text == "")
            {
                smallStrightPlayer1.IsReadOnly = false;
                smallStrightPlayer1.Background = Brushes.Yellow;
                smallStrightPlayer1.UpdateLayout();
            }
            else if (jugada == "Big Straight!" && bigStrightPlayer1.Text == "")
            {
                bigStrightPlayer1.IsReadOnly = false;
                bigStrightPlayer1.Background = Brushes.Yellow;
                bigStrightPlayer1.UpdateLayout();
            }
            else//jugada == "chance" 
            {
                if (chancePlayer1.Text == "")
                {
                    chancePlayer1.IsReadOnly = false;
                    chancePlayer1.Background = Brushes.Yellow;
                    chancePlayer1.UpdateLayout();
                }
            }

        }



        private void closeMoves()
        {
            diceTextOne.IsReadOnly = true;
            diceTextTwo.IsReadOnly = true;
            diceTextThree.IsReadOnly = true;
            diceTextFour.IsReadOnly = true;
            diceTextFive.IsReadOnly = true;
            timesLeft.IsReadOnly = true;
            results.IsReadOnly = true;
            
            if (AcesPlayer1.Text == "")
            {
                AcesPlayer1.Background = Brushes.White;
            }
            if (twosPlayer1.Text == "")
            {
                twosPlayer1.Background = Brushes.White;
            }
            if (threesPlayer1.Text == "")
            {
                threesPlayer1.Background = Brushes.White;
            }
            if (foursPlayer1.Text == "")
            {
                foursPlayer1.Background = Brushes.White;
            }
            if (fivesPlayer1.Text == "")
            {
                fivesPlayer1.Background = Brushes.White;
            }
            if (sixesPlayer1.Text == "")
            {
                sixesPlayer1.Background = Brushes.White;
            }
            AcesPlayer1.IsReadOnly = true;
            twosPlayer1.IsReadOnly = true;
            threesPlayer1.IsReadOnly = true;
            foursPlayer1.IsReadOnly = true;
            fivesPlayer1.IsReadOnly = true;
            sixesPlayer1.IsReadOnly = true;

            totalScorePlayer1.IsReadOnly = true;
            bonusPlayer1.IsReadOnly = true;
            subtotalPlayer1.IsReadOnly = true;

            if (pairPlayer1.Text == "")
            {
                pairPlayer1.Background = Brushes.White;
            }
            if (doblePairPlayer1.Text == "")
            {
                doblePairPlayer1.Background = Brushes.White;
            }
            if (threeKindPlayer1.Text == "")
            {
                threeKindPlayer1.Background = Brushes.White;
            }
            if (fourKindPlayer1.Text == "")
            {
                fourKindPlayer1.Background = Brushes.White;
            }
            if (fullHousePlayer1.Text == "")
            {
                fullHousePlayer1.Background = Brushes.White;
            }
            if (smallStrightPlayer1.Text == "")
            {
                smallStrightPlayer1.Background = Brushes.White;
            }
            if (bigStrightPlayer1.Text == "")
            {
                bigStrightPlayer1.Background = Brushes.White;
            }
            if (chancePlayer1.Text == "")
            {
                chancePlayer1.Background = Brushes.White;
            }
            if (yatzeePlayer1.Text == "")
            {
                yatzeePlayer1.Background = Brushes.White;
            }
            pairPlayer1.IsReadOnly = true;
            doblePairPlayer1.IsReadOnly = true;
            threeKindPlayer1.IsReadOnly = true;
            fourKindPlayer1.IsReadOnly = true;
            fullHousePlayer1.IsReadOnly = true;
            smallStrightPlayer1.IsReadOnly = true;
            bigStrightPlayer1.IsReadOnly = true;
            chancePlayer1.IsReadOnly = true;
            yatzeePlayer1.IsReadOnly = true;

            totalPlayer1.IsReadOnly = true;
        }


        private string selectOption(string actualGame)
        {

            return actualGame;
        }



        private void Restart_Click(object sender, RoutedEventArgs e)
        {

            checkFirstDice.IsChecked = true;
            checkSecondDice.IsChecked = true;
            checkThirdDice.IsChecked = true;
            checkFourthDice.IsChecked = true;
            checkFifthDice.IsChecked = true;
            cont = 3;
            timesLeft.Text = cont.ToString();


            diceTextOne.Text = "";
            diceTextTwo.Text = "";
            diceTextThree.Text = "";
            diceTextFour.Text = "";
            diceTextFive.Text = "";

            AcesPlayer1.Text = "";
            twosPlayer1.Text = "";
            threesPlayer1.Text = "";
            foursPlayer1.Text = "";
            fivesPlayer1.Text = "";
            sixesPlayer1.Text = "";

            pairPlayer1.Text = "";
            doblePairPlayer1.Text = "";
            threeKindPlayer1.Text = "";
            fourKindPlayer1.Text = "";
            fullHousePlayer1.Text = "";
            smallStrightPlayer1.Text = "";
            bigStrightPlayer1.Text = "";
            chancePlayer1.Text = "";
            yatzeePlayer1.Text = "";

            results.Text = "Reset done.\n";
            closeMoves();
        }





        private void AcesPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (AcesPlayer1.Text == "")
                {
                    selectGameTable("Aces", AcesPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Aces already selected. Please choose other.\n");
                }
            }
           
        }

        private void TwosPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (twosPlayer1.Text == "")
                {
                    selectGameTable("Twos", twosPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Twos already selected. Please choose other.\n");
                }
            }
            
        }

        private void ThreesPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (threesPlayer1.Text == "")
                {
                    selectGameTable("Threes", threesPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Threes already selected. Please choose other.\n");
                }
            }
        }

        private void FoursPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (foursPlayer1.Text == "")
                {
                    selectGameTable("Fours", foursPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Fours already selected. Please choose other.\n");
                }
            }
            
        }

        private void FivesPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (fivesPlayer1.Text == "")
                {
                    selectGameTable("Fives", fivesPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Fives already selected. Please choose other.\n");
                }
            }
            
        }

        private void SixesPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (sixesPlayer1.Text == "")
                {
                    selectGameTable("Sixes", sixesPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Sixes already selected. Please choose other.\n");
                }
            }
        }

        private void PairPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (pairPlayer1.Text == "")
                {
                    selectGameTable("Pair!", pairPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Pair already selected. Please choose other.\n");
                }
            }
            
        }

        private void DoblePairPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (doblePairPlayer1.Text == "")
                {
                    selectGameTable("Doble Pair!", doblePairPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Doble Pair already selected. Please choose other.\n");
                }
            }
            
        }

        private void ThreeKindPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (threeKindPlayer1.Text == "")
                {
                    selectGameTable("Three of a Kind!", threeKindPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Three of a Kind already selected. Please choose other.\n");
                }
            }
            
        }

        private void FourKindPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (fourKindPlayer1.Text == "")
                {
                    selectGameTable("Four of a Kind!", fourKindPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Four of a Kind already selected. Please choose other.\n");
                }
            }
            
        }

        private void FullHousePlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (fullHousePlayer1.Text == "")
                {
                    selectGameTable("Full House!", fullHousePlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Full House already selected. Please choose other.\n");
                }
            }
            
        }

        private void smallStrightPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (smallStrightPlayer1.Text == "")
                {
                    selectGameTable("Small Straight!", smallStrightPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Small Stright already selected. Please choose other.\n");
                }
            }
            
        }

        private void BigStrightPlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (bigStrightPlayer1.Text == "")
                {
                    selectGameTable("Big Straight!", bigStrightPlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Big Stright already selected. Please choose other.\n");
                }
            }
        }

        private void ChancePlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (chancePlayer1.Text == "")
                {
                    selectGameTable("chance", chancePlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Chance already selected. Please choose other.\n");
                }
            }
        }

        private void YatzeePlayer1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (timesLeft.Text != 0.ToString())
            {
                MessageBox.Show("Throw dices three time and then choose your game.\n");
            }
            else if (alreadySelected)
            {
                MessageBox.Show("You choose already. Throw dices again.\n");
            }
            else
            {
                if (yatzeePlayer1.Text == "")
                {
                    selectGameTable("Yatzee!", yatzeePlayer1);
                    alreadySelected = true;
                }
                else
                {
                    MessageBox.Show("Yatzee already selected. Please choose other.\n");
                }
            }
            
        }
    }
}
