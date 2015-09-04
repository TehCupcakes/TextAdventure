using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class KeywordHandler
    {
        private Process proc;
        private List<Keyword> keywords;

        public KeywordHandler(Process p)
        {
            proc = p;

            Keyword newKw;
            keywords = new List<Keyword>(); //Stores all keywords in the game

            //Add all the global keywords (Do NOT include items)
            newKw = AddKeyword("help", "Bring up this help dialog.", Keyword.DISCOVERED, true);
                newKw.AddAlias("advice");
                newKw.AddAlias("h");
                newKw.AddAlias("keyword");
                newKw.AddAlias("keywords");
            newKw = AddKeyword("items", "Lists the items in your inventory.", Keyword.DISCOVERED, true);
                newKw.AddAlias("item");
                newKw.AddAlias("inv");
                newKw.AddAlias("inventory");
                newKw.AddAlias("supply");
                newKw.AddAlias("supplies");
            newKw = AddKeyword("save", "", Keyword.HIDDEN, true);     //Hidden because it currently doesn't do anything
                newKw.AddAlias("s");
            newKw = AddKeyword("exit", "Quit the game.", Keyword.DISCOVERED, true);
                newKw.AddAlias("escape");
                newKw.AddAlias("quit");
                newKw.AddAlias("q");
                newKw.AddAlias("x");
            //Add all keywords that are locked at game start (Again, do NOT include items)
            newKw = AddKeyword("climb", "Climb up something.", Keyword.LOCKED);
                newKw.AddAlias("ascend");
                newKw.AddAlias("tower");
            AddKeyword("destroy", "Use your super strength to destroy stuff.", Keyword.LOCKED);
            AddKeyword("down", "Climb down from somewhere high up.", Keyword.LOCKED);
            AddKeyword("hole", "Crawl through a hole.", Keyword.LOCKED);
            AddKeyword("rope", "", Keyword.LOCKED);
            AddKeyword("win", "Win the game!", Keyword.LOCKED);
            //Add all keywords that start unlocked (Defaulted to HIDDEN status)
            newKw = AddKeyword("back", "Go back to where you came from. (Or look back)", Keyword.DISCOVERED);
                newKw.AddAlias("backtrack");
                newKw.AddAlias("leave");
                newKw.AddAlias("withdraw");
            newKw = AddKeyword("crawl");
                newKw.AddAlias("creep");
                newKw.AddAlias("slither");
                newKw.AddAlias("squirm");
            newKw = AddKeyword("cry");
                newKw.AddAlias("bawl");
                newKw.AddAlias("mourn");
                newKw.AddAlias("sob");
                newKw.AddAlias("wail");
                newKw.AddAlias("weep");
            AddKeyword("dig");
            newKw = AddKeyword("eat");
                newKw.AddAlias("bite");
                newKw.AddAlias("chew");
                newKw.AddAlias("devour");
                newKw.AddAlias("ingest");
                newKw.AddAlias("swallow");
            AddKeyword("fly", "Try to fly. (Pfft... Good luck.)");
            newKw = AddKeyword("hit");
                newKw.AddAlias("bang");
                newKw.AddAlias("break");
                newKw.AddAlias("punch");
                newKw.AddAlias("smack");
                newKw.AddAlias("smash");
                newKw.AddAlias("strike");
            newKw = AddKeyword("jump");
                newKw.AddAlias("hurdle");
                newKw.AddAlias("vault");
            newKw = AddKeyword("kick");
                newKw.AddAlias("boot");
                newKw.AddAlias("punt");
            newKw = AddKeyword("look", "Take a closer look at your surroundings.");
                newKw.AddAlias("glance");
                newKw.AddAlias("observe");
                newKw.AddAlias("peek");
                newKw.AddAlias("peer");
                newKw.AddAlias("see");
                newKw.AddAlias("stare");
                newKw.AddAlias("view");
            AddKeyword("open");
            AddKeyword("pray", "Pray for guidance. It can't hurt... Usually.");
            newKw = AddKeyword("pull");
                newKw.AddAlias("drag");
                newKw.AddAlias("tug");
                newKw.AddAlias("yank");
            newKw = AddKeyword("push");
                newKw.AddAlias("nudge");
                newKw.AddAlias("shove");
            newKw = AddKeyword("run");
                newKw.AddAlias("dash");
                newKw.AddAlias("rush");
                newKw.AddAlias("sprint");
            newKw = AddKeyword("scratch");
                newKw.AddAlias("claw");
                newKw.AddAlias("scrape");
            newKw = AddKeyword("sit");
                newKw.AddAlias("rest");
                newKw.AddAlias("squat");
            newKw = AddKeyword("die", "Kill yourself... This isn't really useful, it's just entertaining.", Keyword.DISCOVERED);
                newKw.AddAlias("suicide");
            newKw = AddKeyword("sleep");
                newKw.AddAlias("doze");
                newKw.AddAlias("nap");
                newKw.AddAlias("snooze");
            newKw = AddKeyword("touch");
                newKw.AddAlias("extend");
                newKw.AddAlias("feel");
                newKw.AddAlias("pat");
                newKw.AddAlias("reach");
                newKw.AddAlias("rub");
                newKw.AddAlias("stroke");
                newKw.AddAlias("tap");
            newKw = AddKeyword("walk");
                newKw.AddAlias("jog");
                newKw.AddAlias("move");
                newKw.AddAlias("step");
                newKw.AddAlias("stroll");
                newKw.AddAlias("travel");
            AddKeyword("wall", "Move towards a wall.");
            newKw = AddKeyword("yell");
                newKw.AddAlias("bellow");
                newKw.AddAlias("hoot");
                newKw.AddAlias("holler");
                newKw.AddAlias("howl");
                newKw.AddAlias("roar");
                newKw.AddAlias("scream");
                newKw.AddAlias("shout");
                newKw.AddAlias("yelp");
        }

        public Keyword AddKeyword(string word, string hlpTxt = "", int defaultStatus = Keyword.HIDDEN, bool glob = false, bool itm = false)
        {
            Keyword kw = new Keyword(word, hlpTxt, defaultStatus, glob, itm);
            keywords.Add(kw);
            return kw;
        }

        public bool RemoveKeyword(string alias)
        {
            Keyword kw = GetKeywordFromString(alias);
            if (kw != null)
            {
                keywords.Remove(kw);
                return true;
            }

            return false;
        }

        public bool KeywordUnlocked(string alias)
        {
            Keyword kw = GetKeywordFromString(alias);
            if (!kw.IsLocked())
                return true;

            return false;
        }

        public void LockKeyword(string alias)
        {
            Keyword kw = GetKeywordFromString(alias);
            if (kw != null)
                kw.Lock();
        }

        public void UnlockKeyword(string alias)
        {
            Keyword kw = GetKeywordFromString(alias);
            if (kw != null)
                kw.Unlock();
        }

        public void DiscoverKeyword(string alias, bool msg = true)
        {
            Keyword kw = GetKeywordFromString(alias);
            if (kw != null)
                kw.Discover(msg);
        }

        public bool HandleKeyword(string alias)
        {
            string response = "";
            int loc = proc.GetLocation();

            Keyword kw = ValidateKeyword(alias);

            if (kw != null)
            {
                string input = kw.GetKeyword();
                response = "-------------------------------------------------------------\r\n\r\n";

                if (kw.IsItem() && !proc.inv.HasItem(kw.GetKeyword()))
                    response += "You do not have that item.";
                else
                {
                    // Handle global keywords separately from normal keywords.
                    if (kw.IsGlobal())
                    {
                        switch (input)
                        {
                            case "help":
                            case "h":
                            case "keyword":
                            case "keywords":
                                response += "Text adventure games are played by typing words based on the actions you want to perform and the items you want to interract with. Verbs " +
                                            "are often good keywords. A lot of times, you can also interract with objects in the room you are in by typing the name of whatever it is " +
                                            "you want to interract with. Experiment a bit and you'll be making progress in no time. In this game, all keywords are one word.\r\n" +
                                            ToString();
                                break;
                            case "items":
                            case "item":
                            case "inv":
                            case "inventory":
                                response += proc.inv.ToString();
                                break;
                            case "save":
                            case "s":
                                response += "At this time, there is no save feature. Sorry. :(";
                                break;
                            case "exit":
                            case "quit":
                            case "q":
                            case "x":
                                Console.WriteLine("\r\nAre you sure you want to quit the game? Press Y for yes.");
                                if (Console.ReadKey().Key.ToString().ToUpper() == "Y")
                                    System.Environment.Exit(1);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (loc)
                        {
                            case Process.START:
                                if (input == "back") response += "You look behind you and see a wall.";
                                else if (input == "crawl") response += "You crawl along the floor. It's dirty.";
                                else if (input == "cry" || input == "sit") response += "You sit on the ground and cry. How pitiful.";
                                else if (input == "dig") response += "You can't dig through a cement floor!";
                                else if (input == "eat") response += "You have nothing to eat.";
                                else if (input == "fly") response += "You don't know how to fly. :(";
                                else if (input == "jump") response += "You jump and hit your head on the ceiling. You're pretty tall.";
                                else if (input == "kick") response += "You kick yourself for no reason.";
                                else if (input == "look") response += "You look around the room and just see walls.";
                                else if (input == "match") response += "You don't have anything to light the match on.";
                                else if (input == "open") response += "There's nothing to open here.";
                                else if (input == "pray") response += "You pray for help and you hear a voice say, \"Why don't you try to 'walk'?\"";
                                else if (input == "punch") response += "You punch the air because you can't see anything.";
                                else if (input == "run") response += "You run around in circles, not knowing what to do.";
                                else if (input == "scratch") response += "You scratch your head, unsure of what to do.";
                                else if (input == "sleep") response += "You sleep for a bit and wake up still trapped in this room.";
                                else if (input == "stick") response += "You flail your stick aimlessly.";
                                else if (input == "touch") response += "You touch the floor and nothing happens.";
                                else if (input == "torch") response += "Yes, you have a torch. Good for you.";
                                else if (input == "yell") response += "You let out a yell and all that comes back is your own echo.";
                                else if (input == "candle")
                                {
                                    response += "You attempt to eat the candle. You are allergic to wax. You die.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "die" || input == "hit")
                                {
                                    response += "You brutally beat yourself to death because you can't deal with the stress from being trapped.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "pebble")
                                {
                                    response += "You swallow the pebble (hey, you're starving!) and it gives you clairvoyance. You can see a hidden " +
                                                "compartment in one of the walls. Walk up to it and try pushing on the wall to reveal the compartment.";
                                    proc.inv.RemoveItem("pebble");
                                }
                                else if (input == "wall" || input == "walk")
                                {
                                    //Check if the rope has been unlocked
                                    if (KeywordUnlocked("rope"))
                                    {
                                        response += "You walk up to the wall. You see a rope dangling down.";
                                        proc.SetLocation(Process.WALL_ROPE);
                                    }
                                    else
                                    {
                                        response += "You walk up to the wall.";
                                        proc.SetLocation(Process.WALL);
                                    }
                                }
                                break;
                            case Process.WALL:
                            case Process.WALL_ROPE:
                                if (input == "crawl") response += "You crawl and hit your head on the wall.";
                                else if (input == "cry") response += "You cry like a baby.";
                                else if (input == "dig") response += "The wall is too hard to dig through.";
                                else if (input == "eat") response += "You lick the wall... Mmm, yummy.";
                                else if (input == "fly") response += "You are human. You don't have wings. You could try to 'jump', though.";
                                else if (input == "kick") response += "You kick the wall with all your might. It hurts. You dummy.";
                                else if (input == "look") response += "You're looking straight at a wall. Nothing to see here.";
                                else if (input == "open") response += "How exactly do you plan to open a WALL?";
                                else if (input == "scratch") response += "You try scratching the wall. It does not move.";
                                else if (input == "sit") response += "You sit on the floor and think.";
                                else if (input == "stick") response += "You bang your stick on the wall. Nothing happens.";
                                else if (input == "touch" || input == "wall") response += "You feel along the wall... It feels like it might move with a little more force.";
                                else if (input == "yell") response += "You yell at the wall and hear some rumbling above you.";
                                else if (input == "back")
                                {
                                    response += "You go back to the middle of the room.";
                                    proc.SetLocation(Process.START);
                                }
                                else if (input == "candle")
                                {
                                    if (proc.inv.HasItem("match"))
                                        response += "You should try lighting the match first.";
                                    else
                                    {
                                        response += "You smother the candle on the wall. Well, that was a waste.";
                                        proc.inv.RemoveItem("candle");
                                    }
                                }
                                else if (input == "die")
                                {
                                    if (loc == Process.WALL_ROPE)
                                        response += "You make a noose with the rope and pull it snug.";
                                    else
                                        response += "You bang your head against the wall until you pass out and die of blood loss.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "hit")
                                {
                                    response += "You punch the wall and break your pinky! You can't 'hit' anything until your hand heals.";
                                    LockKeyword("hit");
                                }
                                else if (input == "jump" && loc == Process.WALL)
                                {
                                    response += "You jump and reach for the top of the wall. A rope drops down from the ceiling.";
                                    proc.SetLocation(Process.WALL_ROPE);
                                    UnlockKeyword("rope");
                                }
                                else if ((input == "jump" || input == "rope" || input == "pull") && loc == Process.WALL_ROPE)
                                {
                                    response += "You pull the rope and before you know it, you're knocked out by boulders falling on your head.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "match")
                                {
                                    response += "You light the match on the wall and use it to light your stick. You now have a torch.";
                                    proc.inv.RemoveItem("match");
                                    proc.inv.RemoveItem("stick");
                                    proc.inv.AddItem("torch", "It gives you light and can light other things and such.");
                                }
                                else if (input == "pebble")
                                {
                                    response += "You toss the pebble against the wall and it bounces to the middle of the room.";
                                    proc.SetLocation(Process.START);
                                }
                                else if (input == "pray")
                                {
                                    response += "You pray for a divine sign... A pebble falls on your head.";
                                    proc.inv.AddItem("pebble", "Really small. Not useful for much of anything.");
                                }
                                else if (input == "push")
                                {
                                    Keyword kwMatch = GetKeywordFromString("match");
                                    if (!proc.inv.HasItem("match") && (kwMatch == null || !kwMatch.IsDiscovered()))
                                    {
                                        response += "You push the wall and feel a slight vibration... Suddenly a small compartment opens with a single match. You pick up the match.";
                                        proc.inv.AddItem("match", "Used to light things.");
                                    }
                                    else
                                        response += "You know, just because you keep pushing on the wall doesn't mean more matches are going to magically appear.";
                                }
                                else if (input == "run")
                                {
                                    response += "You run into the wall. The room collapses and you die.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "sleep")
                                {
                                    response += "You go to sleep and never wake up.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "torch")
                                {
                                    if (loc == Process.WALL)
                                        response += "Using your torch to light the wall, you notice a strange pattern just above you. Maybe if you could reach it...";
                                    else if (loc == Process.WALL_ROPE)
                                    {
                                        response += "You use your torch to light the rope. You hear an explosion and realizes that rope was a fuse! Luckily, you stood away " +
                                                    "from the blast and it opened a path. ";
                                        // Get rid of the pebble because I don't want to bother making it a keyword in other rooms. It's served its purpose in START and WALL
                                        if (proc.inv.HasItem("pebble"))
                                        {
                                            response += "Unfortunately, you lost your pebble. :( ";
                                            proc.inv.RemoveItem("pebble");
                                        }
                                        response += "You proceed cautiously.";
                                        proc.SetLocation(Process.CRATES);
                                    }
                                }
                                break;
                            case Process.CRATES:
                            case Process.CRATES_HOLE:
                                if (input == "back") response += "You don't really want to go back to a room where you almost died.";
                                else if (input == "cake") response += "How is that going to help you get out of here?";
                                else if (input == "coat") response += "Your coat floats lightly... Is that a breeze?";
                                else if (input == "cry") response += "Stop it.";
                                else if (input == "dig") response += "There's nothing to dig, ya dig?";
                                else if (input == "eat") response += "Wood is not good for your digestive system.";
                                else if (input == "hit") response += "You break open a crate... It's empty.";
                                else if (input == "kick") response += "You kick a crate out of frustration.";
                                else if (input == "pray") response += "You pray for intervention... Maybe if you could get higher, someone would hear your prayers.";
                                else if (input == "sit") response += "You sit on a crate and it breaks.";
                                else if (input == "sleep") response += "You doze off for a bit.";
                                else if (input == "wall") response += "You go up to one of the walls... It looks like there might be something behind this crate.";
                                else if (input == "walk") response += "You walk around for a while... Some of the crates look a bit unusual.";
                                else if (input == "yell") response += "You are losing your sanity and decide yelling at inanimate objects might be a good idea.";
                                else if (input == "candle")
                                {
                                    response += "You toss your candle in the middle of the room and it splatters all over. Way to go.";
                                    proc.inv.RemoveItem("candle");
                                }
                                else if (input == "climb")
                                {
                                    if (KeywordUnlocked("hit"))
                                    {
                                        response += "You climb the tower of crates to get a better view.";
                                        proc.SetLocation(Process.CRATES_TOWER);
                                    }
                                    else
                                        response += "It's too difficult to climb with a broken pinky.";
                                }
                                else if (input == "crawl" && loc == Process.CRATES)
                                {
                                    response += "You crawl in between crates, but don't really find anything.";
                                }
                                else if ((input == "crawl" || input == "hole") && loc == Process.CRATES_HOLE)
                                {
                                    response += "You crawl through the hole and make it to the other side.";
                                    if (proc.inv.HasItem("coat"))
                                        proc.SetLocation(Process.BEAR_KILLED);
                                    else
                                        proc.SetLocation(Process.BEAR_CAVE);
                                }
                                else if (input == "destroy")
                                {
                                    response += "You start to destroy all the crates in the room... One of the crates contained a toxic gas that is released into the air and kills you in seconds.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "die")
                                {
                                    response += "You climb the tallest tower of crates in the room and nosedive into the cold, hard ground.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "fly")
                                {
                                    if (proc.inv.HasItem("coat"))
                                    {
                                        response += "You fly to an opening in the wall high up where you couldn't reach before.";
                                        DiscoverKeyword("win", false);
                                        proc.SetLocation(Process.WIN);
                                    }
                                    else
                                        response += "You can't fly.";
                                }
                                else if (input == "jump" && GetKeywordFromString("climb") != null)
                                {
                                    response += "You jump up on one of the crates and notice a tall tower of crates in one corner of the room. You might be able to climb that!";
                                    UnlockKeyword("climb");
                                    UnlockKeyword("down");  //Down is only used to get down from places you can climb up to, that's why it's unlocked here
                                }
                                else if (input == "jump" && GetKeywordFromString("climb") == null)
                                {
                                    //climb will only be null if it has been REMOVED from the keyword list, NOT if it is locked.
                                    response += "You jump up on one of the crates. In the corner you see rubble from the tower you destroyed.";
                                }
                                else if (input == "medicine")
                                {
                                    response += "You apply the medicine to your broken pinky and its magical properties heal your hand instantly! You can 'hit' things again!";
                                    DiscoverKeyword("hit", false);
                                }
                                else if (input == "open")
                                {
                                    if (KeywordUnlocked("hit"))
                                    {
                                        if (KeywordUnlocked("destroy"))
                                        {
                                            response += "You open the crate like a boss. You find cake mix! (What the heck is that doing here?)";
                                            proc.inv.AddItem("cake", "Cake mix somebody left in a crate.");
                                        }
                                        else
                                            response += "You aren't strong enough to open a crate with just your hands.";
                                    }
                                    else
                                        response += "This broken pinky is really bothering you! Don't even think about trying to open a crate before you get that fixed.";
                                }
                                else if (input == "push")
                                {
                                    if (KeywordUnlocked("hit"))
                                    {
                                        response += "You push crates around for hours. All that working out has given you super strength! You can now 'destroy' things with your bare hands!";
                                        DiscoverKeyword("destroy", false);
                                    }
                                    else
                                        response += "Pushing crates is tiresome with a broken pinky. Maybe one of these crates has some medicine.";
                                }
                                else if (input == "pull")
                                {
                                    if (KeywordUnlocked("hit"))
                                    {
                                        response += "You pull a box away from the wall and discover a hole just big enough that you might be able to squeeze through.";
                                        UnlockKeyword("hole");
                                        proc.SetLocation(Process.CRATES_HOLE);
                                    }
                                    else
                                        response += "You try pulling a box away from the wall... But it's too heavy to pull with one hand. You need to heal your other hand first.";
                                }
                                else if (input == "run")
                                {
                                    response += "You run through a bunch of crates and get covered in splinters. You soon get infected and die.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "torch")
                                {
                                    response += "You use your torch to burn some crates and the torch burns with them.";
                                    proc.inv.RemoveItem("torch");

                                    if (!KeywordUnlocked("hit"))
                                    {
                                        response += " Luckily, you recover a magical bottle of medicine from the crate!";
                                        proc.inv.AddItem("medicine", "Has some mystical healing properties.");
                                    }
                                }
                                break;
                            case Process.CRATES_TOWER:
                                if (input == "candle") response += "What good will that do up here?";
                                else if (input == "coat") response += "It feels like your coat is tugging you towards the cave where you killed the bear.";
                                else if (input == "eat") response += "You inhale and eat some tasty oxygen.";
                                else if (input == "look") response += "You look around and notice a few crates on the ground that look out of place.";
                                else if (input == "yell") response += "You yell... Actually it sounds a bit more like yodeling when you do it.";
                                else if (input == "back" || input == "down")
                                {
                                    response += "You climb down from the tower and reconsider your options.";
                                    if (KeywordUnlocked("hole"))
                                        proc.SetLocation(Process.CRATES_HOLE);
                                    else
                                        proc.SetLocation(Process.CRATES);
                                }
                                else if (input == "cake")
                                {
                                    response += "You pour out the cake mix like snow over the room.";
                                    proc.inv.RemoveItem("cake");
                                }
                                else if (input == "crawl")
                                {
                                    response += "You crawl to the edge of the tower and drop your torch. The tower catches fire and you go up in flames.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "destroy")
                                {
                                    response += "You have developed a fear of heights and destroy the tower of crates. You vow to never climb anything ever again.";
                                    RemoveKeyword("climb");
                                    if (KeywordUnlocked("hole"))
                                        proc.SetLocation(Process.CRATES_HOLE);
                                    else
                                        proc.SetLocation(Process.CRATES);
                                }
                                else if (input == "die" || input == "jump")
                                {
                                    response += "You jump off the crates and land head first on the ground.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "fly")
                                {
                                    if (proc.inv.HasItem("coat"))
                                    {
                                        response += "You fly to an opening in the wall high up where you couldn't reach before.";
                                        DiscoverKeyword("win", false);
                                        proc.SetLocation(Process.WIN);
                                    }
                                    else
                                    {
                                        response += "You try jumping off the tower and flying, but forgot that you can't fly. " +
                                                    "You smack into the ground and die on impact. (It was a really tall tower.)";
                                        proc.SetLocation(Process.DEATH);
                                    }
                                }
                                else if (input == "run" || input == "walk")
                                {
                                    response += "You walk off the edge and are lucky enough to land on your feet with no broken bones.";
                                    if (KeywordUnlocked("hole"))
                                        proc.SetLocation(Process.CRATES_HOLE);
                                    else
                                        proc.SetLocation(Process.CRATES);
                                }
                                else if (input == "torch")
                                {
                                    response += "You use your torch to light the crates on fire and you burn to death.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                break;
                            case Process.BEAR_CAVE:
                                if (input == "fly") response += "That would be nice, wouldn't it? Nah, you still can't fly.";
                                else if (input == "hit") response += "You decide to hit a giant animal that could easily kill you. It seems to laugh at you. (You're lucky to still be alive.)";
                                else if (input == "jump") response += "The bears eyes watch you closely as you jump up and down like a moron.";
                                else if (input == "look") response += "You look at the bear and he looks kind of tired.";
                                else if (input == "sit") response += "You sit. That's not a very pragmatic move at this moment.";
                                else if (input == "yell") response += "You yell at the bear and he roars back... You're not going to win this screaming competition.";
                                else if (input == "back" || input == "run" || input == "hole")
                                {
                                    response += "You try to run away and go back through the hole you came in, but the bear grabs your leg and tears you limb from limb.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "cake")
                                {
                                    response += "You eat your cake mix. That was your final meal.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "candle")
                                {
                                    response += "The bear eats the candle and is unaffected.";
                                    proc.inv.RemoveItem("candle");
                                }
                                else if (input == "crawl")
                                {
                                    response += "You crawl around, thinking that maybe the bear has compassion for babies. (Hey, why not?). Well... This one doesn't.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "cry")
                                {
                                    response += "You cry for a bit, but it doesn't last long, because the bear claws your eyes out.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "destroy")
                                {
                                    response += "Ok, so you may be super strong... But do you REALLY think you can take on a grizzly bear? Come on. You died.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "die")
                                {
                                    response += "You do nothing. You die.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "eat")
                                {
                                    response += "In Soviet Russia, you eat bear... But this isn't Soviet Russia. Instead, bear eat you.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "kick")
                                {
                                    response += "Did you hear the story about the guy who kicked a grizzly bear? ...Me neither, because he died.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "open")
                                {
                                    response += "You open the bear's mouth and he eats your face off.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "pray")
                                {
                                    response += "Bears are not particularly religious and don't care if you pray; they'll still kill you.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "push" || input == "pull")
                                {
                                    response += "Pushes and pulling on a bear usually doesn't do much except make them mad. He mauls you to death.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "scratch")
                                {
                                    response += "You scratch the bear's fur and he seems to like it. You think, \"Maybe we can be friends?\" Then he bites your head off.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "sleep")
                                {
                                    response += "You decide to make yourself completely vulnerable and sleep... Miraculously, the bear decides to follow suit and hibernate. " +
                                                "When you wake up, the bear is still sleeping.";
                                    proc.SetLocation(Process.BEAR_HIBERNATE);
                                }
                                else if (input == "torch")
                                {
                                    response += "Just like Smokey the Bear, this bear puts out your fire and decides you should pay for trying to use flames against him.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "wall")
                                {
                                    response += "You pin yourself against a wall, making it really easy for the bear to catch you and tear you apart.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                break;
                            case Process.BEAR_HIBERNATE:
                                if (input == "look") response += "The bear doesn't look quite as menancing while he's sleeping...";
                                else if (input == "back" || input == "run" || input == "hole")
                                {
                                    response += "You escape from the bear cave while you have the chance and are back in the room with many crates.";
                                    proc.SetLocation(Process.CRATES_HOLE);
                                }
                                else if (input == "cake")
                                {
                                    response += "You pour out the cake mix on the bear... He looks kinda funny now.";
                                    proc.inv.RemoveItem("cake");
                                }
                                else if (input == "candle")
                                {
                                    response += "You leave the candle for the bear to snack on when he wakes up.";
                                    proc.inv.RemoveItem("candle");
                                }
                                else if (input == "destroy")
                                {
                                    response += "While the bear is sleeping you decide to destroy it with your awesome supah strength. Kind of cheap to kill him while he sleeps if you ask me." +
                                                "\r\nYou decide to make a coat out of the bear's fur.";
                                    proc.inv.AddItem("coat", "A warm, fuzzy coat made from a bear you killed with your bare hands.");
                                    proc.SetLocation(Process.BEAR_KILLED);
                                }
                                else if (input == "die" || input == "hit")
                                {
                                    response += "You repeatedly beat on the bear until it is forced to wake up and eat your flesh.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "dig")
                                {
                                    response += "You dig through some dirt, but accidentally toss it on the bear. He wakes up and kills you.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "eat")
                                {
                                    response += "You dig your teeth into the bear's fur... Not a second later, your entrails are laying on the ground.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "kick" || input == "push" || input == "pull" || input == "scratch" || input == "yell")
                                {
                                    response += "Bears don't like it when you wake them from their sleep.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "torch")
                                {
                                    response += "You use your torch to light the bear on fire, but you accidentally catch fire too and burn up with him.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "walk")
                                {
                                    response += "You walk around the bear... Suddenly, he wakes up and devours you.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else // ANY other valid keywords attempted
                                {
                                    response += "Quit messing around and get out of here!";
                                }
                                break;
                            case Process.BEAR_KILLED:
                                if (input == "candle") response += "Haven't you figured out by now that your candle is useless?";
                                else if (input == "coat") response += "Your coat has a strange aura about it...";
                                else if (input == "cry") response += "You cry because of the horrible act you just committed.";
                                else if (input == "eat") response += "You eat some raw meat from the bear. You're pretty hardcore.";
                                else if (input == "jump") response += "You jump up and down in celebration.";
                                else if (input == "kick") response += "You kick some dirt off the ground.";
                                else if (input == "look") response += "This cave is pretty big, with lots of space to float around.";
                                else if (input == "sit") response += "You're tired and decide to sit down.";
                                else if (input == "torch") response += "Didn't your mother ever tell you that you shouldn't play with fire?";
                                else if (input == "back" || input == "run" || input == "hole")
                                {
                                    response += "You leave the cave and return to the room with the crates.";
                                    proc.SetLocation(Process.CRATES_HOLE);
                                }
                                else if (input == "cake")
                                {
                                    response += "You eat your raw cake mix to celebrate. Ew.";
                                    proc.inv.RemoveItem("cake");
                                }
                                else if (input == "die")
                                {
                                    response += "You are disgusted with your animal-killing habbits. You run into a wall and put an end to your miserable existence.";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "fly")
                                {
                                    response += "You spread out your coat and leap into the air... Suddenly you are lifted by a magically force. Your coat allows you to fly! " +
                                                "Go forth and make use of your new discovery.";
                                }
                                else // ANY other valid keywords attempted
                                {
                                    response += "You already defeated the bear. You can 'leave' now.";
                                }
                                break;
                            case Process.DEATH:
                                // You can't input anything when you're dead... Go to DEATH in DisplayRoom()
                                break;
                            case Process.WIN:
                                if (input == "die")
                                {
                                    response += "You escaped only to die... Did you win or did you lose?";
                                    proc.SetLocation(Process.DEATH);
                                }
                                else if (input == "win")
                                {
                                    Console.WriteLine("\r\n-------------------------------------------------------------\r\n");
                                    if (proc.inv.HasItem("candle") && proc.inv.HasItem("torch") && proc.inv.HasItem("cake"))
                                    {
                                        Console.WriteLine("Everyone is thrilled you are home! They make you a cake with the cake mix you found. You stick your candle in the cake and light it with your torch.\r\n" +
                                                            "You truly won. Congrats.\r\n");
                                    }
                                    else
                                        Console.WriteLine("You won, but could have done better. No cake for you. :(\r\n");

                                    Console.Write("Press any key to quit...");
                                    Console.ReadKey();
                                    System.Environment.Exit(1);
                                }
                                else
                                    response += "You 'win'!";
                                break;
                            default:
                                response += "Wait... Where are you?";
                                break;
                        }
                    }
                }
            }
            else
                response = "That is not a valid keyword. Enter a keyword. (Type help for... Well, help.)\r\n ||\r\n \\/";

            Console.WriteLine();
            if (response != "" && response != "-------------------------------------------------------------\r\n\r\n")
                Console.WriteLine(response);

            return kw != null;
        }

        public Keyword ValidateKeyword(string alias)
        {
            Keyword kw = GetKeywordFromString(alias);
            if (kw != null && !kw.IsLocked())
            {
                //If it's an item, make sure it's in the inventory.
                if (!kw.IsItem() || proc.inv.HasItem(kw.GetKeyword()))
                {
                    //It's valid. Make it discovered if it's not
                    if (kw.IsHidden())
                        kw.Discover();
                    //Now tell the caller it was valid
                    return kw;
                }
            }

            return null;
        }

        public Keyword GetKeywordFromString(string alias)
        {
            foreach (var kw in keywords)
            {
                if (kw.HasAlias(alias))
                    return kw;
            }
            
            return null;
        }

        public override string ToString()
        {
            string globalStr = "Here are some useful keywords you can use at any time:";
            string discoveredStr = "You have discovered these keywords:";
            foreach (var kw in keywords)
            {
                if (kw.IsDiscovered() && !kw.IsItem())
                {
                    if (kw.IsGlobal())
                    {
                        globalStr += "\r\n|| " + kw.GetKeyword();
                        if (kw.GetHelp() != "")
                            globalStr += " - " + kw.GetHelp();
                    }
                    else
                    {
                        discoveredStr += "\r\n|| " + kw.GetKeyword();
                        if (kw.GetHelp() != "")
                            discoveredStr += " - " + kw.GetHelp();
                    }
                }
            }

            return globalStr + "\r\n\r\n" + discoveredStr;
        }
    }
}