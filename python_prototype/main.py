"""DRAFT version only. C# version is more updated as I switched to that one
after becoming more comfortable with that language"""

import json

with open("./map.json", encoding="UTF-8") as f:
    data = json.load(f)

#mapping
ones = data["ones"]
tens = data["tens"]
zeros = data["zeros"]

def convert_2digit(x: str):
    """Convert a 2 digit number to words."""
    if len(x) != 2:
        raise ValueError("Attempting to Convert a non-2-digit number")
    values = []
    values.append(tens[x[0]])
    values.append(ones[x[1]])
    if x[0] == "0":
        words = ones[x[1]]
    else:
        words = "-".join(values)
    if x[0] == "1": #e.g. 11, 14
        words = ones[x]
    return words

def convert_3digit(x: str):
    """Convert a 3 digit number to words"""
    if len(x) != 3:
        raise ValueError("Input is not a 3-digit number")
    words = f"{ones[x[0]]} HUNDRED"
    if x[1:3] == "00":
        return words
    words_b = convert_2digit(x[1:3])
    values = [words, "AND", words_b]
    words = " ".join(values)
    return words

def convert_cents (x: str):
    """Convert decimal part of a number to cents in words"""
    if len(x) > 2:
        raise ValueError("Please round number to 2 decimal places")
    if len(x) == 1: #e.g. .2
        words = tens[x]
    else:
        words = convert_2digit(x)
    return f"{words} CENTS"

def convert_dollars(x: str):
    """Convert whole part of number to dollars in words."""
    i = 0
    i_rev = len(x) - 1 #reverse index
    values = []
    while i < len(x):
        if i_rev % 3 == 2 and x[i:i+3] != "000":
            values.append(convert_3digit(x[i:i+3]))
            i += 3
        elif i_rev % 3 == 1 and x[i:i+2] != "00":
            values.append(convert_2digit(x[i:i+2]))
            i += 2
        else:
            values.append(ones[x[i]])
            i += 1
        i_rev = len(x) - 1 - i
        if i_rev > 0 and (i_rev+1) % 3 == 0:
            values.append(zeros[str(i_rev+1)])
    words = " ".join(values[:]).strip()
    final_words = f"{words} DOLLARS"
    if words == "ONE":
        final_words = f"{words} DOLLAR"
    # print(f"Values: {values}, Words: {words}")
    return final_words

def convert_money(x: str):
    """Convert a number to monetary value in words."""
    try:
        float(x)
        if float(x) < 0:
            raise ValueError("Must be positive number")
    except ValueError:
        print("Please input a positive numerical number.")
    if "." in x:
        try:
            part_dollars, part_cents = x.strip().split(".")
        except ValueError:
            print(f"{x} is not a valid number")
        a = convert_dollars(part_dollars)
        b = convert_cents(part_cents)
        if b:
            value = f"{a} AND {b}"
        else:
            value = a
    else:
        value = convert_dollars(x)
    return value

if __name__ == "__main__":
    user_input = input("Please input number: ")
    print(convert_2digit(user_input))