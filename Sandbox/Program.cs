using System;
using System.Linq;
using System.Collections.Generic;

class Lottery
{
    static void Main()
    {
        /***************************************************************
        |                          Variables                           |
        ***************************************************************/

        // Generate a random number generator object
        Random ranNumberGenerator = new Random();

        // User Guesses to use against the random numbers for the lottery
        int u1;
        int u2;
        int u3;

        // Number of Lottery Numbers to be chosen
        const int lotteryNumbers = 3;

        // Variable to check if user wants to quit at the end
        string userResponse;



        /**************************************************************
        |                       Main Lottery Program                  |
        **************************************************************/
        while (true)
        {

            // Display the rules of the game
            Console.WriteLine("Welcome to Peter's Lottery!");
            Console.WriteLine();
            Console.WriteLine("The rules are simple:");
            Console.WriteLine();
            Console.WriteLine("Pick any number between 1 and 4");
            Console.WriteLine("Press <ENTER> and pick again");
            Console.WriteLine("After 3 picks, you will see if you won!");
            Console.WriteLine("To win the Grand Prize of $10,000.00");
            Console.WriteLine("you must get 3 unique numbers in order");
            Console.WriteLine("Best of luck and may the odds be ever in your favor!");
            Console.WriteLine();
            
            /* Ask the user for 3 lottery numbers
            *  TryParse them incase null or alphabetical chars
            *  are inserted into the variables. Will default
            * to 0's in this case which is a loss in the game.
            */
            Console.Write("Enter Lottery #1 (1-4): ");
            int.TryParse(Console.ReadLine(), out u1);
            Console.Write("Enter Lottery #2 (1-4): ");
            int.TryParse(Console.ReadLine(), out u2);
            Console.Write("Enter Lottery #3 (1-4): ");
            int.TryParse(Console.ReadLine(), out u3);

            // Loop through and store the randomly generated lottery numbers
            // to a list. Store the users lottery numbers to a list
            List<int> results = new List<int> { };
            for (int i = 0; i < lotteryNumbers; i++)
            {
                results.Add(ranNumberGenerator.Next(1, 5));
            }
            List<int> guesses = new List<int> { u1, u2, u3 };
            List<int> matched = new List<int> { };

            // Code to check matches from the user's lottery numbers against 
            // the computers lottery results

            /********************************************************************************************** 
            |   Ok so this section is going to be my little notepad to hash out ideas.                    |
            |   and write down my thoughts/thought process so I can stay organized.                       |
            |                                                                                             |
            | 1.) So first we need to check to see if there is even just one match.                       |
            |    That means that u1-3 has to match r1-3. This is a total of 9 possible checks             |
            |    that we need to do in order to see if there is a match. A foreach loop should            |
            |    achieve this easy enough.                                                                |
            |                                                                                             |
            | 2.) So the foreach loop will go through and look at the user guesses. It loops over         |
            |    each one of those and checks if they are contained within the results list.              |
            |    If there is a match, it will add the match to the matched list, and then remove          |
            |    the match from the results list. This is in an attempt to possibly remove duplicates.    |
            |    Because I am removing items from the list, this will be the last bit of logic that       |
            |    is done since the list will be modified.                                                 |
            |                                                                                             |
            | 3.) Now I need to figure out how to handle duplicates. Since the computer or the user       |
            |    can insert duplicates and the program requires they not be counted for winnings,         |
            |    I must find a way to search and see if they exist and remove them properly so            |
            |    that the proper amount can be assigned to the user.                                      |
            |                                                                                             |
            | 4.) So an idea that I am going to try is to initially remove the first match, and inside    |
            |    that before looping again, check to see if that result appears again in the results      |
            |    list. If it does, I just remove that result so that duplicates are not counted.          |
            |    This should effectivly remove the ability for duplicate numbers granting an              |
            |    additional prize. If no duplicates are found again, then it should just jump             |
            |    back to the foreach loop and continue as normal. For each unique match found             |
            |    it will add that match to a seperate list and I can use the list.Count() method          |
            |    to see how many matches ultimately where achieved regardless of order in the list.       |
            |                                                                                             |
            | 5.) That seems to work beautifully! Yay! Ok, so the only thing this little algorithem       |
            |    doesn't check for is a match in order. Luckily C# has the ability to sequentially        |
            |    check to see if two lists are identical. So I think I should wrap everything inside      |
            |    of an if else statement, and initially check to see if the lists are identical. After    |
            |    that, I can run the loops to see how many matches actually were obtained, and from       |
            |    there go about doing some if-else-if statements to see the Count of the list and         |
            |    assign the appropriate winning amounts.                                                  |
            |                                                                                             |
            | 6.) Sweet baby jesus it all actually works! From what I can tell anyways, this actually     |
            |    works as intended. For anyone that actually bothers to read this though, I can           |
            |    honestly say this code is my own and that this result did not come from any research     |
            |    that I performed looking into possible issues. Pretty proud of that. All suggestions     |
            |    I came across were using multiple if else-if statements or complicated regex. I didn't   |
            |    want to do hard coded if statements because its tedious and limits you. I hate regex     |
            |    so I avoid it at all possible costs. Luckily for this program and the requirements set   | 
            |     forth it seems what I have come up with does the job perfectly.                         |
            |                                                                                             |
            |                                                                                             |
            |                                                                                             |
            **********************************************************************************************/


            // Output the winning numbers with a foreach loop.
            Console.WriteLine();
            Console.Write("Winning Numbers: ");
            foreach (var result in results)
            {
                Console.Write($"# {result} ");
            }

            // Output the user's lottery numbers with a foreach loop
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("User's  Numbers: ");
            foreach (var guess in guesses)
            {
                Console.Write($"# {guess} ");
            }
            Console.WriteLine();
            Console.WriteLine();

            /* Check if the user's lottery numbers match in order to the winning numbers
            *  If they do, output that the user has won $10,000.00
            *  If they are not in order, use Enumerable.All to check if the lists contain
            *  the same numbers regardless of order and if it does, award the proper amount.
            */
            if (guesses.SequenceEqual(results))
            {
                Console.WriteLine("You have won $10,000.00. Congratulations!");
            }
            else
            {
                /* Not going to lie, proud of this bit of logic
                *  Loop through the user's lottery numbers and compare it against the winning numbers
                *  If the number matches, add it to the matched list, and remove it from the results
                *  list. Loop once more to check if any duplicates exist, and remove them completely if
                *  they do. This allows for unique matches only. Then loop again with the user's next
                *  lottery number until all matches have been found.
                */
                foreach (int x in guesses)
                {
                    if (results.Contains(x))
                    {
                        matched.Add(x);
                        results.Remove(x);

                        // Loop through to see if there are any duplicates and remove them
                        while (results.Contains(x))
                        {
                            results.Remove(x);
                        }
                    }
                }

                /* The rules state the following prizes are awarded:
                *  1. No matches                              = $0.00
                *  2. One (1) unique match                    = $10.00
                *  3. Two (2) unique matches                  = $100.00
                *  4. Three (3) matches (in any order)        = $1000.00
                *  5. Three (3) matches in order              = $10,000.00
                *  
                *  This if/else-if statement checks the count of matches
                *  and awards the corresponding prize amount.
                */
                if (matched.Count == 0)
                {
                    Console.WriteLine("Sorry, you did not win anything! Better luck next time.");
                }
                else if (matched.Count == 1)
                {
                    Console.WriteLine("You have won $10.00. Congratulations!");
                }
                else if (matched.Count == 2)
                {
                    Console.WriteLine("You have won $100.00. Congratulations!");
                }
                else if (matched.Count == 3)
                {
                    Console.WriteLine("You have won $1,000.00. Congratulations!");
                }
                else
                {
                    Console.WriteLine("Sorry, if you are seeing this, then something went wrong. Please try again.");
                }
            }
            Console.WriteLine();
            Console.Write("Type Q to quit or Press <ENTER> to play again: ");
            userResponse = Console.ReadLine().ToLower();

            // Loop the program until the user wants to quit
            if (userResponse == "q")
            {
                Environment.Exit(0);
            }
            Console.Clear();
        }
    }
}