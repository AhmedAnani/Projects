using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Services
{
    public static class ConsolePrinter
    {
        // ── Colors ───────────────────────────────────────────
        private const string Green = "\u001b[32m";
        private const string Red = "\u001b[31m";
        private const string Yellow = "\u001b[33m";
        private const string Cyan = "\u001b[36m";
        private const string White = "\u001b[37m";
        private const string Reset = "\u001b[0m";
        private const string Bold = "\u001b[1m";

        // ── Success ──────────────────────────────────────────
        public static void Success(string message)
            => Console.WriteLine($"{Green}{Bold}✅ {message}{Reset}");

        // ── Error ────────────────────────────────────────────
        public static void Error(string message)
            => Console.WriteLine($"{Red}{Bold}❌ {message}{Reset}");

        // ── Warning ──────────────────────────────────────────
        public static void Warning(string message)
            => Console.WriteLine($"{Yellow}{Bold}⚠️  {message}{Reset}");

        // ── Info ─────────────────────────────────────────────
        public static void Info(string message)
            => Console.WriteLine($"{Cyan}{Bold}ℹ️  {message}{Reset}");

        // ── Notification ─────────────────────────────────────
        public static void Notification(string message)
            => Console.WriteLine($"{Yellow}📩 {message}{Reset}");

        // ── Header ───────────────────────────────────────────
        public static void Header(string title)
        {
            var line = new string('═', title.Length + 4);
            Console.WriteLine($"\n{Cyan}{Bold}╔{line}╗");
            Console.WriteLine($"║  {title}  ║");
            Console.WriteLine($"╚{line}╝{Reset}\n");
        }

        // ── Divider ──────────────────────────────────────────
        public static void Divider()
            => Console.WriteLine($"{Cyan}{'─' + new string('─', 40)}{Reset}");

        // ── Item Info ─────────────────────────────────────────
        public static void ItemInfo(string label, string value)
            => Console.WriteLine($"{White}{Bold}{label}:{Reset} {value}");
    }
}
