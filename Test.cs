using Xunit;


public class MoneyTests
{
[Theory]
// --- Single Digits & Tens ---
[InlineData("1", "ONE DOLLAR")]
[InlineData("2", "TWO DOLLARS")]
[InlineData("10", "TEN DOLLARS")]
[InlineData("11", "ELEVEN DOLLARS")] // Tests the "Teens" logic

// --- Compound Numbers (Tens + Ones) ---
[InlineData("21", "TWENTY-ONE DOLLARS")]
[InlineData("45", "FORTY-FIVE DOLLARS")]

// --- Triplets (Hundreds) ---
[InlineData("100", "ONE HUNDRED DOLLARS")]
[InlineData("105", "ONE HUNDRED AND FIVE DOLLARS")]
[InlineData("125", "ONE HUNDRED AND TWENTY-FIVE DOLLARS")]

// --- Scalers (Thousands & Millions) ---
[InlineData("1000", "ONE THOUSAND DOLLARS")]
[InlineData("1200", "ONE THOUSAND TWO HUNDRED DOLLARS")]
[InlineData("1000000", "ONE MILLION DOLLARS")]

// --- Cents ---
[InlineData("0.05", "ZERO DOLLARS AND FIVE CENTS")]
[InlineData("1.50", "ONE DOLLAR AND FIFTY CENTS")]
[InlineData("12.99", "TWELVE DOLLARS AND NINETY-NINE CENTS")]

// --- Edge Cases ---
[InlineData("0", "ZERO DOLLARS")]
[InlineData("0.01", "ZERO DOLLARS AND ONE CENT")]
[InlineData("00000.00", "ZERO DOLLARS AND ZERO CENTS")]
[InlineData("00120.00", "ONE HUNDRED AND TWENTY DOLLARS AND ZERO CENTS")]

public void ConvertMoney_ReturnCorrectWords(string input, string expected)
    {
        Money result = new(input);

        Assert.Equal(result.ConvertMoney(),expected);
    }
}

//new method, Assert.Throws, check 0100, check negatives