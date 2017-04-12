using System;

namespace YourNamespace.Controllers {
    public class Dojodachi {
        Random rand = new Random();
        public int Fullness = 20;
        public int Happiness = 20;
        public int Energy = 50;
        public int Meals = 3;
        public bool Alive = true;
        public bool Win = false;
        public bool Playing = true;
        public string Image = "creature.png";

        public string feed() {
            int odds = rand.Next(1,5);
            
            if (Meals > 0) {
                Meals--;
                if (odds == 1 || odds == 2 || odds == 3) {
                    int number = rand.Next(5,11);
                    Fullness += number;
                    return ("You just fed your Dojodachi and gained " + number + " fullness.");
                }
                else {
                    return ("Eh, your dojodachi doesn't seem to be hungry.");
                }
            }
            else {
                return ("You do not have any remaining meals.");
            }
        }

        public string happiness() {
            int odds = rand.Next(1,5);
            Energy -= 5;

            if (odds == 1 || odds == 2 || odds == 3) {
                int number = rand.Next(5,11);
                Happiness += number;
                return ("You just gained " + number + " happiness.");
            }
            else {
                return ("Your dojodachi doesn't want to play right now.");
            }
        }

        public string work() {
            if (Energy >= 5) {
                Energy -= 5;
                int number = rand.Next(1,4);
                Meals += number;
                return ("Hooray! You just gained " + number + " meals.");
            }
            else {
                return ("You don't have enough energy. Boo!");
            }
        }

        public string sleep() {
            if (Fullness > 0 && Happiness > 0) {
                Energy += 15;
                Fullness -= 5;
                Happiness -= 5;
                return ("You just gained 15 energy.");
            }
            else {
                return ("You don't have enough fullness and/or happiness.");
            }
        }
    }
}