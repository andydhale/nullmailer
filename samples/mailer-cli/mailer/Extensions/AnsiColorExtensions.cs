﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace NullDesk.Cli
{
    public static class AnsiColorExtensions
    {
        public static string Black(this string text)
        {
            return "\x1B[30m" + text + "\x1B[39m";
        }

        public static string Red(this string text)
        {
            return "\x1B[31m" + text + "\x1B[39m";
        }
        public static string Green(this string text)
        {
            return "\x1B[32m" + text + "\x1B[39m";
        }

        public static string Yellow(this string text)
        {
            return "\x1B[33m" + text + "\x1B[39m";
        }

        public static string Blue(this string text)
        {
            return "\x1B[34m" + text + "\x1B[39m";
        }

        public static string Magenta(this string text)
        {
            return "\x1B[35m" + text + "\x1B[39m";
        }

        public static string Cyan(this string text)
        {
            return "\x1B[36m" + text + "\x1B[39m";
        }

        public static string White(this string text)
        {
            return "\x1B[37m" + text + "\x1B[39m";
        }

        public static string Bold(this string text)
        {
            return "\x1B[1m" + text + "\x1B[22m";
        }
    }
}
