using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Keyword
    {
        private string keyword;             //This is what shows in the keyword list; multiple strings can share one assigned keyword.
        private string help;                //This is the text that shows for this keyword when the player types "help"
        private int status;                 //Whether the word starts as hidden, unlocked, or discovered.
        private bool global;                //Whether or not this word has the same response everywhere. (Special keywords like "quit" and "items".)
        private bool item;                  //Whether or not the keyword references an item.
        private List<string> aliases;       //A list of strings that can share this keyword.

        public const int LOCKED = 0;        //Some condition must be met before this keyword can be used.
        public const int HIDDEN = 1;        //Player has unlocked this keyword, but hasn't used it yet. (Not listed in help text.)
        public const int DISCOVERED = 2;    //Player has used this word at least once. (Will be listed in "help" if keyword has help text.)

        public Keyword(string word, string helpTxt, int defaultStatus, bool glob, bool itm)
        {
            keyword = word;
            help = helpTxt;
            status = defaultStatus;
            global = glob;
            item = itm;
            aliases = new List<string>();
            aliases.Add(word);          //The string of the keyword itself is always an alias.
        }

        public void Discover(bool msg = true)
        {
            status = DISCOVERED;

            if (msg)
                Console.WriteLine("\r\nYou have discovered a new word! Keyword: " + GetKeyword());
        }

        public void Unlock()
        {
            status = HIDDEN;
        }

        public void Lock()
        {
            status = LOCKED;
        }

        public void AddAlias(string alias)
        {
            aliases.Add(alias);
        }

        public bool HasAlias(string alias)
        {
            foreach (var word in aliases)
            {
                if (word == alias)
                    return true;
            }

            return false;
        }

        public string GetKeyword()
        {
            return keyword;
        }

        public string GetHelp()
        {
            return help;
        }

        public int GetStatus()
        {
            return status;
        }

        public bool IsLocked()
        {
            return status == LOCKED;
        }

        public bool IsHidden()
        {
            return status == HIDDEN;
        }

        public bool IsDiscovered()
        {
            return status == DISCOVERED;
        }

        public bool IsItem()
        {
            return item;
        }

        public bool IsGlobal()
        {
            return global;
        }

        public override string ToString()
        {
            return keyword;
        }
    }
}
