using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Inventory
    {
        private Process proc;
        private List<string> inv;

        public Inventory(Process p)
        {
            proc = p;
            inv = new List<string>(); //Create a list for the inventory
            AddItem("candle", "A big wax candle. Could burn for a while if lit.");
            AddItem("stick", "A fat piece of wood.");
        }

        public void AddItem(string itm, string helpTxt = "")
        {
            inv.Add(itm);
            //Check if a keyword exists for this item; if so, unlock; if not, add one
            Keyword kw = proc.kwh.GetKeywordFromString(itm);
            if (kw != null)
                kw.Unlock();
            else
                proc.kwh.AddKeyword(itm, helpTxt, Keyword.HIDDEN, false, true);
        }

        public bool HasItem(string itm)
        {
            if(inv.IndexOf(itm) >= 0)
                return true;

            return false;
        }

        //Removes an inv from the inventory
        //returns true if item was removed successfully
        public bool RemoveItem(string itm)
        {
            if(HasItem(itm))
            {
                inv.Remove(itm);
                return true;
            }

            return false;
        }

        public string ToShortString()
        {
            string str = "You have ";
            int size = inv.Count;

            if (size > 0)
            {
                int i = 1;
                foreach (var itm in inv)
                {
                    char firstChar = itm.ToLower()[0];
                    if (firstChar.Equals('a') || firstChar.Equals('e') || firstChar.Equals('i') || firstChar.Equals('o') || firstChar.Equals('u'))
                        str += "an ";
                    else
                        str += "a ";
                    str += itm.ToLower();
                    if (i < size)
                    {
                        if(size > 2)
                            str += ",";
                        str += " ";
                        if (i == size - 1)
                            str += "and ";
                    }
                    else
                        str += ".";

                    i++;
                }
            }
            else
                str += "no items.";

            return str;
        }

        public override string ToString()
        {
            string str = "";
            int size = inv.Count;

            if (size > 0)
            {
                str = "Here are the items you have:";
                int i = 1;
                foreach (var itm in inv)
                {
                    str += "\r\n|| " + itm.ToLower();
                    if (proc.kwh.GetKeywordFromString(itm).GetHelp() != "")
                        str += " - " + proc.kwh.GetKeywordFromString(itm).GetHelp();

                    i++;
                }
            }
            else
                str += "You have no items.";

            return str;
        }
    }
}