public static class SymbolCatalog
{
    private static readonly string[] Symbols = new string[]
    {
        "🍏","🐸","🔋","🌵","🥑","🥦","🎾",
        "🍇","🍆","👾","🔮","😈","🛼","☂️",
        "⭐","🍌","🧀","🔔","👑","🍋","🐥",
        "🍎","🚗","🎈","🥊","🌶️","🎯","🧯",
        "🐬","🧢","💎","🫐","🌀","🚙","🧿",
        "🏀","🎃","📙","🦊","🥕","🐯","🥐",
        "💣","🕷️","🖤","🎩","🎥","🎓","🎬",
        "🎲","⚽","🐼","⛵","🥛","🥚","🪐","🍦"
    };

    public static string Get(int id) => Symbols[id];
}