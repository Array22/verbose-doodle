import json

with open("./map.json", encoding="UTF-8") as f:
    data = json.load(f)

#mapping
ones = data["ones"]
double_digit = data["double_digit"]
tens = data["tens"]
zeros = data["zeros"]

def convert_2digit(x: str):
    """Convert a 2 digit number to words."""
    if len(x) > 2:
        raise ValueError("Input is not a 2-digit number")
    if len(x) == 1: #e.g. .2
        return tens[x]
    values = []
    values.append(tens[x[0]])
    values.append(ones[x[1]])
    words = " ".join(values)
    if x[0] == "1": #e.g. .11, .14
        words = double_digit[x]
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
    return f"{words} CENTS"

def convert_dollars(x: str):
    """Convert whole part of number to dollars in words."""
    y = x[::-1] #reverse indices, e.g 123 -> 321
    i = len(y) - 1
    words_list = []
    while i > -1:
        if i % 3 == 1:
            # print(reversed[i-1:i+1][::-1])
            words_list.append(convert_2digit(y[i-1:i+1][::-1]))
            j = i - 2
        else:
            words_list.append(ones[y[i]]) #append value
        if i > 1:
            words_list.append(zeros[str(i)]) # append zeroes based on index
            words_list.append("AND")
        i -= 1
        if i+1 % 3 == 1:
            i = j
    words = " ".join(words_list)
    return f"{words} DOLLARS"

def convert_money(x: str):
    """Convert a number to monetary value in words."""
    try:
        float(x)
    except ValueError:
        print("Please input a numerical number.")
    if "." in x:
        try:
            part_dollars, part_cents = x.strip().strip("0").split(".")
        except ValueError:
            print(f"{x} is not a valid number")
        a = convert_dollars(part_dollars)
        b = convert_cents(part_cents)
        value = f"{a} AND {b}"
    else:
        value = convert_dollars(x)
    return value

if __name__ == "__main__":
    user_input = input("Please input number: ")
    # # # # # print(convert_cents(user_input))
    print(convert_3digit(user_input))