﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactionBot;

namespace ReactionBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new ReactionBot();
            bot.run();
            appendToLog();
        }

        static void appendToLog()
        {

        }
    }
}
