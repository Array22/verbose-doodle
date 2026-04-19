import json

with open("./map.json", encoding="UTF-8") as f:
    data = json.load(f)

#mapping
ones = data["ones"]
tens = data["tens"]
zeros = data["zeros"]

def convert_2digit(x: str):
    """Convert a 2 digit number to words."""
    if len(x) > 2:
        raise ValueError("Attempting to Convert a non-2-digit number")
    if len(x) == 1: #e.g. .2
        return tens[x]
    values = []
    values.append(tens[x[0]])
    values.append(ones[x[1]])
    if x[0] == "0":
        words = ones[x[1]]
    else:
        words = "-".join(values)
    if x[0] == "1" or x == "00": #e.g. .11, .14
        words = ones[x]
    return words

def convert_3digit(x: str):
    """Convert a 3 digit number to words"""
    if len(x) != 3:
        raise ValueError("Input is not a 3-digit number")
    values = []
    values.append(f"{ones[x[0]]} HUNDRED AND")
    values.append(convert_2digit(x[1:3]))
    words = " ".join(values)
    return words

def convert_cents (x: str):
    """Convert decimal part of a number to cents in words"""
    if len(x) > 2:
        raise ValueError("Please round number to 2 decimal places")
    words = convert_2digit(x)
    if words == "":
        return None
    return f"{words} CENTS"

def convert_dollars(x: str):
    """Convert whole part of number to dollars in words."""
    length = len(x) - 1
    i = length
    values = []
    while i > -1:
        if i % 3 == 2:
            values.append(convert_3digit(x[i-2:i+1]))
            i -= 3
        elif i % 3 == 1:
            values.append(convert_2digit(x[i-1:i+1]))
            i -= 2
        else:
            values.append(ones[x[i]])
            i -= 1
        if i > 0:
            values.append(zeros[str(length - i)])
    words = " ".join(values[::-1]).strip()
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
    # # # # # # print(convert_cents(user_input))
    print(convert_dollars(user_input))
    
    
    # a = "1."
    # b, c = a.strip(".")
    # print(f"{b} AND {c}")