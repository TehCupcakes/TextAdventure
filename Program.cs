using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextAdventure
{
    public class Process
    {
        private int location, prev_location;
        public Inventory inv;
        public KeywordHandler kwh;

        private const bool CHEAT = false;       // For testing purposes; Response in the room you died in.

        public const int START = 0;
        public const int WALL = 1;
        public const int WALL_ROPE = 2;
        public const int CRATES = 3;
        public const int CRATES_TOWER = 4;
        public const int CRATES_HOLE = 5;
        public const int BEAR_CAVE = 6;
        public const int BEAR_HIBERNATE = 7;
        public const int BEAR_KILLED = 8;
        public const int DEATH = 999;
        public const int WIN = 1000;

        public Process()
        {
            location = START;
            prev_location = START;
            //kwh MUST be created before inv
            kwh = new KeywordHandler(this);
            inv = new Inventory(this);
        }

        public bool AcceptInput()
        {
            string input = "";
            input = Console.ReadLine().ToLower();

            return kwh.HandleKeyword(input);
        }

        public void DisplayRoom()
        {
            string response = "";
            Console.WriteLine("\r\n-------------------------------------------------------------\r\n");
            switch(location)
            {
                case START:
                    response = "You are in a room... You are surrounded by four walls. The room is dark and it's hard to see. " + inv.ToShortString() + " Enter a keyword.";
                    break;
                case WALL:
                    response = "There is a solid cement wall in front of you.";
                    break;
                case WALL_ROPE:
                    response = "There is a solid cement wall in front of you and a rope dangling from the ceiling.";
                    break;
                case CRATES:
                    response = "You look around the room and see many crates of various sizes.";
                    break;
                case CRATES_TOWER:
                    response = "From here you can see an opening on the other side of the room that you can't get to.";
                    break;
                case CRATES_HOLE:
                    response = "You look around the room and see many crates of various sizes. There is a hole in one of the walls that " +
                                "you might be able to squeeze through.";
                    break;
                case BEAR_CAVE:
                    response = "There's a giant grizzly bear standing right in front of you! Better act quick!";
                    break;
                case BEAR_HIBERNATE:
                    response = "The grizzly bear is sleeping soundly... Now might be your only chance to escape while he hibernates.";
                    break;
                case BEAR_KILLED:
                    response = "A room where you once killed a bear. Nothing to do here.";
                    break;
                case DEATH:
                    Console.WriteLine("      GAME OVER      \r\n" +
"     _.--\"\"--._          \r\n" +
"    /  _    _  \\          \r\n" +
" _  ( (_\\  /_) )  _       \r\n" +
"{ \\._\\   /\\   /_./ }    \r\n" +
"/_\"=-.}______{.-=\"_\\    \r\n" +
" _  _.=(\"\"\"\")=._  _    \r\n" +
"(_'\"_.-\"`~~`\"-._\"'_)   \r\n" +
" {_\"            \"_}     ");
                    Console.Write("Press any key to try again...");
                    Console.ReadKey();
                    Restart();
                    return;
                case WIN:
                    response = "CONGRATULATIONS! You have escaped and made your way to freedom!";
                    break;
                default:
                    response = "I don't know where you are. You should probably quit.";
                    break;
            }

            Console.WriteLine(response + "\r\n ||\r\n \\/");
        }

        public void SetLocation(int loc)
        {
            this.prev_location = this.location;
            this.location = loc;
        }

        public int GetLocation()
        {
            return location;
        }

        public void Restart()
        {
            if(CHEAT)
                location = prev_location;
            else
            {
                location = START;
                prev_location = START;
                //kwh MUST be created before inv
                kwh = new KeywordHandler(this);
                inv = new Inventory(this);
                Console.Clear();
            }
            DisplayRoom();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Process proc = new Process();
            proc.DisplayRoom();

            while (true)
            {
                if(proc.AcceptInput())
                    proc.DisplayRoom();
            }
        }
    }
}
